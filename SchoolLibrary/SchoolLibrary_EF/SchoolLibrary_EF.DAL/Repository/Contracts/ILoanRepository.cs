using SchoolLibrary_EF.DAL.Entities;

namespace SchoolLibrary_EF.DAL.Repository.Contracts
{
    public interface ILoanRepository : IGenericRepository<Loan, Guid>
    {
    }
}
