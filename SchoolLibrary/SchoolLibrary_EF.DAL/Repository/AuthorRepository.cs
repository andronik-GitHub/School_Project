using SchoolLibrary_EF.DAL.Entities;
using SchoolLibrary_EF.DAL.Repositories.Contracts;
using SchoolLibrary_EF.DAL.Repository;
using SchoolLibrary_EF.DAL.Data;

namespace SchoolLibrary_EF.DAL.Repositories
{
    public class AuthorRepository : GenericRepository<Author> , IAuthorRepository
    {
        public AuthorRepository(SchoolLibraryContext dbContext)
            : base(dbContext)
        {
        }
    }
}
