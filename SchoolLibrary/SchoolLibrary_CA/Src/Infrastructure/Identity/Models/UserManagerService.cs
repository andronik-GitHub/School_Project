using Application.Common.Interfaces;
using Infrastructure.Identity.Models.Authentication;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Identity.Models
{
    public class UserManagerService : IUserManager
    {
        private readonly UserManager<UserIdentity> _userManager;

        public UserManagerService(UserManager<UserIdentity> userManager)
        {
            _userManager = userManager;
        }


        public async Task<(Guid, bool)> RegisterAsync(string userName, string email, string password)
        {
            if (string.IsNullOrWhiteSpace(password)) throw new Exception("Password is empty!");
            if (string.IsNullOrWhiteSpace(email)) throw new Exception("Email is empty!");
            
            var userWithSameEmail = await _userManager.FindByEmailAsync(email); // ckeck user by email
            if (userWithSameEmail != null) return (Guid.Empty, false);
            
            
            var user = new UserIdentity
            {
                UserName = userName,
                Email = email
            };
            
            var result = await _userManager.CreateAsync(user, password); // register user
            if (result.Succeeded) // add default role 
                await _userManager.AddToRoleAsync(user, AuthorizationRoles.User.ToString());

            return (user.Id, true);
        }
        public async Task DeleteUserAsync(Guid UserId)
        {
            var user = _userManager.Users.SingleOrDefault(u => u.Id == UserId);

            if (user == null) throw new Exception($"{nameof(UserIdentity)} with id: [{UserId}] was not found");
            await _userManager.DeleteAsync(user);
        }
    }
}