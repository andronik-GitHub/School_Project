using SchoolLibrary_Dapper.DAL.Repositories.Contracts;
using System.Data.SqlClient;
using System.Data;

namespace SchoolLibrary_Dapper.DAL.Repositories
{
    public class PublisherRepository : GenericRepository<PublisherRepository> , IPublisherRepository
    {
        public PublisherRepository(
            SqlConnection sqlConnection,
            IDbTransaction dbTransaction)
            : base(sqlConnection, dbTransaction, "Publishers")
        {
        }
    }
}
