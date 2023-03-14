using SchoolLibrary_Dapper.DAL.Entities;
using SchoolLibrary_Dapper.DAL.Repositories.Contracts;
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

        public async Task<Guid> CreateAsync(Review entity)
        {
            var id = await _uow.Reviews.CreateAsync(entity);
            _uow.Commit();

            return id;
        }
        public async Task<Review> GetAsync(Guid id)
        {
            return await _uow.Reviews.GetByIdAsync(id);
        }
        public async Task<IEnumerable<Review>> GetAllAsync()
        {
            return await _uow.Reviews.GetAllAsync();
        }
        public async Task UpdateAsync(Review entity)
        {
            await _uow.Reviews.UpdateAsync(entity);
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
