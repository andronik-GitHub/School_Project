using System.Data.SqlClient;
using System.Data;
using SchoolLibrary_Dapper.DAL.Entities;
using SchoolLibrary_Dapper.DAL.Repositories.Contracts;

namespace SchoolLibrary_Dapper.DAL.Repositories
{
    public class GenreRepository : GenericRepository<Genre>, IGenreRepository
    {
        public GenreRepository(
            SqlConnection sqlConnection,
            IDbTransaction dbTransaction)
            : base(sqlConnection, dbTransaction, "Genres")
        {
        }
    }
}
