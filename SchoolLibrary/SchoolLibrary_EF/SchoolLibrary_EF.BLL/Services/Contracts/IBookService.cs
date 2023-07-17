using SchoolLibrary_EF.BLL.DTOs.BookDTOs;

namespace SchoolLibrary_EF.BLL.Services.Contracts
{
    public interface IBookService : IGenericService<Guid, GetDTO_Book, InsertDTO_Book, UpdateDTO_Book>
    {
    }
}
