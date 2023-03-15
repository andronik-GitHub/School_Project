using SchoolLibrary_Dapper.DAL.Entities;
using SchoolLibrary_Dapper.DAL.Repositories.Contracts;
using System.Data.SqlClient;
using System.Data;

namespace SchoolLibrary_Dapper.DAL.Repositories
{
    public class LoanRepository : GenericRepository<Loan>, ILoanRepository
    {
        public LoanRepository(
            SqlConnection sqlConnection,
            IDbTransaction dbTransaction)
            : base(sqlConnection, dbTransaction, "Loans")
        {
        }
    }
}
