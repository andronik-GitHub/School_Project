using AutoMapper;
using SchoolLibrary_EF.BLL.Services.Contracts;
using SchoolLibrary_EF.DAL.Entities;
using SchoolLibrary_EF.DAL.Paging;
using SchoolLibrary_EF.DAL.Paging.Entities;
using SchoolLibrary_EF.DAL.Repository.Contracts;
using System.Dynamic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SchoolLibrary_EF.BLL.DTOs.UserDTOs;
using SchoolLibrary_EF.BLL.Mapping;
using SchoolLibrary_EF.DAL.Entities.Identity;

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


        public async Task<Guid> CreateAsync(InsertDTO_User entity)
        {
            User user = _mapper.Map<User>(entity); // Mapping with AutoMapper

            var id = await _uow.Users.CreateAsync(user);
            await _uow.SaveChangesAsync();

            return id;
        }
        public async Task<IEnumerable<GetDTO_User>> GetAllAsync(BaseParameters parameters)
        {
            // Use AutoMapper to project one collection onto another
            return _mapper.Map<IEnumerable<User>, IEnumerable<GetDTO_User>>
                (await _uow.Users.GetAllAsync(parameters));
        }
        public async Task<GetDTO_User?> GetAsync(Guid id)
        {
            User? user = await _uow.Users.GetByIdAsync(id);

            GetDTO_User? userDTO = _mapper.Map<GetDTO_User?>(user); // Mapping with AutoMapper

            return  userDTO;
        }
        public async Task<User?> GetUserWithRefreshTokensAsync(Guid id)
        {
            return await _uow.Users.GetByIdAsync(id);
        }
        public async Task UpdateAsync(UpdateDTO_User entity)
        {
            var checkUser = await _uow.Users.GetByIdAsync(entity.Id);
            if (checkUser == null) throw new Exception($"{nameof(User)} with id: [{entity.Id}] was not found");

            _uow._dbContext.Entry(checkUser).State = EntityState.Detached;
            User user = (User)checkUser.Clone();
            
            foreach (var keyValuePair in entity.Values)
                if (keyValuePair.Key != "DateCreated" && 
                    keyValuePair.Key != "DateUpdated" && 
                    keyValuePair.Key != "DateDeleted")
                {
                    var property = user.GetType().GetProperty(keyValuePair.Key);

                    if (property != null && property.CanWrite)
                    {
                        if (property.Name == "PasswordHash")
                        {
                            property.SetValue
                                (user, new PasswordHasher<User>().HashPassword(null!, keyValuePair.Value));
                        }
                        else
                        {
                            property.SetValue(user, keyValuePair.Value);
                        }

                        if (property.Name is "PasswordHash" or "UserName")
                        {
                            user.GetType().GetProperty("SecurityStamp")?.SetValue
                                (user, Guid.NewGuid().ToString("N").ToUpper());
                        }
                    }
                }
            
            _uow._dbContext.Entry(user).State = EntityState.Modified;

            await _uow.Users.UpdateAsync(user);
            await _uow.SaveChangesAsync();
        }
        public async Task DeleteAsync(Guid id)
        {
            await _uow.Users.DeleteAsync(id);
            await _uow.SaveChangesAsync();
        }

        public async Task<PagedList<ExpandoObject>> GetAll_DataShaping_Async(BaseParameters parameters)
        {
            return await _uow.Users.GetAll_DataShaping_Async(parameters);
        }
        public async Task<ExpandoObject?> GetById_DataShaping_Async(Guid id, BaseParameters parameters)
        {
            return await _uow.Users.GetById_DataShaping_Async(id, parameters);
        }

        
        public async Task<IEnumerable<GetDTO_NumBooksIssuedToUser>> GetNumBooksIssuedToUserAsync
            (UserParameters parameters)
        {
            return _mapper
                .Map<
                        IEnumerable<(string FirstName, string LastName, int BooksLoaned)>, 
                        IEnumerable<GetDTO_NumBooksIssuedToUser>>
                    (await _uow.Users.GetNumBooksIssuedToUserAsync(parameters));
        }
        
        
        #region IDENTITY

        public async Task<bool> RegisterAsync(RegisterModel model)
        {
            if (string.IsNullOrWhiteSpace(model.Password)) throw new Exception("Password is empty!");
            if (string.IsNullOrWhiteSpace(model.Email)) throw new Exception("Email is empty!");
            
            var user = MappingFunctions.MapSourceToDestination<RegisterModel, User>(model); // Mapping using Mapster
            // Checking if the email is already registered
            if ((await _uow._userManager.FindByEmailAsync(model.Email)) != null) 
                throw new Exception("Email is already registered!");

            // If no username has been set, then the default username is set
            if (string.IsNullOrEmpty(user.UserName)) user.UserName = Authorization.default_username + Guid.NewGuid();
            
            var result = await _uow.Users.CreateAsync(user);
            if (result == Guid.Empty) return false;
            
            return (await _uow._userManager.AddToRoleAsync(user, Authorization.default_role.ToString()))
                .Succeeded;
        }
        public async Task<bool> RegisterModeratorAsync(RegisterModel model)
        {
            var result = await RegisterAsync(model); // Register new user
            if (result == false) return false; // Checking whether the user has been created correctly
            
            // Mapping using Mapster
            var addRoleModel = MappingFunctions.MapSourceToDestination<RegisterModel, AddRoleModel>(model);
            addRoleModel.Role = Authorization.Roles.Moderator.ToString();
            
            return await AddRoleAsync(addRoleModel); // Add to user new role and return result
        }
        public async Task<bool> RegisterAdministratorAsync(RegisterModel model)
        {
            var result = await RegisterAsync(model); // Register new user
            if (result == false) return false; // Checking whether the user has been created correctly
            
            // Mapping using Mapster
            var addRoleModel = MappingFunctions.MapSourceToDestination<RegisterModel, AddRoleModel>(model);
            addRoleModel.Role = Authorization.Roles.Administrator.ToString();
            
            return await AddRoleAsync(addRoleModel); // Add to user new role and return result
        }

        public async Task<bool> AddRoleAsync(AddRoleModel model)
        {
            var user = await _uow._userManager.FindByEmailAsync(model.Email);
            
            if (user == null || (await _uow._userManager.CheckPasswordAsync(user, model.Password)) == false)
                throw new Exception("Incorrect Credentials for user!");
            
            //  If the user is a valid one
            // Check if the passed Role is present in our system 
            var roleExists = Enum
                .GetNames(typeof(Authorization.Roles))
                .Any(x => x.ToLower() == model.Role?.ToLower());

            // If not, throws an error message
            if (roleExists == false) throw new Exception($"Role {model.Role} not found!");
            
            var validRole = Enum
                .GetValues(typeof(Authorization.Roles))
                .Cast<Authorization.Roles>()
                .FirstOrDefault(x => x.ToString().ToLower() == model.Role?.ToLower());

            return (await _uow._userManager.AddToRoleAsync(user, validRole.ToString()))
                .Succeeded;
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


                // Check if there are any active refresh tokens available for the authenticated user
                if (user.RefreshTokens == null || user.RefreshTokens.Any(a => a.IsActive))
                {
                    // Set the available active refresh token to response
                    var activeRefreshToken = user.RefreshTokens?.FirstOrDefault(a => a.IsActive);
                    if (activeRefreshToken != null)
                    {
                        authenticationModel.RefreshToken = activeRefreshToken.Token;
                        authenticationModel.RefreshTokenExpiration = activeRefreshToken.Expires;
                    }
                }
                else
                {
                    // If there are not active Refresh Token available, we call our
                    // CreateRefreshToken method to generate a refresh token
                    var refreshToken = CreateRefreshToken();

                    //  Once generated, we set the details of the Refresh Token to the Response Object
                    authenticationModel.RefreshToken = refreshToken.Token;
                    authenticationModel.RefreshTokenExpiration = refreshToken.Expires;
                    user.RefreshTokens.Add(refreshToken);
                    
                    // Finally, we need to add these tokens into our RefreshTokens Table, so that we can reuse them
                    await _uow._userManager.UpdateAsync(user);
                    await _uow.SaveChangesAsync();
                }
                
                
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
        
        public async Task<AuthenticationModel> GetRefreshTokenAsync(string token)
        {
            // Create a new Response object
            var authenticationModel = new AuthenticationModel();
            
            // Check if there any matching user for the token in database
            var user = _uow._userManager.Users
                .SingleOrDefault(u => u.RefreshTokens == null || u.RefreshTokens.Any(t => t.Token == token));
            if (user == null) // If no matching user found, pass a message “Token did not match any users.”
            {
                authenticationModel.IsAuthenticated = false;
                authenticationModel.Message = "Token did not match any users.";
                
                return authenticationModel;
            }

            //  Get the Refresh token object of the matching record
            var refreshToken = user.RefreshTokens?.Single(x => x.Token == token);
            
            // Check is the selected token is active, if not active, send a message “Token Not Active.”
            if (refreshToken == null || !refreshToken.IsActive)
            {
                authenticationModel.IsAuthenticated = false;
                authenticationModel.Message = "Token Not Active.";
                
                return authenticationModel;
            }

            // Revoke Current Refresh Token. Every time we request a new JWT, we have to make sure
            // that we replace the refresh token with a new one
            refreshToken.Revoked = DateTime.UtcNow;
            
            // Generate new Refresh Token and save to Database
            var newRefreshToken = CreateRefreshToken();
            user.RefreshTokens?.Add(newRefreshToken);
            await _uow._userManager.UpdateAsync(user);
            await _uow.SaveChangesAsync();
            
            // Generates new jwt
            authenticationModel.IsAuthenticated = true;
            
            JwtSecurityToken jwtSecurityToken = await CreateJwtToken(user);
            authenticationModel.Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
            authenticationModel.Email = user.Email;
            authenticationModel.UserName = user.UserName;
            
            var roleList = await _uow._userManager.GetRolesAsync(user).ConfigureAwait(false);
            authenticationModel.RefreshToken = newRefreshToken.Token;
            authenticationModel.RefreshTokenExpiration = newRefreshToken.Expires;
            authenticationModel.Roles = roleList.ToList();
            
            return authenticationModel;
        }
        private static RefreshToken CreateRefreshToken()
        {
            var randomNumber = new byte[32];

            using (var generator = RandomNumberGenerator.Create())
            {
                generator.GetBytes(randomNumber);

                return new RefreshToken
                {
                    Token = Convert.ToBase64String(randomNumber),
                    Expires = DateTime.UtcNow.AddDays(10),
                    Created = DateTime.UtcNow
                };
            }
        }

        public async Task<bool> RevokeTokenAsync(string token)
        {
            var user = _uow._userManager.Users
                .SingleOrDefault(u => u.RefreshTokens != null && u.RefreshTokens.Any(t => t.Token == token));

            // Return false if no user found with token
            if (user == null) return await Task.FromResult(false);

            var refreshToken = user.RefreshTokens?.Single(x => x.Token == token);
            
            // Return false if token is not active
            if (refreshToken == null || !refreshToken.IsActive) return await Task.FromResult(false);
            
            // If the passed refresh token is valid, we revoke it here and save to the database
            refreshToken.Revoked = DateTime.UtcNow;
            await _uow._userManager.UpdateAsync(user);
            await _uow.SaveChangesAsync();

            return await Task.FromResult(true);
        }


        #endregion
    }
}
