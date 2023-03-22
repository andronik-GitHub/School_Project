using SchoolLibrary_EF.DAL.Entities;
using SchoolLibrary_EF.DAL.Repositories.Contracts;
using SchoolLibrary_EF.DAL.Repository;
using SchoolLibrary_EF.DAL.Data;
using SchoolLibrary_EF.DAL.Pagging;
using Microsoft.EntityFrameworkCore;

namespace SchoolLibrary_EF.DAL.Repositories
{
    public class BookDetailsRepository : GenericRepository<BookDetails>, IBookDetailsRepository
    {
        public BookDetailsRepository(SchoolLibraryContext dbContext)
            : base(dbContext)
        {
        }


        public override async Task<Guid> CreateAsync(BookDetails bookDetails)
        {
            await entities.AddAsync(bookDetails);

            return bookDetails.BookDetailId;
        }
        public override async Task<IEnumerable<BookDetails>> GetAllAsync<TOrderBy>
            (BaseParameters? parameters = null, Func<BookDetails, TOrderBy>? orderBy = null)
        {
            if (parameters == null)
                return await entities
                    .Include(entity => entity.Book)
                    .ToListAsync();

            return await entities
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
