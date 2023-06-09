﻿using System.Dynamic;
using SchoolLibrary_EF.API.Mapping.Configurations;
using SchoolLibrary_EF.BLL.DTO;
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


        public async Task<Guid> CreateAsync(LoanDTO entity)
        {
            // We create a Loan object and copy the values ​​of the properties
            // of the entity object into its properties (we perform mapping)
            Loan loan = MappingFunctions.MapSourceToDestination<LoanDTO, Loan>(entity);


            await SeedingLoanObject(entity, loan);

            var id = await _uow.Loans.CreateAsync(loan);
            await _uow.SaveChangesAsync();

            return id;
        }
        public async Task<IEnumerable<LoanDTO>> GetAllAsync(BaseParameters parameters)
        {
            // Use Mapster to project one collection onto another
            return MappingFunctions.MapListSourceToDestination<Loan, LoanDTO>
                (await _uow.Loans.GetAllAsync(parameters));
        }
        public async Task<LoanDTO?> GetAsync(Guid id)
        {
            // Get entity from db
            Loan? loan = await _uow.Loans.GetByIdAsync(id);

            // We create a LoanDTO object and copy the values ​​of the properties
            // of the loan object into its properties (we perform mapping)
            LoanDTO? loanDTO =
                // There may be no entity in the database,
                // exception catching must be implemented on the controller side
                loan == null ?
                null : MappingFunctions.MapSourceToDestination<Loan, LoanDTO>(loan);

            return loanDTO;
        }
        public async Task UpdateAsync(LoanDTO entity)
        {
            // We create a Loan object and copy the values ​​of the properties
            // of the entity object into its properties (we perform mapping)
            Loan loan = MappingFunctions.MapSourceToDestination<LoanDTO, Loan>(entity);

            await SeedingLoanObject(entity, loan);

            await _uow.Loans.UpdateAsync(loan);
            await _uow.SaveChangesAsync();
        }
        public async Task DeleteAsync(Guid id)
        {
            await _uow.Loans.DeleteAsync(id);
            await _uow.SaveChangesAsync();
        }

        public async Task<PagedList<ExpandoObject>> GetAll_DataShaping_Async(BaseParameters? parameters = null)
        {
            return await _uow.Loans.GetAll_DataShaping_Async(parameters);
        }
        public async Task<ExpandoObject?> GetById_DataShaping_Async(Guid id, BaseParameters? parameters = null)
        {
            return await _uow.Loans.GetById_DataShaping_Async(id, parameters);
        }

        private async Task SeedingLoanObject(LoanDTO entity, Loan loan)
        {
            var book = (await _uow.Books.GetAllAsync()).ToList()
                .FirstOrDefault(b => b.Title == entity.BookTitle);
            var user = (await _uow.Users.GetAllAsync()).ToList()
                .FirstOrDefault(u => $"{u.FirstName} {u.LastName}" == entity.UserFullName);

            if (book == null) throw new Exception("No book with this title was found");
            if (user == null) throw new Exception("No user with this name was found");
            
            loan.User = user;
            loan.Book = book;
        }
    }
}
