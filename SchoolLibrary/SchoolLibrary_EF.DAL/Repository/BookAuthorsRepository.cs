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
    }
}
