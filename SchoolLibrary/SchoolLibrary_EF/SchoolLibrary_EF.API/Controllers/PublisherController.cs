﻿using System.Dynamic;
using Microsoft.AspNetCore.Mvc;
using SchoolLibrary_EF.BLL.DTO;
using SchoolLibrary_EF.BLL.DTO.HATEOAS;
using SchoolLibrary_EF.BLL.Services.Contracts;
using SchoolLibrary_EF.DAL.Paging.Entities;

namespace SchoolLibrary_EF.API.Controllers
{
    [Produces("application/json")]
    [Route("ef/[controller]")]
    [ApiController]
    public class PublisherController : ControllerBase
    {
        private readonly IPublisherService _publisherService;
        private readonly ILogger _logger;
        private readonly IUrlHelper _urlHelper;

        public PublisherController(IPublisherService publisherService, ILoggerFactory loggerFactory, IUrlHelper urlHelper)
        {
            _publisherService = publisherService;
            _urlHelper = urlHelper;
            _logger = loggerFactory.CreateLogger($"{this.GetType().Name}_Logger");
        }


        /// <summary>
        /// Gets the list of all Publishers
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// GET ef/publisher?PageNumber=5(amp)PageSize=10
        /// </remarks>
        /// <returns>Returns list of PublisherDTO</returns>
        /// <response code="200">Success</response>
        /// <response code="500">If it was not possible to get a list of elements from the database</response>
        [HttpGet(Name = nameof(GetAllPublishersAsync))] // GET: ef/publisher?PageNumber=5&PageSize=10
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<PublisherDTO>>> GetAllPublishersAsync
            ([FromQuery] AuthorParameters parameters)
        {
            try
            {
                var collection = (await _publisherService.GetAllAsync(parameters)).ToList();
                _logger.LogInformation
                    ("{Count} entities were successfully extracted from [Publishers]", collection.Count);

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
        [HttpGet("{id:guid}", Name = nameof(GetPublisherByIdAsync))] // GET: ef/publisher/id
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<PublisherDTO>> GetPublisherByIdAsync(Guid id)
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

                    return Ok(this.CreateLinksForEntity(entity)); // HATEOAS
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
        [HttpPost(Name = nameof(AddPublisherAsync))] // POST: ef/publisher
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Guid>> AddPublisherAsync(PublisherDTO newPublisher)
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
        [HttpPut(Name = nameof(UpdatePublisherAsync))] // PUT: ef/publisher
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> UpdatePublisherAsync(PublisherDTO updatePublisher)
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
        [HttpDelete("{id:guid}", Name = nameof(DeletePublisherAsync))] // DELETE: ef/publisher/id
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> DeletePublisherAsync(Guid id)
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

        

        /// <summary>
        /// Gets the list of all Publishers
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// GET ef/publisher/datashaping?Fields=UserId%2C%20FirstName%2C%20LastName%2C%20Password
        /// </remarks>
        /// <returns>Returns list of ExpandoObject(Publisher)</returns>
        /// <response code="200">Success</response>
        /// <response code="500">If it was not possible to get a list of elements from the database</response>
        [HttpGet("datashaping/", Name = nameof(GetAllPublishers_DataShaping_Async))] // ef/publisher/datashaping?Fields=UserId%2C%20FirstName%2C%20LastName%2C%20Password
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> GetAllPublishers_DataShaping_Async([FromQuery] PublisherParameters parameters)
        {
            try
            {
                var collection = await _publisherService.GetAll_DataShaping_Async(parameters);
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
        /// GET ef/publisher/datashaping/b12c5ca7-ab3f-4d0c-bc58-0512bbb30e69?Fields=UserId%2C%20FirstName%2C%20Email
        /// </remarks>
        /// <param name="id">Publisher id (Guid)</param>
        /// <param name="parameters">Publisher parameters for sort/paging/... (PublisherParameters)</param>
        /// <returns>Returns element of ExpandoObject(Publisher)</returns>
        /// <response code="200">Success</response>
        /// <response code="404">If the element with such ID is not found in the database</response>
        /// <response code="500">If it was not possible to get element from the database</response>
        [HttpGet("datashaping/{id:guid}", Name = nameof(GetPublisherById_DataShaping_Async))] // ef/publisher/datashaping/b12c5ca7-ab3f-4d0c-bc58-0512bbb30e69?Fields=UserId%2C%20FirstName%2C%20Email
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> GetPublisherById_DataShaping_Async
            (Guid id, [FromQuery] PublisherParameters? parameters)
        {
            try
            {
                var entity = await _publisherService.GetById_DataShaping_Async(id, parameters);

                if (entity == default(ExpandoObject))
                {
                    _logger.LogError("Publisher with id: {id}, hasn't been found in db.", id);
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
        



        private PublisherDTO CreateLinksForEntity(PublisherDTO entity) // HATEOAS
        {
            var idObj = new { id = entity.PublisherId };
            
            entity.Links.Add(
                new Link(this._urlHelper.Link(nameof(this.GetPublisherByIdAsync), idObj)!,
                    "self",
                    "GET"));
            
            entity.Links.Add(
                new Link(this._urlHelper.Link(nameof(this.UpdatePublisherAsync), idObj)!,
                    "update_user",
                    "UPDATE"));
            
            entity.Links.Add(
                new Link(this._urlHelper.Link(nameof(this.DeletePublisherAsync), idObj)!,
                    "delete_user",
                    "DELETE"));

            return entity;
        } 
    }
}
