using SchoolLibrary_Dapper.DAL.Entities;

namespace SchoolLibrary_Dapper.DAL.Repositories.Contracts
{
    public interface IReviewRepository : IGenericRepository<Review>
    {
        Task<(Review, User, Book)> GetByIdReviewWithUserAndBookAsync(Guid id);
        Task<IEnumerable<(Review, User, Book)>> GetReviewsWithUsersAndBooksAsync();
    }
}
