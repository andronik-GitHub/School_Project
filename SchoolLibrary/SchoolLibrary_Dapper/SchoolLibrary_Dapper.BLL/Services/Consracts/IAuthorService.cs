using SchoolLibrary_Dapper.BLL.DTOs.AuthorDTOs;

namespace SchoolLibrary_Dapper.BLL.Services.Consracts
{
    public interface IAuthorService : IGenericService
    {
        Task<Guid> CreateAsync(InsertDTO_Author entity);
        Task<GetDTO_Author?> GetAsync(Guid id);
        Task<IEnumerable<GetDTO_Author>> GetAllAsync();
        Task UpdateAsync(UpdateDTO_Author entity);
        Task DeleteAsync(Guid id);
    }
}
