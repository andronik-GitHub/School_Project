using System.Dynamic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchoolLibrary_EF.BLL.DTO;
using SchoolLibrary_EF.BLL.DTO.HATEOAS;
using SchoolLibrary_EF.BLL.Services.Contracts;
using SchoolLibrary_EF.DAL.Entities.Identity;
using SchoolLibrary_EF.DAL.Paging.Entities;

namespace SchoolLibrary_EF.API.Controllers
{
    [Produces("application/json")]
    [Route("ef/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ILogger _logger;
        private readonly IUrlHelper _urlHelper;

        public UserController(IUserService userService, ILoggerFactory loggerFactory, IUrlHelper urlHelper)
        {
            _userService = userService;
            _urlHelper = urlHelper;
            _logger = loggerFactory.CreateLogger($"{this.GetType().Name}_Logger");
        }


        /// <summary>
        /// Gets the list of all Users
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// GET ef/user?UserName=Bob(amp)PageNumber=5(amp)PageSize=10
        /// </remarks>
        /// <param name="parameters">User parameters for sort/paging/... (UserParameters)</param>
        /// <returns>Returns list of UserDTO</returns>
        /// <response code="200">Success</response>
        /// <response code="500">If it was not possible to get a list of elements from the database</response>
        [HttpGet(Name = nameof(GetAllUsersAsync))] // GET: ef/user?UserName=Bob&PageNumber=5&PageSize=10
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<UserDTO>>> GetAllUsersAsync([FromQuery] UserParameters parameters)
        {
            try
            {
                var collection = (await _userService.GetAllAsync(parameters)).ToList();
                _logger.LogInformation
                    ("{Count} entities were successfully extracted from [Users]", collection.Count);

                collection.ForEach(item => this.CreateLinksForEntity(item)); // HATEOAS
                return Ok(collection);
            }
            catch (Exception ex)
            {
                _logger.LogError
                    ("Error in [{ErrorClassName}]->[GetAllAsync] => {ErrorMessage}", this.GetType().Name, ex.Message);

                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Gets the User by id
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// GET ef/user/13ce1333-7b7c-4395-8565-0474a6ad05ad
        /// </remarks>
        /// <param name="id">User id (Guid)</param>
        /// <returns>Returns element of UserDTO</returns>
        /// <response code="200">Success</response>
        /// <response code="404">If the element with such ID is not found in the database</response>
        /// <response code="500">If it was not possible to get element from the database</response>
        [HttpGet("{id:guid}", Name = nameof(GetUserByIdAsync))] // GET: ef/user/id
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<UserDTO>> GetUserByIdAsync(Guid id)
        {
            try
            {
                var entity = await _userService.GetAsync(id);

                if (entity == null)
                {
                    _logger.LogError("Entity with id: [{EntityId}] from [Users] not found", id);

                    return StatusCode(StatusCodes.Status404NotFound);
                    //return NotFound();
                }
                else
                {
                    _logger.LogInformation
                        ("Entity with id: [{EntityId}] were successfully extracted from [Users]", id);

                    return Ok(this.CreateLinksForEntity(entity)); // HATEOAS
                }
            }
            catch(Exception ex)
            {
                _logger.LogError
                    ("Error in [{ErrorClassName}]->[GetAllAsync] => {ErrorMessage}", this.GetType().Name, ex.Message);

                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Creates new User
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     POST: ef/user
        ///     {
        ///         "userId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
        ///         "firstName": "string",
        ///         "lastName": "string",
        ///         "email": "string"
        ///     }
        /// </remarks>
        /// <param name="newUser">UserDTO newEntity</param>
        /// <returns>Returns id (Guid)</returns>
        /// <response code="200">Success</response>
        /// <response code="400">If invalid data entered</response>
        /// <response code="500">If it was not possible to adding element to the database</response>
        [HttpPost(Name = nameof(AddUserAsync))] // POST: ef/user
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Guid>> AddUserAsync(UserDTO newUser)
        {
            try
            {
                // Checking whether valid data has been entered
                if (newUser.FirstName == null || newUser.LastName == null || newUser.Email == null)
                {
                    _logger.LogError("Invalid data entered");

                    return StatusCode(StatusCodes.Status400BadRequest);
                    //return BadRequest("Invalid data entered");
                }
                else
                {
                    var id = await _userService.CreateAsync(newUser);
                    _logger.LogInformation
                        ("Entity with id: [{EntityId}] were successfully added to [Users]", id);

                    return Ok(id);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError
                    ("Error in [{ErrorClassName}]->[GetAllAsync] => {ErrorMessage}", this.GetType().Name, ex.Message);

                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Update the User
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     PUT: ef/user
        ///     {
        ///         "userId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
        ///         "firstName": "string",
        ///         "lastName": "string",
        ///         "email": "string"
        ///     }
        /// </remarks>
        /// <param name="updateUser">UserDTO updateEntity</param>
        /// <returns>Returns NoContent</returns>
        /// <response code="204">Success</response>
        /// <response code="400">If invalid data entered</response>
        /// <response code="500">If it was not possible to adding element to the database</response>
        [HttpPut(Name = nameof(UpdateUserAsync))] // PUT: ef/user
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> UpdateUserAsync(UserDTO updateUser)
        {
            try
            {
                // Checking whether valid data has been entered
                if (updateUser.FirstName == null || updateUser.LastName == null || updateUser.Email == null)
                {
                    _logger.LogError("Invalid data entered");

                    return StatusCode(StatusCodes.Status400BadRequest);
                    //return BadRequest("Invalid data entered");
                }
                else
                {
                    // Whether there is such a record in the database at all
                    var findResult = await _userService.GetAsync(updateUser.UserId);

                    if (findResult == null)
                    {
                        _logger.LogError
                            ("Entity with id: [{EntityId}] from [Users] not found", updateUser.UserId);

                        return StatusCode(StatusCodes.Status404NotFound);
                        //return NotFound();
                    }
                    else
                    {
                        await _userService.UpdateAsync(updateUser);
                        _logger.LogInformation
                            ("Entity with id: [{EntityId}] were successfully updated from [Users]",
                                updateUser.UserId);

                        return StatusCode(StatusCodes.Status204NoContent);
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError
                    ("Error in [{ErrorClassName}]->[GetAllAsync] => {ErrorMessage}", this.GetType().Name, ex.Message);

                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Delete the User by id
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// DELETE: ef/user/13ce1333-7b7c-4395-8565-0474a6ad05ad
        /// </remarks>
        /// <param name="id">User id (Guid)</param>
        /// <returns>Returns NoContent</returns>
        /// <response code="204">Success</response>
        /// <response code="400">If invalid data entered</response>
        /// <response code="500">If it was not possible to adding element to the database</response>
        [HttpDelete("{id:guid}", Name = nameof(DeleteUserAsync))] // DELETE: ef/user/id
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> DeleteUserAsync(Guid id)
        {
            try
            {
                // Whether there is such a record in the database at all
                var findResult = await _userService.GetAsync(id);

                if (findResult == null)
                {
                    _logger.LogError("Entity with id: [{EntityId}] from [Users] not found", id);

                    return StatusCode(StatusCodes.Status404NotFound);
                    //return NotFound();
                }
                else
                {
                    await _userService.DeleteAsync(id);
                    _logger.LogInformation
                        ("Entity with id: [{EntityId}] were successfully deleted from [Users]", id);

                    return StatusCode(StatusCodes.Status204NoContent);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError
                    ("Error in [{ErrorClassName}]->[GetAllAsync] => {ErrorMessage}", this.GetType().Name, ex.Message);

                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        

        /// <summary>
        /// Gets the list of all Users
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// GET ef/user/datashaping?Fields=UserId%2C%20FirstName%2C%20LastName%2C%20Password
        /// </remarks>
        /// <returns>Returns list of ExpandoObject(User)</returns>
        /// <response code="200">Success</response>
        /// <response code="500">If it was not possible to get a list of elements from the database</response>
        [HttpGet("datashaping/", Name = nameof(GetAllUsers_DataShaping_Async))] // ef/user/datashaping?Fields=UserId%2C%20FirstName%2C%20LastName%2C%20Password
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> GetAllUsers_DataShaping_Async([FromQuery] UserParameters parameters)
        {
            try
            {
                var collection = await _userService.GetAll_DataShaping_Async(parameters);
                _logger.LogInformation
                    ("{Count} entities were successfully extracted from [Users]", collection.Count());

                return Ok(collection);
            }
            catch (Exception ex)
            {
                _logger.LogError
                    ("Error in [{ErrorClassName}]->[GetAllAsync] => {ErrorMessage}", this.GetType().Name, ex.Message);

                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Gets the User by id
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// GET ef/user/datashaping/b12c5ca7-ab3f-4d0c-bc58-0512bbb30e69?Fields=UserId%2C%20FirstName%2C%20Email
        /// </remarks>
        /// <param name="id">User id (Guid)</param>
        /// <param name="parameters">User parameters for sort/paging/... (UserParameters)</param>
        /// <returns>Returns element of ExpandoObject(User)</returns>
        /// <response code="200">Success</response>
        /// <response code="404">If the element with such ID is not found in the database</response>
        /// <response code="500">If it was not possible to get element from the database</response>
        [HttpGet("datashaping/{id:guid}", Name = nameof(GetUserById_DataShaping_Async))] // ef/user/datashaping/b12c5ca7-ab3f-4d0c-bc58-0512bbb30e69?Fields=UserId%2C%20FirstName%2C%20Email
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> GetUserById_DataShaping_Async(Guid id, [FromQuery] UserParameters? parameters)
        {
            try
            {
                var entity = await _userService.GetById_DataShaping_Async(id, parameters);

                if (entity == default(ExpandoObject))
                {
                    _logger.LogError("User with id: {id}, hasn't been found in db.", id);
                    return StatusCode(StatusCodes.Status404NotFound);
                }

                return Ok(entity);
            }
            catch (Exception ex)
            {
                _logger.LogError
                    ("Error in [{ErrorClassName}]->[GetAllAsync] => {ErrorMessage}", this.GetType().Name, ex.Message);

                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        
        /// <summary>
        /// Get secured data
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// GET https://localhost:5001/ef/User/secure%data
        /// </remarks>
        /// <returns>Returns secured data</returns>
        /// <response code="200">Success</response>
        /// <response code="401">If user not authentication</response>
        [Authorize]
        [HttpGet("secure%data/")] // GET: ef/User/secure%data
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> GetSecuredData()
        {
            return await Task.Run(() => Ok("Secured data"));
        }

        /// <summary>
        /// Get secured data for user with role "Administrator"
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// GET https://localhost:5001/ef/User/secure%data/authorization
        /// </remarks>
        /// <returns>Returns secured data</returns>
        /// <response code="200">Success</response>
        /// <response code="403">Is user role is not "Administrator"</response>
        /// <response code="401">If user not authentication</response>
        [Authorize(Roles = "Administrator")]
        [HttpGet("secure%data/authorization")] // GET: ef/User/secure%data/authorization
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> GerSecuredDataForAdministrator()
        {
            return await Task.Run(() => Ok("This Secured Data is available only for Users-Administrator."));
        }

        /// <summary>
        /// Get all the refresh tokens
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     POST https://localhost:5001/ef/User/tokens/b12c5ca7-ab3f-4d0c-bc58-0512bbb30e69
        /// </remarks>
        /// <returns>returns all the refresh tokens</returns>
        /// <response code="200">Success</response>
        /// <response code="401">If user not authentication</response>
        /// <response code="404">If the element with such ID is not found in the database</response>
        /// <response code="500">If it was not possible to adding element to the database</response>
        [Authorize]
        [HttpPost("tokens/{id:guid}")] // POST: ef/User/tokens/b12c5ca7-ab3f-4d0c-bc58-0512bbb30e69
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> GetUserRefreshTokens(Guid id)
        {
            var user = await _userService.GetUserWithRefreshTokensAsync(id);

            if (user == null)
            {
                _logger.LogError("Entity with id: [{EntityId}] from [Users] not found", id);

                return StatusCode(StatusCodes.Status404NotFound);
            }
            
            return Ok(user.RefreshTokens);
        }

        /// <summary>
        /// Registering new user
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     POST https://localhost:5001/ef/User/register
        ///     {
        ///         "FirstName": "John",
        ///         "LastName": "Snow",
        ///         "UserName": "John_Snow",
        ///         "Email": "john_snow@gmail.com",
        ///         "Password": "Pa$$w0rd"
        ///     }
        /// </remarks>
        /// <param name="model">RegisterModel for creating new user</param>
        /// <returns>Returns a message about successful user registration</returns>
        /// <response code="200">Success</response>
        [HttpPost("register")] // POST: ef/User/register
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> RegisterAsync(RegisterModel model)
        {
            var result = await _userService.RegisterAsync(model);
            return Ok(result);
        }
        /// <summary>
        /// Registering new user-administrator
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     POST https://localhost:5001/ef/User/register-administrator
        ///     {
        ///         "FirstName": "John",
        ///         "LastName": "Snow",
        ///         "UserName": "John_Snow-administrator",
        ///         "Email": "john_snow-administrator@gmail.com",
        ///         "Password": "Pa$$w0rd"
        ///     }
        /// </remarks>
        /// <param name="model">RegisterModel for creating new user-administrator</param>
        /// <returns>Returns a message about successful user-administrator registration</returns>
        /// <response code="200">Success</response>
        [HttpPost("register-administrator")] // POST: ef/User/register-administrator
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> RegisterAdministratorAsync(RegisterModel model)
        {
            var result = await _userService.RegisterAdministratorAsync(model);
            return Ok(result);
        }
        /// <summary>
        /// Add role ot user
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     POST https://localhost:5001/ef/User/add-role
        ///     {
        ///         "Email": "john_snow@gmail.com",
        ///         "Password": "Pa$$w0rd"
        ///         "Role": "Administrator"
        ///     }
        /// </remarks>
        /// <param name="model">RegisterModel for creating new user-administrator</param>
        /// <returns>Returns a message about successful user-administrator registration</returns>
        /// <response code="200">Success</response>
        [HttpPost("add-role")] // POST: ef/User/add-role
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> AddRoleAsync(AddRoleModel model)
        {
            var result = await _userService.AddRoleAsync(model);
            return Ok(result);
        }

        /// <summary>
        /// Get token
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     POST https://localhost:5001/ef/User/token
        ///     {
        ///         "Email": "john_snow@gmail.com",
        ///         "Password": "Pa$$w0rd"
        ///     }
        /// </remarks>
        /// <returns>Returns JWT Security token</returns>
        /// <response code="200">Success</response>
        [HttpPost("token")] // POST: ef/User/token
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> GetTokenAsync(TokenRequestModel model)
        {
            var result = await _userService.GetTokenAsync(model);
            
            // Save refreshTokens as cookies
            if (result.RefreshToken != null) SetRefreshTokenInCookie(result.RefreshToken);

            return Ok(result);
        }
        /// <summary>
        /// Get refresh-token
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     POST https://localhost:5001/ef/User/refresh-token
        ///     {
        ///         "Email": "john_snow@gmail.com",
        ///         "Password": "Pa$$w0rd"
        ///     }
        /// </remarks>
        /// <returns>Returns JWT Security token</returns>
        /// <response code="200">Success</response>
        [HttpPost("refresh-token")] // POST: ef/User/refresh-token
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> GetRefreshTokenAsync()
        {
            // Get the Refresh Token from our cookies
            var refreshToken = Request.Cookies["refreshToken"];
            // Returns the response object from the Service Method
            var response = await _userService
                .GetRefreshTokenAsync(refreshToken ?? throw new InvalidOperationException());
            
            // Sets the new Refresh Token to our Cookie
            if (!string.IsNullOrEmpty(response.RefreshToken))
                SetRefreshTokenInCookie(response.RefreshToken);

            return Ok(response);
        }

        [HttpPost("revoke-token")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> RevokeTokenAsync([FromBody] RevokeTokenRequest model)
        {
            // Accept token from request body or cookie
            var token = model.Token ?? Request.Cookies["refreshToken"];
            if (string.IsNullOrEmpty(token)) return BadRequest(new { message = "Token is required" });

            // Revoke the Token
            var response = await _userService.RevokeTokenAsync(token);
            if (!response) return NotFound(new { message = "Token not found" });
            
            return Ok(new { message = "Token revoked" });
        }


        private void SetRefreshTokenInCookie(string refreshToken)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = DateTime.UtcNow.AddDays(10)
            };
            Response.Cookies.Append("refreshToken", refreshToken, cookieOptions);
        }
        private UserDTO CreateLinksForEntity(UserDTO entity) // HATEOAS
        {
            var idObj = new { id = entity.UserId };
            
            entity.Links.Add(
                new Link(this._urlHelper.Link(nameof(this.GetUserByIdAsync), idObj)!,
                    "self",
                    "GET"));
            
            entity.Links.Add(
                new Link(this._urlHelper.Link(nameof(this.UpdateUserAsync), idObj)!,
                    "update_user",
                    "UPDATE"));
            
            entity.Links.Add(
                new Link(this._urlHelper.Link(nameof(this.DeleteUserAsync), idObj)!,
                    "delete_user",
                    "DELETE"));

            return entity;
        } 
    }
}
