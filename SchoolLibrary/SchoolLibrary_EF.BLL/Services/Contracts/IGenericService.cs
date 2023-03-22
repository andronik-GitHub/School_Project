using SchoolLibrary_EF.DAL.Pagging;

namespace SchoolLibrary_EF.BLL.Services.Contracts
{
    public interface IGenericService<TEntity> where TEntity : class
    {
        Task<Guid> CreateAsync(TEntity entity);
        Task<TEntity?> GetAsync(Guid id);
        Task<IEnumerable<TEntity>> GetAllAsync(BaseParameters parameters);
        Task UpdateAsync(TEntity entity);
        Task DeleteAsync(Guid id);
    }
}
