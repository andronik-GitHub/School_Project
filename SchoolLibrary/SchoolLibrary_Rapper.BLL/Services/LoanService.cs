using SchoolLibrary_Dapper.DAL.Entities;
using SchoolLibrary_Dapper.DAL.Repositories.Contracts;
using SchoolLibrary_Rapper.BLL.DTO;
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

        public async Task<Guid> CreateAsync(LoanDTO entity)
        {
            // Mapping without AutoMapper
            var id = await _uow.Loans.CreateAsync(new Loan
            {
                LoanId = entity.LoanId,
                UserId = entity.UserId,
                BookId = entity.BookId,
                LoanDate = entity.LoanDate,
                ReturnDate = entity.ReturnDate,
            });
            _uow.Commit();

            return id;
        }
        public async Task<LoanDTO> GetAsync(Guid id)
        {
            var entity = await _uow.Loans.GetByIdAsync(id);

            // Mapping without AutoMapper
            return new LoanDTO
            {
                LoanId = entity.LoanId,
                UserId = entity.UserId,
                BookId = entity.BookId,
                LoanDate = entity.LoanDate,
                ReturnDate = entity.ReturnDate,
            };
        }
        public async Task<IEnumerable<LoanDTO>> GetAllAsync()
        {
            var list = await _uow.Loans.GetAllAsync();
            var result = new List<LoanDTO>();

            // Mapping without AutoMapper
            list.ToList().ForEach(entity => result.Add(new LoanDTO
            {
                LoanId = entity.LoanId,
                UserId = entity.UserId,
                BookId = entity.BookId,
                LoanDate = entity.LoanDate,
                ReturnDate = entity.ReturnDate,
            }));

            return result;
        }
        public async Task UpdateAsync(LoanDTO entity)
        {
            // Mapping without AutoMapper
            await _uow.Loans.UpdateAsync(new Loan
            {
                LoanId = entity.LoanId,
                UserId = entity.UserId,
                BookId = entity.BookId,
                LoanDate = entity.LoanDate,
                ReturnDate = entity.ReturnDate,
            });
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
