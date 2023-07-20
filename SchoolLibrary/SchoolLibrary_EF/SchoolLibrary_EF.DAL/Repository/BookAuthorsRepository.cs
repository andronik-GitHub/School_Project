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
    public class BookAuthorsRepository : IBookAuthorsRepository
    {
        private readonly SchoolLibraryContext dbContext;
        private readonly DbSet<BookAuthors> entities;
        private readonly ISortHelper<BookAuthors> _sortHelper;
        private readonly IDataShaper<BookAuthors> _dataShaper;

        public BookAuthorsRepository(
            SchoolLibraryContext dbContext, 
            ISortHelper<BookAuthors> sortHelper,
            IDataShaper<BookAuthors> dataShaper)
        {
            this.dbContext = dbContext;
            this.entities = dbContext.Set<BookAuthors>();
            
            _sortHelper = sortHelper;
            _dataShaper = dataShaper;
        }


        public async Task<(Guid, Guid)> CreateAsync(BookAuthors entity)
        {
            await entities.AddAsync(entity);
            return (entity.BookId, entity.AuthorId);
        }
        public async Task<PagedList<BookAuthors>> GetAllAsync(BaseParameters parameters)
        {
            var collection = entities
                .AsNoTracking()
                .Include(ba => ba.Book)
                .Include(ba => ba.Author);
            var sortCollection = _sortHelper.ApplySort(collection, parameters.OrderBy); // sorting

            return await Task.Run(() => // paging
                PagedList<BookAuthors>.ToPagedList(sortCollection, parameters.PageNumber, parameters.PageSize));
        }
        public async Task<BookAuthors?> GetByIdAsync((Guid, Guid) key)
        {
            var enitty = await entities
                .AsNoTracking()
                .Include(ba => ba.Book)
                .Include(ba => ba.Author)
                .FirstOrDefaultAsync(ba => ba.BookId == key.Item1 && ba.AuthorId == key.Item2);

            return enitty;
        }
        public async Task UpdateAsync(BookAuthors entity)
        {
            var existingEntity = await GetByIdAsync((entity.BookId, entity.AuthorId));
            
            if (existingEntity == null) 
                throw new Exception
                    ($"{this.GetType().Name} with id: [{entity.BookId} - {entity.AuthorId}] was not found"); 
            if (existingEntity.DateDeleted != null) 
                throw new Exception
                    ($"{this.GetType().Name} with id: [{entity.BookId} - {entity.AuthorId}] already deleted");
            
            
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
                .Include(ba => ba.Book)
                .Include(ba => ba.Author);
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
        
        public async Task<IQueryable<BookAuthors>> GetByConditionAsync(Expression<Func<BookAuthors, bool>> expression)
        {
            return await Task.Run(() => entities.Where(expression).AsNoTracking());
        }
    }
}
