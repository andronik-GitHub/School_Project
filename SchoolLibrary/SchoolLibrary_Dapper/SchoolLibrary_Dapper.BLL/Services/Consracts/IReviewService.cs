using SchoolLibrary_Dapper.BLL.DTOs.ReviewDTOs;

namespace SchoolLibrary_Dapper.BLL.Services.Consracts
{
    public interface IReviewService : IGenericService
    {
        Task<Guid> CreateAsync(InsertDTO_Review entity);
        Task<GetDTO_Review?> GetAsync(Guid id);
        Task<IEnumerable<GetDTO_Review>> GetAllAsync();
        Task UpdateAsync(UpdateDTO_Review entity);
        Task DeleteAsync(Guid id);
        
        
        Task<IEnumerable<GetDTO_ReviewWithUserAndBook>> GetReviewsWithUsersAndBooksAsync();
        Task<GetDTO_ReviewWithUserAndBook?> GetReviewWithUserAndBookAsync(Guid id);
    }
}
