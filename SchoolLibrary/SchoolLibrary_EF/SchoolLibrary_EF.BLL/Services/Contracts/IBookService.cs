using SchoolLibrary_EF.BLL.DTOs.BookDTOs;
using SchoolLibrary_EF.DAL.Paging.Entities;

namespace SchoolLibrary_EF.BLL.Services.Contracts
{
    public interface IBookService : IGenericService<Guid, GetDTO_Book, InsertDTO_Book, UpdateDTO_Book>
    {
        Task<IEnumerable<GetDTO_AvgRatingBook>> AvgRatingForBook(BookParameters parameters);
    }
}
