using SchoolLibrary_EF.DAL.Repositories.Contracts;
using SchoolLibrary_EF.DAL.Entities;
using SchoolLibrary_EF.DAL.Repository;
using SchoolLibrary_EF.DAL.Data;

namespace SchoolLibrary_EF.DAL.Repositories
{
    public class PublisherRepository : GenericRepository<Publisher>, IPublisherRepository
    {
        public PublisherRepository(SchoolLibraryContext dbContext)
            : base(dbContext)
        {
        }


        public override async Task<Guid> CreateAsync(Publisher publisher)
        {
            await entities.AddAsync(publisher);

            return publisher.PublisherId;
        }
    }
}
