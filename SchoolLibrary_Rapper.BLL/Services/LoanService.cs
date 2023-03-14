using SchoolLibrary_Dapper.DAL.Entities;
using SchoolLibrary_Dapper.DAL.Repositories.Contracts;
using SchoolLibrary_Rapper.BLL.Services.Consracts;

namespace SchoolLibrary_Rapper.BLL.Services
{
    public class LoanService : ILoanService
    {
        IUnitOfWork _uow;

        public LoanService(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<Guid> CreateAsync(Loan entity)
        {
            var id = await _uow.Loans.CreateAsync(entity);
            _uow.Commit();

            return id;
        }
        public async Task<Loan> GetAsync(Guid id)
        {
            return await _uow.Loans.GetByIdAsync(id);
        }
        public async Task<IEnumerable<Loan>> GetAllAsync()
        {
            return await _uow.Loans.GetAllAsync();
        }
        public async Task UpdateAsync(Loan entity)
        {
            await _uow.Loans.UpdateAsync(entity);
            _uow.Commit();
        }
        public async Task DeleteAsync(Guid id)
        {
            await _uow.Loans.DeleteAsync(id);
            _uow.Commit();
        }

        public void Dispose()
        {
            _uow.Dispose();
        }
    }
}
