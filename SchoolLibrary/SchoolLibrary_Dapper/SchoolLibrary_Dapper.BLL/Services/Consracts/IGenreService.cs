using SchoolLibrary_Dapper.BLL.DTOs.GenreDTOs;

namespace SchoolLibrary_Dapper.BLL.Services.Consracts
{
    public interface IGenreService : IGenericService
    {
        Task<Guid> CreateAsync(InsertDTO_Genre entity);
        Task<GetDTO_Genre?> GetAsync(Guid id);
        Task<IEnumerable<GetDTO_Genre>> GetAllAsync();
        Task UpdateAsync(UpdateDTO_Genre entity);
        Task DeleteAsync(Guid id);
    }
}
