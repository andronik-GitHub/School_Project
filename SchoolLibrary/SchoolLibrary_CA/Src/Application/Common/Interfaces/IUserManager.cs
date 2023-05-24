using Application.Features.UserIdentityFeatures.Commands.UpdateUserIdentity;
using Application.Features.UserIdentityFeatures.Queries.Common;

namespace Application.Common.Interfaces
{
    public interface IUserManager
    {
        Task<UserIdentityDTO> GetUserByIdAsync(Guid UserId);
        Task<IEnumerable<UserIdentityDTO>> GetUsersAsync();
        Task<Guid> RegisterUserAsync(string userName, string email, string password, string? role);
        Task<Guid> RegisterAdministratorAsync(string userName, string email, string password);
        Task<Guid> RegisterModeratorAsync(string userName, string email, string password);
        Task UpdateUserAsync(UpdateUserIdentityCommand command);
        Task DeleteUserAsync(Guid UserId);

        Task<AuthenticationModel> GetTokenAsync(TokenRequestModel model);
        Task<AuthenticationModel> RefreshToken(string token);
        
        Task<Guid> AddRoleAsync(AddRoleModel model);
    }
}