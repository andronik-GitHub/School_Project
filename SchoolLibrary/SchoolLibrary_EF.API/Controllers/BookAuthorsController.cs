using Microsoft.AspNetCore.Mvc;
using SchoolLibrary_EF.BLL.DTO;
using SchoolLibrary_EF.BLL.Services.Contracts;
using SchoolLibrary_EF.DAL.Pagging.Entities;

namespace SchoolLibrary_EF.API.Controllers
{
    [Produces("application/json")]
    [Route("ef/[controller]")]
    [ApiController]
    public class BookAuthorsController : ControllerBase
    {
        private readonly IBookAuthorsService _bookAuthorsService;
        private readonly ILogger _logger;

        public BookAuthorsController(IBookAuthorsService bookAuthorsService, ILoggerFactory loggerFactory)
        {
            _bookAuthorsService = bookAuthorsService;
            _logger = loggerFactory.CreateLogger($"{this.GetType().Name}_Logger");
        }


        /// <summary>
        /// Gets the list of all BookAuthors
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// GET ef/bookauthors?PageNumber=5(amp)PageSize=10
        /// </remarks>
        /// <returns>Returns list of BookAuthorsDTO</returns>
        /// <response code="200">Success</response>
        /// <response code="500">If it was not possible to get a list of elements from the database</response>
        [HttpGet] // GET: ef/bookauthors?PageNumber=5&PageSize=10
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<BookAuthorsDTO>>> GetAllAsync([FromQuery] AuthorParameters parameters)
        {
            try
            {
                var collection = await _bookAuthorsService.GetAllAsync(parameters);
                _logger.LogInformation
                    ("{Count} entities were successfully extracted from [BookAuthors]", collection.Count());

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
        /// Gets the BookAuthors by id
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// GET ef/bookauthors/68c64e3b-7bc4-4ce2-b9c4-005783abb248/27402968-975b-47f7-8586-0dffdf32b78d
        /// </remarks>
        /// <param name="bookId">BookAuthors BookId (Guid)</param>
        /// <param name="authorId">BookAuthors AuthorId (Guid)</param>
        /// <returns>Returns element of BookAuthorsDTO</returns>
        /// <response code="200">Success</response>
        /// <response code="404">If the element with such ID is not found in the database</response>
        /// <response code="500">If it was not possible to get element from the database</response>
        [HttpGet("{bookId}/{authorId}")] // GET: ef/bookauthors/id
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<BookAuthorsDTO>> GetByIdAsync(Guid bookId, Guid authorId)
        {
            try
            {
                var entity = await _bookAuthorsService.GetByIdAsync(bookId, authorId);

                if (entity == null)
                {
                    _logger.LogError
                        ("Entity with id: [{FirstId}]-[{SecondId}] from [BookAuthors] not found", bookId, authorId);

                    return StatusCode(StatusCodes.Status404NotFound);
                    //return NotFound();
                }
                else
                {
                    _logger.LogInformation
                        ("Entity with id: [{FirstId}]-[{SecondId}] were successfully extracted from [BookAuthors]",
                        bookId, authorId);

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
        /// Creates new BookAuthors
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     POST: ef/bookauthors
        ///     {
        ///         "bookTitle": "string",
        ///         "authorFullName": "string string"
        ///     }
        /// </remarks>
        /// <param name="newBookAuthors">BookAuthorsDTO newEntity</param>
        /// <returns>Returns id (Guid, Guid)</returns>
        /// <response code="200">Success</response>
        /// <response code="400">If invalid data entered</response>
        /// <response code="409">If an existing object is adding</response>
        /// <response code="500">If it was not possible to adding element to the database</response>
        [HttpPost] // POST: ef/bookauthors
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<(Guid, Guid)>> AddAsync(BookAuthorsDTO newBookAuthors)
        {
            try
            {
                // Checking whether valid data has been entered
                if (newBookAuthors.BookTitle == null || newBookAuthors.AuthorFullName == null)
                {
                    _logger.LogError("Invalid data entered");

                    return StatusCode(StatusCodes.Status400BadRequest);
                    //return BadRequest("Invalid data entered");
                }
                else
                {
                    // Checking whether such an object does not already exist
                    var id = await _bookAuthorsService.CreateAsync(newBookAuthors);

                    if (id == null)
                    {
                        _logger.LogInformation
                            ("Entity with id: [{FirstId}]-[{SecondId}] may already exist in [BookAuthors]",
                                id?.Item1, id?.Item2);

                        return StatusCode(StatusCodes.Status409Conflict);
                    }
                    else
                    {
                        _logger.LogInformation
                            ("Entity with id: [{FirstId}]-[{SecondId}] were successfully added to [BookAuthors]",
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
        /// Update the BookAuthors
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     PUT: ef/bookauthors
        ///     {
        ///         "bookTitle": "string",
        ///         "authorFullName": "string string"
        ///     }
        /// </remarks>
        /// <param name="updateBookAuthors">BookAuthorsDTO updateEntity</param>
        /// <returns>Returns NoContent</returns>
        /// <response code="204">Success</response>
        /// <response code="400">If invalid data entered</response>
        /// <response code="500">If it was not possible to adding element to the database</response>
        [HttpPut] // PUT: ef/bookauthors
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> UpdateAsync(BookAuthorsDTO updateBookAuthors)
        {
            try
            {
                // Checking whether valid data has been entered
                if (updateBookAuthors.BookTitle == null || updateBookAuthors.AuthorFullName == null)
                {
                    _logger.LogError("Invalid data entered");

                    return StatusCode(StatusCodes.Status400BadRequest);
                    //return BadRequest("Invalid data entered");
                }
                else
                {
                    // Mapping dest->src and get ids
                    var item = _bookAuthorsService.GetIdsToOjbect(updateBookAuthors);
                    // Whether there is such a record in the database at all
                    var findResult = await _bookAuthorsService.GetByIdAsync(item.Result.Item1, item.Result.Item2);

                    if (findResult == null)
                    {
                        _logger.LogError
                            ("Entity: [{Entity}] from [BookAuthors] not found",
                                updateBookAuthors.ToString());

                        return StatusCode(StatusCodes.Status404NotFound);
                        //return NotFound();
                    }
                    else
                    {
                        await _bookAuthorsService.UpdateAsync(updateBookAuthors);
                        _logger.LogInformation
                            ("Entity: [{Entity}] were successfully updated from [BookAuthors]",
                                updateBookAuthors.ToString());

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
        [HttpDelete("{bookId}/{authorId}")] // DELETE: ef/bookauthors/id
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> DeleteAsync(Guid bookId, Guid authorId)
        {
            try
            {
                // Whether there is such a record in the database at all
                var findResult = await _bookAuthorsService.GetByIdAsync(bookId, authorId);

                if (findResult == null)
                {
                    _logger.LogError("Entity with id: [{FirstId}]-[{SecondId}] from [BookAuthors] not found",
                        bookId, authorId);

                    return StatusCode(StatusCodes.Status404NotFound);
                    //return NotFound();
                }
                else
                {
                    await _bookAuthorsService.DeleteAsync(bookId, authorId);
                    _logger.LogInformation
                        ("Entity with id: [{FirstId}]-[{SecondId}] were successfully deleted from [BookAuthors]",
                            bookId, authorId);

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
