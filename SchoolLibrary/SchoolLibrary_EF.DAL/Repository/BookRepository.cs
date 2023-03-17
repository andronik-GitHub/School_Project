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
    }
}
