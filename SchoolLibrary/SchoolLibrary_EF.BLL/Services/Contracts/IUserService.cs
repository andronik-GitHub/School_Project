using SchoolLibrary_EF.BLL.DTO;
using SchoolLibrary_EF.DAL.Paging.Entities;
using SchoolLibrary_EF.DAL.Paging;
using System.Dynamic;
using SchoolLibrary_EF.BLL.DTO.Identity;

namespace SchoolLibrary_EF.BLL.Services.Contracts
{
    public interface IUserService : IGenericService<UserDTO>
    {
        Task<string> RegisterAsync(RegisterModel model);
        Task<string> RegisterAdministratorAsync(RegisterModel model);
        Task<string> AddRoleAsync(AddRoleModel model);
        Task<AuthenticationModel> GetTokenAsync(TokenRequestModel model);
    }
}
