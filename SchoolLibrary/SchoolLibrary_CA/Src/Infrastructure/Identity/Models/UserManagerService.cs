using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Features.UserIdentityFeatures.Commands.UpdateUserIdentity;
using Application.Features.UserIdentityFeatures.Queries.Common;
using Infrastructure.Identity.Models.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure.Identity.Models
{
    public class UserManagerService : IUserManager
    {
        private readonly ISchoolLibraryContext _context;
        private readonly UserManager<UserIdentity> _userManager;
        private readonly RoleManager<IdentityRole<Guid>> _roleManager;
        private readonly JWT _jwt;

        public UserManagerService(
            ISchoolLibraryContext context, 
            UserManager<UserIdentity> userManager, 
            RoleManager<IdentityRole<Guid>> roleManager, 
            IOptions<JWT> jwt)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
            _jwt = jwt.Value;
        }


        public async Task<UserIdentityDTO> GetUserByIdAsync(Guid UserId)
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
        public async Task<IEnumerable<UserIdentityDTO>> GetUsersAsync()
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
                Email = email,
                FirstName = String.Empty,
                LastName = String.Empty,
                Street = String.Empty,
                City = String.Empty,
                Country = String.Empty,
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
            
                return user.Id;
            }

            // If user creation failed
            var errors = result.Errors
                .GroupBy(
                    e => e.Code,
                    e => e.Description,
                    (Code, Description) => new
                    {
                        Key = Code,
                        Values = Description.Distinct().ToArray()
                    })
                .ToDictionary(e => e.Key, e => e.Values);
            throw new ValidationException(errors);
        }
        public async Task<Guid> RegisterAdministratorAsync(string userName, string email, string password)
        {
            return await RegisterUserAsync(userName, email, password, AuthorizationRoles.Administrator.ToString());
        }
        public async Task<Guid> RegisterModeratorAsync(string userName, string email, string password)
        {
            return await RegisterUserAsync(userName, email, password, AuthorizationRoles.Moderator.ToString());
        }
        public async Task UpdateUserAsync(UpdateUserIdentityCommand command)
        {
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

        public async Task<AuthenticationModel> GetTokenAsync(TokenRequestModel model)
        {
            var authenticationModel = new AuthenticationModel();
            var user = await _userManager.FindByEmailAsync(model.Email);

            if (user == null) // Return a message if not find
            {
                authenticationModel.IsAuthenticated = false;
                authenticationModel.Message = $"No Accounts Registered with {model.Email}";
                return authenticationModel;
            }
            if (await _userManager.CheckPasswordAsync(user, model.Password)) // Checking if the password is valid
            {
                authenticationModel.IsAuthenticated = true;
                
                JwtSecurityToken jwtSecurityToken = await CreateJwtToken(user); // create jwt token
                authenticationModel.Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
                authenticationModel.UserName = user.UserName ?? user.FirstName;
                authenticationModel.Email = user.Email!;

                var roleList = await _userManager.GetRolesAsync(user).ConfigureAwait(false);
                authenticationModel.Roles = roleList.ToList();
                
                return authenticationModel;
            }
            
            authenticationModel.IsAuthenticated = false;
            authenticationModel.Message = $"Incorrect Credentials for user {model.Email}";
            return authenticationModel;
        }
        private async Task<JwtSecurityToken> CreateJwtToken(UserIdentity user)
        {
            var userClaims = await _userManager.GetClaimsAsync(user);
            var roles = await _userManager.GetRolesAsync(user);

            // Creating a new JWT Security Token and returns them
            var roleClaims = new List<Claim>();
            roles.ToList().ForEach(role => roleClaims.Add(new Claim("roles", role)));

            var claims = new[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Email, user.Email),
                    new Claim("uid", user.Id.ToString())
                }
                .Union(userClaims)
                .Union(roleClaims);

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.Key));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            return new JwtSecurityToken(
                issuer: _jwt.Issuer,
                audience: _jwt.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_jwt.DurationInMinutes),
                signingCredentials: signingCredentials);
        }
    }
}