using SchoolLibrary_EF.DAL.Entities;

namespace SchoolLibrary_EF.DAL.Repository.Contracts
{
    public interface IBookAuthorsRepository : IGenericRepository<BookAuthors, (Guid, Guid)>
    {
    }
}
