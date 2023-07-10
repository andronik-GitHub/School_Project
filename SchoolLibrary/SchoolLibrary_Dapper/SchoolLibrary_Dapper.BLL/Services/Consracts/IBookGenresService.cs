using SchoolLibrary_Dapper.BLL.DTOs.BookGenreDTOs;

namespace SchoolLibrary_Dapper.BLL.Services.Consracts
{
    public interface IBookGenresService
    {
        Task<(Guid, Guid)> CreateAsync(InsertDTO_BookGenres entity);
        Task<GetDTO_BookGenres?> GetAsync(Guid BookId, Guid GenreId);
        Task<GetDTOInclude_BookGenres?> Include_GetAsync(Guid BookId, Guid GenreId);
        Task<IEnumerable<GetDTO_BookGenres>> GetAllAsync();
        Task<IEnumerable<GetDTOInclude_BookGenres>> Include_GetAllAsync();
        Task UpdateAsync(UpdateDTO_BookGenres entity);
        Task DeleteAsync(Guid BookId, Guid GenreId);
        
        void Dispose();
    }
}
