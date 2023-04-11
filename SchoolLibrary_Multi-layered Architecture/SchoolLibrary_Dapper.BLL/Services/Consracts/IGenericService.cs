using SchoolLibrary_Dapper.DAL.Entities;

namespace SchoolLibrary_Dapper.BLL.Services.Consracts
{
    public interface IGenericService<TEntity>
    {
        Task<Guid> CreateAsync(TEntity entity);
        Task<TEntity> GetAsync(Guid id);
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task UpdateAsync(TEntity entity);
        Task DeleteAsync(Guid id);

        void Dispose();
    }
}
