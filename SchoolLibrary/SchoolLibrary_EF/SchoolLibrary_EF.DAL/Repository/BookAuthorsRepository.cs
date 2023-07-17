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
            return await entities
                .AsNoTracking()
                .Include(ba => ba.Book)
                .Include(ba => ba.Author)
                .FirstOrDefaultAsync(ba => ba.BookId == key.Item1 && ba.AuthorId == key.Item2);
        }
        public async Task UpdateAsync(BookAuthors entity)
        {
            await Task.Run(() => entities.Update(entity));
        }
        public async Task DeleteAsync((Guid, Guid) key)
        {
            var result = await GetByIdAsync(key);
            if (result != null) await Task.Run(() => entities.Remove(result));
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
