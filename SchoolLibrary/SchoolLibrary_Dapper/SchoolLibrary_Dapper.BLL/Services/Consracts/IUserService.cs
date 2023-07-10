using SchoolLibrary_Dapper.BLL.DTOs.UserDTOs;

namespace SchoolLibrary_Dapper.BLL.Services.Consracts
{
    public interface IUserService : IGenericService
    {
        Task<Guid> CreateAsync(InsertDTO_User entity);
        Task<GetDTO_User?> GetAsync(Guid id);
        Task<IEnumerable<GetDTO_User>> GetAllAsync();
        Task UpdateAsync(UpdateDTO_User entity);
        Task DeleteAsync(Guid id);
    }
}
