using SchoolLibrary_Dapper.BLL.DTOs.BookDTOs;

namespace SchoolLibrary_Dapper.BLL.Services.Consracts
{
    public interface IBookService : IGenericService
    {
        Task<Guid> CreateAsync(InsertDTO_Book entity);
        Task<GetDTO_Book?> GetAsync(Guid id);
        Task<IEnumerable<GetDTO_Book>> GetAllAsync();
        Task UpdateAsync(UpdateDTO_Book entity);
        Task DeleteAsync(Guid id);
        
        
        Task<IEnumerable<GetDTO_BookWithAuthorsAndGenres>> GetBooksWithAuthorsAndGenresAsync();
        Task<GetDTO_BookWithAuthorsAndGenres?> GetBookWithAuthorAndGenreAsync(Guid id);
    }
}
