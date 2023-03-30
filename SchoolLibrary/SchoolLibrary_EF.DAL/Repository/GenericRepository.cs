using Microsoft.EntityFrameworkCore;
using SchoolLibrary_EF.DAL.Data;
using SchoolLibrary_EF.DAL.Paging.Entities;
using SchoolLibrary_EF.DAL.Repository.Contracts;
using System.Linq.Expressions;

namespace SchoolLibrary_EF.DAL.Repository
{
    public abstract class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        protected readonly SchoolLibraryContext dbContext;
        protected readonly DbSet<TEntity> entities;

        public GenericRepository(SchoolLibraryContext dbContext)
        {
            this.dbContext = dbContext;
            this.entities = dbContext.Set<TEntity>();
        }


        public abstract Task<Guid> CreateAsync(TEntity entity); // the method must return the id of the added entity
        public virtual async Task<IEnumerable<TEntity>> GetAllAsync(BaseParameters? parameters = null)
        {
            return await entities.AsNoTracking().ToListAsync();
        }
        public virtual async Task<TEntity?> GetByIdAsync(Guid id)
        {
            return await entities.FindAsync(id);
        }
        public virtual async Task UpdateAsync(TEntity entity)
        {
            await Task.Run(() => entities.Update(entity));
        }
        public virtual async Task DeleteAsync(Guid id)
        {
            var entity = await GetByIdAsync(id);
            if (entity == null) throw new Exception($"{typeof(TEntity).Name} with id: [{id}] was not found");

            await Task.Run(() => entities.Remove(entity));
        }


        public virtual async Task<IQueryable<TEntity>> GetByConditionAsync(Expression<Func<TEntity, bool>> expression)
        {
            return await Task.Run(() => entities.Where(expression).AsNoTracking());
        }
    }
}
