using AutoMapper;
using SchoolLibrary_EF.BLL.DTO;
using SchoolLibrary_EF.BLL.Services.Contracts;
using SchoolLibrary_EF.DAL.Entities;
using SchoolLibrary_EF.DAL.Paging;
using SchoolLibrary_EF.DAL.Paging.Entities;
using SchoolLibrary_EF.DAL.Repository.Contracts;
using System.Dynamic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SchoolLibrary_EF.API.Mapping.Configurations;
using SchoolLibrary_EF.BLL.DTO.Identity;
using SchoolLibrary_EF.DAL.Entities.Constants;

namespace SchoolLibrary_EF.BLL.Services
{
    public class UserService : IUserService
    {
        private IUnitOfWork _uow { get; set; }
        private readonly IMapper _mapper;
        private readonly JWT _jwt;
        
        public UserService(IUnitOfWork uow, IMapper mapper, IOptions<JWT> jwt)
        {
            _uow = uow;
            _mapper = mapper;
            _jwt = jwt.Value;
        }


        public async Task<Guid> CreateAsync(UserDTO entity)
        {
            // We create a User object and copy the values of the properties
            // of the entity object into its properties (we perform mapping)
            User user = _mapper.Map<User>(entity);

            var id = await _uow.Users.CreateAsync(user);
            await _uow.SaveChangesAsync();

            return id;
        }
        public async Task<IEnumerable<UserDTO>> GetAllAsync(BaseParameters parameters)
        {
            // Use AutoMapper to project one collection onto another
            return _mapper.Map<IEnumerable<User>, IEnumerable<UserDTO>>
                (await _uow.Users.GetAllAsync(parameters));
        }
        public async Task<UserDTO?> GetAsync(Guid id)
        {
            // Get entity from db
            User? user = await _uow.Users.GetByIdAsync(id);

            // We create a UserDTO object and copy the values of the properties
            // of the user object into its properties (we perform mapping)
            UserDTO? userDTO = _mapper.Map<UserDTO?>(user);

            return  userDTO;
        }
        public async Task UpdateAsync(UserDTO entity)
        {
            // We create a User object and copy the values ​​of the properties
            // of the entity object into its properties (we perform mapping)
            User user = _mapper.Map<User>(entity);

            await _uow.Users.UpdateAsync(user);
            await _uow.SaveChangesAsync();
        }
        public async Task DeleteAsync(Guid id)
        {
            await _uow.Users.DeleteAsync(id);
            await _uow.SaveChangesAsync();
        }

        public async Task<PagedList<ExpandoObject>> GetAll_DataShaping_Async(BaseParameters? parameters = null)
        {
            return await _uow.Users.GetAll_DataShaping_Async(parameters);
        }
        public async Task<ExpandoObject?> GetById_DataShaping_Async(Guid id, BaseParameters? parameters = null)
        {
            return await _uow.Users.GetById_DataShaping_Async(id, parameters);
        }

        public async Task<string> RegisterAsync(RegisterModel model)
        {
            if (string.IsNullOrWhiteSpace(model.Password)) throw new Exception("Password is empty!");
            if (string.IsNullOrWhiteSpace(model.Email)) throw new Exception("Email is empty!");
            
            // Mapping using Mapster
            var user = MappingFunctions.MapSourceToDestination<RegisterModel, User>(model);
            // To check if the email is already registered with our api
            var userWithSameEmail = await _uow._userManager.FindByEmailAsync(model.Email);

            if (userWithSameEmail != null) return "Email is already registered!";

            var result = await _uow._userManager.CreateAsync(user, model.Password);
            if (result.Succeeded) await _uow._userManager.AddToRoleAsync(user, Authorization.default_role.ToString());

            return $"User registered with username {user.UserName}";
        }

        public async Task<AuthenticationModel> GetTokenAsync(TokenRequestModel model)
        {
            // Creating a new Response Object,
            var authenticationModel = new AuthenticationModel();
            // Checking if the passeed email is valid
            var user = await _uow._userManager.FindByEmailAsync(model.Email!);

            // Return a message if not valid
            if (user == null)
            {
                authenticationModel.IsAuthenticated = false;
                authenticationModel.Message = "No Accounts Registered with this email";
                return authenticationModel;
            }

            // Checking if the password is valid
            if (await _uow._userManager.CheckPasswordAsync(user, model.Password ?? string.Empty))
            {
                authenticationModel.IsAuthenticated = true;
                
                // Call the CreateJWTToken function
                JwtSecurityToken token = await CreateJwtToken(user);
                authenticationModel.Token = new JwtSecurityTokenHandler().WriteToken(token);
                authenticationModel.Email = user.Email;
                authenticationModel.UserName = user.UserName;

                var roles = await _uow._userManager.GetRolesAsync(user).ConfigureAwait(false);
                authenticationModel.Roles = roles.ToList();

                // Return the response object
                return authenticationModel;
            }
            
            // else returns a message saying incorrect credentials
            authenticationModel.IsAuthenticated = false;
            authenticationModel.Message = "Incorrect Credentials for user";
            return authenticationModel;
        }
        private async Task<JwtSecurityToken> CreateJwtToken(User user)
        {
            // Gets all the claims of the user(user details)
            var userClaims = await _uow._userManager.GetClaimsAsync(user);
            // Gets all the roles of the user
            var roles = await _uow._userManager.GetRolesAsync(user);

            // Creating a new JWT Security Token and returns them
            var roleClaims = new List<Claim>();
            roles.ToList().ForEach(role => roleClaims.Add(new Claim("roles", role)));

            var claims = new[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub, user.UserName!),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Email, user.Email!),
                    new Claim("uid", user.Id.ToString()),
                }
                .Union(userClaims)
                .Union(roleClaims);

            var symetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.Key!));
            var signingCredentials = new SigningCredentials(symetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _jwt.Issuer,
                audience: _jwt.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_jwt.DurationInMinutes),
                signingCredentials: signingCredentials);

            return jwtSecurityToken;
        }
    }
}
