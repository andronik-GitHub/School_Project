using Microsoft.EntityFrameworkCore;
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
            return await entities
                .AsNoTracking()
                .Include(b => b.Publisher)
                .FirstOrDefaultAsync(b => b.BookId == id);
        }
    }
}
