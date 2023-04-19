using System.Dynamic;
using SchoolLibrary_EF.DAL.Paging.Entities;
using System.Linq.Expressions;
using SchoolLibrary_EF.DAL.Paging;

namespace SchoolLibrary_EF.DAL.Repository.Contracts
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        Task<Guid> CreateAsync(TEntity entity);
        Task<IEnumerable<TEntity>> GetAllAsync(BaseParameters? parameters = null);
        Task<TEntity?> GetByIdAsync(Guid id);
        Task UpdateAsync(TEntity entity);
        Task DeleteAsync(Guid id);
        
        Task<PagedList<ExpandoObject>> GetAll_DataShaping_Async(BaseParameters? parameters = null);
        Task<ExpandoObject?> GetById_DataShaping_Async(Guid id, BaseParameters? parameters = null);

        Task<IQueryable<TEntity>> GetByConditionAsync(Expression<Func<TEntity, bool>> expression);
    }
}
