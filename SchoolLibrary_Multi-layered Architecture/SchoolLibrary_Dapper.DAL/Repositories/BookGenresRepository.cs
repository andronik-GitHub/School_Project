using System.Data.SqlClient;
using System.Data;
using SchoolLibrary_Dapper.DAL.Entities;
using SchoolLibrary_Dapper.DAL.Repositories.Contracts;
using Dapper;

namespace SchoolLibrary_Dapper.DAL.Repositories
{
    public class BookGenresRepository : GenericRepository<BookGenres>, IBookGenresRepository
    {
        public BookGenresRepository(
            SqlConnection sqlConnection,
            IDbTransaction dbTransaction)
            : base(sqlConnection, dbTransaction, "BookGenres")
        {
        }


        public override async Task<Guid> CreateAsync(BookGenres entity)
        {
            return await sqlConnection.ExecuteScalarAsync<Guid>(
                $"INSERT INTO {tableName} (BookId, GenreId) " +
                $"VALUES (@BookId, @GenreId); SELECT SCOPE_IDENTITY()",
                param: entity,
                transaction: dbTransaction);
        }
        public override async Task UpdateAsync(BookGenres entity)
        {
            await sqlConnection.ExecuteAsync(
                $"UPDATE {tableName} SET " +
                $"BookId=@BookId, GenreId=@GenreId " +
                $"WHERE BookId=@BookId AND GenreId=@GenreId",
                param: entity,
                transaction: dbTransaction);
        }
    }
}
