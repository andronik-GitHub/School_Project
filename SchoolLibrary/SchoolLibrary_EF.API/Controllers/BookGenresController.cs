using Microsoft.AspNetCore.Mvc;
using SchoolLibrary_EF.BLL.DTO;
using SchoolLibrary_EF.BLL.Services.Contracts;
using SchoolLibrary_EF.DAL.Pagging;

namespace SchoolLibrary_EF.API.Controllers
{
    [Produces("application/json")]
    [Route("ef/[controller]")]
    [ApiController]
    public class BookGenresController : ControllerBase
    {
        private readonly IBookGenresService _bookGenresService;
        private readonly ILogger _logger;

        public BookGenresController(IBookGenresService bookGenresService, ILoggerFactory loggerFactory)
        {
            _bookGenresService = bookGenresService;
            _logger = loggerFactory.CreateLogger($"{this.GetType().Name}_Logger");
        }


        /// <summary>
        /// Gets the list of all BookGenres
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// GET ef/bookgenres?PageNumber=5(amp)PageSize=10
        /// </remarks>
        /// <returns>Returns list of BookGenresDTO</returns>
        /// <response code="200">Success</response>
        /// <response code="500">If it was not possible to get a list of elements from the database</response>
        [HttpGet] // GET: ef/bookgenres?PageNumber=5&PageSize=10
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<BookGenresDTO>>> GetAllAsync([FromQuery] AuthorParameters parameters)
        {
            try
            {
                var collection = await _bookGenresService.GetAllAsync(parameters);
                _logger.LogInformation
                    ("{Count} entities were successfully extracted from [BookGenres]", collection.Count());

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
        /// Gets the BookGenres by id
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// GET ef/bookgenres/68c64e3b-7bc4-4ce2-b9c4-005783abb248/27402968-975b-47f7-8586-0dffdf32b78d
        /// </remarks>
        /// <param name="bookId">BookGenres BookId (Guid)</param>
        /// <param name="genreId">BookGenres GenreId (Guid)</param>
        /// <returns>Returns element of BookGenresDTO</returns>
        /// <response code="200">Success</response>
        /// <response code="404">If the element with such ID is not found in the database</response>
        /// <response code="500">If it was not possible to get element from the database</response>
        [HttpGet("{bookId}/{genreId}")] // GET: ef/bookgenres/id
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<BookGenresDTO>> GetByIdAsync(Guid bookId, Guid genreId)
        {
            try
            {
                var entity = await _bookGenresService.GetByIdAsync(bookId, genreId);

                if (entity == null)
                {
                    _logger.LogError
                        ("Entity with id: [{FirstId}]-[{SecondId}] from [BookGenres] not found", bookId, genreId);

                    return StatusCode(StatusCodes.Status404NotFound);
                    //return NotFound();
                }
                else
                {
                    _logger.LogInformation
                        ("Entity with id: [{FirstId}]-[{SecondId}] were successfully extracted from [BookGenres]", 
                        bookId, genreId);

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
        /// Creates new BookGenres
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     POST: ef/bookgenres
        ///     {
        ///         "bookTitle": "string",
        ///         "genreName": "string"
        ///     }
        /// </remarks>
        /// <param name="newBookGenres">BookGenresDTO newEntity</param>
        /// <returns>Returns id (Guid, Guid)</returns>
        /// <response code="200">Success</response>
        /// <response code="400">If invalid data entered</response>
        /// <response code="409">If an existing object is adding</response>
        /// <response code="500">If it was not possible to adding element to the database</response>
        [HttpPost] // POST: ef/bookgenres
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<(Guid, Guid)>> AddAsync(BookGenresDTO newBookGenres)
        {
            try
            {
                // Checking whether valid data has been entered
                if (newBookGenres.BookTitle == null || newBookGenres.GenreName == null)
                {
                    _logger.LogError("Invalid data entered");

                    return StatusCode(StatusCodes.Status400BadRequest);
                    //return BadRequest("Invalid data entered");
                }
                else
                {
                    // Checking whether such an object does not already exist
                    var id = await _bookGenresService.CreateAsync(newBookGenres);
                    
                    if (id == null)
                    {
                        _logger.LogInformation
                            ("Entity with id: [{FirstId}]-[{SecondId}] may already exist in [BookGenres]",
                                id?.Item1, id?.Item2);

                        return StatusCode(StatusCodes.Status409Conflict);
                    }
                    else
                    {
                        _logger.LogInformation
                            ("Entity with id: [{FirstId}]-[{SecondId}] were successfully added to [BookGenres]",
                                id?.Item1, id?.Item2);

                        return Ok($"{id?.Item1}, {id?.Item2}");
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
        /// Update the BookGenres
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     PUT: ef/bookgenres
        ///     {
        ///         "bookTitle": "string",
        ///         "genreName": "string"
        ///     }
        /// </remarks>
        /// <param name="updateBookGenres">BookGenresDTO updateEntity</param>
        /// <returns>Returns NoContent</returns>
        /// <response code="204">Success</response>
        /// <response code="400">If invalid data entered</response>
        /// <response code="500">If it was not possible to adding element to the database</response>
        [HttpPut] // PUT: ef/bookgenres
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> UpdateAsync(BookGenresDTO updateBookGenres)
        {
            try
            {
                // Checking whether valid data has been entered
                if (updateBookGenres.BookTitle == null || updateBookGenres.GenreName == null)
                {
                    _logger.LogError("Invalid data entered");

                    return StatusCode(StatusCodes.Status400BadRequest);
                    //return BadRequest("Invalid data entered");
                }
                else
                {
                    // Mapping dest->src and get ids
                    var item = _bookGenresService.GetIdsToOjbect(updateBookGenres);
                    // Whether there is such a record in the database at all
                    var findResult = await _bookGenresService.GetByIdAsync(item.Result.Item1, item.Result.Item2);

                    if (findResult == null)
                    {
                        _logger.LogError
                            ("Entity: [{Entity}] from [BookGenres] not found",
                                updateBookGenres.ToString());

                        return StatusCode(StatusCodes.Status404NotFound);
                        //return NotFound();
                    }
                    else
                    {
                        await _bookGenresService.UpdateAsync(updateBookGenres);
                        _logger.LogInformation
                            ("Entity: [{Entity}] were successfully updated from [BookGenres]",
                                updateBookGenres.ToString());

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
        [HttpDelete("{bookId}/{genreId}")] // DELETE: ef/bookgenres/id
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> DeleteAsync(Guid bookId, Guid genreId)
        {
            try
            {
                // Whether there is such a record in the database at all
                var findResult = await _bookGenresService.GetByIdAsync(bookId, genreId);

                if (findResult == null)
                {
                    _logger.LogError("Entity with id: [{FirstId}]-[{SecondId}] from [BookGenres] not found", 
                        bookId, genreId);

                    return StatusCode(StatusCodes.Status404NotFound);
                    //return NotFound();
                }
                else
                {
                    await _bookGenresService.DeleteAsync(bookId, genreId);
                    _logger.LogInformation
                        ("Entity with id: [{FirstId}]-[{SecondId}] were successfully deleted from [BookGenres]",
                            bookId, genreId);

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
