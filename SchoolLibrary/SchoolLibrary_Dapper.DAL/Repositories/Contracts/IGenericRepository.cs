namespace SchoolLibrary_Dapper.DAL.Repositories.Contracts
{
    public interface IGenericRepository<TEntity>
    {
        Task<Guid> CreateAsync(TEntity entity);
        Task<TEntity> GetByIdAsync(Guid id);
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task UpdateAsync(TEntity entity);
        Task DeleteAsync(Guid id);
    }
}
