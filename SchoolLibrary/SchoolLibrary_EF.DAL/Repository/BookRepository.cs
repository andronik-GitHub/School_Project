using Microsoft.EntityFrameworkCore;
using SchoolLibrary_EF.DAL.Data;
using SchoolLibrary_EF.DAL.Entities;
using SchoolLibrary_EF.DAL.Pagging;
using SchoolLibrary_EF.DAL.Repositories.Contracts;
using SchoolLibrary_EF.DAL.Repository;

namespace SchoolLibrary_EF.DAL.Repositories
{
    public class BookRepository : GenericRepository<Book>, IBookRepository
    {
        public BookRepository(SchoolLibraryContext dbContext)
            : base(dbContext)
        {
        }


        public override async Task<Guid> CreateAsync(Book book)
        {
            await entities.AddAsync(book);

            return book.BookId;
        }
        public override async Task<IEnumerable<Book>> GetAllAsync<TOrderBy>
            (BaseParameters? parameters = null, Func<Book, TOrderBy>? orderBy = null)
        {
            if (parameters == null)
                return await entities
                    .Include(entity => entity.Publisher)
                    .ToListAsync();

            return await entities
                .OrderBy(entity => entity.BookId)
                .Skip((parameters.PageNumber - 1) * parameters.PageSize)
                .Take(parameters.PageSize)
                .Include(entity => entity.Publisher)
                .ToListAsync();
        }
        public override async Task<Book?> GetByIdAsync(Guid id)
        {
            return await entities.Include(b => b.Publisher).FirstOrDefaultAsync(b => b.BookId == id);
        }
    }
}
