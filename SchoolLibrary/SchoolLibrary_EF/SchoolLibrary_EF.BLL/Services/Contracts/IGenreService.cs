using SchoolLibrary_EF.BLL.DTOs.GenreDTOs;

namespace SchoolLibrary_EF.BLL.Services.Contracts
{
    public interface IGenreService : IGenericService<Guid, GetDTO_Genre, InsertDTO_Genre, UpdateDTO_Genre>
    {
    }
}
