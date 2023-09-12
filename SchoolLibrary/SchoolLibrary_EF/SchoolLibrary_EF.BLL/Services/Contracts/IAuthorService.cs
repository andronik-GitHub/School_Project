using SchoolLibrary_EF.BLL.DTOs.AuthorDTOs;
using SchoolLibrary_EF.DAL.Paging.Entities;

namespace SchoolLibrary_EF.BLL.Services.Contracts
{
    public interface IAuthorService : IGenericService<Guid, GetDTO_Author, InsertDTO_Author, UpdateDTO_Author>
    {
        /// <summary>
        /// Gets author with highest avg book rating
        /// </summary>
        /// <returns>Returns author with highest avg book rating</returns>
        Task<GetDTO_AuthorWithHighestAvgBookRating> GetAuthorWithHighestAvgBookRatingAsync();
    }
}
