using SchoolLibrary_Dapper.DAL.Entities;
using SchoolLibrary_Dapper.DAL.Repositories.Contracts;
using System.Data.SqlClient;
using System.Data;

namespace SchoolLibrary_Dapper.DAL.Repositories
{
    public class AuthorRepository : GenericRepository<Author> , IAuthorRepository
    {
        public AuthorRepository(
            SqlConnection sqlConnection,
            IDbTransaction dbTransaction)
            : base(sqlConnection, dbTransaction, "Authors")
        {
        }
    }
}
