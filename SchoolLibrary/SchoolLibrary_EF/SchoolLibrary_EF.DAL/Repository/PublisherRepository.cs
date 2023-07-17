using SchoolLibrary_EF.DAL.Repository.Contracts;
using SchoolLibrary_EF.DAL.Entities;
using SchoolLibrary_EF.DAL.Data;
using SchoolLibrary_EF.DAL.Helper.Contracts;
using SchoolLibrary_EF.DAL.Helpers.Contracts;

namespace SchoolLibrary_EF.DAL.Repository
{
    public class PublisherRepository : GenericRepository<Publisher>, IPublisherRepository
    {
        public PublisherRepository(
            SchoolLibraryContext dbContext, 
            ISortHelper<Publisher> sortHelper,
            IDataShaper<Publisher> dataShaper)
            : base(dbContext, dataShaper, sortHelper)
        {
        }
    }
}
