using AutoMapper;
using SchoolLibrary_Dapper.DAL.Entities;
using SchoolLibrary_Dapper.DAL.Repositories.Contracts;
using SchoolLibrary_Dapper.BLL.DTOs.ReviewDTOs;
using SchoolLibrary_Dapper.BLL.Services.Consracts;

namespace SchoolLibrary_Dapper.BLL.Services
{
    public class ReviewService : IReviewService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public ReviewService(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<Guid> CreateAsync(InsertDTO_Review entity)
        {
            Review newEntity = _mapper.Map<Review>(entity); // Mapping without AutoMapper
            
            var id = await _uow.Reviews.CreateAsync(newEntity);
            _uow.Commit();

            return id;
        }
        public async Task<GetDTO_Review?> GetAsync(Guid id)
        {
            var entity = await _uow.Reviews.GetByIdAsync(id);

            GetDTO_Review? dto = _mapper.Map<GetDTO_Review?>(entity); // Mapping without AutoMapper
            
            return dto;
        }
        public async Task<IEnumerable<GetDTO_Review>> GetAllAsync()
        {
            // Use AutoMapper to project one collection onto another
            var list = _mapper.Map<IEnumerable<Review>, IEnumerable<GetDTO_Review>>(await _uow.Reviews.GetAllAsync());
            return list;
        }
        public async Task UpdateAsync(UpdateDTO_Review entity)
        {
            Review upEntity = _mapper.Map<Review>(entity); // Mapping without AutoMapper
            
            upEntity.DateCreated = (await _uow.Reviews.GetByIdAsync(upEntity.ReviewId))!.DateCreated;
            upEntity.DateUpdated = DateTime.UtcNow;

            await _uow.Reviews.UpdateAsync(upEntity);
            _uow.Commit();
        }
        public async Task DeleteAsync(Guid id)
        {
            await _uow.Reviews.DeleteAsync(id);
            _uow.Commit();
        }
        
        public async Task<IEnumerable<GetDTO_ReviewWithUserAndBook>> GetReviewsWithUsersAndBooksAsync()
        {
            // Use AutoMapper to project one collection onto another
            var list = _mapper.Map<IEnumerable<(Review, User, Book)>, IEnumerable<GetDTO_ReviewWithUserAndBook>>
                (await _uow.Reviews.GetReviewsWithUsersAndBooksAsync());
            return list;
        }
        public async Task<GetDTO_ReviewWithUserAndBook?> GetReviewWithUserAndBookAsync(Guid id)
        {
            var entity = await _uow.Reviews.GetByIdReviewWithUserAndBookAsync(id);

            GetDTO_ReviewWithUserAndBook? dto = _mapper.Map<GetDTO_ReviewWithUserAndBook?>(entity); // Mapping without AutoMapper
            
            return dto;
        }

        public void Dispose()
        {
            _uow.Dispose();
        }
    }
}
