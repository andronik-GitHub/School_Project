using SchoolLibrary_EF.DAL.Entities;
using SchoolLibrary_EF.DAL.Paging;
using SchoolLibrary_EF.DAL.Paging.Entities;

namespace SchoolLibrary_EF.DAL.Repository.Contracts
{
    public interface IBookRepository : IGenericRepository<Book, Guid>
    {
        Task<PagedList<(string BookTitle, decimal? Average)>> AvgRatingForBook(BookParameters parameters);
    }
}
