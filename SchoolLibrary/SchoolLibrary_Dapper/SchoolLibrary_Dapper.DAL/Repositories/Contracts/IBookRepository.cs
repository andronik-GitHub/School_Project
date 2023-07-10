using SchoolLibrary_Dapper.DAL.Entities;

namespace SchoolLibrary_Dapper.DAL.Repositories.Contracts
{
    public interface IBookRepository : IGenericRepository<Book>
    {
        Task<(Book, Author, Genre)?> GetByIdBookWithAuthorAndGenreAsync(Guid id);
        Task<IEnumerable<(Book, Author, Genre)>> GetBooksWithAuthorsAndGenresAsync();
    }
}
