using System.Dynamic;
using SchoolLibrary_EF.DAL.Entities;
using SchoolLibrary_EF.DAL.Paging;
using SchoolLibrary_EF.DAL.Paging.Entities;

namespace SchoolLibrary_EF.DAL.Repository.Contracts
{
    public interface IUserRepository : IGenericRepository<User>
    {
        Task<PagedList<ExpandoObject>> GetAll_DataShaping_Async(BaseParameters? parameters = null);
        Task<ExpandoObject> GetById_DataShaping_Async(Guid id, BaseParameters? parameters = null);
    }
}
