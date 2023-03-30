using SchoolLibrary_EF.BLL.DTO;
using SchoolLibrary_EF.DAL.Paging.Entities;
using SchoolLibrary_EF.DAL.Paging;
using System.Dynamic;

namespace SchoolLibrary_EF.BLL.Services.Contracts
{
    public interface IUserService : IGenericService<UserDTO>
    {
        Task<PagedList<ExpandoObject>> GetAll_DataShaping_Async(BaseParameters? parameters = null);
        Task<ExpandoObject> GetById_DataShaping_Async(Guid id, BaseParameters? parameters = null);
    }
}
