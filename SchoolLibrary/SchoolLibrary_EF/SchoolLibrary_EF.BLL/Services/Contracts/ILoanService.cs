using SchoolLibrary_EF.BLL.DTOs.LoanDTOs;

namespace SchoolLibrary_EF.BLL.Services.Contracts
{
    public interface ILoanService : IGenericService<Guid, GetDTO_Loan, InsertDTO_Loan, UpdateDTO_Loan>
    {
    }
}
