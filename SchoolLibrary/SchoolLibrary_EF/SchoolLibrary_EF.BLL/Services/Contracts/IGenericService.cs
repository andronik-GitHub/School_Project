using System.Dynamic;
using SchoolLibrary_EF.DAL.Paging;
using SchoolLibrary_EF.DAL.Paging.Entities;

namespace SchoolLibrary_EF.BLL.Services.Contracts
{
    public interface IGenericService<TKey, TGetDTO, TInsertDTO, TUpdateDTO>
    {
        Task<TKey> CreateAsync(TInsertDTO entity);
        Task<TGetDTO?> GetAsync(TKey id);
        Task<IEnumerable<TGetDTO>> GetAllAsync(BaseParameters parameters);
        Task UpdateAsync(TUpdateDTO entity);
        Task DeleteAsync(TKey key);
        
        Task<PagedList<ExpandoObject>> GetAll_DataShaping_Async(BaseParameters parameters);
        Task<ExpandoObject?> GetById_DataShaping_Async(TKey key, BaseParameters parameters);
    }
}
