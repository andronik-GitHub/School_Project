using Microsoft.AspNetCore.Mvc;
using SchoolLibrary_EF.BLL.DTO;
using SchoolLibrary_EF.BLL.Services.Contracts;
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

        public UserController(IUserService userService, ILoggerFactory loggerFactory)
        {
            _userService = userService;
            _logger = loggerFactory.CreateLogger($"{this.GetType().Name}_Logger");
        }


        /// <summary>
        /// Gets the list of all Users
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// GET ef/user?UserName=Bob(amp)PageNumber=5(amp)PageSize=10
        /// </remarks>
        /// <returns>Returns list of UserDTO</returns>
        /// <response code="200">Success</response>
        /// <response code="500">If it was not possible to get a list of elements from the database</response>
        [HttpGet] // GET: ef/user?UserName=Bob&PageNumber=5&PageSize=10
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<UserDTO>>> GetAllAsync([FromQuery] UserParameters parameters)
        {
            try
            {


                var collection = await _userService.GetAllAsync(parameters);
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
        /// GET ef/user/13ce1333-7b7c-4395-8565-0474a6ad05ad
        /// </remarks>
        /// <param name="id">User id (Guid)</param>
        /// <returns>Returns element of UserDTO</returns>
        /// <response code="200">Success</response>
        /// <response code="404">If the element with such ID is not found in the database</response>
        /// <response code="500">If it was not possible to get element from the database</response>
        [HttpGet("{id}")] // GET: ef/user/id
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<UserDTO>> GetByIdAsync(Guid id)
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

                    return Ok(entity);
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
        [HttpPost] // POST: ef/user
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Guid>> AddAsync(UserDTO newUser)
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
        [HttpPut] // PUT: ef/user
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> UpdateAsync(UserDTO updateUser)
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
        [HttpDelete("{id}")] // DELETE: ef/user/id
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> DeleteAsync(Guid id)
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
    }
}
