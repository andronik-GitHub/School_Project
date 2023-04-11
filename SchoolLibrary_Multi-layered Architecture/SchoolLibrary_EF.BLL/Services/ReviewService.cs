using System.Dynamic;
using SchoolLibrary_EF.API.Mapping.Configurations;
using SchoolLibrary_EF.BLL.DTO;
using SchoolLibrary_EF.BLL.Services.Contracts;
using SchoolLibrary_EF.DAL.Entities;
using SchoolLibrary_EF.DAL.Paging;
using SchoolLibrary_EF.DAL.Paging.Entities;
using SchoolLibrary_EF.DAL.Repository.Contracts;

namespace SchoolLibrary_EF.BLL.Services
{
    public class ReviewService : IReviewService
    {
        private readonly IUnitOfWork _uow;

        public ReviewService(IUnitOfWork uow)
        {
            _uow = uow;
        }


        public async Task<Guid> CreateAsync(ReviewDTO entity)
        {
            // We create a Review object and copy the values ​​of the properties
            // of the entity object into its properties (we perform mapping)
            Review review = MappingFunctions.MapSourceToDestination<ReviewDTO, Review>(entity);

            await SeedingReviewObject(entity, review);

            var id = await _uow.Reviews.CreateAsync(review);
            await _uow.SaveChangesAsync();

            return id;
        }
        public async Task<IEnumerable<ReviewDTO>> GetAllAsync(BaseParameters parameters)
        {
            // Use Mapster to project one collection onto another
            return MappingFunctions.MapListSourceToDestination<Review, ReviewDTO>
                (await _uow.Reviews.GetAllAsync(parameters));
        }
        public async Task<ReviewDTO?> GetAsync(Guid id)
        {
            // Get entity from db
            Review? review = await _uow.Reviews.GetByIdAsync(id);

            // We create a ReviewDTO object and copy the values ​​of the properties
            // of the review object into its properties (we perform mapping)
            ReviewDTO? reviewDTO =
                // There may be no entity in the database,
                // exception catching must be implemented on the controller side
                review == null ?
                null : MappingFunctions.MapSourceToDestination<Review, ReviewDTO>(review);

            return reviewDTO;
        }
        public async Task UpdateAsync(ReviewDTO entity)
        {
            // We create a Loan object and copy the values ​​of the properties
            // of the entity object into its properties (we perform mapping)
            Review review = MappingFunctions.MapSourceToDestination<ReviewDTO, Review>(entity);

            await SeedingReviewObject(entity, review);

            await _uow.Reviews.UpdateAsync(review);
            await _uow.SaveChangesAsync();
        }
        public async Task DeleteAsync(Guid id)
        {
            await _uow.Reviews.DeleteAsync(id);
            await _uow.SaveChangesAsync();
        }
        

        public async Task<PagedList<ExpandoObject>> GetAll_DataShaping_Async(BaseParameters? parameters = null)
        {
            return await _uow.Reviews.GetAll_DataShaping_Async(parameters);
        }
        public async Task<ExpandoObject?> GetById_DataShaping_Async(Guid id, BaseParameters? parameters = null)
        {
            return await _uow.Reviews.GetById_DataShaping_Async(id, parameters);
        }

        private async Task SeedingReviewObject(ReviewDTO entity, Review review)
        {
            var book = (await _uow.Books.GetAllAsync()).ToList()
                .FirstOrDefault(b => b.Title == entity.BookTitle);
            var user = (await _uow.Users.GetAllAsync()).ToList()
                .FirstOrDefault(u => $"{u.FirstName} {u.LastName}" == entity.UserFullName);

            if (book == null) throw new Exception("No book with this title was found");
            if (user == null) throw new Exception("No user with this name was found");
            
            review.User = user;
            review.Book = book;
        }
    }
}
