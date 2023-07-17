using System.Dynamic;
using SchoolLibrary_EF.DAL.Paging.Entities;
using System.Linq.Expressions;
using SchoolLibrary_EF.DAL.Paging;

namespace SchoolLibrary_EF.DAL.Repository.Contracts
{
    public interface IGenericRepository<TEntity, TKey> where TEntity : class
    {
        Task<TKey> CreateAsync(TEntity entity);
        Task<PagedList<TEntity>> GetAllAsync(BaseParameters parameters);
        Task<TEntity?> GetByIdAsync(TKey key);
        Task UpdateAsync(TEntity entity);
        Task DeleteAsync(TKey key);
        
        Task<PagedList<ExpandoObject>> GetAll_DataShaping_Async(BaseParameters parameters);
        Task<ExpandoObject?> GetById_DataShaping_Async(TKey key, BaseParameters parameters);

        Task<IQueryable<TEntity>> GetByConditionAsync(Expression<Func<TEntity, bool>> expression);
    }
}
