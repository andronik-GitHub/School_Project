using SchoolLibrary_EF.BLL.DTOs.BookAuthorDTOs;

namespace SchoolLibrary_EF.BLL.Services.Contracts
{
    public interface IBookAuthorsService : IGenericService
        <(Guid, Guid), GetDTO_BookAuthors, InsertDTO_BookAuthors, UpdateDTO_BookAuthors>
    {
    }
}
