﻿using Microsoft.EntityFrameworkCore;
using SchoolLibrary_EF.DAL.Data;
using SchoolLibrary_EF.DAL.Entities;
using SchoolLibrary_EF.DAL.Helper.Contracts;
using SchoolLibrary_EF.DAL.Helpers.Contracts;
using SchoolLibrary_EF.DAL.Paging;
using SchoolLibrary_EF.DAL.Paging.Entities;
using SchoolLibrary_EF.DAL.Repository.Contracts;

namespace SchoolLibrary_EF.DAL.Repository
{
    public class BookRepository : GenericRepository<Book>, IBookRepository
    {
        public BookRepository(
            SchoolLibraryContext dbContext, 
            ISortHelper<Book> sortHelper,
            IDataShaper<Book> dataShaper)
            : base(dbContext, dataShaper, sortHelper)
        {
        }


        public override async Task<PagedList<Book>> GetAllAsync(BaseParameters parameters)
        {
            var collection = entities
                .AsNoTracking()
                .Include(b => b.Publisher);
            var sortCollection = _sortHelper.ApplySort(collection, parameters.OrderBy); // sorting

            return await Task.Run(() => // paging
                PagedList<Book>.ToPagedList(sortCollection, parameters.PageNumber, parameters.PageSize));
        }
        public override async Task<Book?> GetByIdAsync(Guid id)
        {
            var entity = await entities
                .AsNoTracking()
                .Include(b => b.Publisher)
                .FirstOrDefaultAsync(b => b.BookId == id);
            
            return entity;
        }

        
        /*  Average rating for each book
         
            SELECT B.Title AS BookTitle, AVG(R.Rating) AS AverageRating
            FROM Books B
            LEFT JOIN Reviews R ON B.BookId = R.BookId
            GROUP BY B.BookId, B.Title;
            
         */
        public async Task<PagedList<(string BookTitle, decimal? Average)>> GetAvgRatingForBookAsync
            (BookParameters parameters)
        {
            var collection = await entities
                .AsNoTracking()
                .GroupJoin(
                    dbContext.Reviews.AsNoTracking(),
                    b => b.BookId,
                    r => r.BookId,
                    (b, r) => new
                    {
                        BookTitle = b.Title, 
                        Average = r.DefaultIfEmpty().Average(review => review != null ? review.Rating : null)
                    })
                .ToListAsync();
            
            return PagedList<(string BookTitle, decimal? Average)>
                .ToPagedList(
                    collection.Select(item => (item.BookTitle, item.Average)).AsQueryable(), 
                    parameters.PageNumber, 
                    parameters.PageSize);
        }
        
        /*  Find books that don't have reviews
         
            SELECT B.Title AS BookTitle
            FROM Books B
            LEFT JOIN Reviews R ON B.BookId = R.BookId
            WHERE R.ReviewId IS NULL;
            
         */
        public async Task<PagedList<Book>> GetBooksWithoutReviewsAsync(BookParameters parameters)
        {
            var collection = await entities
                .AsNoTracking()
                .GroupJoin(
                    dbContext.Reviews.AsNoTracking(),
                    b => b.BookId,
                    r => r.BookId,
                    (b, r) => new
                    {
                        Book = b,
                        ReviewsCount = r.Count()
                    })
                .Where(result => result.ReviewsCount <= 0)
                .ToListAsync();

            return PagedList<Book>
                .ToPagedList(
                    collection.Select(item => item.Book).AsQueryable(), 
                    parameters.PageNumber, 
                    parameters.PageSize);
        }
    }
}
