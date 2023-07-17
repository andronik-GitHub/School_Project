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
    public class ReviewRepository : GenericRepository<Review>, IReviewRepository
    {
        public ReviewRepository(
            SchoolLibraryContext dbContext, 
            ISortHelper<Review> sortHelper,
            IDataShaper<Review> dataShaper)
            : base(dbContext, dataShaper, sortHelper)
        {
        }


        public override async Task<PagedList<Review>> GetAllAsync(BaseParameters parameters)
        {
            var collection = entities
                .AsNoTracking()
                .Include(entity => entity.Book)
                .Include(entity => entity.User);
            var sortCollection = _sortHelper.ApplySort(collection, parameters.OrderBy); // sorting

            return await Task.Run(() => // paging
                PagedList<Review>.ToPagedList(sortCollection, parameters.PageNumber, parameters.PageSize));
        }
        public override async Task<Review?> GetByIdAsync(Guid id)
        {
            return await entities
                .AsNoTracking()
                .Include(r => r.Book)
                .Include(r => r.User)
                .FirstOrDefaultAsync(r => r.ReviewId == id);
        }
    }
}
