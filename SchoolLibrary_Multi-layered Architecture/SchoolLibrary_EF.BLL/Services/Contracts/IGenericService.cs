using System.Dynamic;
using SchoolLibrary_EF.DAL.Paging;
using SchoolLibrary_EF.DAL.Paging.Entities;

namespace SchoolLibrary_EF.BLL.Services.Contracts
{
    public interface IGenericService<TEntity> where TEntity : class
    {
        Task<Guid> CreateAsync(TEntity entity);
        Task<TEntity?> GetAsync(Guid id);
        Task<IEnumerable<TEntity>> GetAllAsync(BaseParameters parameters);
        Task UpdateAsync(TEntity entity);
        Task DeleteAsync(Guid id);
        
        Task<PagedList<ExpandoObject>> GetAll_DataShaping_Async(BaseParameters? parameters = null);
        Task<ExpandoObject?> GetById_DataShaping_Async(Guid id, BaseParameters? parameters = null);
    }
}
