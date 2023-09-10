using SchoolLibrary_EF.BLL.DTOs.BookDTOs;
using SchoolLibrary_EF.DAL.Paging.Entities;

namespace SchoolLibrary_EF.BLL.Services.Contracts
{
    public interface IBookService : IGenericService<Guid, GetDTO_Book, InsertDTO_Book, UpdateDTO_Book>
    {
        /// <summary>
        /// Get average rating for each book
        /// </summary>
        /// <param name="parameters">BookParameters for paging</param>
        /// <returns>Returns collection of book titles with average rating</returns>
        Task<IEnumerable<GetDTO_AvgRatingBook>> GetAvgRatingForBookAsync(BookParameters parameters);
        
        /// <summary>
        /// Gets books without reviews
        /// </summary>
        /// <param name="parameters">BookParameters for paging</param>
        /// <returns>Returns collection of book without reviews</returns>
        Task<IEnumerable<GetDTO_BookWithoutReviews>> GetBooksWithoutReviewsAsync(BookParameters parameters);
    }
}
