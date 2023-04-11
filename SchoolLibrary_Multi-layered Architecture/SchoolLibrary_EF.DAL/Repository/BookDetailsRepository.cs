using System.Dynamic;
using SchoolLibrary_EF.DAL.Entities;
using SchoolLibrary_EF.DAL.Repository.Contracts;
using SchoolLibrary_EF.DAL.Data;
using Microsoft.EntityFrameworkCore;
using SchoolLibrary_EF.DAL.Paging.Entities;
using SchoolLibrary_EF.DAL.Helper.Contracts;
using SchoolLibrary_EF.DAL.Helpers.Contracts;
using SchoolLibrary_EF.DAL.Paging;

namespace SchoolLibrary_EF.DAL.Repository
{
    public class BookDetailsRepository : GenericRepository<BookDetails>, IBookDetailsRepository
    {
        private readonly ISortHelper<BookDetails> _sortHelper;
        private readonly IDataShaper<BookDetails> _dataShaper;

        public BookDetailsRepository(
            SchoolLibraryContext dbContext, 
            ISortHelper<BookDetails> sortHelper,
            IDataShaper<BookDetails> dataShaper)
            : base(dbContext, dataShaper)
        {
            _sortHelper = sortHelper;
            _dataShaper = dataShaper;
        }


        public override async Task<Guid> CreateAsync(BookDetails bookDetails)
        {
            await entities.AddAsync(bookDetails);

            return bookDetails.BookDetailId;
        }
        public override async Task<IEnumerable<BookDetails>> GetAllAsync(BaseParameters? parameters = null)
        {
            if (parameters == null) return await base.GetAllAsync(parameters);
            var collection = entities.AsNoTracking();

            if (parameters is not BookDetailsParameters param)
                return await entities
                    .AsNoTracking()
                    .OrderBy(entity => entity.BookDetailId)
                    .Skip((parameters.PageNumber - 1) * parameters.PageSize)
                    .Take(parameters.PageSize)
                    .Include(entity => entity.Book)
                    .ToListAsync();
            
            
            var newCollection = _sortHelper.ApplySort(collection, param.OrderBy); // sorting

            return await newCollection
                //.OrderBy(entity => entity.BookDetailId)  after sorting, it makes no sense to sort by id
                .Skip((parameters.PageNumber - 1) * parameters.PageSize)
                .Take(parameters.PageSize)
                .Include(entity => entity.Book)
                .ToListAsync();
        }
        public override async Task<BookDetails?> GetByIdAsync(Guid id)
        {
            return await entities.Include(bd => bd.Book).FirstOrDefaultAsync(bd => bd.BookDetailId == id);
        }
        
        
        public override async Task<PagedList<ExpandoObject>> GetAll_DataShaping_Async(BaseParameters? parameters)
        {
            if (parameters == null) return await base.GetAll_DataShaping_Async(parameters);
            var collection = entities.AsNoTracking(); // filtering

            if (parameters is not UserParameters param)
                return await Task.Run(() =>
                    PagedList<ExpandoObject>.ToPagedList(
                        _dataShaper.ShapeData(collection, parameters.Fields ?? "").AsQueryable(),
                        parameters.PageNumber,
                        parameters.PageSize));
            
            
            collection = _sortHelper.ApplySort(collection, param.OrderBy); // sorting

            return await Task.Run(() =>
                PagedList<ExpandoObject>.ToPagedList(
                    _dataShaper.ShapeData(collection, parameters.Fields ?? "").AsQueryable(),
                    parameters.PageNumber,
                    parameters.PageSize));
        }
        public override async Task<ExpandoObject?> GetById_DataShaping_Async(Guid id, BaseParameters? parameters = null)
        {
            var entity = (await GetByConditionAsync(temp => temp.BookDetailId.Equals(id)))
                .FirstOrDefault();

            return entity == null ? null :
                _dataShaper.ShapeData(entity, parameters?.Fields ?? "");
        }
    }
}
