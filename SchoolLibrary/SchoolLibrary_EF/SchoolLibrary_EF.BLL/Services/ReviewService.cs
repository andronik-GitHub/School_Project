using System.Dynamic;
using SchoolLibrary_EF.BLL.DTOs.ReviewDTOs;
using SchoolLibrary_EF.BLL.Mapping;
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


        public async Task<Guid> CreateAsync(InsertDTO_Review entity)
        {
            // Mapping with Mapster
            Review review = MappingFunctions.MapSourceToDestination<InsertDTO_Review, Review>(entity);

            review.User = default!;
            review.Book = default!;

            var id = await _uow.Reviews.CreateAsync(review);
            await _uow.SaveChangesAsync();

            return id;
        }
        public async Task<IEnumerable<GetDTO_Review>> GetAllAsync(BaseParameters parameters)
        {
            // Use Mapster to project one collection onto another
            return MappingFunctions.MapListSourceToDestination<Review, GetDTO_Review>
                (await _uow.Reviews.GetAllAsync(parameters));
        }
        public async Task<GetDTO_Review?> GetAsync(Guid id)
        {
            Review? review = await _uow.Reviews.GetByIdAsync(id);
            
            GetDTO_Review? reviewDTO = MappingFunctions
                .MapSourceToDestination<Review?, GetDTO_Review?>(review); // Mapping with Mapster

            return reviewDTO;
        }
        public async Task UpdateAsync(UpdateDTO_Review entity)
        {
            Review review = MappingFunctions
                .MapSourceToDestination<UpdateDTO_Review, Review>(entity); // Mapping with Mapster

            await _uow.Reviews.UpdateAsync(review);
            await _uow.SaveChangesAsync();
        }
        public async Task DeleteAsync(Guid id)
        {
            await _uow.Reviews.DeleteAsync(id);
            await _uow.SaveChangesAsync();
        }
        

        public async Task<PagedList<ExpandoObject>> GetAll_DataShaping_Async(BaseParameters parameters)
        {
            return await _uow.Reviews.GetAll_DataShaping_Async(parameters);
        }
        public async Task<ExpandoObject?> GetById_DataShaping_Async(Guid id, BaseParameters parameters)
        {
            return await _uow.Reviews.GetById_DataShaping_Async(id, parameters);
        }
    }
}
