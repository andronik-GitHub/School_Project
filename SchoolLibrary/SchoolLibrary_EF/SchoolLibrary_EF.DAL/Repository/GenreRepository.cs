using Bogus;
using Microsoft.EntityFrameworkCore;
using SchoolLibrary_EF.DAL.Data;
using SchoolLibrary_EF.DAL.Entities;
using SchoolLibrary_EF.DAL.Repository.Contracts;
using SchoolLibrary_EF.DAL.Helper.Contracts;
using SchoolLibrary_EF.DAL.Helpers.Contracts;
using SchoolLibrary_EF.DAL.Paging;
using SchoolLibrary_EF.DAL.Paging.Entities;

namespace SchoolLibrary_EF.DAL.Repository
{
    public class GenreRepository : GenericRepository<Genre>, IGenreRepository
    {
        public GenreRepository(
            SchoolLibraryContext dbContext,
            IDataShaper<Genre> dataShaper,
            ISortHelper<Genre> sortHelper)
            : base(dbContext, dataShaper, sortHelper)
        {
        }

        
        /*  Count the number of books of each genre
         
            SELECT G.Name AS GenreName, COUNT(BG.BookId) AS BookCount
            FROM Genres G
            LEFT JOIN BookGenres BG ON G.GenreId = BG.GenreId
            GROUP BY G.GenreId, G.Name;
            
         */
        public async Task<PagedList<(string GenreName, int BookCount)>> GetCountOfBooksEachGenreAsync
            (GenreParameters parameters)
        {
            var collection = await entities
                .AsNoTracking()
                .GroupJoin(
                    dbContext.BookGenres.AsNoTracking(),
                    g => g.GenreId,
                    bg => bg.GenreId,
                    (g, bg) => new
                    {
                        GenreName = g.Name,
                        BookCount = bg.Count()
                    })
                .ToListAsync();
            
            return PagedList<(string GenreName, int BookCount)>
                .ToPagedList(
                    collection.Select(item => (item.GenreName, item.BookCount)).AsQueryable(), 
                    parameters.PageNumber, 
                    parameters.PageSize);
        }
    }
}
