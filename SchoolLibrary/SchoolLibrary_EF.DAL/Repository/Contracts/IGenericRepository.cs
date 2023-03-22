using SchoolLibrary_EF.DAL.Pagging.Entities;
using System.Linq.Expressions;

namespace SchoolLibrary_EF.DAL.Repository.Contracts
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        Task<Guid> CreateAsync(TEntity entity);
        Task<IEnumerable<TEntity>> GetAllAsync(BaseParameters? parameters = null);
        Task<TEntity?> GetByIdAsync(Guid id);
        Task UpdateAsync(TEntity entity);
        Task DeleteAsync(Guid id);

        Task<IQueryable<TEntity>> GetByConditionAsync(Expression<Func<TEntity, bool>> expression);
    }
}
