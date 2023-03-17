namespace SchoolLibrary_EF.BLL.Services.Contracts
{
    public interface IGenericService<TEntity> where TEntity : class
    {
        Task<Guid> CreateAsync(TEntity entity);
        Task<TEntity?> GetAsync(Guid id);
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task UpdateAsync(TEntity entity);
        Task DeleteAsync(Guid id);
    }
}
