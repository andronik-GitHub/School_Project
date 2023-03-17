using SchoolLibrary_EF.DAL.Data;
using SchoolLibrary_EF.DAL.Entities;
using SchoolLibrary_EF.DAL.Repositories.Contracts;
using SchoolLibrary_EF.DAL.Repository;

namespace SchoolLibrary_EF.DAL.Repositories
{
    public class BookAuthorsRepository : GenericRepository<BookAuthors>, IBookAuthorsRepository
    {
        public BookAuthorsRepository(SchoolLibraryContext dbContext)
            : base(dbContext)
        {
        }


        public override async Task<Guid> CreateAsync(BookAuthors bookAuthors)
        {
            await entities.AddAsync(bookAuthors);

            return bookAuthors.AuthorId;
        }
    }
}
