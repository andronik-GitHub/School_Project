using SchoolLibrary_EF.DAL.Entities;
using SchoolLibrary_EF.DAL.Paging;
using SchoolLibrary_EF.DAL.Paging.Entities;

namespace SchoolLibrary_EF.DAL.Repository.Contracts
{
    public interface IBookRepository : IGenericRepository<Book, Guid>
    {
        /// <summary>
        /// Get average rating for each book
        /// </summary>
        /// <param name="parameters">BookParameters for paging</param>
        /// <returns>Returns collection of book titles with average rating</returns>
        Task<PagedList<(string BookTitle, decimal? Average)>> GetAvgRatingForBookAsync(BookParameters parameters);
        
        /// <summary>
        /// Gets books without reviews
        /// </summary>
        /// <param name="parameters">BookParameters for paging</param>
        /// <returns>Returns collection of book without reviews</returns>
        Task<PagedList<Book>> GetBooksWithoutReviewsAsync(BookParameters parameters);
    }
}
