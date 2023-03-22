using SchoolLibrary_EF.DAL.Entities;
using SchoolLibrary_EF.DAL.Repositories.Contracts;
using SchoolLibrary_EF.DAL.Repository;
using SchoolLibrary_EF.DAL.Data;
using Microsoft.EntityFrameworkCore;
using SchoolLibrary_EF.DAL.Pagging.Entities;
using SchoolLibrary_EF.DAL.Helper.Contracts;

namespace SchoolLibrary_EF.DAL.Repositories
{
    public class BookDetailsRepository : GenericRepository<BookDetails>, IBookDetailsRepository
    {
        private readonly ISortHelper<BookDetails> _sortHelper;

        public BookDetailsRepository(SchoolLibraryContext dbContext, ISortHelper<BookDetails> sortHelper)
            : base(dbContext)
        {
            _sortHelper = sortHelper;
        }


        public override async Task<Guid> CreateAsync(BookDetails bookDetails)
        {
            await entities.AddAsync(bookDetails);

            return bookDetails.BookDetailId;
        }
        public override async Task<IEnumerable<BookDetails>> GetAllAsync(BaseParameters? parameters = null)
        {
            if (parameters == null) return await base.GetAllAsync();

            if (parameters is BookDetailsParameters param)
            {
                var collection = entities.AsNoTracking();

                collection = _sortHelper.ApplySort(collection, param.OrderBy); // sorting

                return await collection
                    .OrderBy(entity => entity.BookDetailId)
                    .Skip((parameters.PageNumber - 1) * parameters.PageSize)
                    .Take(parameters.PageSize)
                    .Include(entity => entity.Book)
                    .ToListAsync();
            }
            else
                return await entities
                    .AsNoTracking()
                    .OrderBy(entity => entity.BookDetailId)
                    .Skip((parameters.PageNumber - 1) * parameters.PageSize)
                    .Take(parameters.PageSize)
                    .Include(entity => entity.Book)
                    .ToListAsync();
        }
        public override async Task<BookDetails?> GetByIdAsync(Guid id)
        {
            return await entities.Include(bd => bd.Book).FirstOrDefaultAsync(bd => bd.BookDetailId == id);
        }
    }
}
