using Microsoft.AspNetCore.Mvc;
using SchoolLibrary_EF.BLL.DTOs.__HATEOAS;
using SchoolLibrary_EF.BLL.DTOs.BookGenreDTOs;
using SchoolLibrary_EF.BLL.Services.Contracts;
using SchoolLibrary_EF.DAL.Paging.Entities;

namespace SchoolLibrary_EF.API.Controllers
{
    [Produces("application/json")]
    [Route("ef/[controller]")]
    [ApiController]
    public class BookGenresController : ControllerBase
    {
        private readonly IBookGenresService _bookGenresService;
        private readonly ILogger _logger;
        private readonly IUrlHelper _urlHelper;
        private readonly string _tableName;

        public BookGenresController(
            IBookGenresService bookGenresService, 
            ILoggerFactory loggerFactory, 
            IUrlHelper urlHelper)
        {
            _bookGenresService = bookGenresService;
            _logger = loggerFactory.CreateLogger($"{this.GetType().Name}_Logger");
            _urlHelper = urlHelper;
            
            _tableName = this.GetType().Name.Replace("Controller", "");
            _tableName = _tableName is "BookDetails" or "BookAuthors" or "BookGenres" ? _tableName : _tableName + "s";
        }


        /// <summary>
        /// Gets the list of all BookGenres
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// GET ef/bookgenres
        /// </remarks>
        /// <returns>Returns list of GetDTO_BookGenres</returns>
        /// <response code="200">Success</response>
        /// <response code="500">If it was not possible to get a list of elements from the database</response>
        [HttpGet(Name = nameof(GetAllBookGenresAsync))] // GET: ef/bookgenres?PageNumber=5&PageSize=10
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<GetDTO_BookGenres>>> GetAllBookGenresAsync
            ([FromQuery] BookGenresParameters parameters)
        {
            try
            {
                var collection = (await _bookGenresService.GetAllAsync(parameters)).ToList();
                _logger.LogInformation
                    ("{Count} entities were successfully extracted from [{Table}]", collection.Count, _tableName);

                collection.ForEach(item => this.CreateLinksForEntity(item)); // HATEOAS
                return Ok(collection);
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    "Error in [{ErrorClassName}]->[{MethodName}] => {ErrorMessage}", 
                    this.GetType().Name, nameof(GetAllBookGenresAsync), ex.Message);

                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Gets the BookGenres by id
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// GET ef/bookgenres/68c64e3b-7bc4-4ce2-b9c4-005783abb248/27402968-975b-47f7-8586-0dffdf32b78d
        /// </remarks>
        /// <param name="bookId">BookGenres BookId (Guid)</param>
        /// <param name="genreId">BookGenres GenreId (Guid)</param>
        /// <returns>Returns element of GetDTO_BookGenres</returns>
        /// <response code="200">Success</response>
        /// <response code="404">If the element with such ID is not found in the database</response>
        /// <response code="500">If it was not possible to get element from the database</response>
        [HttpGet("{bookId:guid}/{genreId:guid}", Name = nameof(GetBookGenresByIdAsync))] // GET: ef/bookgenres/id
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<GetDTO_BookGenres>> GetBookGenresByIdAsync(Guid bookId, Guid genreId)
        {
            try
            {
                var entity = await _bookGenresService.GetAsync((bookId, genreId));

                if (entity == null)
                {
                    _logger.LogError
                        ("Entity with id: [{FirstId}]-[{SecondId}] from [{Table}] not found", 
                            bookId, genreId, _tableName);

                    return StatusCode(StatusCodes.Status404NotFound);
                }
                
                
                _logger.LogInformation
                    ("Entity with id: [{FirstId}]-[{SecondId}] were successfully extracted from [{Table}]", 
                    bookId, genreId, _tableName);

                return Ok(this.CreateLinksForEntity(entity)); // HATEOAS
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    "Error in [{ErrorClassName}]->[{MethodName}] => {ErrorMessage}", 
                    this.GetType().Name, nameof(GetBookGenresByIdAsync), ex.Message);

                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Creates new BookGenres
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     POST: ef/bookgenres
        ///     {
        ///         "bookId": "68c64e3b-7bc4-4ce2-b9c4-005783abb248",
        ///         "genreId": "68c64e3b-7bc4-4ce2-b9c4-005783abb248"
        ///     }
        /// </remarks>
        /// <param name="newEntity">InsertDTO_BookGenres newEntity</param>
        /// <returns>Returns id (Guid, Guid)</returns>
        /// <response code="200">Success</response>
        /// <response code="400">If invalid data entered</response>
        /// <response code="409">If an existing object is adding</response>
        /// <response code="500">If it was not possible to adding element to the database</response>
        [HttpPost(Name = nameof(AddBookGenresAsync))] // POST: ef/bookgenres
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<string>> AddBookGenresAsync(InsertDTO_BookGenres newEntity)
        {
            try
            {
                var id = await _bookGenresService.CreateAsync(newEntity);
                
                _logger.LogInformation
                    ("Entity with id: [{FirstId}]-[{SecondId}] were successfully added to [{Table}]",
                        id.Item1, id.Item2, _tableName);

                return Ok($"{id.Item1}/{id.Item2}");
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    "Error in [{ErrorClassName}]->[{MethodName}] => {ErrorMessage}", 
                    this.GetType().Name, nameof(AddBookGenresAsync), ex.Message);

                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Update the BookGenres
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     PUT: ef/bookgenres
        ///     {
        ///         "bookId": "68c64e3b-7bc4-4ce2-b9c4-005783abb248",
        ///         "genreId": "68c64e3b-7bc4-4ce2-b9c4-005783abb248"
        ///     }
        /// </remarks>
        /// <param name="updateEntity">UpdateDTO_BookGenres updateEntity</param>
        /// <returns>Returns NoContent</returns>
        /// <response code="204">Success</response>
        /// <response code="400">If invalid data entered</response>
        /// <response code="500">If it was not possible to adding element to the database</response>
        [HttpPut(Name = nameof(UpdateBookGenresAsync))] // PUT: ef/bookgenres
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> UpdateBookGenresAsync(UpdateDTO_BookGenres updateEntity)
        {
            try
            {
                // Whether there is such a record in the database at all
                var findResult = await _bookGenresService.GetAsync((updateEntity.BookId, updateEntity.GenreId));
                if (findResult == null)
                {
                    _logger.LogError(
                        "Entity with id: [{FirstId}]-[{SecondId}] from [{Table}] not found", 
                        updateEntity.BookId, updateEntity.GenreId, _tableName);

                    return StatusCode(StatusCodes.Status404NotFound);
                }
                
                
                await _bookGenresService.UpdateAsync(updateEntity);
                _logger.LogInformation(
                    "Entity with id: [{FirstId}]-[{SecondId}] were successfully updated from [{Table}]",
                    updateEntity.BookId, updateEntity.GenreId, _tableName);

                return StatusCode(StatusCodes.Status204NoContent);
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    "Error in [{ErrorClassName}]->[{MethodName}] => {ErrorMessage}", 
                    this.GetType().Name, nameof(UpdateBookGenresAsync), ex.Message);

                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Delete the BookGenres by id
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// DELETE: ef/bookgenres/68c64e3b-7bc4-4ce2-b9c4-005783abb248/27402968-975b-47f7-8586-0dffdf32b78d
        /// </remarks>
        /// <param name="bookId">BookGenres BookId (Guid)</param>
        /// <param name="genreId">BookGenres GenreId (Guid)</param>
        /// <returns>Returns NoContent</returns>
        /// <response code="204">Success</response>
        /// <response code="400">If invalid data entered</response>
        /// <response code="500">If it was not possible to adding element to the database</response>
        [HttpDelete("{bookId:guid}/{genreId:guid}", Name = nameof(DeleteBookGenresAsync))] // DELETE: ef/bookgenres/id
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> DeleteBookGenresAsync(Guid bookId, Guid genreId)
        {
            try
            {
                // Whether there is such a record in the database at all
                var findResult = await _bookGenresService.GetAsync((bookId, genreId));

                if (findResult == null)
                {
                    _logger.LogError("Entity with id: [{FirstId}]-[{SecondId}] from [{Table}] not found", 
                        bookId, genreId, _tableName);

                    return StatusCode(StatusCodes.Status404NotFound);
                }
                
                
                await _bookGenresService.DeleteAsync((bookId, genreId));
                _logger.LogInformation(
                    "Entity with id: [{FirstId}]-[{SecondId}] were successfully deleted from [{Table}]",
                    bookId, genreId, _tableName);

                return StatusCode(StatusCodes.Status204NoContent);
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    "Error in [{ErrorClassName}]->[{MethodName}] => {ErrorMessage}", 
                    this.GetType().Name, nameof(DeleteBookGenresAsync), ex.Message);

                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        
        
        /// <summary>
        /// Gets the list of all BookGenres
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// GET ef/bookgenres/datashaping?Fields=UserId%2C%20FirstName%2C%20LastName%2C%20Password
        /// </remarks>
        /// <returns>Returns list of ExpandoObject(BookGenres)</returns>
        /// <response code="200">Success</response>
        /// <response code="500">If it was not possible to get a list of elements from the database</response>
        [HttpGet("datashaping/", Name = nameof(GetAllBookGenres_DataShaping_Async))] // ef/bookgenres/datashaping?Fields=UserId%2C%20FirstName%2C%20LastName%2C%20Password
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> GetAllBookGenres_DataShaping_Async
            ([FromQuery] BookGenresParameters parameters)
        {
            try
            {
                var collection = await _bookGenresService.GetAll_DataShaping_Async(parameters);
                _logger.LogInformation
                    ("{Count} entities were successfully extracted from [{Table}]", collection.Count, _tableName);

                return Ok(collection);
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    "Error in [{ErrorClassName}]->[{MethodName}] => {ErrorMessage}", 
                    this.GetType().Name, nameof(GetAllBookGenres_DataShaping_Async), ex.Message);

                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        
        /// <summary>
        /// Gets the BookGenres by id
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// GET ef/bookgenres/datashaping/b12c5ca7-ab3f-4d0c-bc58-0512bbb30e69?Fields=UserId%2C%20FirstName%2C%20Email
        /// </remarks>
        /// <param name="bookId">BookGenres BookId (Guid)</param>
        /// <param name="genreId">BookGenres GenreId (Guid)</param>
        /// <param name="parameters">Book parameters for sort/paging/... (BookAuthorsParameters)</param>
        /// <returns>Returns element of ExpandoObject(Book)</returns>
        /// <response code="200">Success</response>
        /// <response code="404">If the element with such ID is not found in the database</response>
        /// <response code="500">If it was not possible to get element from the database</response>
        [HttpGet("datashaping/{bookId:guid}/{genreId:guid}", Name = nameof(GetBookGenreById_DataShaping_Async))] // ef/bookgenres/datashaping/bookId/genreId
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> GetBookGenreById_DataShaping_Async
            (Guid bookId, Guid genreId, [FromQuery] BookGenresParameters parameters)
        {
            try
            {
                var entity = await _bookGenresService.GetById_DataShaping_Async((bookId, genreId), parameters);
                if (entity == null)
                {
                    _logger.LogError(
                        "Entity with id: [{FirstId}]-[{SecondId}] from [{Table}] not found", 
                        bookId, genreId, _tableName);
                    return StatusCode(StatusCodes.Status404NotFound);
                }

                _logger.LogInformation(
                    "Entity with id: [{FirstId}]-[{SecondId}] were successfully extracted from [{Table}]",
                    bookId, genreId, _tableName);
                return Ok(entity);
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    "Error in [{ErrorClassName}]->[{MethodName}] => {ErrorMessage}", 
                    this.GetType().Name, nameof(GetBookGenreById_DataShaping_Async), ex.Message);

                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        



        private GetDTO_BookGenres CreateLinksForEntity(GetDTO_BookGenres entity) // HATEOAS
        {
            var objId = new { bookId = entity.BookId, genreId = entity.GenreId };
             
            entity.Links.Add
                (new Link(this._urlHelper.Link(nameof(this.GetBookGenresByIdAsync), objId)!, "self", "GET"));
            
            entity.Links.Add
                (new Link(this._urlHelper.Link(nameof(this.UpdateBookGenresAsync), null)!, "update_entity", "PUT"));
            
            entity.Links.Add
                (new Link(this._urlHelper.Link(nameof(this.DeleteBookGenresAsync), objId)!, "delete_entity", "DELETE"));

            return entity;
        } 
    }
}
