using SchoolLibrary_Dapper.DAL.Entities;
using SchoolLibrary_Dapper.DAL.Repositories.Contracts;
using System.Data.SqlClient;
using System.Data;
using Dapper;

namespace SchoolLibrary_Dapper.DAL.Repositories
{
    public class BookAuthorsRepository : IBookAuthorsRepository
    {
        private readonly SqlConnection sqlConnection;
        private readonly IDbTransaction dbTransaction;
        private readonly string tableName;
        
        public BookAuthorsRepository(SqlConnection sqlConnection, IDbTransaction dbTransaction)
        {
            this.sqlConnection = sqlConnection;
            this.dbTransaction = dbTransaction;
            this.tableName = "BookAuthors";
        }


        public async Task<IEnumerable<BookAuthors>> GetAllAsync()
        {
            var retult =  await sqlConnection.QueryAsync<BookAuthors>(
                $"SELECT * FROM [{tableName}]",
                transaction: dbTransaction);

            return retult;
        }
        public async Task<IEnumerable<(Book, Author)>> Include_GetAllAsync()
        {
            var result = await sqlConnection.QueryAsync<Book, Author, (Book, Author)>(
                "SELECT b.BookId AS BookId, b.Title AS Title, b.ISBN AS ISBN, a.AuthorId AS AuthorId, a.FirstName AS FirstName, a.LastName AS LastName " +
                $"FROM [{tableName}] ba " +
                "INNER JOIN [Books] b ON ba.BookId = b.BookId " +
                "INNER JOIN [Authors] a ON ba.AuthorId = a.AuthorId",
                (book, author) => (book, author),
                splitOn: "BookId, AuthorId",
                transaction: dbTransaction);

            return result.AsEnumerable();
        }
        public async Task<BookAuthors?> GetByIdsAsync(Guid BookId, Guid AuthorId)
        {
            var retult =  await sqlConnection.QuerySingleOrDefaultAsync<BookAuthors>(
                $"SELECT * FROM [{tableName}] WHERE BookId=@BookId AND AuthorId=@AuthorId",
                param: new { BookId, AuthorId },
                transaction: dbTransaction);

            return retult;
        }
        public async Task<(Book, Author)?> Include_GetByIdsAsync(Guid BookId, Guid AuthorId)
        {
            var book =  await sqlConnection.QuerySingleOrDefaultAsync<Book>(
                "SELECT * FROM [Books] WHERE BookId=@BookId",
                param: new { BookId },
                transaction: dbTransaction);
            
            var author =  await sqlConnection.QuerySingleOrDefaultAsync<Author>(
                "SELECT * FROM [Authors] WHERE AuthorId=@AuthorId",
                param: new { AuthorId },
                transaction: dbTransaction);

            return (book, author);
        }
        public async Task<(Guid, Guid)> CreateAsync(BookAuthors entity)
        {
            return await sqlConnection.ExecuteScalarAsync<(Guid, Guid)>(
                $"INSERT INTO {tableName} (BookId, AuthorId) " +
                "VALUES (@BookId, @AuthorId); SELECT SCOPE_IDENTITY()",
                param: entity,
                transaction: dbTransaction);
        }
        public async Task UpdateAsync(BookAuthors entity)
        {
            await sqlConnection.ExecuteAsync(
                $"UPDATE {tableName} SET " +
                "BookId=@BookId, AuthorId=@AuthorId " +
                "WHERE BookId=@BookId AND AuthorId=@AuthorId",
                param: entity,
                transaction: dbTransaction);
        }
        public async Task DeleteAsync(Guid BookId, Guid AuthorId)
        {
            await sqlConnection.ExecuteAsync(
                $"DELETE FROM [{tableName}] WHERE BookId=@BookId AND AuthorId=@AuthorId",
                param: new { BookId, AuthorId },
                transaction: dbTransaction);
        }
    }
}
