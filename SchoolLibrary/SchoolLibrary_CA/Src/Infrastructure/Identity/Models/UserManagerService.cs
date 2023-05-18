using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Features.UserIdentityFeatures.Commands.UpdateUserIdentity;
using Application.Features.UserIdentityFeatures.Queries.Common;
using Infrastructure.Identity.Models.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Identity.Models
{
    public class UserManagerService : IUserManager
    {
        private readonly ISchoolLibraryContext _context;
        private readonly UserManager<UserIdentity> _userManager;

        public UserManagerService(ISchoolLibraryContext context, UserManager<UserIdentity> userManager)
        {
            _context = context;
            _userManager = userManager;
        }


        public async Task<object> GetUserByIdAsync(Guid UserId)
        {
            var user = await _userManager.FindByIdAsync(UserId.ToString());
            if (user == null) throw new NotFoundException(nameof(UserIdentity), UserId);

            return new UserIdentityDTO
            {
                DateCreated = user.DateCreated,
                Id = user.Id,
                UserName = user.UserName,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Street = user.Street,
                City = user.City,
                Country = user.Country,
            };
        }
        public async Task<IEnumerable<object>> GetUsersAsync()
        {
            return await _userManager.Users
                .AsNoTracking()
                .Select(u => new UserIdentityDTO
                {
                    DateCreated = u.DateCreated,
                    Id = u.Id,
                    UserName = u.UserName,
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    Email = u.Email,
                    Street = u.Street,
                    City = u.City,
                    Country = u.Country,
                })
                .ToListAsync();
        }
        public async Task<Guid> RegisterUserAsync(string userName, string email, string password, string? role = null)
        {
            if (string.IsNullOrWhiteSpace(password)) throw new BadRequestException("Password is empty!");
            if (string.IsNullOrWhiteSpace(email)) throw new BadRequestException("Email is empty!");
            
            var userWithSameEmail = await _userManager.FindByEmailAsync(email); // ckeck user by email
            if (userWithSameEmail != null) 
                throw new BadRequestException("The user with this email is already registered!");
            
            
            var user = new UserIdentity
            {
                UserName = userName,
                Email = email
            };
            
            var result = await _userManager.CreateAsync(user, password); // register user
            if (result.Succeeded) // add role 
            {
                if (role?.ToLower() == AuthorizationRoles.Administrator.ToString().ToLower())
                    await _userManager.AddToRoleAsync(user, AuthorizationRoles.Administrator.ToString());
                else if (role?.ToLower() == AuthorizationRoles.Moderator.ToString().ToLower())
                    await _userManager.AddToRoleAsync(user, AuthorizationRoles.Moderator.ToString());
                else 
                    await _userManager.AddToRoleAsync(user, AuthorizationRoles.User.ToString());
            }

            return user.Id;
        }
        public async Task<Guid> RegisterAdministratorAsync(string userName, string email, string password)
        {
            return await RegisterUserAsync(userName, email, password, AuthorizationRoles.Administrator.ToString());
        }
        public async Task<Guid> RegisterModeratorAsync(string userName, string email, string password)
        {
            return await RegisterUserAsync(userName, email, password, AuthorizationRoles.Moderator.ToString());
        }
        public async Task UpdateUserAsync(object entity)
        {
            if (entity is not UpdateUserIdentityCommand command) 
                throw new BadRequestException("Invalid user update data!");

            var user = await _userManager.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Id == command.Id);
            if (user == null) throw new NotFoundException(nameof(UserIdentity), command.Id);
            
            user.Id = command.Id;
            user.UserName = command.UserName;
            user.Email = command.Email;
            user.PasswordHash = new PasswordHasher<UserIdentity>().HashPassword(null!, command.Password);
            user.FirstName = command.FirstName;
            user.LastName = command.LastName;
            user.Street = command.Street;
            user.City = command.City;
            user.Country = command.Country;

            await _context.SaveChangesAsync();
        }
        public async Task DeleteUserAsync(Guid UserId)
        {
            var user = _userManager.Users.AsNoTracking().FirstOrDefault(u => u.Id == UserId);

            if (user == null) throw new NotFoundException(nameof(UserIdentity), UserId);
            await _userManager.DeleteAsync(user);
        }
    }
}