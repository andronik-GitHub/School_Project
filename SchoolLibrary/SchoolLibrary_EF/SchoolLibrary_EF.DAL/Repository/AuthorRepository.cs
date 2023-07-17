using SchoolLibrary_EF.DAL.Entities;
using SchoolLibrary_EF.DAL.Repository.Contracts;
using SchoolLibrary_EF.DAL.Data;
using SchoolLibrary_EF.DAL.Helper.Contracts;
using SchoolLibrary_EF.DAL.Helpers.Contracts;

namespace SchoolLibrary_EF.DAL.Repository
{
    public class AuthorRepository : GenericRepository<Author> , IAuthorRepository
    {
        public AuthorRepository(
            SchoolLibraryContext dbContext,
            IDataShaper<Author> dataShaper,
            ISortHelper<Author> sortHelper)
            : base(dbContext, dataShaper, sortHelper)
        {
        }
    }
}
