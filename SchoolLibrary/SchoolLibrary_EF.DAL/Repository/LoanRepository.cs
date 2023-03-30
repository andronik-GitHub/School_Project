using Microsoft.EntityFrameworkCore;
using SchoolLibrary_EF.DAL.Data;
using SchoolLibrary_EF.DAL.Entities;
using SchoolLibrary_EF.DAL.Helper.Contracts;
using SchoolLibrary_EF.DAL.Paging.Entities;
using SchoolLibrary_EF.DAL.Repository.Contracts;

namespace SchoolLibrary_EF.DAL.Repository
{
    public class LoanRepository : GenericRepository<Loan>, ILoanRepository
    {
        private readonly ISortHelper<Loan> _sortHelper;
        public LoanRepository(SchoolLibraryContext dbContext, ISortHelper<Loan> sortHelper)
            : base(dbContext)
        {
            _sortHelper = sortHelper;
        }


        public override async Task<Guid> CreateAsync(Loan loan)
        {
            await entities.AddAsync(loan);

            return loan.LoanId;
        }
        public override async Task<IEnumerable<Loan>> GetAllAsync(BaseParameters? parameters = null)
        {
            if (parameters == null) return await base.GetAllAsync();


            var collection = entities.AsNoTracking();

            if (parameters is LoanParameters param)
            {
                var newCollection = _sortHelper.ApplySort(collection, param.OrderBy); // sorting

                return await newCollection
                    //.OrderBy(entity => entity.LoanId) after sorting, it makes no sense to sort by id
                    .Skip((parameters.PageNumber - 1) * parameters.PageSize)
                    .Take(parameters.PageSize)
                    .Include(entity => entity.Book)
                    .Include(entity => entity.User)
                    .ToListAsync();
            }

            return await collection
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
