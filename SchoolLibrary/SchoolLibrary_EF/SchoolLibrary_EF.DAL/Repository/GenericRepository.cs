using System.Dynamic;
using Microsoft.EntityFrameworkCore;
using SchoolLibrary_EF.DAL.Data;
using SchoolLibrary_EF.DAL.Paging.Entities;
using SchoolLibrary_EF.DAL.Repository.Contracts;
using System.Linq.Expressions;
using SchoolLibrary_EF.DAL.Entities;
using SchoolLibrary_EF.DAL.Helper.Contracts;
using SchoolLibrary_EF.DAL.Helpers.Contracts;
using SchoolLibrary_EF.DAL.Paging;

namespace SchoolLibrary_EF.DAL.Repository
{
    public abstract class GenericRepository<TEntity> : IGenericRepository<TEntity, Guid> where TEntity : BaseEntity
    {
        private readonly SchoolLibraryContext dbContext;
        protected readonly DbSet<TEntity> entities;
        protected readonly IDataShaper<TEntity> _dataShaper;
        protected readonly ISortHelper<TEntity> _sortHelper;

        protected GenericRepository(
            SchoolLibraryContext dbContext, 
            IDataShaper<TEntity> dataShaper, 
            ISortHelper<TEntity> sortHelper)
        {
            this.dbContext = dbContext;
            this.entities = dbContext.Set<TEntity>();

            _dataShaper = dataShaper;
            _sortHelper = sortHelper;
        }


        public virtual async Task<Guid> CreateAsync(TEntity entity)
        {
            await entities.AddAsync(entity);
            return entity.Id;
        }
        public virtual async Task<PagedList<TEntity>> GetAllAsync(BaseParameters parameters)
        {
            var collection = entities.AsNoTracking();
            var sortCollection = _sortHelper.ApplySort(collection, parameters.OrderBy); // sorting

            return await Task.Run(() => // paging
                PagedList<TEntity>.ToPagedList(sortCollection, parameters.PageNumber, parameters.PageSize));
        }
        public virtual async Task<TEntity?> GetByIdAsync(Guid id)
        {
            return await entities.FirstOrDefaultAsync(e => e.Id == id);
        }
        public virtual async Task UpdateAsync(TEntity entity)
        {
            var existingEntity = await GetByIdAsync(entity.Id);
            
            if (existingEntity == null) 
                throw new Exception($"{typeof(TEntity).Name} with id: [{entity.Id}] was not found"); 
            if (existingEntity.DateDeleted != null) 
                throw new Exception($"{typeof(TEntity).Name} with id: [{entity.Id}] already deleted");
            
            
            dbContext.Entry(existingEntity).State = EntityState.Detached;
            
            entity.DateUpdated = DateTime.UtcNow;
            entity.DateCreated = existingEntity.DateCreated;
            dbContext.Entry(entity).State = EntityState.Modified;
        }
        public virtual async Task DeleteAsync(Guid id)
        {
            var entity = await GetByIdAsync(id);
            
            if (entity == null) 
                throw new Exception($"{typeof(TEntity).Name} with id: [{id}] was not found");
            if (entity.DateDeleted != null) 
                throw new Exception($"{typeof(TEntity).Name} with id: [{id}] already deleted");

            dbContext.Entry(entity).State = EntityState.Detached;
            entity.DateDeleted = DateTime.UtcNow;
            await UpdateAsync(entity);
        }

        public virtual async Task<PagedList<ExpandoObject>> GetAll_DataShaping_Async(BaseParameters parameters)
        {
            var collection = entities.AsNoTracking();
            var sortCollection = _sortHelper.ApplySort(collection, parameters.OrderBy);
            var shapedCollection = _dataShaper.ShapeData(sortCollection, parameters.Fields ?? "");
            
            return await Task.Run(() =>
                PagedList<ExpandoObject>.ToPagedList(
                    shapedCollection.AsQueryable(),
                    parameters.PageNumber,
                    parameters.PageSize));
        }
        public virtual async Task<ExpandoObject?> GetById_DataShaping_Async(Guid id, BaseParameters parameters)
        {
            var entity = await GetByIdAsync(id);
            return entity == null ? 
                null : 
                _dataShaper.ShapeData(entity, parameters.Fields ?? "");
        }

        public virtual async Task<IQueryable<TEntity>> GetByConditionAsync(Expression<Func<TEntity, bool>> expression)
        {
            return await Task.Run(() => entities.Where(expression).AsNoTracking());
        }
    }
}
