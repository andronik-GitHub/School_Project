using SchoolLibrary_EF.DAL.Entities;
using SchoolLibrary_EF.DAL.Paging.Entities;

namespace SchoolLibrary_EF.DAL.Repository.Contracts
{
    public interface IAuthorRepository : IGenericRepository<Author, Guid>
    {
        /// <summary>
        /// Gets author with highest avg book rating
        /// </summary>
        /// <returns>Returns author with highest avg book rating</returns>
        Task<(string FirstName, string LastName, decimal AverageRating)> GetAuthorWithHighestAvgBookRatingAsync();
    }
}
