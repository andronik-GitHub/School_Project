using Microsoft.EntityFrameworkCore;
using SchoolLibrary_EF.DAL.Data;
using SchoolLibrary_EF.DAL.Entities;
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
        public override async Task<IEnumerable<Book>> GetAllAsync()
        {
            entities.ToList().ForEach(book => 
                dbContext.Books.Include(b => b.Publisher).FirstOrDefault(b => b.BookId == book.BookId));

            return await entities.ToListAsync();
        }
        public override async Task<Book?> GetByIdAsync(Guid id)
        {
            return await entities.Include(b => b.Publisher).FirstOrDefaultAsync(b => b.BookId == id);
        }
    }
}
