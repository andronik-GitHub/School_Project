using SchoolLibrary_EF.BLL.DTOs.BookDetailsDTOs;

namespace SchoolLibrary_EF.BLL.Services.Contracts
{
    public interface IBookDetailsService : IGenericService
        <Guid, GetDTO_BookDetails, InsertDTO_BookDetails, UpdateDTO_BookDetails>
    {
    }
}
