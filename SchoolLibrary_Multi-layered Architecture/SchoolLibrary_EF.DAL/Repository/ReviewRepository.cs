using System.Dynamic;
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
    public class ReviewRepository : GenericRepository<Review>, IReviewRepository
    {
        private readonly ISortHelper<Review> _sortHelper;
        private readonly IDataShaper<Review> _dataShaper;

        public ReviewRepository(
            SchoolLibraryContext dbContext,
            ISortHelper<Review> sortHelper,
            IDataShaper<Review> dataShaper)
            : base(dbContext, dataShaper)
        {
            _sortHelper = sortHelper;
            _dataShaper = dataShaper;
        }


        public override async Task<Guid> CreateAsync(Review review)
        {
            await entities.AddAsync(review);

            return review.ReviewId;
        }
        public override async Task<IEnumerable<Review>> GetAllAsync(BaseParameters? parameters = null)
        {
            if (parameters == null) return await base.GetAllAsync(parameters);
            var collection = entities.AsNoTracking();

            if (parameters is not ReviewParameters param)
                return await collection
                    .OrderBy(entity => entity.ReviewId)
                    .Skip((parameters.PageNumber - 1) * parameters.PageSize)
                    .Take(parameters.PageSize)
                    .Include(entity => entity.Book)
                    .Include(entity => entity.User)
                    .ToListAsync();
            
            
            var newCollection = _sortHelper.ApplySort(collection, param.OrderBy); // sorting

            return await newCollection
                //.OrderBy(entity => entity.ReviewId) after sorting, it makes no sense to sort by id
                .Skip((parameters.PageNumber - 1) * parameters.PageSize)
                .Take(parameters.PageSize)
                .Include(entity => entity.Book)
                .Include(entity => entity.User)
                .ToListAsync();

        }
        public override async Task<Review?> GetByIdAsync(Guid id)
        {
            return await entities
                .Include(r => r.Book)
                .Include(r => r.User)
                .FirstOrDefaultAsync(r => r.ReviewId == id);
        }
        
        public override async Task<PagedList<ExpandoObject>> GetAll_DataShaping_Async(BaseParameters? parameters)
        {
            if (parameters == null) return await base.GetAll_DataShaping_Async(parameters);
            var collection = entities.AsNoTracking(); // filtering

            if (parameters is not ReviewParameters param)
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
            var entity = (await GetByConditionAsync(temp => temp.ReviewId.Equals(id)))
                .FirstOrDefault();

            return entity == null ? null :
                _dataShaper.ShapeData(entity, parameters?.Fields ?? "");
        }
    }
}
