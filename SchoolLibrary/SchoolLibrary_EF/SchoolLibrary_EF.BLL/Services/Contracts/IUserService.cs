using SchoolLibrary_EF.BLL.DTOs.UserDTOs;
using SchoolLibrary_EF.DAL.Entities;
using SchoolLibrary_EF.DAL.Entities.Identity;
using SchoolLibrary_EF.DAL.Paging.Entities;

namespace SchoolLibrary_EF.BLL.Services.Contracts
{
    public interface IUserService : IGenericService<Guid, GetDTO_User, InsertDTO_User, UpdateDTO_User>
    {
        Task<User?> GetUserWithRefreshTokensAsync(Guid id);
        
        Task<bool> RegisterAsync(RegisterModel model);
        Task<bool> RegisterModeratorAsync(RegisterModel model);
        Task<bool> RegisterAdministratorAsync(RegisterModel model);
        Task<bool> AddRoleAsync(AddRoleModel model);
        
        Task<AuthenticationModel> GetTokenAsync(TokenRequestModel model);
        Task<AuthenticationModel> GetRefreshTokenAsync(string token);
        Task<bool> RevokeTokenAsync(string token);
        
        
        /// <summary>
        /// Gets number of books issued per user
        /// </summary>
        /// <param name="parameters">UserParameters for paging</param>
        /// <returns>Returns collection of users with count of books loaned</returns>
        Task<IEnumerable<GetDTO_NumBooksIssuedToUser>> GetNumBooksIssuedToUserAsync(UserParameters parameters);
    }
}
