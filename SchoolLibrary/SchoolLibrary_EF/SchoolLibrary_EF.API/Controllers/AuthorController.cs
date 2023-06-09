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
    public class AuthorController : ControllerBase
    {
        private readonly IAuthorService _authorService;
        private readonly ILogger _logger;
        private readonly IUrlHelper _urlHelper;

        public AuthorController(IAuthorService authorService, ILoggerFactory loggerFactory, IUrlHelper urlHelper)
        {
            _authorService = authorService;
            _urlHelper = urlHelper;
            _logger = loggerFactory.CreateLogger($"{this.GetType().Name}_Logger");
        }


        /// <summary>
        /// Gets the list of all Authors
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// GET ef/author?MinYearOfBirth=1990(amp)MaxYearOfBirth=2000(amp)PageNumber=5(amp)PageSize=10
        /// </remarks>
        /// <returns>Returns list of AuthorDTO</returns>
        /// <response code="200">Success</response>
        /// <response code="400">If invalid filtering data is entered</response>
        /// <response code="500">If it was not possible to get a list of elements from the database</response>
        [HttpGet(Name = nameof(GetAllAuthorsAsync))] // GET: ef/author?MinYearOfBirth=1990&MaxYearOfBirth=2000&PageNumber=5&PageSize=10
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Produces("application/json")]
        public async Task<ActionResult<IEnumerable<AuthorDTO>>> GetAllAuthorsAsync
            ([FromQuery] AuthorParameters parameters)
        {
            try
            {
                if (!parameters.ValidYearRand) // if invalid filtering data is entered
                    return StatusCode(StatusCodes.Status400BadRequest);

                var collection = (await _authorService.GetAllAsync(parameters)).ToList();
                _logger.LogInformation
                    ("{Count} entities were successfully extracted from [Authors]", collection.Count);

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
        /// Gets the Author by id
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// GET ef/author/13ce1333-7b7c-4395-8565-0474a6ad05ad
        /// </remarks>
        /// <param name="id">Author id (Guid)</param>
        /// <returns>Returns element of AuthorDTO</returns>
        /// <response code="200">Success</response>
        /// <response code="404">If the element with such ID is not found in the database</response>
        /// <response code="500">If it was not possible to get element from the database</response>
        [HttpGet("{id:guid}", Name = nameof(GetAuthorByIdAsync))] // GET: ef/author/id
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<AuthorDTO>> GetAuthorByIdAsync(Guid id)
        {
            try
            {
                var entity = await _authorService.GetAsync(id);

                if (entity == null)
                {
                    _logger.LogError("Entity with id: [{EntityId}] from [Authors] not found", id);

                    return StatusCode(StatusCodes.Status404NotFound);
                    //return NotFound();
                }
                else
                {
                    _logger.LogInformation
                        ("Entity with id: [{EntityId}] were successfully extracted from [Authors]", id);

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
        /// Creates new Author
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     POST: ef/author
        ///     {
        ///         "authorid": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
        ///         "firstName": "string",
        ///         "lastName": "string",
        ///         "birthdate": "2023-03-20T14:31:27.294Z",
        ///         "country": "string"
        ///     }
        /// </remarks>
        /// <param name="newAuthor">AuthorDTO newEntity</param>
        /// <returns>Returns id (Guid)</returns>
        /// <response code="200">Success</response>
        /// <response code="400">If invalid data entered</response>
        /// <response code="500">If it was not possible to adding element to the database</response>
        [HttpPost(Name = nameof(AddAuthorAsync))] // POST: ef/author
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Guid>> AddAuthorAsync(AuthorDTO newAuthor)
        {
            try
            {
                // Checking whether valid data has been entered
                if (newAuthor.FirstName == null || newAuthor.LastName == null || newAuthor.Country == null)
                {
                    _logger.LogError("Invalid data entered");

                    return StatusCode(StatusCodes.Status400BadRequest);
                    //return BadRequest("Invalid data entered");
                }
                else
                {
                    var id = await _authorService.CreateAsync(newAuthor);
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
        /// Update the Author
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     PUT: ef/author
        ///     {
        ///         "authorid": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
        ///         "firstName": "string",
        ///         "lastName": "string",
        ///         "birthdate": "2023-03-20T14:31:27.294Z",
        ///         "country": "string"
        ///     }
        /// </remarks>
        /// <param name="updateAuthor">AuthorDTO updateEntity</param>
        /// <returns>Returns NoContent</returns>
        /// <response code="204">Success</response>
        /// <response code="400">If invalid data entered</response>
        /// <response code="500">If it was not possible to adding element to the database</response>
        [HttpPut(Name = nameof(UpdateAuthorAsync))] // PUT: ef/author
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> UpdateAuthorAsync(AuthorDTO updateAuthor)
        {
            try
            {
                // Checking whether valid data has been entered
                if (updateAuthor.FirstName == null || updateAuthor.LastName == null || updateAuthor.Country == null)
                {
                    _logger.LogError("Invalid data entered");

                    return StatusCode(StatusCodes.Status400BadRequest);
                    //return BadRequest("Invalid data entered");
                }
                else
                {
                    // Whether there is such a record in the database at all
                    var findResult = await _authorService.GetAsync(updateAuthor.Authorid);

                    if (findResult == null)
                    {
                        _logger.LogError
                            ("Entity with id: [{EntityId}] from [Authors] not found", updateAuthor.Authorid);

                        return StatusCode(StatusCodes.Status404NotFound);
                        //return NotFound();
                    }
                    else
                    {
                        await _authorService.UpdateAsync(updateAuthor);
                        _logger.LogInformation
                            ("Entity with id: [{EntityId}] were successfully updated from [Publishers]", 
                                updateAuthor.Authorid);

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
        /// Delete the Author by id
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// DELETE: ef/author/13ce1333-7b7c-4395-8565-0474a6ad05ad
        /// </remarks>
        /// <param name="id">Author id (Guid)</param>
        /// <returns>Returns NoContent</returns>
        /// <response code="204">Success</response>
        /// <response code="400">If invalid data entered</response>
        /// <response code="500">If it was not possible to adding element to the database</response>
        [HttpDelete("{id:guid}", Name = nameof(DeleteAuthorAsync))] // DELETE: ef/author/id
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> DeleteAuthorAsync(Guid id)
        {
            try
            {
                // Whether there is such a record in the database at all
                var findResult = await _authorService.GetAsync(id);

                if (findResult == null)
                {
                    _logger.LogError("Entity with id: [{EntityId}] from [Authors] not found", id);

                    return StatusCode(StatusCodes.Status404NotFound);
                    //return NotFound();
                }
                else
                {
                    await _authorService.DeleteAsync(id);
                    _logger.LogInformation
                        ("Entity with id: [{EntityId}] were successfully deleted from [Authors]", id);

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
        /// Gets the list of all Authors
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// GET ef/author/datashaping?Fields=UserId%2C%20FirstName%2C%20LastName%2C%20Password
        /// </remarks>
        /// <returns>Returns list of ExpandoObject(Author)</returns>
        /// <response code="200">Success</response>
        /// <response code="500">If it was not possible to get a list of elements from the database</response>
        [HttpGet("datashaping/", Name = nameof(GetAllAuthors_DataShaping_Async))] // ef/author/datashaping?Fields=UserId%2C%20FirstName%2C%20LastName%2C%20Password
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> GetAllAuthors_DataShaping_Async([FromQuery] AuthorParameters parameters)
        {
            try
            {
                var collection = await _authorService.GetAll_DataShaping_Async(parameters);
                _logger.LogInformation
                    ("{Count} entities were successfully extracted from [Authors]", collection.Count());

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
        /// Gets the Author by id
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// GET ef/author/datashaping/b12c5ca7-ab3f-4d0c-bc58-0512bbb30e69?Fields=UserId%2C%20FirstName%2C%20Email
        /// </remarks>
        /// <param name="id">Author id (Guid)</param>
        /// <param name="parameters">Author parameters for sort/paging/... (AuthorParameters)</param>
        /// <returns>Returns element of ExpandoObject(Author)</returns>
        /// <response code="200">Success</response>
        /// <response code="404">If the element with such ID is not found in the database</response>
        /// <response code="500">If it was not possible to get element from the database</response>
        [HttpGet("datashaping/{id:guid}", Name = nameof(GetAuthorById_DataShaping_Async))] // ef/author/datashaping/b12c5ca7-ab3f-4d0c-bc58-0512bbb30e69?Fields=UserId%2C%20FirstName%2C%20Email
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> GetAuthorById_DataShaping_Async
            (Guid id, [FromQuery] AuthorParameters? parameters)
        {
            try
            {
                var entity = await _authorService.GetById_DataShaping_Async(id, parameters);

                if (entity == default(ExpandoObject))
                {
                    _logger.LogError("Author with id: {id}, hasn't been found in db.", id);
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
        
        



        private AuthorDTO CreateLinksForEntity(AuthorDTO entity) // HATEOAS
        {
            var idObj = new { id = entity.Authorid };
            
            entity.Links.Add(
                new Link(this._urlHelper.Link(nameof(this.GetAuthorByIdAsync), idObj)!,
                    "self",
                    "GET"));
            
            entity.Links.Add(
                new Link(this._urlHelper.Link(nameof(this.UpdateAuthorAsync), idObj)!,
                    "update_user",
                    "UPDATE"));
            
            entity.Links.Add(
                new Link(this._urlHelper.Link(nameof(this.DeleteAuthorAsync), idObj)!,
                    "delete_user",
                    "DELETE"));

            return entity;
        } 
    }
}
