using SchoolLibrary_Dapper.DAL.Entities;

namespace SchoolLibrary_Dapper.DAL.Repositories.Contracts
{
    public interface IBookAuthorsRepository
    {
        Task<(Guid, Guid)> CreateAsync(BookAuthors entity);
        Task<IEnumerable<BookAuthors>> GetAllAsync();
        Task<IEnumerable<(Book, Author)>> Include_GetAllAsync();
        Task<BookAuthors?> GetByIdsAsync(Guid BookId, Guid AuthorId);
        Task<(Book, Author)?> Include_GetByIdsAsync(Guid BookId, Guid AuthorId);
        Task UpdateAsync(BookAuthors entity);
        Task DeleteAsync(Guid BookId, Guid AuthorId);
    }
}
