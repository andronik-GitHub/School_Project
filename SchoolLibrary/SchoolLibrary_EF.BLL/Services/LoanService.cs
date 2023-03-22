using SchoolLibrary_EF.API.Mapping.Configurations;
using SchoolLibrary_EF.BLL.DTO;
using SchoolLibrary_EF.BLL.Services.Contracts;
using SchoolLibrary_EF.DAL.Entities;
using SchoolLibrary_EF.DAL.Pagging;
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


            var book = (await _uow.Books.GetAllAsync(new BookParameters()))
                .ToList()
                .Where(b => b.Title == entity.BookTitle)
                .FirstOrDefault();
            var user = (await _uow.Users.GetAllAsync(new UserParameters()))
                .ToList()
                .Where(u => $"{u.FirstName} {u.LastName}" == entity.UserFullName)
                .FirstOrDefault();

            if (book == null) throw new Exception("No book with this title was found");
            else loan.Book = book;
            if (user == null) throw new Exception("No user with this name was found");
            else loan.User = user;


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

            await _uow.Loans.UpdateAsync(loan);
            await _uow.SaveChangesAsync();
        }
        public async Task DeleteAsync(Guid id)
        {
            await _uow.Loans.DeleteAsync(id);
            await _uow.SaveChangesAsync();
        }
    }
}
