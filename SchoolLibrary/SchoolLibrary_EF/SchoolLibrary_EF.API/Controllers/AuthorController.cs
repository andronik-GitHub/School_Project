using System.Dynamic;
using Microsoft.AspNetCore.Mvc;
using SchoolLibrary_EF.BLL.DTOs.__HATEOAS;
using SchoolLibrary_EF.BLL.DTOs.AuthorDTOs;
using SchoolLibrary_EF.BLL.Services.Contracts;
using SchoolLibrary_EF.DAL.Paging.Entities;

namespace SchoolLibrary_EF.API.Controllers
{
    [Produces("application/json")]
    [Route("ef/[controller]/")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        private readonly IAuthorService _authorService;
        private readonly ILogger _logger;
        private readonly IUrlHelper _urlHelper;
        private readonly string _tableName;

        public AuthorController(IAuthorService authorService, ILoggerFactory loggerFactory, IUrlHelper urlHelper)
        {
            _authorService = authorService;
            _urlHelper = urlHelper;
            _logger = loggerFactory.CreateLogger($"{this.GetType().Name}_Logger");
            
            _tableName = this.GetType().Name.Replace("Controller", "");
            _tableName = _tableName is "BookDetails" or "BookAuthors" or "BookGenres" ? _tableName : _tableName + "s";
        }


        /// <summary>
        /// Gets the list of all Authors
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// GET ef/author
        /// </remarks>
        /// <returns>Returns list of AuthorDTO</returns>
        /// <response code="200">Success</response>
        /// <response code="400">If invalid filtering data is entered</response>
        /// <response code="500">If it was not possible to get a list of elements from the database</response>
        [HttpGet(Name = nameof(GetAllAuthorsAsync))] // GET: ef/author
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<GetDTO_Author>>> GetAllAuthorsAsync
            ([FromQuery] AuthorParameters parameters)
        {
            try
            {
                var collection = (await _authorService.GetAllAsync(parameters)).ToList();
                _logger.LogInformation
                    ("{Count} entities were successfully extracted from [{Table}]", collection.Count, _tableName);

                collection.ForEach(item => this.CreateLinksForEntity(item)); // HATEOAS
                return Ok(collection);
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    "Error in [{ErrorClassName}]->[{MethodName}] => {ErrorMessage}", 
                    this.GetType().Name, nameof(GetAllAuthorsAsync), ex.Message);

                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Gets the Author by id
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// GET ef/author/id
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
        public async Task<ActionResult<GetDTO_Author>> GetAuthorByIdAsync(Guid id)
        {
            try
            {
                var entity = await _authorService.GetAsync(id);

                if (entity == null)
                {
                    _logger.LogError("Entity with id: [{EntityId}] from [{Table}] not found", id, _tableName);
                    return StatusCode(StatusCodes.Status404NotFound);
                }
                
                
                _logger.LogInformation
                    ("Entity with id: [{EntityId}] were successfully extracted from [{Table}]", id, _tableName);

                return Ok(this.CreateLinksForEntity(entity)); // HATEOAS
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    "Error in [{ErrorClassName}]->[{MethodName}] => {ErrorMessage}", 
                    this.GetType().Name, nameof(GetAuthorByIdAsync), ex.Message);

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
        ///         "authorId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
        ///         "firstName": "string",
        ///         "lastName": "string",
        ///         "birthdate": "2023-03-20T14:31:27.294Z",
        ///         "nationality": "string"
        ///     }
        /// </remarks>
        /// <param name="newEntity">InsertDTO_Author newEntity</param>
        /// <returns>Returns id (Guid)</returns>
        /// <response code="200">Success</response>
        /// <response code="400">If invalid data entered</response>
        /// <response code="500">If it was not possible to adding element to the database</response>
        [HttpPost(Name = nameof(AddAuthorAsync))] // POST: ef/author
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Guid>> AddAuthorAsync(InsertDTO_Author newEntity)
        {
            try
            {
                var id = await _authorService.CreateAsync(newEntity);
                _logger.LogInformation
                    ("Entity with id: [{EntityId}] were successfully added to [{Table}]", id, _tableName);

                return Ok(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    "Error in [{ErrorClassName}]->[{MethodName}] => {ErrorMessage}", 
                    this.GetType().Name, nameof(AddAuthorAsync), ex.Message);

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
        ///         "authorId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
        ///         "firstName": "string",
        ///         "lastName": "string",
        ///         "birthdate": "2023-03-20T14:31:27.294Z",
        ///         "nationality": "string"
        ///     }
        /// </remarks>
        /// <param name="updateDTO">AuthorDTO updateEntity</param>
        /// <returns>Returns NoContent</returns>
        /// <response code="204">Success</response>
        /// <response code="400">If invalid data entered</response>
        /// <response code="500">If it was not possible to adding element to the database</response>
        [HttpPut(Name = nameof(UpdateAuthorAsync))] // PUT: ef/author
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> UpdateAuthorAsync(UpdateDTO_Author updateDTO)
        {
            try
            {
                // Whether there is such a record in the database at all
                var findResult = await _authorService.GetAsync(updateDTO.AuthorId);

                if (findResult == null)
                {
                    _logger.LogError
                        ("Entity with id: [{EntityId}] from [{Table}] not found", updateDTO.AuthorId, _tableName);

                    return StatusCode(StatusCodes.Status404NotFound);
                }
                
                
                await _authorService.UpdateAsync(updateDTO);
                _logger.LogInformation
                    ("Entity with id: [{EntityId}] were successfully updated from [{Table}]", 
                        updateDTO.AuthorId, _tableName);

                return StatusCode(StatusCodes.Status204NoContent);
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    "Error in [{ErrorClassName}]->[{MethodName}] => {ErrorMessage}", 
                    this.GetType().Name, nameof(UpdateAuthorAsync), ex.Message);

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
                    _logger.LogError("Entity with id: [{EntityId}] from [{Table}] not found", id, _tableName);

                    return StatusCode(StatusCodes.Status404NotFound);
                }
                
                
                await _authorService.DeleteAsync(id);
                _logger.LogInformation
                    ("Entity with id: [{EntityId}] were successfully deleted from [{Table}]", id, _tableName);

                return StatusCode(StatusCodes.Status204NoContent);
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    "Error in [{ErrorClassName}]->[{MethodName}] => {ErrorMessage}", 
                    this.GetType().Name, nameof(DeleteAuthorAsync), ex.Message);

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
                    ("{Count} entities were successfully extracted from [{Table}]", collection.Count, _tableName);

                return Ok(collection);
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    "Error in [{ErrorClassName}]->[{MethodName}] => {ErrorMessage}", 
                    this.GetType().Name, nameof(GetAllAuthors_DataShaping_Async), ex.Message);

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
            (Guid id, [FromQuery] AuthorParameters parameters)
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
                _logger.LogError(
                    "Error in [{ErrorClassName}]->[{MethodName}] => {ErrorMessage}", 
                    this.GetType().Name, nameof(GetAuthorById_DataShaping_Async), ex.Message);

                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


        
        /// <summary>
        /// Gets author with highest avg book rating
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// GET ef/author/extension/author-with-highest-average-book-rating
        /// </remarks>
        /// <returns>Returns element of GetDTO_AuthorWithHighestAvgBookRating</returns>
        /// <response code="200">Success</response>
        /// <response code="500">If it was not possible to get element from the database</response>
        [HttpGet(
            "extension/author-with-highest-average-book-rating", 
            Name = nameof(GetAuthorWithHighestAvgBookRating_Async))] // GET: ef/author/extension/author-with-highest-average-book-rating
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> GetAuthorWithHighestAvgBookRating_Async()
        {
            try
            {
                var entity = await _authorService.GetAuthorWithHighestAvgBookRatingAsync();
                
                _logger.LogInformation
                    ("Entity were successfully extracted from [{Table}]", _tableName);

                return Ok(entity);
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    "Error in [{ErrorClassName}]->[{MethodName}] => {ErrorMessage}", 
                    this.GetType().Name, nameof(GetAuthorWithHighestAvgBookRating_Async), ex.Message);

                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }  
        }



        private GetDTO_Author CreateLinksForEntity(GetDTO_Author entity) // HATEOAS
        {
            var objId = new { id = entity.AuthorId };
             
            entity.Links.Add
                (new Link(this._urlHelper.Link(nameof(this.GetAuthorByIdAsync), objId)!, "self", "GET"));
            
            entity.Links.Add
                (new Link(this._urlHelper.Link(nameof(this.UpdateAuthorAsync), objId)!, "update_entity", "UPDATE"));
            
            entity.Links.Add
                (new Link(this._urlHelper.Link(nameof(this.DeleteAuthorAsync), objId)!, "delete_entity", "DELETE"));

            return entity;
        } 
    }
}
