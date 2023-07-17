using SchoolLibrary_EF.DAL.Data;
using SchoolLibrary_EF.DAL.Entities;
using SchoolLibrary_EF.DAL.Repository.Contracts;
using SchoolLibrary_EF.DAL.Helper.Contracts;
using SchoolLibrary_EF.DAL.Helpers.Contracts;

namespace SchoolLibrary_EF.DAL.Repository
{
    public class GenreRepository : GenericRepository<Genre>, IGenreRepository
    {
        public GenreRepository(
            SchoolLibraryContext dbContext,
            IDataShaper<Genre> dataShaper,
            ISortHelper<Genre> sortHelper)
            : base(dbContext, dataShaper, sortHelper)
        {
        }
    }
}
