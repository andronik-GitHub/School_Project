using SchoolLibrary_EF.BLL.DTOs.AuthorDTOs;

namespace SchoolLibrary_EF.BLL.Services.Contracts
{
    public interface IAuthorService : IGenericService<Guid, GetDTO_Author, InsertDTO_Author, UpdateDTO_Author>
    {
    }
}
