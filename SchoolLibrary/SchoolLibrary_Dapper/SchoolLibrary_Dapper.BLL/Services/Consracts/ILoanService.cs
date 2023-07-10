using SchoolLibrary_Dapper.BLL.DTOs.LoanDTOs;

namespace SchoolLibrary_Dapper.BLL.Services.Consracts
{
    public interface ILoanService : IGenericService
    {
        Task<Guid> CreateAsync(InsertDTO_Loan entity);
        Task<GetDTO_Loan?> GetAsync(Guid id);
        Task<IEnumerable<GetDTO_Loan>> GetAllAsync();
        Task UpdateAsync(UpdateDTO_Loan entity);
        Task DeleteAsync(Guid id);
    }
}
