using SchoolLibrary_EF.API.Mapping.Configurations;
using SchoolLibrary_EF.DAL.Pagging;

namespace SchoolLibrary_EF.BLL.Services.Contracts
{
    // For Intermediate tables
    public interface IGenericIntermediateService<TEntity> where TEntity : class
    {
        Task<(Guid, Guid)?> CreateAsync(TEntity entity);
        Task<IEnumerable<TEntity>> GetAllAsync(BaseParameters parameters);
        Task<TEntity?> GetByIdAsync(Guid firstId, Guid secondId);
        Task UpdateAsync(TEntity entity);
        Task DeleteAsync(Guid firstId, Guid secondId);

        Task<(Guid, Guid)> GetIdsToOjbect(TEntity entity);
    }
}
