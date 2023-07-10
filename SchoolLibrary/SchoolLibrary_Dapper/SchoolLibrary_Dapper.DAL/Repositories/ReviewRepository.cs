using SchoolLibrary_Dapper.DAL.Entities;
using SchoolLibrary_Dapper.DAL.Repositories.Contracts;
using System.Data.SqlClient;
using System.Data;
using Dapper;

namespace SchoolLibrary_Dapper.DAL.Repositories
{
    public class ReviewRepository : GenericRepository<Review>, IReviewRepository
    {
        public ReviewRepository(
            SqlConnection sqlConnection,
            IDbTransaction dbTransaction)
            : base(sqlConnection, dbTransaction, "Reviews")
        {
        }

        public async Task<(Review, User, Book)> GetByIdReviewWithUserAndBookAsync(Guid id)
        {
            var query = "SELECT r.ReviewId AS ReviewId, r.Rating AS Rating, " + 
                        "u.Id AS Id, u.FirstName AS FirstName, u.LastName AS LastName, " + 
                        "b.BookId AS BookId, b.Title AS Title, b.ISBN AS ISBN " +
                        $"FROM [{tableName}] r " +
                        "INNER JOIN AspNetUsers u ON r.UserId = u.Id " +
                        "INNER JOIN Books b ON r.BookId = b.BookId " +
                        "WHERE r.ReviewId = @Id";
    
            var result = await sqlConnection.QueryAsync<Review, User, Book, (Review, User, Book)>(
                query,
                (review, user, book) => (review, user, book),
                param: new { Id = id },
                transaction: dbTransaction,
                splitOn: "ReviewId, Id, BookId");

            return result.SingleOrDefault();
        }
        public async Task<IEnumerable<(Review, User, Book)>> GetReviewsWithUsersAndBooksAsync()
        {
            var query = "SELECT r.ReviewId AS ReviewId, r.Rating AS Rating, " + 
                        "u.Id AS Id, u.FirstName AS FirstName, u.LastName AS LastName, " + 
                        "b.BookId AS BookId, b.Title AS Title, b.ISBN AS ISBN " +
                        $"FROM [{tableName}] r " +
                        "INNER JOIN AspNetUsers u ON r.UserId = u.Id " +
                        "INNER JOIN Books b ON r.BookId = b.BookId ";
            
            var result = await sqlConnection.QueryAsync<Review, User, Book, (Review, User, Book)>(
                query,
                (review, user, book) => (review, user, book),
                splitOn: "ReviewId, Id, BookId",
                transaction: dbTransaction);

            return result.AsEnumerable();
        }
    }
}
