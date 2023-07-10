using SchoolLibrary_Dapper.BLL.DTOs.PublisherDTOs;

namespace SchoolLibrary_Dapper.BLL.Services.Consracts
{
    public interface IPublisherService : IGenericService
    {
        Task<Guid> CreateAsync(InsertDTO_Publisher entity);
        Task<GetDTO_Publisher?> GetAsync(Guid id);
        Task<IEnumerable<GetDTO_Publisher>> GetAllAsync();
        Task UpdateAsync(UpdateDTO_Publisher entity);
        Task DeleteAsync(Guid id);
    }
}
