using SchoolLibrary_EF.DAL.Entities;
using SchoolLibrary_EF.DAL.Repository.Contracts;
using SchoolLibrary_EF.DAL.Data;
using Microsoft.EntityFrameworkCore;
using SchoolLibrary_EF.DAL.Paging.Entities;
using SchoolLibrary_EF.DAL.Helper.Contracts;
using SchoolLibrary_EF.DAL.Helpers.Contracts;
using SchoolLibrary_EF.DAL.Paging;

namespace SchoolLibrary_EF.DAL.Repository
{
    public class BookDetailsRepository : GenericRepository<BookDetails>, IBookDetailsRepository
    {
        public BookDetailsRepository(
            SchoolLibraryContext dbContext, 
            ISortHelper<BookDetails> sortHelper,
            IDataShaper<BookDetails> dataShaper)
            : base(dbContext, dataShaper, sortHelper)
        {
        }


        public override async Task<PagedList<BookDetails>> GetAllAsync(BaseParameters parameters)
        {
            var collection = entities
                .AsNoTracking()
                .Include(bd => bd.Book);
            var sortCollection = _sortHelper.ApplySort(collection, parameters.OrderBy); // sorting

            return await Task.Run(() => // paging
                PagedList<BookDetails>.ToPagedList(sortCollection, parameters.PageNumber, parameters.PageSize));
        }
        public override async Task<BookDetails?> GetByIdAsync(Guid id)
        {
            return await entities
                .AsNoTracking()
                .Include(bd => bd.Book)
                .FirstOrDefaultAsync(bd => bd.BookDetailId == id);
        }
    }
}
