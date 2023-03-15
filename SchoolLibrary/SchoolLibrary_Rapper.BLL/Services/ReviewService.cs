using SchoolLibrary_Dapper.DAL.Entities;
using SchoolLibrary_Dapper.DAL.Repositories.Contracts;
using SchoolLibrary_Rapper.BLL.DTO;
using SchoolLibrary_Rapper.BLL.Services.Consracts;

namespace SchoolLibrary_Rapper.BLL.Services
{
    public class ReviewService : IReviewService
    {
        IUnitOfWork _uow;

        public ReviewService(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<Guid> CreateAsync(ReviewDTO entity)
        {
            // Mapping without AutoMapper
            var id = await _uow.Reviews.CreateAsync(new Review
            {
                ReviewId = entity.ReviewId,
                UserId = entity.UserId,
                Rating = entity.Rating,
                ReviewText = entity.ReviewText,
            });
            _uow.Commit();

            return id;
        }
        public async Task<ReviewDTO> GetAsync(Guid id)
        {
            var entity = await _uow.Reviews.GetByIdAsync(id);

            // Mapping without AutoMapper
            return new ReviewDTO
            {
                ReviewId = entity.ReviewId,
                UserId = entity.UserId,
                Rating = entity.Rating,
                ReviewText = entity.ReviewText,
            };
        }
        public async Task<IEnumerable<ReviewDTO>> GetAllAsync()
        {
            var list = await _uow.Reviews.GetAllAsync();
            var result = new List<ReviewDTO>();

            // Mapping without AutoMapper
            list.ToList().ForEach(entity => result.Add(new ReviewDTO
            {
                ReviewId = entity.ReviewId,
                UserId = entity.UserId,
                Rating = entity.Rating,
                ReviewText = entity.ReviewText,
            }));

            return result;
        }
        public async Task UpdateAsync(ReviewDTO entity)
        {
            // Mapping without AutoMapper
            await _uow.Reviews.UpdateAsync(new Review
            {
                ReviewId = entity.ReviewId,
                UserId = entity.UserId,
                Rating = entity.Rating,
                ReviewText = entity.ReviewText,
            });
            _uow.Commit();
        }
        public async Task DeleteAsync(Guid id)
        {
            await _uow.Reviews.DeleteAsync(id);
            _uow.Commit();
        }

        public void Dispose()
        {
            _uow.Dispose();
        }
    }
}
