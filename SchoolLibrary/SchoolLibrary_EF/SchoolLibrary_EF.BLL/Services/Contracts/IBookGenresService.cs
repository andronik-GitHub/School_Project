using SchoolLibrary_EF.BLL.DTOs.BookGenreDTOs;

namespace SchoolLibrary_EF.BLL.Services.Contracts
{
    public interface IBookGenresService : IGenericService
        <(Guid, Guid), GetDTO_BookGenres, InsertDTO_BookGenres, UpdateDTO_BookGenres>
    {
    }
}
