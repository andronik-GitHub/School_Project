using SchoolLibrary_EF.DAL.Entities;
using SchoolLibrary_EF.DAL.Paging;
using SchoolLibrary_EF.DAL.Paging.Entities;

namespace SchoolLibrary_EF.DAL.Repository.Contracts
{
    public interface IUserRepository : IGenericRepository<User, Guid>
    {
        /// <summary>
        /// Gets number of books issued per user
        /// </summary>
        /// <param name="parameters">UserParameters for paging</param>
        /// <returns>Returns collection of users with count of books loaned</returns>
        Task<PagedList<(string FirstName, string LastName, int BooksLoaned)>> GetNumBooksIssuedToUserAsync
            (UserParameters parameters);
    }
}
