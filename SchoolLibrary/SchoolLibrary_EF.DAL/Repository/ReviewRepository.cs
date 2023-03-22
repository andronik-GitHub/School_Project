using Microsoft.EntityFrameworkCore;
using SchoolLibrary_EF.DAL.Data;
using SchoolLibrary_EF.DAL.Entities;
using SchoolLibrary_EF.DAL.Pagging;
using SchoolLibrary_EF.DAL.Repositories.Contracts;
using SchoolLibrary_EF.DAL.Repository;

namespace SchoolLibrary_EF.DAL.Repositories
{
    public class ReviewRepository : GenericRepository<Review>, IReviewRepository
    {
        public ReviewRepository(SchoolLibraryContext dbContext)
            : base(dbContext)
        {
        }


        public override async Task<Guid> CreateAsync(Review review)
        {
            await entities.AddAsync(review);

            return review.ReviewId;
        }
        public override async Task<IEnumerable<Review>> GetAllAsync<TOrderBy>
            (BaseParameters? parameters = null, Func<Review, TOrderBy>? orderBy = null)
        {
            if (parameters == null)
                return await entities
                    .Include(entity => entity.Book)
                    .Include(entity => entity.User)
                    .ToListAsync();

            return await entities
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
