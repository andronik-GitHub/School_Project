using SchoolLibrary_Dapper.BLL.DTOs.BookDetailsDTOs;

namespace SchoolLibrary_Dapper.BLL.Services.Consracts
{
    public interface IBookDetailsService : IGenericService
    {
        Task<Guid> CreateAsync(InsertDTO_BookDetails entity);
        Task<GetDTO_BookDetails?> GetAsync(Guid id);
        Task<IEnumerable<GetDTO_BookDetails>> GetAllAsync();
        Task UpdateAsync(UpdateDTO_BookDetails entity);
        Task DeleteAsync(Guid id);
    }
}
