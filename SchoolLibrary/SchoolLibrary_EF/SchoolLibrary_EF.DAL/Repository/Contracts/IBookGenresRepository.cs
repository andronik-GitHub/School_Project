using SchoolLibrary_EF.DAL.Entities;

namespace SchoolLibrary_EF.DAL.Repository.Contracts
{
    public interface IBookGenresRepository : IGenericRepository<BookGenres, (Guid, Guid)>
    {
    }
}
