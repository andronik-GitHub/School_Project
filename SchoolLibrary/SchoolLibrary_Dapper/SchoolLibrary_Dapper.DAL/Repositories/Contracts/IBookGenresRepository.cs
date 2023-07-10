using SchoolLibrary_Dapper.DAL.Entities;

namespace SchoolLibrary_Dapper.DAL.Repositories.Contracts
{
    public interface IBookGenresRepository
    {
        Task<(Guid, Guid)> CreateAsync(BookGenres entity);
        Task<BookGenres?> GetByIdsAsync(Guid BookId, Guid GenreId);
        Task<(Book, Genre)?> Include_GetByIdsAsync(Guid BookId, Guid GenreId);
        Task<IEnumerable<BookGenres>> GetAllAsync();
        Task<IEnumerable<(Book, Genre)>> Include_GetAllAsync();
        Task UpdateAsync(BookGenres entity);
        Task DeleteAsync(Guid BookId, Guid GenreId);
    }
}
