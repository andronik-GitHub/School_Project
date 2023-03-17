namespace SchoolLibrary_EF.DAL.Repository.Contracts
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        Task<Guid> CreateAsync(TEntity entity);
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<TEntity?> GetByIdAsync(Guid id);
        Task UpdateAsync(TEntity entity);
        Task DeleteAsync(Guid id);
    }
}
