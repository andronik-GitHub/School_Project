using SchoolLibrary_EF.DAL.Pagging;

namespace SchoolLibrary_EF.DAL.Repository.Contracts
{
    // For Intermediate tables
    public interface IGenericIntermediateRepository<TEntity> where TEntity : class
    {
        Task<(Guid, Guid)> CreateAsync(Guid firstId, Guid secondId, TEntity entity);
        Task<IEnumerable<TEntity>> GetAllAsync(BaseParameters parameters);
        Task<TEntity?> GetByIdAsync(Guid firstId, Guid secondId);
        Task UpdateAsync(TEntity entity);
        Task DeleteAsync(TEntity entity);
    }
}
