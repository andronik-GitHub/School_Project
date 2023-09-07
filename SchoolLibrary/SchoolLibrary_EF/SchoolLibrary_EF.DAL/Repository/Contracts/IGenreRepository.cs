using SchoolLibrary_EF.DAL.Entities;
using SchoolLibrary_EF.DAL.Paging;
using SchoolLibrary_EF.DAL.Paging.Entities;

namespace SchoolLibrary_EF.DAL.Repository.Contracts
{
    public interface IGenreRepository : IGenericRepository<Genre, Guid>
    {
        /// <summary>
        /// Get count the number of books of each genre
        /// </summary>
        /// <param name="parameters">GenreParameters for paging</param>
        /// <returns>Returns collection of genre names with number of books</returns>
        Task<PagedList<(string GenreName, int BookCount)>> GetCountOfBooksEachGenreAsync(GenreParameters parameters);
    }
}
