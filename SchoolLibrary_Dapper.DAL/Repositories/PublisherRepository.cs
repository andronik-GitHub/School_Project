using SchoolLibrary_Dapper.DAL.Repositories.Contracts;
using System.Data.SqlClient;
using System.Data;
using SchoolLibrary_Dapper.DAL.Entities;

namespace SchoolLibrary_Dapper.DAL.Repositories
{
    public class PublisherRepository : GenericRepository<Publisher>, IPublisherRepository
    {
        public PublisherRepository(
            SqlConnection sqlConnection,
            IDbTransaction dbTransaction)
            : base(sqlConnection, dbTransaction, "Publishers")
        {
        }
    }
}
