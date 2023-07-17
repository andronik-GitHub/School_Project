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
            return await entities
                .AsNoTracking()
                .Include(bg => bg.Book)
                .Include(bg => bg.Genre)
                .FirstOrDefaultAsync(bg => bg.BookId == key.Item1 && bg.GenreId == key.Item2);
        }
        public async Task UpdateAsync(BookGenres entity)
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
