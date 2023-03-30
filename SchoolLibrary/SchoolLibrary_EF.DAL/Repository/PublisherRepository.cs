using SchoolLibrary_EF.DAL.Repository.Contracts;
using SchoolLibrary_EF.DAL.Entities;
using SchoolLibrary_EF.DAL.Data;
using SchoolLibrary_EF.DAL.Helper.Contracts;
using SchoolLibrary_EF.DAL.Paging.Entities;
using Microsoft.EntityFrameworkCore;

namespace SchoolLibrary_EF.DAL.Repository
{
    public class PublisherRepository : GenericRepository<Publisher>, IPublisherRepository
    {
        private readonly ISortHelper<Publisher> _sortHelper;
        public PublisherRepository(SchoolLibraryContext dbContext, ISortHelper<Publisher> sortHelper)
            : base(dbContext)
        {
            _sortHelper = sortHelper;
        }


        public override async Task<Guid> CreateAsync(Publisher publisher)
        {
            await entities.AddAsync(publisher);

            return publisher.PublisherId;
        }

        public override async Task<IEnumerable<Publisher>> GetAllAsync(BaseParameters? parameters = null)
        {
            if (parameters == null) return await base.GetAllAsync();


            var collection = entities.AsNoTracking();

            if (parameters is PublisherParameters param)
            {
                var newCollection = _sortHelper.ApplySort(collection, param.OrderBy); // sorting

                return await newCollection
                    //.OrderBy(entity => entity.PublisherId) after sorting, it makes no sense to sort by id
                    .Skip((parameters.PageNumber - 1) * parameters.PageSize)
                    .Take(parameters.PageSize)
                    .ToListAsync();
            }

            return await collection
                .OrderBy(entity => entity.PublisherId)
                .Skip((parameters.PageNumber - 1) * parameters.PageSize)
                .Take(parameters.PageSize)
                .ToListAsync();
        }
    }
}
