using SchoolLibrary_Dapper.BLL.DTOs.BookAuthorDTOs;

namespace SchoolLibrary_Dapper.BLL.Services.Consracts
{
    public interface IBookAuthorsService
    {
        Task<(Guid, Guid)> CreateAsync(InsertDTO_BookAuthors entity);
        Task<GetDTO_BookAuthors?> GetAsync(Guid BookId, Guid AuthorId);
        Task<GetDTOInclude_BookAuthors?> Include_GetAsync(Guid BookId, Guid AuthorId);
        Task<IEnumerable<GetDTO_BookAuthors>> GetAllAsync();
        Task<IEnumerable<GetDTOInclude_BookAuthors>> Include_GetAllAsync();
        Task UpdateAsync(UpdateDTO_BookAuthors entity);
        Task DeleteAsync(Guid BookId, Guid AuthorId);
        
        void Dispose();
    }
}
