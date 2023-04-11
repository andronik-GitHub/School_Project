using System.Dynamic;
using Microsoft.AspNetCore.Mvc;
using SchoolLibrary_EF.BLL.DTO;
using SchoolLibrary_EF.BLL.DTO.HATEOAS;
using SchoolLibrary_EF.BLL.Services.Contracts;
using SchoolLibrary_EF.DAL.Helper.Contracts;
using SchoolLibrary_EF.DAL.Paging.Entities;

namespace SchoolLibrary_EF.API.Controllers
{
    [Produces("application/json")]
    [Route("ef/[controller]")]
    [ApiController]
    public class BookDetailsController : ControllerBase
    {
        private readonly IBookDetailsService _bookDetailsService;
        private readonly ILogger _logger;
        private readonly IUrlHelper _urlHelper;

        public BookDetailsController(
            IBookDetailsService bookDetailsService,
            ILoggerFactory loggerFactory,
            IUrlHelper urlHelper)
        {
            _bookDetailsService = bookDetailsService;
            _urlHelper = urlHelper;
            _logger = loggerFactory.CreateLogger($"{this.GetType().Name}_Logger");
        }


        /// <summary>
        /// Gets the list of all BookDetails
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// GET ef/bookdetails?PageNumber=5(amp)PageSize=10
        /// </remarks>
        /// <returns>Returns list of BookDetailsDTO</returns>
        /// <response code="200">Success</response>
        /// <response code="500">If it was not possible to get a list of elements from the database</response>
        [HttpGet(Name = nameof(GetAllBookDetailsAsync))] // GET: ef/bookdetails?PageNumber=5&PageSize=10
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<BookDetailsDTO>>> GetAllBookDetailsAsync
            ([FromQuery] BookDetailsParameters parameters)
        {
            try
            {
                var collection = (await _bookDetailsService.GetAllAsync(parameters)).ToList();
                _logger.LogInformation
                    ("{Count} entities were successfully extracted from [BookDetails]", collection.Count);

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
        /// Gets the BookDetails by id
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// GET ef/bookdetails/13ce1333-7b7c-4395-8565-0474a6ad05ad
        /// </remarks>
        /// <param name="id">BookDetails id (Guid)</param>
        /// <returns>Returns element of BookDetailsDTO</returns>
        /// <response code="200">Success</response>
        /// <response code="404">If the element with such ID is not found in the database</response>
        /// <response code="500">If it was not possible to get element from the database</response>
        [HttpGet("{id:guid}", Name = nameof(GetBookDetailsByIdAsync))] // GET: ef/bookdetails/id
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<BookDetailsDTO>> GetBookDetailsByIdAsync(Guid id)
        {
            try
            {
                var entity = await _bookDetailsService.GetAsync(id);

                if (entity == null)
                {
                    _logger.LogError("Entity with id: [{EntityId}] from [BookDetails] not found", id);

                    return StatusCode(StatusCodes.Status404NotFound);
                    //return NotFound();
                }
                else
                {
                    _logger.LogInformation
                        ("Entity with id: [{EntityId}] were successfully extracted from [BookDetails]", id);

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
        /// Creates new BookDetails
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     POST: ef/bookdetails
        ///     {
        ///         "bookDetailId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
        ///         "pages": 0,
        ///         "language": "string",
        ///         "format": "string",
        ///         "bookTitle": "string",
        ///         "bookPublishingYear": 0
        ///     }
        /// </remarks>
        /// <param name="newBookDetails">BookDetailsDTO newEntity</param>
        /// <returns>Returns id (Guid)</returns>
        /// <response code="200">Success</response>
        /// <response code="400">If invalid data entered</response>
        /// <response code="500">If it was not possible to adding element to the database</response>
        [HttpPost(Name = nameof(AddBookDetailsAsync))] // POST: ef/bookdetails
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Guid>> AddBookDetailsAsync(BookDetailsDTO newBookDetails)
        {
            try
            {
                // Checking whether valid data has been entered
                if (newBookDetails.Language == null || newBookDetails.Format == null ||
                    newBookDetails.BookTitle == null)
                {
                    _logger.LogError("Invalid data entered");

                    return StatusCode(StatusCodes.Status400BadRequest);
                    //return BadRequest("Invalid data entered");
                }
                else
                {
                    var id = await _bookDetailsService.CreateAsync(newBookDetails);
                    _logger.LogInformation
                        ("Entity with id: [{EntityId}] were successfully added to [BookDetails]", id);

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
        /// Update the BookDetails
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     PUT: ef/bookdetails
        ///     {
        ///         "bookDetailId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
        ///         "pages": 0,
        ///         "language": "string",
        ///         "format": "string",
        ///         "bookTitle": "string",
        ///         "bookPublishingYear": 0
        ///     }
        /// </remarks>
        /// <param name="updateBookDetails">BookDetailsDTO updateEntity</param>
        /// <returns>Returns NoContent</returns>
        /// <response code="204">Success</response>
        /// <response code="400">If invalid data entered</response>
        /// <response code="500">If it was not possible to adding element to the database</response>
        [HttpPut(Name = nameof(UpdateBookDetailsAsync))] // PUT: ef/bookdetails
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> UpdateBookDetailsAsync(BookDetailsDTO updateBookDetails)
        {
            try
            {
                // Checking whether valid data has been entered
                if (updateBookDetails.Language == null || updateBookDetails.Format == null ||
                    updateBookDetails.BookTitle == null)
                {
                    _logger.LogError("Invalid data entered");

                    return StatusCode(StatusCodes.Status400BadRequest);
                    //return BadRequest("Invalid data entered");
                }
                else
                {
                    // Whether there is such a record in the database at all
                    var findResult = await _bookDetailsService.GetAsync(updateBookDetails.BookDetailId);

                    if (findResult == null)
                    {
                        _logger.LogError
                            ("Entity with id: [{EntityId}] from [BookDetails] not found",
                                updateBookDetails.BookDetailId);

                        return StatusCode(StatusCodes.Status404NotFound);
                        //return NotFound();
                    }
                    else
                    {
                        await _bookDetailsService.UpdateAsync(updateBookDetails);
                        _logger.LogInformation
                            ("Entity with id: [{EntityId}] were successfully updated from [BookDetails]",
                                updateBookDetails.BookDetailId);

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
        /// Delete the BookDetails by id
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// DELETE: ef/bookdetails/13ce1333-7b7c-4395-8565-0474a6ad05ad
        /// </remarks>
        /// <param name="id">BookDetails id (Guid)</param>
        /// <returns>Returns NoContent</returns>
        /// <response code="204">Success</response>
        /// <response code="400">If invalid data entered</response>
        /// <response code="500">If it was not possible to adding element to the database</response>
        [HttpDelete("{id:guid}", Name = nameof(DeleteBookDetailsAsync))] // DELETE: ef/bookdetails/id
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> DeleteBookDetailsAsync(Guid id)
        {
            try
            {
                // Whether there is such a record in the database at all
                var findResult = await _bookDetailsService.GetAsync(id);

                if (findResult == null)
                {
                    _logger.LogError("Entity with id: [{EntityId}] from [BookDetails] not found", id);

                    return StatusCode(StatusCodes.Status404NotFound);
                    //return NotFound();
                }
                else
                {
                    await _bookDetailsService.DeleteAsync(id);
                    _logger.LogInformation
                        ("Entity with id: [{EntityId}] were successfully deleted from [BookDetails]", id);

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
        /// Gets the list of all BookDetails
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// GET ef/bookdetails/datashaping?Fields=UserId%2C%20FirstName%2C%20LastName%2C%20Password
        /// </remarks>
        /// <returns>Returns list of ExpandoObject(BookDetails)</returns>
        /// <response code="200">Success</response>
        /// <response code="500">If it was not possible to get a list of elements from the database</response>
        [HttpGet("datashaping/", Name = nameof(GetAllBookDetails_DataShaping_Async))] // ef/bookdetails/datashaping?Fields=UserId%2C%20FirstName%2C%20LastName%2C%20Password
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> GetAllBookDetails_DataShaping_Async([FromQuery] BookDetailsParameters parameters)
        {
            try
            {
                var collection = await _bookDetailsService.GetAll_DataShaping_Async(parameters);
                _logger.LogInformation
                    ("{Count} entities were successfully extracted from [BookDetails]", collection.Count());

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
        /// Gets the BookDetails by id
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// GET ef/bookdetails/datashaping/b12c5ca7-ab3f-4d0c-bc58-0512bbb30e69?Fields=UserId%2C%20FirstName%2C%20Email
        /// </remarks>
        /// <param name="id">BookDetails id (Guid)</param>
        /// <param name="parameters">BookDetails parameters for sort/paging/... (BookDetailsParameters)</param>
        /// <returns>Returns element of ExpandoObject(BookDetails)</returns>
        /// <response code="200">Success</response>
        /// <response code="404">If the element with such ID is not found in the database</response>
        /// <response code="500">If it was not possible to get element from the database</response>
        [HttpGet("datashaping/{id:guid}", Name = nameof(GetBookDetailsById_DataShaping_Async))] // ef/bookdetails/datashaping/b12c5ca7-ab3f-4d0c-bc58-0512bbb30e69?Fields=UserId%2C%20FirstName%2C%20Email
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> GetBookDetailsById_DataShaping_Async
            (Guid id, [FromQuery] BookDetailsParameters? parameters)
        {
            try
            {
                var entity = await _bookDetailsService.GetById_DataShaping_Async(id, parameters);

                if (entity == default(ExpandoObject))
                {
                    _logger.LogError("BookDetails with id: {id}, hasn't been found in db.", id);
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
        



        private BookDetailsDTO CreateLinksForEntity(BookDetailsDTO entity) // HATEOAS
        {
            var idObj = new { id = entity.BookDetailId };
            
            entity.Links.Add(
                new Link(this._urlHelper.Link(nameof(this.GetBookDetailsByIdAsync), idObj)!,
                    "self",
                    "GET"));
            
            entity.Links.Add(
                new Link(this._urlHelper.Link(nameof(this.UpdateBookDetailsAsync), idObj)!,
                    "update_user",
                    "UPDATE"));
            
            entity.Links.Add(
                new Link(this._urlHelper.Link(nameof(this.DeleteBookDetailsAsync), idObj)!,
                    "delete_user",
                    "DELETE"));

            return entity;
        }
    }
}
