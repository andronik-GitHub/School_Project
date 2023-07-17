using Microsoft.AspNetCore.Mvc;
using SchoolLibrary_EF.BLL.DTOs.BookAuthorDTOs;
using SchoolLibrary_EF.BLL.Services.Contracts;
using SchoolLibrary_EF.DAL.Paging.Entities;

namespace SchoolLibrary_EF.API.Controllers
{
    [Produces("application/json")]
    [Route("ef/[controller]")]
    [ApiController]
    public class BookAuthorsController : ControllerBase
    {
        private readonly IBookAuthorsService _bookAuthorsService;
        private readonly ILogger _logger;
        private readonly string _tableName;

        public BookAuthorsController(IBookAuthorsService bookAuthorsService, ILoggerFactory loggerFactory)
        {
            _bookAuthorsService = bookAuthorsService;
            _logger = loggerFactory.CreateLogger($"{this.GetType().Name}_Logger");
            
            _tableName = this.GetType().Name.Replace("Controller", "");
            _tableName = _tableName is "BookDetails" or "BookAuthors" or "BookGenres" ? _tableName : _tableName + "s";
        }


        /// <summary>
        /// Gets the list of all BookAuthors
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// GET ef/bookauthors?PageNumber=5(amp)PageSize=10
        /// </remarks>
        /// <returns>Returns list of GetDTO_BookAuthors</returns>
        /// <response code="200">Success</response>
        /// <response code="500">If it was not possible to get a list of elements from the database</response>
        [HttpGet(Name = nameof(GetAllBookAuthorsAsync))] // GET: ef/bookauthors?PageNumber=5&PageSize=10
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<GetDTO_BookAuthors>>> GetAllBookAuthorsAsync
            ([FromQuery] AuthorParameters parameters)
        {
            try
            {
                var collection = await _bookAuthorsService.GetAllAsync(parameters);
                _logger.LogInformation
                    ("{Count} entities were successfully extracted from [{Table}]", collection.Count(), _tableName);

                return Ok(collection);
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    "Error in [{ErrorClassName}]->[{MethodName}] => {ErrorMessage}", 
                    this.GetType().Name, nameof(GetAllBookAuthorsAsync), ex.Message);

                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Gets the BookAuthors by id
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// GET ef/bookauthors/68c64e3b-7bc4-4ce2-b9c4-005783abb248/27402968-975b-47f7-8586-0dffdf32b78d
        /// </remarks>
        /// <param name="bookId">BookAuthors BookId (Guid)</param>
        /// <param name="authorId">BookAuthors AuthorId (Guid)</param>
        /// <returns>Returns element of GetDTO_BookAuthors</returns>
        /// <response code="200">Success</response>
        /// <response code="404">If the element with such ID is not found in the database</response>
        /// <response code="500">If it was not possible to get element from the database</response>
        [HttpGet("{bookId:guid}/{authorId:guid}", Name = nameof(GetBookAuthorsByIdAsync))] // GET: ef/bookauthors/bookId/authorId
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<GetDTO_BookAuthors>> GetBookAuthorsByIdAsync(Guid bookId, Guid authorId)
        {
            try
            {
                var entity = await _bookAuthorsService.GetAsync((bookId, authorId));

                if (entity == null)
                {
                    _logger.LogError
                        ("Entity with id: [{FirstId}]-[{SecondId}] from [{Table}] not found", 
                        bookId, authorId, _tableName);

                    return StatusCode(StatusCodes.Status404NotFound);
                }
                
                
                _logger.LogInformation
                    ("Entity with id: [{FirstId}]-[{SecondId}] were successfully extracted from [{Table}]",
                    bookId, authorId, _tableName);

                return Ok(entity);
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    "Error in [{ErrorClassName}]->[{MethodName}] => {ErrorMessage}", 
                    this.GetType().Name, nameof(GetBookAuthorsByIdAsync), ex.Message);

                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Creates new BookAuthors
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     POST: ef/bookauthors
        ///     {
        ///         "bookId": 68c64e3b-7bc4-4ce2-b9c4-005783abb248,
        ///         "authorId": 68c64e3b-7bc4-4ce2-b9c4-005783abb248
        ///     }
        /// </remarks>
        /// <param name="newEntity">InsertDTO_BookAuthors newEntity</param>
        /// <returns>Returns ids (Guid, Guid)</returns>
        /// <response code="200">Success</response>
        /// <response code="400">If invalid data entered</response>
        /// <response code="409">If an existing object is adding</response>
        /// <response code="500">If it was not possible to adding element to the database</response>
        [HttpPost(Name = nameof(AddBookAuthorsAsync))] // POST: ef/bookauthors
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<string>> AddBookAuthorsAsync(InsertDTO_BookAuthors newEntity)
        {
            try
            {
                var id = await _bookAuthorsService.CreateAsync(newEntity);

                _logger.LogInformation
                    ("Entity with id: [{FirstId}]-[{SecondId}] were successfully added to [{Table}]",
                        id.Item1, id.Item2, _tableName);

                return Ok($"{id.Item1}, {id.Item2}");
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    "Error in [{ErrorClassName}]->[{MethodName}] => {ErrorMessage}", 
                    this.GetType().Name, nameof(AddBookAuthorsAsync), ex.Message);

                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Update the BookAuthors
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     PUT: ef/bookauthors
        ///     {
        ///         "bookId": 68c64e3b-7bc4-4ce2-b9c4-005783abb248,
        ///         "authorId": 68c64e3b-7bc4-4ce2-b9c4-005783abb248
        ///     }
        /// </remarks>
        /// <param name="updateEntity">UpdateDTO_BookAuthors updateEntity</param>
        /// <returns>Returns NoContent</returns>
        /// <response code="204">Success</response>
        /// <response code="400">If invalid data entered</response>
        /// <response code="500">If it was not possible to adding element to the database</response>
        [HttpPut(Name = nameof(UpdateBookAuthorsAsync))] // PUT: ef/bookauthors
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> UpdateBookAuthorsAsync(UpdateDTO_BookAuthors updateEntity)
        {
            try
            {
                var findResult = await _bookAuthorsService.GetAsync((updateEntity.BookId, updateEntity.AuthorId));

                if (findResult == null)
                {
                    _logger.LogError
                    ("Entity with id: [{FirstId}]-[{SecondId}] from [{Table}] not found", 
                        updateEntity.BookId, updateEntity.AuthorId, _tableName);

                    return StatusCode(StatusCodes.Status404NotFound);
                }
                
                await _bookAuthorsService.UpdateAsync(updateEntity);
                _logger.LogInformation
                    ("Entity with id: [{FirstId}]-[{SecondId}] were successfully updated from [{Table}]",
                        updateEntity.BookId, updateEntity.AuthorId, _tableName);

                return StatusCode(StatusCodes.Status204NoContent);
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    "Error in [{ErrorClassName}]->[{MethodName}] => {ErrorMessage}", 
                    this.GetType().Name, nameof(UpdateBookAuthorsAsync), ex.Message);

                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Delete the BookAuthors by id
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// DELETE: ef/bookauthors/68c64e3b-7bc4-4ce2-b9c4-005783abb248/27402968-975b-47f7-8586-0dffdf32b78d
        /// </remarks>
        /// <param name="bookId">BookAuthors BookId (Guid)</param>
        /// <param name="authorId">BookAuthors AuthorId (Guid)</param>
        /// <returns>Returns NoContent</returns>
        /// <response code="204">Success</response>
        /// <response code="400">If invalid data entered</response>
        /// <response code="500">If it was not possible to adding element to the database</response>
        [HttpDelete("{bookId:guid}/{authorId:guid}", Name = nameof(DeleteBookAuthorsAsync))] // DELETE: ef/bookauthors/bookId/authorId
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> DeleteBookAuthorsAsync(Guid bookId, Guid authorId)
        {
            try
            {
                var findResult = await _bookAuthorsService.GetAsync((bookId, authorId));

                if (findResult == null)
                {
                    _logger.LogError("Entity with id: [{FirstId}]-[{SecondId}] from [{Table}] not found",
                        bookId, authorId, _tableName);

                    return StatusCode(StatusCodes.Status404NotFound);
                }
                
                
                await _bookAuthorsService.DeleteAsync((bookId, authorId));
                _logger.LogInformation
                    ("Entity with id: [{FirstId}]-[{SecondId}] were successfully deleted from [{Table}]",
                        bookId, authorId, _tableName);

                return StatusCode(StatusCodes.Status204NoContent);
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    "Error in [{ErrorClassName}]->[{MethodName}] => {ErrorMessage}", 
                    this.GetType().Name, nameof(DeleteBookAuthorsAsync), ex.Message);

                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        
        
        /// <summary>
        /// Gets the list of all BookAuthors
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// GET ef/bookauthors/datashaping?Fields=UserId%2C%20FirstName%2C%20LastName%2C%20Password
        /// </remarks>
        /// <returns>Returns list of ExpandoObject(BookAuthors)</returns>
        /// <response code="200">Success</response>
        /// <response code="500">If it was not possible to get a list of elements from the database</response>
        [HttpGet("datashaping/", Name = nameof(GetAllBookAuthors_DataShaping_Async))] // ef/bookauthors/datashaping?Fields=UserId%2C%20FirstName%2C%20LastName%2C%20Password
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> GetAllBookAuthors_DataShaping_Async([FromQuery] BookAuthorsParameters parameters)
        {
            try
            {
                var collection = await _bookAuthorsService.GetAll_DataShaping_Async(parameters);
                _logger.LogInformation
                    ("{Count} entities were successfully extracted from [{Table}]", collection.Count, _tableName);

                return Ok(collection);
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    "Error in [{ErrorClassName}]->[{MethodName}] => {ErrorMessage}", 
                    this.GetType().Name, nameof(GetAllBookAuthors_DataShaping_Async), ex.Message);

                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        
        /// <summary>
        /// Gets the BookAuthors by id
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// GET ef/bookauthors/datashaping/b12c5ca7-ab3f-4d0c-bc58-0512bbb30e69?Fields=UserId%2C%20FirstName%2C%20Email
        /// </remarks>
        /// <param name="bookId">BookAuthors BookId (Guid)</param>
        /// <param name="authorId">BookAuthors AuthorId (Guid)</param>
        /// <param name="parameters">Book parameters for sort/paging/... (BookAuthorsParameters)</param>
        /// <returns>Returns element of ExpandoObject(Book)</returns>
        /// <response code="200">Success</response>
        /// <response code="404">If the element with such ID is not found in the database</response>
        /// <response code="500">If it was not possible to get element from the database</response>
        [HttpGet("datashaping/{bookId:guid}/{authorId:guid}", Name = nameof(BookAuthors_DataShaping_Async))] // ef/bookauthors/datashaping/bookId/authorId
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> BookAuthors_DataShaping_Async
            (Guid bookId, Guid authorId, [FromQuery] BookAuthorsParameters parameters)
        {
            try
            {
                var entity = await _bookAuthorsService.GetById_DataShaping_Async((bookId, authorId), parameters);

                if (entity == null)
                {
                    _logger.LogError
                    ("Entity with id: [{FirstId}]-[{SecondId}] from [{Table}] not found", 
                        bookId, authorId, _tableName);
                    return StatusCode(StatusCodes.Status404NotFound);
                }

                _logger.LogInformation(
                    "Entity with id: [{FirstId}]-[{SecondId}] were successfully extracted from [{Table}]",
                    bookId, authorId, _tableName);
                return Ok(entity);
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    "Error in [{ErrorClassName}]->[{MethodName}] => {ErrorMessage}", 
                    this.GetType().Name, nameof(BookAuthors_DataShaping_Async), ex.Message);

                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

    }
}
