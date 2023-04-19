using SchoolLibrary_Dapper.DAL.Entities;
using SchoolLibrary_Dapper.DAL.Repositories.Contracts;
using System.Data.SqlClient;
using System.Data;
using Dapper;

namespace SchoolLibrary_Dapper.DAL.Repositories
{
    public class BookAuthorsRepository : GenericRepository<BookAuthors>, IBookAuthorsRepository
    {
        public BookAuthorsRepository(
            SqlConnection sqlConnection,
            IDbTransaction dbTransaction)
            : base(sqlConnection, dbTransaction, "BookAuthors")
        {
        }


        public override async Task<Guid> CreateAsync(BookAuthors entity)
        {
            return await sqlConnection.ExecuteScalarAsync<Guid>(
                $"INSERT INTO {tableName} (BookId, AuthorId) " +
                $"VALUES (@BookId, @AuthorId); SELECT SCOPE_IDENTITY()",
                param: entity,
                transaction: dbTransaction);
        }
        public override async Task UpdateAsync(BookAuthors entity)
        {
            await sqlConnection.ExecuteAsync(
                $"UPDATE {tableName} SET " +
                $"BookId=@BookId, AuthorId=@AuthorId " +
                $"WHERE BookId=@BookId AND AuthorId=@AuthorId",
                param: entity,
                transaction: dbTransaction);
        }
    }
}
