using System.Dynamic;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using SchoolLibrary_EF.DAL.Data;
using SchoolLibrary_EF.DAL.Entities;
using SchoolLibrary_EF.DAL.Helper.Contracts;
using SchoolLibrary_EF.DAL.Helpers.Contracts;
using SchoolLibrary_EF.DAL.Paging;
using SchoolLibrary_EF.DAL.Paging.Entities;
using SchoolLibrary_EF.DAL.Repository.Contracts;

namespace SchoolLibrary_EF.DAL.Repository
{
    public class BookGenresRepository : IBookGenresRepository
    {
        private readonly SchoolLibraryContext dbContext;
        private readonly DbSet<BookGenres> entities;
        private readonly ISortHelper<BookGenres> _sortHelper;
        private readonly IDataShaper<BookGenres> _dataShaper;

        public BookGenresRepository(
            SchoolLibraryContext dbContext, 
            ISortHelper<BookGenres> sortHelper,
            IDataShaper<BookGenres> dataShaper)
        {
            this.dbContext = dbContext;
            this.entities = dbContext.Set<BookGenres>();
            
            _sortHelper = sortHelper;
            _dataShaper = dataShaper;
        }


        public async Task<(Guid, Guid)> CreateAsync(BookGenres entity)
        {
            await entities.AddAsync(entity);
            return (entity.BookId, entity.GenreId);
        }
        public async Task<PagedList<BookGenres>> GetAllAsync(BaseParameters parameters)
        {
            var collection = entities
                .AsNoTracking()
                .Include(bg => bg.Book)
                .Include(bg => bg.Genre);
            var sortCollection = _sortHelper.ApplySort(collection, parameters.OrderBy); // sorting

            return await Task.Run(() => // paging
                PagedList<BookGenres>.ToPagedList(sortCollection, parameters.PageNumber, parameters.PageSize));
        }
        public async Task<BookGenres?> GetByIdAsync((Guid, Guid) key)
        {
            var entity = await entities
                .AsNoTracking()
                .Include(bg => bg.Book)
                .Include(bg => bg.Genre)
                .FirstOrDefaultAsync(bg => bg.BookId == key.Item1 && bg.GenreId == key.Item2);

            return entity;
        }
        public async Task UpdateAsync(BookGenres entity)
        {
            var existingEntity = await GetByIdAsync((entity.BookId, entity.GenreId));
            
            if (existingEntity == null) 
                throw new Exception
                    ($"{this.GetType().Name} with id: [{entity.BookId} - {entity.GenreId}] was not found"); 
            if (existingEntity.DateDeleted != null) 
                throw new Exception
                    ($"{this.GetType().Name} with id: [{entity.BookId} - {entity.GenreId}] already deleted");
            
            
            dbContext.Entry(existingEntity).State = EntityState.Detached;
            
            entity.DateUpdated = DateTime.UtcNow;
            entity.DateCreated = existingEntity.DateCreated;
            dbContext.Entry(entity).State = EntityState.Modified;
        }
        public async Task DeleteAsync((Guid, Guid) key)
        {
            var entity = await GetByIdAsync(key);
            
            if (entity == null) 
                throw new Exception($"{this.GetType().Name} with id: [{key.Item1} - {key.Item2}] was not found");
            if (entity.DateDeleted != null) 
                throw new Exception($"{this.GetType().Name} with id: [{key.Item1} - {key.Item2}] already deleted");

            dbContext.Entry(entity).State = EntityState.Detached;
            entity.DateDeleted = DateTime.UtcNow;
            await UpdateAsync(entity);
        }

        public async Task<PagedList<ExpandoObject>> GetAll_DataShaping_Async(BaseParameters parameters)
        {
            var collection = entities
                .AsNoTracking()
                .Include(bg => bg.Book)
                .Include(bg => bg.Genre);
            var sortCollection = _sortHelper.ApplySort(collection, parameters.OrderBy);
            var shapedCollection = _dataShaper.ShapeData(sortCollection, parameters.Fields ?? "");
            
            return await Task.Run(() =>
                PagedList<ExpandoObject>.ToPagedList(
                    shapedCollection.AsQueryable(),
                    parameters.PageNumber,
                    parameters.PageSize));
        }
        public async Task<ExpandoObject?> GetById_DataShaping_Async((Guid, Guid) key, BaseParameters parameters)
        {
            var entity = await GetByIdAsync(key);
            return entity == null ? 
                null : 
                _dataShaper.ShapeData(entity, parameters?.Fields ?? "");
        }
        
        public async Task<IQueryable<BookGenres>> GetByConditionAsync(Expression<Func<BookGenres, bool>> expression)
        {
            return await Task.Run(() => entities.Where(expression).AsNoTracking());
        }
    }
}
