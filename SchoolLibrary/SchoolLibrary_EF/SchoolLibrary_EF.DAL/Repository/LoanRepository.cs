using Microsoft.EntityFrameworkCore;
using SchoolLibrary_EF.DAL.Data;
using SchoolLibrary_EF.DAL.Entities;
using SchoolLibrary_EF.DAL.Helper.Contracts;
using SchoolLibrary_EF.DAL.Helpers.Contracts;
using SchoolLibrary_EF.DAL.Paging;
using SchoolLibrary_EF.DAL.Paging.Entities;
using SchoolLibrary_EF.DAL.Repository.Contracts;

namespace SchoolLibrary_EF.DAL.Repository
{
    public class LoanRepository : GenericRepository<Loan>, ILoanRepository
    {
        public LoanRepository(
            SchoolLibraryContext dbContext, 
            ISortHelper<Loan> sortHelper,
            IDataShaper<Loan> dataShaper)
            : base(dbContext, dataShaper, sortHelper)
        {
        }


        public override async Task<PagedList<Loan>> GetAllAsync(BaseParameters parameters)
        {
            var collection = entities
                .AsNoTracking()
                .Include(l => l.User)
                .Include(l => l.Book);
            
            var sortCollection = _sortHelper.ApplySort(collection, parameters.OrderBy); // sorting

            return await Task.Run(() => // paging
                PagedList<Loan>.ToPagedList(sortCollection, parameters.PageNumber, parameters.PageSize));
        }
        public override async Task<Loan?> GetByIdAsync(Guid id)
        {
            return await entities
                .AsNoTracking()
                .Include(l => l.Book)
                .Include(l => l.User)
                .FirstOrDefaultAsync(b => b.LoanId == id);
        }
    }
}
