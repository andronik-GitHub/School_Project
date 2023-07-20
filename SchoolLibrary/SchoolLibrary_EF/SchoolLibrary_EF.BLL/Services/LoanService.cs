using System.Dynamic;
using SchoolLibrary_EF.BLL.DTOs.LoanDTOs;
using SchoolLibrary_EF.BLL.Mapping;
using SchoolLibrary_EF.BLL.Services.Contracts;
using SchoolLibrary_EF.DAL.Entities;
using SchoolLibrary_EF.DAL.Paging;
using SchoolLibrary_EF.DAL.Paging.Entities;
using SchoolLibrary_EF.DAL.Repository.Contracts;

namespace SchoolLibrary_EF.BLL.Services
{
    public class LoanService : ILoanService
    {
        private readonly IUnitOfWork _uow;

        public LoanService(IUnitOfWork uow)
        {
            _uow = uow;
        }


        public async Task<Guid> CreateAsync(InsertDTO_Loan entity)
        {
            // Mapping with Mapster
            Loan loan = MappingFunctions.MapSourceToDestination<InsertDTO_Loan, Loan>(entity);

            loan.User = default!;
            loan.Book = default!;
            
            var id = await _uow.Loans.CreateAsync(loan);
            await _uow.SaveChangesAsync();

            return id;
        }
        public async Task<IEnumerable<GetDTO_Loan>> GetAllAsync(BaseParameters parameters)
        {
            // Use Mapster to project one collection onto another
            return MappingFunctions.MapListSourceToDestination<Loan, GetDTO_Loan>
                (await _uow.Loans.GetAllAsync(parameters));
        }
        public async Task<GetDTO_Loan?> GetAsync(Guid id)
        {
            Loan? loan = await _uow.Loans.GetByIdAsync(id);

            GetDTO_Loan? loanDTO = MappingFunctions
                .MapSourceToDestination<Loan?, GetDTO_Loan?>(loan); // Mapping with Mapster

            return loanDTO;
        }
        public async Task UpdateAsync(UpdateDTO_Loan entity)
        {
            Loan loan = MappingFunctions.MapSourceToDestination<UpdateDTO_Loan, Loan>(entity); // Mapping with Mapster

            await _uow.Loans.UpdateAsync(loan);
            await _uow.SaveChangesAsync();
        }
        public async Task DeleteAsync(Guid id)
        {
            await _uow.Loans.DeleteAsync(id);
            await _uow.SaveChangesAsync();
        }

        public async Task<PagedList<ExpandoObject>> GetAll_DataShaping_Async(BaseParameters parameters)
        {
            return await _uow.Loans.GetAll_DataShaping_Async(parameters);
        }
        public async Task<ExpandoObject?> GetById_DataShaping_Async(Guid id, BaseParameters parameters)
        {
            return await _uow.Loans.GetById_DataShaping_Async(id, parameters);
        }
    }
}
