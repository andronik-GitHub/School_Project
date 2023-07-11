using AutoMapper;
using SchoolLibrary_Dapper.DAL.Entities;
using SchoolLibrary_Dapper.DAL.Repositories.Contracts;
using SchoolLibrary_Dapper.BLL.DTOs.LoanDTOs;
using SchoolLibrary_Dapper.BLL.Services.Consracts;

namespace SchoolLibrary_Dapper.BLL.Services
{
    public class LoanService : ILoanService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public LoanService(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<Guid> CreateAsync(InsertDTO_Loan entity)
        {
            Loan newEntity = _mapper.Map<Loan>(entity); // Mapping without AutoMapper

            var id = await _uow.Loans.CreateAsync(newEntity);
            _uow.Commit();

            return id;
        }
        public async Task<GetDTO_Loan?> GetAsync(Guid id)
        {
            var entity = await _uow.Loans.GetByIdAsync(id);

            GetDTO_Loan? dto = _mapper.Map<GetDTO_Loan?>(entity); // Mapping without AutoMapper
            
            return dto;
        }
        public async Task<IEnumerable<GetDTO_Loan>> GetAllAsync()
        {
            // Use AutoMapper to project one collection onto another
            var list = _mapper.Map<IEnumerable<Loan>, IEnumerable<GetDTO_Loan>>(await _uow.Loans.GetAllAsync());
            return list;
        }
        public async Task UpdateAsync(UpdateDTO_Loan entity)
        {
            Loan upEntity = _mapper.Map<Loan>(entity); // Mapping without AutoMapper
            
            upEntity.DateCreated = (await _uow.Loans.GetByIdAsync(upEntity.LoanId))!.DateCreated;
            upEntity.DateUpdated = DateTime.UtcNow;

            await _uow.Loans.UpdateAsync(upEntity);
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
