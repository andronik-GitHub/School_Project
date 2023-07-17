using SchoolLibrary_EF.BLL.DTOs.PublisherDTOs;

namespace SchoolLibrary_EF.BLL.Services.Contracts
{
    public interface IPublisherService : IGenericService
        <Guid, GetDTO_Publisher, InsertDTO_Publisher, UpdateDTO_Publisher>
    {
    }
}
