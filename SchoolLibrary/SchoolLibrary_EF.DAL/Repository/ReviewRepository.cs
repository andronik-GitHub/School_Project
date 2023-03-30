using Microsoft.EntityFrameworkCore;
using SchoolLibrary_EF.DAL.Data;
using SchoolLibrary_EF.DAL.Entities;
using SchoolLibrary_EF.DAL.Helper.Contracts;
using SchoolLibrary_EF.DAL.Paging.Entities;
using SchoolLibrary_EF.DAL.Repository.Contracts;

namespace SchoolLibrary_EF.DAL.Repository
{
    public class ReviewRepository : GenericRepository<Review>, IReviewRepository
    {
        private readonly ISortHelper<Review> _sortHelper;
        public ReviewRepository(SchoolLibraryContext dbContext, ISortHelper<Review> sortHelper)
            : base(dbContext)
        {
            _sortHelper = sortHelper;
        }


        public override async Task<Guid> CreateAsync(Review review)
        {
            await entities.AddAsync(review);

            return review.ReviewId;
        }
        public override async Task<IEnumerable<Review>> GetAllAsync(BaseParameters? parameters = null)
        {
            if (parameters == null) return await base.GetAllAsync();


            var collection = entities.AsNoTracking();

            if (parameters is ReviewParameters param)
            {
                var newCollection = _sortHelper.ApplySort(collection, param.OrderBy); // sorting

                return await newCollection
                    //.OrderBy(entity => entity.ReviewId) after sorting, it makes no sense to sort by id
                    .Skip((parameters.PageNumber - 1) * parameters.PageSize)
                    .Take(parameters.PageSize)
                    .Include(entity => entity.Book)
                    .Include(entity => entity.User)
                    .ToListAsync();
            }

            return await collection
                .OrderBy(entity => entity.ReviewId)
                .Skip((parameters.PageNumber - 1) * parameters.PageSize)
                .Take(parameters.PageSize)
                .Include(entity => entity.Book)
                .Include(entity => entity.User)
                .ToListAsync();
        }
        public override async Task<Review?> GetByIdAsync(Guid id)
        {
            return await entities
                .Include(r => r.Book)
                .Include(r => r.User)
                .FirstOrDefaultAsync(r => r.ReviewId == id);
        }
    }
}
