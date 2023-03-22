using SchoolLibrary_EF.DAL.Pagging;

namespace SchoolLibrary_EF.DAL.Repository.Contracts
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        Task<Guid> CreateAsync(TEntity entity);
        Task<IEnumerable<TEntity>> GetAllAsync<TOrderBy>
            (BaseParameters? parameters = null, Func<TEntity, TOrderBy>? orderBy = null);
        Task<TEntity?> GetByIdAsync(Guid id);
        Task UpdateAsync(TEntity entity);
        Task DeleteAsync(Guid id);
    }
}
