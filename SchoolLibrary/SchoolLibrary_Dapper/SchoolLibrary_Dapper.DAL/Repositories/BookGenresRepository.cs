using System.Data.SqlClient;
using System.Data;
using SchoolLibrary_Dapper.DAL.Entities;
using SchoolLibrary_Dapper.DAL.Repositories.Contracts;
using Dapper;

namespace SchoolLibrary_Dapper.DAL.Repositories
{
    public class BookGenresRepository : IBookGenresRepository
    {
        private readonly SqlConnection sqlConnection;
        private readonly IDbTransaction dbTransaction;
        private readonly string tableName;
        
        public BookGenresRepository(SqlConnection sqlConnection, IDbTransaction dbTransaction)
        {
            this.sqlConnection = sqlConnection;
            this.dbTransaction = dbTransaction;
            this.tableName = "BookGenres";
        }


        public async Task<IEnumerable<BookGenres>> GetAllAsync()
        {
            var retult =  await sqlConnection.QueryAsync<BookGenres>(
                $"SELECT * FROM [{tableName}]",
                transaction: dbTransaction);

            return retult;
        }
        public async Task<IEnumerable<(Book, Genre)>> Include_GetAllAsync()
        {
            var result = await sqlConnection.QueryAsync<Book, Genre, (Book, Genre)>(
                "SELECT b.BookId AS BookId, b.Title AS Title, b.ISBN AS ISBN, g.GenreId AS GenreId, g.Name AS Name " +
                $"FROM [{tableName}] bg " +
                "INNER JOIN [Books] b ON bg.BookId = b.BookId " +
                "INNER JOIN [Genres] g ON bg.GenreId = g.GenreId",
                (book, genre) => (book, genre),
                splitOn: "BookId, GenreId",
                transaction: dbTransaction);

            return result.AsEnumerable();
        }
        public async Task<BookGenres?> GetByIdsAsync(Guid BookId, Guid GenreId)
        {
            var retult =  await sqlConnection.QuerySingleOrDefaultAsync<BookGenres>(
                $"SELECT * FROM [{tableName}] WHERE BookId=@BookId AND GenreId=@GenreId",
                param: new { BookId, GenreId },
                transaction: dbTransaction);

            return retult;
        }
        public async Task<(Book, Genre)?> Include_GetByIdsAsync(Guid BookId, Guid GenreId)
        {
            var book =  await sqlConnection.QuerySingleOrDefaultAsync<Book>(
                "SELECT * FROM [Books] WHERE BookId=@BookId",
                param: new { BookId },
                transaction: dbTransaction);
            
            var genre =  await sqlConnection.QuerySingleOrDefaultAsync<Genre>(
                "SELECT * FROM [Genres] WHERE GenreId=@GenreId",
                param: new { GenreId },
                transaction: dbTransaction);

            return (book, genre);
        }
        public async Task<(Guid, Guid)> CreateAsync(BookGenres entity)
        {
            return await sqlConnection.ExecuteScalarAsync<(Guid, Guid)>(
                $"INSERT INTO {tableName} (BookId, GenreId) " +
                "VALUES (@BookId, @GenreId); SELECT SCOPE_IDENTITY()",
                param: entity,
                transaction: dbTransaction);
        }
        public async Task UpdateAsync(BookGenres entity)
        {
            await sqlConnection.ExecuteAsync(
                $"UPDATE {tableName} SET " +
                "BookId=@BookId, GenreId=@GenreId " +
                "WHERE BookId=@BookId AND GenreId=@GenreId",
                param: entity,
                transaction: dbTransaction);
        }
        public async Task DeleteAsync(Guid BookId, Guid GenreId)
        {
            await sqlConnection.ExecuteAsync(
                $"DELETE FROM [{tableName}] WHERE BookId=@BookId AND GenreId=@GenreId",
                param: new { BookId, GenreId },
                transaction: dbTransaction);
        }
    }
}
