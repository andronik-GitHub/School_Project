using Microsoft.EntityFrameworkCore;
using SchoolLibrary_EF.DAL.Data;
using SchoolLibrary_EF.DAL.Entities;
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
        public override async Task<IEnumerable<Review>> GetAllAsync()
        {
            entities.ToList().ForEach(review =>
                dbContext.Reviews
                    .Include(r => r.Book)
                    .Include(r => r.User)
                    .FirstOrDefault(r => r.BookId == review.BookId && r.UserId == review.UserId)
                );

            return await entities.ToListAsync();
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
