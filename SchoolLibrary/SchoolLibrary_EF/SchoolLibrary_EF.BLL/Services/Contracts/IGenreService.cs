using SchoolLibrary_EF.BLL.DTOs.GenreDTOs;
using SchoolLibrary_EF.DAL.Paging.Entities;

namespace SchoolLibrary_EF.BLL.Services.Contracts
{
    public interface IGenreService : IGenericService<Guid, GetDTO_Genre, InsertDTO_Genre, UpdateDTO_Genre>
    {
        /// <summary>
        /// Get count the number of books of each genre
        /// </summary>
        /// <param name="parameters">GenreParameters for paging</param>
        /// <returns>Returns collection of genre names with number of books</returns>
        Task<IEnumerable<GetDTO_CountOfBooksEachGenre>> GetCountOfBooksEachGenreAsync(GenreParameters parameters);
    }
}
