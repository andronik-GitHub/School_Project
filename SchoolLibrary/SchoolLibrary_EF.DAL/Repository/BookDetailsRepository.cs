using SchoolLibrary_EF.DAL.Entities;
using SchoolLibrary_EF.DAL.Repositories.Contracts;
using SchoolLibrary_EF.DAL.Repository;
using SchoolLibrary_EF.DAL.Data;

namespace SchoolLibrary_EF.DAL.Repositories
{
    public class BookDetailsRepository : GenericRepository<BookDetails>, IBookDetailsRepository
    {
        public BookDetailsRepository(SchoolLibraryContext dbContext)
            : base(dbContext)
        {
        }
    }
}
