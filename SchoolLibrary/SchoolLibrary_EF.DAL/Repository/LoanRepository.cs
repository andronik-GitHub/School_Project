using Microsoft.EntityFrameworkCore;
using SchoolLibrary_EF.DAL.Data;
using SchoolLibrary_EF.DAL.Entities;
using SchoolLibrary_EF.DAL.Pagging.Entities;
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
        public override async Task<IEnumerable<Loan>> GetAllAsync(BaseParameters? parameters = null)
        {
            if (parameters == null) return await base.GetAllAsync();

            return await entities
                .OrderBy(entity => entity.LoanId)
                .Skip((parameters.PageNumber - 1) * parameters.PageSize)
                .Take(parameters.PageSize)
                .Include(entity => entity.Book)
                .Include(entity => entity.User)
                .ToListAsync();
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
