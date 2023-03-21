using Microsoft.EntityFrameworkCore;
using SchoolLibrary_EF.DAL.Data;
using SchoolLibrary_EF.DAL.Entities;
using SchoolLibrary_EF.DAL.Repositories.Contracts;
using SchoolLibrary_EF.DAL.Repository;

namespace SchoolLibrary_EF.DAL.Repositories
{
    public class LoanRepository : GenericRepository<Loan>, ILoanRepository
    {
        public LoanRepository(SchoolLibraryContext dbContext)
            : base(dbContext)
        {
        }


        public override async Task<Guid> CreateAsync(Loan loan)
        {
            await entities.AddAsync(loan);

            return loan.LoanId;
        }
        public override async Task<IEnumerable<Loan>> GetAllAsync()
        {
            entities.ToList().ForEach(loan =>
                dbContext.Loans
                    .Include(l => l.Book)
                    .Include(l => l.User)
                    .FirstOrDefault(l => l.BookId == loan.BookId && l.UserId == loan.UserId)
                );

            return await entities.ToListAsync();
        }
        public override async Task<Loan?> GetByIdAsync(Guid id)
        {
            return await entities
                .Include(l => l.Book)
                .Include(l => l.User)
                .FirstOrDefaultAsync(b => b.LoanId == id);
        }
    }
}
