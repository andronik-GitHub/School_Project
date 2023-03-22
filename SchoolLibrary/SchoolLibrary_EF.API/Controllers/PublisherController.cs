using Microsoft.AspNetCore.Mvc;
using SchoolLibrary_EF.BLL.DTO;
using SchoolLibrary_EF.BLL.Services.Contracts;
using SchoolLibrary_EF.DAL.Pagging;

namespace SchoolLibrary_EF.API.Controllers
{
    [Produces("application/json")]
    [Route("ef/[controller]")]
    [ApiController]
    public class PublisherController : ControllerBase
    {
        private readonly IPublisherService _publisherService;
        private readonly ILogger _logger;

        public PublisherController(IPublisherService publisherService, ILoggerFactory loggerFactory)
        {
            _publisherService = publisherService;
            _logger = loggerFactory.CreateLogger($"{this.GetType().Name}_Logger");
        }


        /// <summary>
        /// Gets the list of all Publishers
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// GET ef/publisher
        /// </remarks>
        /// <returns>Returns list of PublisherDTO</returns>
        /// <response code="200">Success</response>
        /// <response code="500">If it was not possible to get a list of elements from the database</response>
        [HttpGet] // GET: ef/publisher
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<PublisherDTO>>> GetAllAsync([FromQuery] AuthorParameters parameters)
        {
            try
            {
                var collection = await _publisherService.GetAllAsync(parameters);
                _logger.LogInformation
                    ("{Count} entities were successfully extracted from [Publishers]", collection.Count());

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
        /// Gets the Publisher by id
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// GET ef/publisher/13ce1333-7b7c-4395-8565-0474a6ad05ad
        /// </remarks>
        /// <param name="id">Publisher id (Guid)</param>
        /// <returns>Returns element of PublisherDTO</returns>
        /// <response code="200">Success</response>
        /// <response code="404">If the element with such ID is not found in the database</response>
        /// <response code="500">If it was not possible to get element from the database</response>
        [HttpGet("{id}")] // GET: ef/publisher/id
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<PublisherDTO>> GetByIdAsync(Guid id)
        {
            try
            {
                var entity = await _publisherService.GetAsync(id);

                if (entity == null)
                {
                    _logger.LogError("Entity with id: [{EntityId}] from [Publishers] not found", id);

                    return StatusCode(StatusCodes.Status404NotFound);
                    //return NotFound();
                }
                else
                {
                    _logger.LogInformation
                        ("Entity with id: [{EntityId}] were successfully extracted from [Publishers]", id);

                    return Ok(entity);
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
        /// Creates new Publisher
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     POST: ef/publisher
        ///     {
        ///         "publisherId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
        ///         "name": "string",
        ///         "country": "string",
        ///         "city": "string",
        ///         "street": "string"
        ///     }
        /// </remarks>
        /// <param name="newPublisher">PublisherDTO newEntity</param>
        /// <returns>Returns id (Guid)</returns>
        /// <response code="200">Success</response>
        /// <response code="400">If invalid data entered</response>
        /// <response code="500">If it was not possible to adding element to the database</response>
        [HttpPost] // POST: ef/publisher
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Guid>> AddAsync(PublisherDTO newPublisher)
        {
            try
            {
                // Checking whether valid data has been entered
                if (newPublisher.Name == null || newPublisher.Country == null ||
                    newPublisher.Country == null || newPublisher.Street == null)
                {
                    _logger.LogError("Invalid data entered");

                    return StatusCode(StatusCodes.Status400BadRequest);
                    //return BadRequest("Invalid data entered");
                }
                else
                {
                    var id = await _publisherService.CreateAsync(newPublisher);
                    _logger.LogInformation
                        ("Entity with id: [{EntityId}] were successfully added to [Publishers]", id);

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
        /// Update the Publisher
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     PUT: ef/publisher
        ///     {
        ///         "authorid": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
        ///         "firstName": "string",
        ///         "lastName": "string",
        ///         "birthdate": "2023-03-20T14:31:27.294Z",
        ///         "country": "string"
        ///     }
        /// </remarks>
        /// <param name="updatePublisher">PublisherDTO updateEntity</param>
        /// <returns>Returns NoContent</returns>
        /// <response code="204">Success</response>
        /// <response code="400">If invalid data entered</response>
        /// <response code="500">If it was not possible to adding element to the database</response>
        [HttpPut] // PUT: ef/publisher
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> UpdateAsync(PublisherDTO updatePublisher)
        {
            try
            {
                // Checking whether valid data has been entered
                if (updatePublisher.Name == null || updatePublisher.Country == null ||
                    updatePublisher.Country == null || updatePublisher.Street == null)
                    {
                    _logger.LogError("Invalid data entered");

                    return StatusCode(StatusCodes.Status400BadRequest);
                    //return BadRequest("Invalid data entered");
                }
                else
                {
                    // Whether there is such a record in the database at all
                    var findResult = await _publisherService.GetAsync(updatePublisher.PublisherId);

                    if (findResult == null)
                    {
                        _logger.LogError
                            ("Entity with id: [{EntityId}] from [Publishers] not found",
                                updatePublisher.PublisherId);

                        return StatusCode(StatusCodes.Status404NotFound);
                        //return NotFound();
                    }
                    else
                    {
                        await _publisherService.UpdateAsync(updatePublisher);
                        _logger.LogInformation
                            ("Entity with id: [{EntityId}] were successfully updated from [Publishers]",
                                updatePublisher.PublisherId);

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
        /// Delete the Publisher by id
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// DELETE: ef/publisher/13ce1333-7b7c-4395-8565-0474a6ad05ad
        /// </remarks>
        /// <param name="id">Publisher id (Guid)</param>
        /// <returns>Returns NoContent</returns>
        /// <response code="204">Success</response>
        /// <response code="400">If invalid data entered</response>
        /// <response code="500">If it was not possible to adding element to the database</response>
        [HttpDelete("{id}")] // DELETE: ef/publisher/id
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> DeleteAsync(Guid id)
        {
            try
            {
                // Whether there is such a record in the database at all
                var findResult = await _publisherService.GetAsync(id);

                if (findResult == null)
                {
                    _logger.LogError("Entity with id: [{EntityId}] from [Publishers] not found", id);

                    return StatusCode(StatusCodes.Status404NotFound);
                    //return NotFound();
                }
                else
                {
                    await _publisherService.DeleteAsync(id);
                    _logger.LogInformation
                        ("Entity with id: [{EntityId}] were successfully deleted from [Publishers]", id);

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
