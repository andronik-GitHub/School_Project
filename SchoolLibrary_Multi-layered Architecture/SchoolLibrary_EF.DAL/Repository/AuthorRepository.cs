using System.Dynamic;
using SchoolLibrary_EF.DAL.Entities;
using SchoolLibrary_EF.DAL.Repository.Contracts;
using SchoolLibrary_EF.DAL.Data;
using SchoolLibrary_EF.DAL.Paging.Entities;
using Microsoft.EntityFrameworkCore;
using SchoolLibrary_EF.DAL.Helper.Contracts;
using SchoolLibrary_EF.DAL.Helpers.Contracts;
using SchoolLibrary_EF.DAL.Paging;

namespace SchoolLibrary_EF.DAL.Repository
{
    public class AuthorRepository : GenericRepository<Author> , IAuthorRepository
    {
        private readonly ISortHelper<Author> _sortHelper;
        private readonly IDataShaper<Author> _dataShaper;

        public AuthorRepository(
            SchoolLibraryContext dbContext,
            ISortHelper<Author> sortHelper,
            IDataShaper<Author> dataShaper)
            : base(dbContext, dataShaper)
        {
            _sortHelper = sortHelper;
            _dataShaper = dataShaper;
        }


        public override async Task<Guid> CreateAsync(Author author)
        {
            await entities.AddAsync(author);

            return author.AuthorId;
        }
        public override async Task<IEnumerable<Author>> GetAllAsync(BaseParameters? parameters = null)
        {
            if (parameters == null) return await base.GetAllAsync(parameters);
            var collection = entities.AsNoTracking();

            if (parameters is not AuthorParameters param) // filtering
                return await collection
                    .OrderBy(a => a.AuthorId)
                    .Skip((parameters.PageNumber - 1) * parameters.PageSize)
                    .Take(parameters.PageSize)
                    .ToListAsync();
            
            
            var newCollection = _sortHelper.ApplySort(collection, param.OrderBy); // sorting

            return await newCollection
                //.OrderBy(a => a.AuthorId) after sorting, it makes no sense to sort by id
                .Skip((parameters.PageNumber - 1) * parameters.PageSize)
                .Take(parameters.PageSize)
                .ToListAsync();
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
            var entity = (await GetByConditionAsync(temp => temp.AuthorId.Equals(id)))
                .FirstOrDefault();

            return entity == null ? null :
                _dataShaper.ShapeData(entity, parameters?.Fields ?? "");
        }
    }
}
