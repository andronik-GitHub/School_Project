using System.Dynamic;
using Microsoft.AspNetCore.Mvc;
using SchoolLibrary_EF.BLL.DTOs.__HATEOAS;
using SchoolLibrary_EF.BLL.DTOs.GenreDTOs;
using SchoolLibrary_EF.BLL.Services.Contracts;
using SchoolLibrary_EF.DAL.Paging.Entities;

namespace SchoolLibrary_EF.API.Controllers
{
    [Produces("application/json")]
    [Route("ef/[controller]")]
    [ApiController]
    public class GenreController : ControllerBase
    {
        private readonly IGenreService _genreService;
        private readonly ILogger _logger;
        private readonly IUrlHelper _urlHelper;
        private readonly string _tableName;

        public GenreController(IGenreService genreService, ILoggerFactory loggerFactory, IUrlHelper urlHelper)
        {
            _genreService = genreService;
            _urlHelper = urlHelper;
            _logger = loggerFactory.CreateLogger($"{this.GetType().Name}_Logger");
            
            _tableName = this.GetType().Name.Replace("Controller", "");
            _tableName = _tableName is "BookDetails" or "BookAuthors" or "BookGenres" ? _tableName : _tableName + "s";
        }


        /// <summary>
        /// Gets the list of all Genres
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// GET ef/genre
        /// </remarks>
        /// <returns>Returns list of GetDTO_Genre</returns>
        /// <response code="200">Success</response>
        /// <response code="500">If it was not possible to get a list of elements from the database</response>
        [HttpGet(Name = nameof(GetAllGenresAsync))] // GET: ef/genre?PageNumber=5&PageSize=10
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<GetDTO_Genre>>> GetAllGenresAsync
            ([FromQuery] GenreParameters parameters)
        {
            try
            {
                var collection = (await _genreService.GetAllAsync(parameters)).ToList();
                _logger.LogInformation
                    ("{Count} entities were successfully extracted from [{Table}]", collection.Count, _tableName);

                collection.ForEach(item => this.CreateLinksForEntity(item)); // HATEOAS
                return Ok(collection);
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    "Error in [{ErrorClassName}]->[{MethodName}] => {ErrorMessage}", 
                    this.GetType().Name, nameof(GetAllGenresAsync), ex.Message);

                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Gets the Genre by id
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// GET ef/genre/13ce1333-7b7c-4395-8565-0474a6ad05ad
        /// </remarks>
        /// <param name="id">Genre id (Guid)</param>
        /// <returns>Returns element of GetDTO_Genre</returns>
        /// <response code="200">Success</response>
        /// <response code="404">If the element with such ID is not found in the database</response>
        /// <response code="500">If it was not possible to get element from the database</response>
        [HttpGet("{id:guid}", Name = nameof(GetGenreByIdAsync))] // GET: ef/genre/id
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<GetDTO_Genre>> GetGenreByIdAsync(Guid id)
        {
            try
            {
                var entity = await _genreService.GetAsync(id);
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
                    this.GetType().Name, nameof(GetGenreByIdAsync), ex.Message);

                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Creates new Genre
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     POST: ef/genre
        ///     {
        ///         "name": "string",
        ///     }
        /// </remarks>
        /// <param name="newEntity">InsertDTO_Genre newEntity</param>
        /// <returns>Returns id (Guid)</returns>
        /// <response code="200">Success</response>
        /// <response code="400">If invalid data entered</response>
        /// <response code="500">If it was not possible to adding element to the database</response>
        [HttpPost(Name = nameof(AddGenreAsync))] // POST: ef/genre
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Guid>> AddGenreAsync(InsertDTO_Genre newEntity)
        {
            try
            {
                // Checking whether valid data has been entered
                if (newEntity?.Name == null)
                {
                    _logger.LogError("Invalid data entered");
                    return StatusCode(StatusCodes.Status400BadRequest);
                }
                
                
                var id = await _genreService.CreateAsync(newEntity);
                _logger.LogInformation
                    ("Entity with id: [{EntityId}] were successfully added to [{Table}]", id, _tableName);

                return Ok(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    "Error in [{ErrorClassName}]->[{MethodName}] => {ErrorMessage}", 
                    this.GetType().Name, nameof(AddGenreAsync), ex.Message);

                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Update the Genre
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     PUT: ef/genre
        ///     {
        ///         "genreId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
        ///         "name": "string",
        ///     }
        /// </remarks>
        /// <param name="updateEntity">UpdateDTO_Genre updateEntity</param>
        /// <returns>Returns NoContent</returns>
        /// <response code="204">Success</response>
        /// <response code="400">If invalid data entered</response>
        /// <response code="500">If it was not possible to adding element to the database</response>
        [HttpPut(Name = nameof(UpdateGenreAsync))] // PUT: ef/genre
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> UpdateGenreAsync(UpdateDTO_Genre updateEntity)
        {
            try
            {
                // Checking whether valid data has been entered
                if (updateEntity?.Name == null)
                {
                    _logger.LogError("Invalid data entered");
                    return StatusCode(StatusCodes.Status400BadRequest);
                }
                
                
                // Whether there is such a record in the database at all
                var findResult = await _genreService.GetAsync(updateEntity.GenreId);
                if (findResult == null)
                {
                    _logger.LogError
                        ("Entity with id: [{EntityId}] from [{Table}] not found", updateEntity.GenreId, _tableName);

                    return StatusCode(StatusCodes.Status404NotFound);
                }
                
                
                await _genreService.UpdateAsync(updateEntity);
                _logger.LogInformation(
                    "Entity with id: [{EntityId}] were successfully updated from [{Table}]",
                    updateEntity.GenreId, _tableName);

                return StatusCode(StatusCodes.Status204NoContent);
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    "Error in [{ErrorClassName}]->[{MethodName}] => {ErrorMessage}", 
                    this.GetType().Name, nameof(UpdateGenreAsync), ex.Message);

                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Delete the Genre by id
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// DELETE: ef/genre/13ce1333-7b7c-4395-8565-0474a6ad05ad
        /// </remarks>
        /// <param name="id">Genre id (Guid)</param>
        /// <returns>Returns NoContent</returns>
        /// <response code="204">Success</response>
        /// <response code="400">If invalid data entered</response>
        /// <response code="500">If it was not possible to adding element to the database</response>
        [HttpDelete("{id:guid}", Name = nameof(DeleteGenreAsync))] // DELETE: ef/genre/id
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> DeleteGenreAsync(Guid id)
        {
            try
            {
                // Whether there is such a record in the database at all
                var findResult = await _genreService.GetAsync(id);
                if (findResult == null)
                {
                    _logger.LogError("Entity with id: [{EntityId}] from [{Table}] not found", id, _tableName);
                    return StatusCode(StatusCodes.Status404NotFound);
                }
                
                
                await _genreService.DeleteAsync(id);
                _logger.LogInformation
                    ("Entity with id: [{EntityId}] were successfully deleted from [{Table}]", id, _tableName);

                return StatusCode(StatusCodes.Status204NoContent);
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    "Error in [{ErrorClassName}]->[{MethodName}] => {ErrorMessage}", 
                    this.GetType().Name, nameof(DeleteGenreAsync), ex.Message);

                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        

        /// <summary>
        /// Gets the list of all Genres
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// GET ef/genre/datashaping?Fields=UserId%2C%20FirstName%2C%20LastName%2C%20Password
        /// </remarks>
        /// <returns>Returns list of ExpandoObject(Genre)</returns>
        /// <response code="200">Success</response>
        /// <response code="500">If it was not possible to get a list of elements from the database</response>
        [HttpGet("datashaping/", Name = nameof(GetAllGenres_DataShaping_Async))]
        [ProducesResponseType(StatusCodes.Status200OK)] // ef/genre/datashaping?Fields=UserId%2C%20FirstName%2C%20LastName%2C%20Password
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> GetAllGenres_DataShaping_Async([FromQuery] GenreParameters parameters)
        {
            try
            {
                var collection = await _genreService.GetAll_DataShaping_Async(parameters);
                _logger.LogInformation
                    ("{Count} entities were successfully extracted from [{Table}]", collection.Count, _tableName);

                return Ok(collection);
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    "Error in [{ErrorClassName}]->[{MethodName}] => {ErrorMessage}", 
                    this.GetType().Name, nameof(GetAllGenres_DataShaping_Async), ex.Message);

                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Gets the Genre by id
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// GET ef/genre/datashaping/b12c5ca7-ab3f-4d0c-bc58-0512bbb30e69?Fields=UserId%2C%20FirstName%2C%20Email
        /// </remarks>
        /// <param name="id">Genre id (Guid)</param>
        /// <param name="parameters">Genre parameters for sort/paging/... (GenreParameters)</param>
        /// <returns>Returns element of ExpandoObject(Genre)</returns>
        /// <response code="200">Success</response>
        /// <response code="404">If the element with such ID is not found in the database</response>
        /// <response code="500">If it was not possible to get element from the database</response>
        [HttpGet("datashaping/{id:guid}", Name = nameof(GetGenreById_DataShaping_Async))]
        [ProducesResponseType(StatusCodes.Status200OK)] // ef/genre/datashaping/id?Fields=UserId%2C%20FirstName%2C%20Email
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> GetGenreById_DataShaping_Async
            (Guid id, [FromQuery] GenreParameters parameters)
        {
            try
            {
                var entity = await _genreService.GetById_DataShaping_Async(id, parameters);

                if (entity == default(ExpandoObject))
                {
                    _logger.LogError("Entity with id: [{EntityId}] from [{Table}] not found", id, _tableName);
                    return StatusCode(StatusCodes.Status404NotFound);
                }

                return Ok(entity);
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    "Error in [{ErrorClassName}]->[{MethodName}] => {ErrorMessage}", 
                    this.GetType().Name, nameof(GetGenreById_DataShaping_Async), ex.Message);

                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


        
        /// <summary>
        /// Gets count the number of books of each genre
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// GET ef/book/extension/count-of-book-each-genre
        /// </remarks>
        /// <returns>Returns list of GetDTO_CountOfBooksEachGenre</returns>
        /// <response code="200">Success</response>
        /// <response code="500">If it was not possible to get a list of elements from the database</response>
        [HttpGet("extension/count-of-book-each-genre", Name = nameof(GetCountOfBooksEachGenre_Async))]
        [ProducesResponseType(StatusCodes.Status200OK)] // ef/genre/count-of-book-each-genre
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> GetCountOfBooksEachGenre_Async([FromQuery] GenreParameters parameters)
        {
            try
            {
                var collection = (await _genreService.GetCountOfBooksEachGenreAsync(parameters)).ToList();
                
                _logger.LogInformation
                    ("{Count} entities were successfully extracted from [{Table}]", collection.Count, _tableName);
                
                return Ok(collection);
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    "Error in [{ErrorClassName}]->[{MethodName}] => {ErrorMessage}", 
                    this.GetType().Name, nameof(GetCountOfBooksEachGenre_Async), ex.Message);

                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }



        private GetDTO_Genre CreateLinksForEntity(GetDTO_Genre entity) // HATEOAS
        {
            var idObj = new { id = entity.GenreId };
            
            entity.Links.Add(
                new Link(this._urlHelper.Link(nameof(this.GetGenreByIdAsync), idObj)!,
                    "self",
                    "GET"));
            
            entity.Links.Add(
                new Link(this._urlHelper.Link(nameof(this.UpdateGenreAsync), idObj)!,
                    "update_user",
                    "UPDATE"));
            
            entity.Links.Add(
                new Link(this._urlHelper.Link(nameof(this.DeleteGenreAsync), idObj)!,
                    "delete_user",
                    "DELETE"));

            return entity;
        } 
    }
}
