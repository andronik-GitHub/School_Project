using System.Dynamic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchoolLibrary_EF.BLL.DTOs.__HATEOAS;
using SchoolLibrary_EF.BLL.DTOs.UserDTOs;
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
        private readonly string _tableName;

        public UserController(IUserService userService, ILoggerFactory loggerFactory, IUrlHelper urlHelper)
        {
            _userService = userService;
            _urlHelper = urlHelper;
            _logger = loggerFactory.CreateLogger($"{this.GetType().Name}_Logger");
            
            _tableName = this.GetType().Name.Replace("Controller", "");
            _tableName = _tableName is "BookDetails" or "BookAuthors" or "BookGenres" ? _tableName : _tableName + "s";
        }


        #region CRUD

        
        /// <summary>
        /// Gets the list of all Users
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// GET ef/user
        /// </remarks>
        /// <param name="parameters">User parameters for sort/paging/... (UserParameters)</param>
        /// <returns>Returns list of GetDTO_User</returns>
        /// <response code="200">Success</response>
        /// <response code="500">If it was not possible to get a list of elements from the database</response>
        [HttpGet(Name = nameof(GetAllUsersAsync))] // GET: ef/user?UserName=Bob&PageNumber=5&PageSize=10
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<GetDTO_User>>> GetAllUsersAsync
            ([FromQuery] UserParameters parameters)
        {
            try
            {
                var collection = (await _userService.GetAllAsync(parameters)).ToList();
                _logger.LogInformation
                    ("{Count} entities were successfully extracted from [{Table}]", collection.Count, _tableName);

                collection.ForEach(item => this.CreateLinksForEntity(item)); // HATEOAS
                return Ok(collection);
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    "Error in [{ErrorClassName}]->[{MethodName}] => {ErrorMessage}", 
                    this.GetType().Name, nameof(GetAllUsersAsync), ex.Message);

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
        /// <returns>Returns element of GetDTO_User</returns>
        /// <response code="200">Success</response>
        /// <response code="404">If the element with such ID is not found in the database</response>
        /// <response code="500">If it was not possible to get element from the database</response>
        [HttpGet("{id:guid}", Name = nameof(GetUserByIdAsync))] // GET: ef/user/id
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<GetDTO_User>> GetUserByIdAsync(Guid id)
        {
            try
            {
                var entity = await _userService.GetAsync(id);
                if (entity == null)
                {
                    _logger.LogError("Entity with id: [{EntityId}] from [{Table}] not found", id, _tableName);
                    return StatusCode(StatusCodes.Status404NotFound);
                }
                    
                    
                _logger.LogInformation
                    ("Entity with id: [{EntityId}] were successfully extracted from [{Table}]", id, _tableName);

                return Ok(this.CreateLinksForEntity(entity)); // HATEOAS
            }
            catch(Exception ex)
            {
                _logger.LogError(
                    "Error in [{ErrorClassName}]->[{MethodName}] => {ErrorMessage}", 
                    this.GetType().Name, nameof(GetUserByIdAsync), ex.Message);

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
        ///         "firstName": "string",
        ///         "lastName": "string",
        ///         "street": "string",
        ///         "city": "string",
        ///         "country": "string"
        ///     }
        /// </remarks>
        /// <param name="newEntity">InsertDTO_User newEntity</param>
        /// <returns>Returns id (Guid)</returns>
        /// <response code="200">Success</response>
        /// <response code="400">If invalid data entered</response>
        /// <response code="500">If it was not possible to adding element to the database</response>
        [HttpPost(Name = nameof(AddUserAsync))] // POST: ef/user
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Guid>> AddUserAsync(InsertDTO_User newEntity)
        {
            try
            {
                // Checking whether valid data has been entered
                if (newEntity?.FirstName == null || newEntity?.LastName == null || newEntity?.Street == null ||
                    newEntity?.City == null || newEntity?.Country == null || newEntity?.UserName == null
                    || newEntity?.Email == null)
                {
                    _logger.LogError("Invalid data entered");
                    return StatusCode(StatusCodes.Status400BadRequest);
                }
                
                
                var id = await _userService.CreateAsync(newEntity);
                _logger.LogInformation
                    ("Entity with id: [{EntityId}] were successfully added to [{Table}]", id, _tableName);

                return Ok(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    "Error in [{ErrorClassName}]->[{MethodName}] => {ErrorMessage}", 
                    this.GetType().Name, nameof(AddUserAsync), ex.Message);

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
        ///         "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
        ///         "values":
        ///         {
        ///             "additionalProp1": "string",
        ///             "additionalProp2": "string",
        ///             "additionalProp3": "string",
        ///         }
        ///     }
        /// </remarks>
        /// <param name="upEntity">UpdateDTO_User updateEntity</param>
        /// <returns>Returns NoContent</returns>
        /// <response code="204">Success</response>
        /// <response code="400">If invalid data entered</response>
        /// <response code="500">If it was not possible to adding element to the database</response>
        [HttpPut(Name = nameof(UpdateUserAsync))] // PUT: ef/user
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> UpdateUserAsync([FromBody]UpdateDTO_User upEntity)
        {
            try
            {
                // Checking whether valid data has been entered
                if (upEntity.Values.Count <= 0)
                {
                    _logger.LogError("Invalid data entered");
                    return StatusCode(StatusCodes.Status400BadRequest);
                }

                    
                // Whether there is such a record in the database at all
                var findResult = await _userService.GetAsync(upEntity.Id);
                if (findResult == null)
                {
                    _logger.LogError
                        ("Entity with id: [{EntityId}] from [{Table}] not found", upEntity.Id, _tableName);

                    return StatusCode(StatusCodes.Status404NotFound);
                }

                
                await _userService.UpdateAsync(upEntity);
                _logger.LogInformation
                ("Entity with id: [{EntityId}] were successfully updated from [{Table}]",
                    upEntity.Id, _tableName);

                return StatusCode(StatusCodes.Status204NoContent);
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    "Error in [{ErrorClassName}]->[{MethodName}] => {ErrorMessage}", 
                    this.GetType().Name, nameof(UpdateUserAsync), ex.Message);

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
                    _logger.LogError("Entity with id: [{EntityId}] from [{Table}] not found", id, _tableName);
                    return StatusCode(StatusCodes.Status404NotFound);
                }

                
                await _userService.DeleteAsync(id);
                _logger.LogInformation
                    ("Entity with id: [{EntityId}] were successfully deleted from [{Table}]", id, _tableName);

                return StatusCode(StatusCodes.Status204NoContent);
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    "Error in [{ErrorClassName}]->[{MethodName}] => {ErrorMessage}", 
                    this.GetType().Name, nameof(DeleteUserAsync), ex.Message);

                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        
        #endregion

        #region Extended

        
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
        [HttpGet("datashaping/", Name = nameof(GetAllUsers_DataShaping_Async))]
        [ProducesResponseType(StatusCodes.Status200OK)] // ef/user/datashaping?Fields=UserId%2C%20FirstName%2C%20LastName%2C%20Password
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
                _logger.LogError(
                    "Error in [{ErrorClassName}]->[{MethodName}] => {ErrorMessage}", 
                    this.GetType().Name, nameof(GetAllUsers_DataShaping_Async), ex.Message);

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
        [HttpGet("datashaping/{id:guid}", Name = nameof(GetUserById_DataShaping_Async))]
        [ProducesResponseType(StatusCodes.Status200OK)] // ef/user/datashaping/id?Fields=UserId%2C%20FirstName%2C%20Email
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> GetUserById_DataShaping_Async(Guid id, [FromQuery] UserParameters parameters)
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
                _logger.LogError(
                    "Error in [{ErrorClassName}]->[{MethodName}] => {ErrorMessage}", 
                    this.GetType().Name, nameof(GetUserById_DataShaping_Async), ex.Message);

                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        
        /// <summary>
        /// Gets number of books issued per user
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// GET ef/user
        /// </remarks>
        /// <param name="parameters">User parameters for sort/paging/... (UserParameters)</param>
        /// <returns>Returns list of GetDTO_NumBooksIssuedToUser</returns>
        /// <response code="200">Success</response>
        /// <response code="500">If it was not possible to get a list of elements from the database</response>
        [HttpGet("extension/num-books-issued-to-user", Name = nameof(GetNumBooksIssuedToUserAsync))]
        [ProducesResponseType(StatusCodes.Status200OK)] // GET: ef/User/extension/num-books-issued-to-user
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<GetDTO_NumBooksIssuedToUser>>> GetNumBooksIssuedToUserAsync
            ([FromQuery] UserParameters parameters)
        {
            try
            {
                var collection = (await _userService.GetNumBooksIssuedToUserAsync(parameters)).ToList();
                _logger.LogInformation
                    ("{Count} entities were successfully extracted from [{Table}]", collection.Count, _tableName);

                return Ok(collection);
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    "Error in [{ErrorClassName}]->[{MethodName}] => {ErrorMessage}", 
                    this.GetType().Name, nameof(GetNumBooksIssuedToUserAsync), ex.Message);

                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        #endregion

        #region IDENTITY
        
        
        /// <summary>
        /// Get secured data
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// GET https://localhost:5001/ef/User/secure-data
        /// </remarks>
        /// <returns>Returns secured data</returns>
        /// <response code="200">Success</response>
        /// <response code="401">If user not authentication</response>
        [Authorize]
        [HttpGet("secure-data/", Name = nameof(GetSecuredData))] // GET: ef/User/secure-data
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> GetSecuredData()
        {
            try
            {
                return await Task.Run(() => Ok("Secured data"));
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    "Error in [{ErrorClassName}]->[{MethodName}] => {ErrorMessage}", 
                    this.GetType().Name, nameof(GetSecuredData), ex.Message);

                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Get secured data for user with role "Moderator"
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// GET https://localhost:5001/ef/User/secure-data/authorization
        /// </remarks>
        /// <returns>Returns secured data</returns>
        /// <response code="200">Success</response>
        /// <response code="403">Is user role is not "Moderator"</response>
        /// <response code="401">If user not authentication</response>
        [Authorize(Roles = "Moderator")]
        // GET: ef/User/secure-data/authorization-moderator
        [HttpGet("secure-data/authorization-moderator", Name = nameof(GerSecuredDataForModerator))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> GerSecuredDataForModerator()
        {
            try
            {
                return await Task.Run(() => Ok("This Secured Data is available only for Users-Moderator."));
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    "Error in [{ErrorClassName}]->[{MethodName}] => {ErrorMessage}", 
                    this.GetType().Name, nameof(GerSecuredDataForModerator), ex.Message);

                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Get secured data for user with role "Administrator"
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// GET https://localhost:5001/ef/User/secure-data/authorization
        /// </remarks>
        /// <returns>Returns secured data</returns>
        /// <response code="200">Success</response>
        /// <response code="403">Is user role is not "Administrator"</response>
        /// <response code="401">If user not authentication</response>
        [Authorize(Roles = "Administrator")] // GET: ef/User/secure-data/authorization-administrator
        [HttpGet("secure-data/authorization-administrator", Name = nameof(GerSecuredDataForAdministrator))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> GerSecuredDataForAdministrator()
        {
            try
            {
                return await Task.Run(() => Ok("This Secured Data is available only for Users-Administrator."));
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    "Error in [{ErrorClassName}]->[{MethodName}] => {ErrorMessage}", 
                    this.GetType().Name, nameof(GerSecuredDataForAdministrator), ex.Message);

                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
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
        [HttpPost("tokens/{id:guid}", Name = nameof(GetUserRefreshTokens))] // POST: ef/User/tokens/id
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> GetUserRefreshTokens(Guid id)
        {
            try
            {
                var user = await _userService.GetUserWithRefreshTokensAsync(id);

                if (user == null)
                {
                    _logger.LogError("Entity with id: [{EntityId}] from [{Table}] not found", id, _tableName);
                    return StatusCode(StatusCodes.Status404NotFound);
                }
            
                return Ok(user.RefreshTokens);
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    "Error in [{ErrorClassName}]->[{MethodName}] => {ErrorMessage}", 
                    this.GetType().Name, nameof(GetUserRefreshTokens), ex.Message);

                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
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
        [HttpPost("register", Name = nameof(RegisterAsync))] // POST: ef/User/register
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> RegisterAsync(RegisterModel model)
        {
            try
            {
                var result = await _userService.RegisterAsync(model);

                var resultMessage = result ? 
                    $"Successfully registered User with username {model.UserName}" : 
                    $"Failed to register User with username {model.UserName}";
                
                _logger.LogInformation("Result: {Result}", resultMessage);
                return Ok(resultMessage);
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    "Error in [{ErrorClassName}]->[{MethodName}] => {ErrorMessage}", 
                    this.GetType().Name, nameof(RegisterAsync), ex.Message);

                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        
        /// <summary>
        /// Registering new user-moderator
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     POST https://localhost:5001/ef/User/register-moderator
        ///     {
        ///         "FirstName": "John",
        ///         "LastName": "Snow",
        ///         "UserName": "John_Snow-administrator",
        ///         "Email": "john_snow-administrator@gmail.com",
        ///         "Password": "Pa$$w0rd"
        ///     }
        /// </remarks>
        /// <param name="model">RegisterModel for creating new user-moderator</param>
        /// <returns>Returns a message about successful user-moderator registration</returns>
        /// <response code="200">Success</response>
        [HttpPost("register-moderator", Name = nameof(RegisterModeratorAsync))] // POST: ef/User/register-moderator
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> RegisterModeratorAsync(RegisterModel model)
        {
            try
            {
                var result = await _userService.RegisterModeratorAsync(model);

                var resultMessage = result ? 
                    $"Successfully registered Moderator with username {model.UserName}" : 
                    $"Failed to register Moderator with username {model.UserName}";
                
                _logger.LogInformation("Result: {Result}", resultMessage);
                return Ok(resultMessage);
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    "Error in [{ErrorClassName}]->[{MethodName}] => {ErrorMessage}", 
                    this.GetType().Name, nameof(RegisterAdministratorAsync), ex.Message);

                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
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
        [HttpPost("register-administrator", Name = nameof(RegisterAdministratorAsync))] // POST: ef/User/register-administrator
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> RegisterAdministratorAsync(RegisterModel model)
        {
            try
            {
                var result = await _userService.RegisterAdministratorAsync(model);

                var resultMessage = result ? 
                    $"Successfully registered Administrator with username {model.UserName}" : 
                    $"Failed to register Administrator with username {model.UserName}";
                
                _logger.LogInformation("Result: {Result}", resultMessage);
                return Ok(resultMessage);
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    "Error in [{ErrorClassName}]->[{MethodName}] => {ErrorMessage}", 
                    this.GetType().Name, nameof(RegisterAdministratorAsync), ex.Message);

                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
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
        [HttpPost("add-role", Name = nameof(AddRoleAsync))] // POST: ef/User/add-role
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> AddRoleAsync(AddRoleModel model)
        {
            try 
            {
                var result = await _userService.AddRoleAsync(model);

                var resultMessage = result ? 
                    $"Role [{model.Role}] is successfully added to user with email [{model.Email}]." : 
                    $"Role [{model.Role}] has not been added to user with email address [{model.Email}].";
                
                _logger.LogInformation("Result: {Result}", resultMessage);
                return Ok(resultMessage);
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    "Error in [{ErrorClassName}]->[{MethodName}] => {ErrorMessage}", 
                    this.GetType().Name, nameof(AddRoleAsync), ex.Message);

                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
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
        [HttpPost("token", Name = nameof(GetTokenAsync))] // POST: ef/User/token
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> GetTokenAsync(TokenRequestModel model)
        {
            try
            {
                var result = await _userService.GetTokenAsync(model);
            
                // Save refreshTokens as cookies
                if (result.RefreshToken != null) SetRefreshTokenInCookie(result.RefreshToken);

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    "Error in [{ErrorClassName}]->[{MethodName}] => {ErrorMessage}", 
                    this.GetType().Name, nameof(GetTokenAsync), ex.Message);

                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
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
        [HttpPost("refresh-token", Name = nameof(GetRefreshTokenAsync))] // POST: ef/User/refresh-token
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> GetRefreshTokenAsync()
        {
            try
            {
                var refreshToken = Request.Cookies["refreshToken"]; // Get the Refresh Token from our cookies
                
                var response = await _userService // Returns the response object from the Service Method
                    .GetRefreshTokenAsync(refreshToken ?? throw new InvalidOperationException());
            
                if (!string.IsNullOrEmpty(response.RefreshToken)) // Sets the new Refresh Token to our Cookie
                    SetRefreshTokenInCookie(response.RefreshToken);

                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    "Error in [{ErrorClassName}]->[{MethodName}] => {ErrorMessage}", 
                    this.GetType().Name, nameof(GetRefreshTokenAsync), ex.Message);

                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Revoke token
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     POST https://localhost:5001/ef/User/revoke-token
        ///     {
        ///         "token": "string-token"
        ///     }
        /// </remarks>
        /// <returns>Returns a message whether the token was successfully revoked</returns>
        /// <response code="200">Success</response>
        [HttpPost("revoke-token", Name = nameof(RevokeTokenAsync))] // POST: ef/User/revoke-token
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> RevokeTokenAsync([FromBody] RevokeTokenRequest model)
        {
            try
            {
                // Accept token from request body or cookie
                var token = model.Token ?? Request.Cookies["RefreshToken"];
                if (string.IsNullOrEmpty(token)) return BadRequest(new { message = "Token is required" });

                // Revoke the Token
                var response = await _userService.RevokeTokenAsync(token);
                if (!response) return NotFound(new { message = "Token not found" });
            
                return Ok(new { message = "Token revoked" });
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    "Error in [{ErrorClassName}]->[{MethodName}] => {ErrorMessage}", 
                    this.GetType().Name, nameof(RevokeTokenAsync), ex.Message);

                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


        #endregion


        private void SetRefreshTokenInCookie(string refreshToken) // IDENTITY
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = DateTime.UtcNow.AddDays(10)
            };
            Response.Cookies.Append("RefreshToken", refreshToken, cookieOptions);
        }
        private GetDTO_User CreateLinksForEntity(GetDTO_User entity) // HATEOAS
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
