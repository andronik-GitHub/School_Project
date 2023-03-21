using SchoolLibrary_EF.API.Mapping.Configurations;
using SchoolLibrary_EF.BLL.DTO;
using SchoolLibrary_EF.BLL.Services.Contracts;
using SchoolLibrary_EF.DAL.Entities;
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


            var book = (await _uow.Books.GetAllAsync())
                .ToList()
                .Where(b => b.Title == entity.BookTitle)
                .FirstOrDefault();
            var user = (await _uow.Users.GetAllAsync())
                .ToList()
                .Where(u => $"{u.FirstName} {u.LastName}" == entity.UserFullName)
                .FirstOrDefault();

            if (book == null) throw new Exception("No book with this title was found");
            else review.Book = book;
            if (user == null) throw new Exception("No user with this name was found");
            else review.User = user;


            var id = await _uow.Reviews.CreateAsync(review);
            await _uow.SaveChangesAsync();

            return id;
        }
        public async Task<IEnumerable<ReviewDTO>> GetAllAsync()
        {
            // Use Mapster to project one collection onto another
            return MappingFunctions.MapListSourceToDestination<Review, ReviewDTO>
                (await _uow.Reviews.GetAllAsync());
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

            await _uow.Reviews.UpdateAsync(review);
            await _uow.SaveChangesAsync();
        }
        public async Task DeleteAsync(Guid id)
        {
            await _uow.Reviews.DeleteAsync(id);
            await _uow.SaveChangesAsync();
        }
    }
}
