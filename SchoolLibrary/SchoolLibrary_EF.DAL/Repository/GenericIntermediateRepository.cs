using Microsoft.EntityFrameworkCore;
using SchoolLibrary_EF.DAL.Data;
using SchoolLibrary_EF.DAL.Pagging.Entities;
using SchoolLibrary_EF.DAL.Repository.Contracts;
using System.Linq.Expressions;

namespace SchoolLibrary_EF.DAL.Repository
{
    // For Intermediate tables
    public abstract class GenericIntermediateRepository<TEntity> : 
        IGenericIntermediateRepository<TEntity> where TEntity : class
    {
        protected readonly SchoolLibraryContext dbContext;
        protected readonly DbSet<TEntity> entities;

        public GenericIntermediateRepository(SchoolLibraryContext dbContext)
        {
            this.dbContext = dbContext;
            this.entities = dbContext.Set<TEntity>();
        }


        public virtual async Task<(Guid, Guid)> CreateAsync(Guid firstId, Guid secondId, TEntity entity)
        {
            await entities.AddAsync(entity);
            return (firstId, secondId);
        }
        public virtual async Task<IEnumerable<TEntity>> GetAllAsync(BaseParameters? parameters = null)
        {
            return await entities.AsNoTracking().ToListAsync();
        }
        public virtual async Task<TEntity?> GetByIdAsync(Guid firstId, Guid secondId)
        {
            return await entities.FindAsync(firstId, secondId);
        }
        public virtual async Task UpdateAsync(TEntity entity)
        {
            await Task.Run(() => entities.Update(entity));
        }
        public virtual async Task DeleteAsync(TEntity entity)
        {
            await Task.Run(() => entities.Remove(entity));
        }

        public virtual async Task<IQueryable<TEntity>> GetByConditionAsync(Expression<Func<TEntity, bool>> expression)
        {
            return await Task.Run(() => entities.Where(expression).AsNoTracking());
        }
    }
}
