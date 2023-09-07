using Microsoft.AspNetCore.Mvc;
using SchoolLibrary_EF.BLL.DTOs.__HATEOAS;
using SchoolLibrary_EF.BLL.DTOs.BookDTOs;
using SchoolLibrary_EF.BLL.Services.Contracts;
using SchoolLibrary_EF.DAL.Paging.Entities;

namespace SchoolLibrary_EF.API.Controllers
{
    [Produces("application/json")]
    [Route("ef/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookService _bookService;
        private readonly ILogger _logger;
        private readonly IUrlHelper _urlHelper;
        private readonly string _tableName;

        public BookController(IBookService bookService, ILoggerFactory loggerFactory, IUrlHelper urlHelper)
        {
            _bookService = bookService;
            _urlHelper = urlHelper;
            _logger = loggerFactory.CreateLogger($"{this.GetType().Name}_Logger");
            
            _tableName = this.GetType().Name.Replace("Controller", "");
            _tableName = _tableName is "BookDetails" or "BookAuthors" or "BookGenres" ? _tableName : _tableName + "s";
        }


        /// <summary>
        /// Gets the list of all Books
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// GET ef/book
        /// </remarks>
        /// <returns>Returns list of GetDTO_Book</returns>
        /// <response code="200">Success</response>
        /// <response code="500">If it was not possible to get a list of elements from the database</response>
        [HttpGet(Name = nameof(GetAllBooksAsync))] // GET: ef/book?PageNumber=5&PageSize=10
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<GetDTO_Book>>> GetAllBooksAsync
            ([FromQuery] BookParameters parameters)
        {
            try
            {
                var collection = (await _bookService.GetAllAsync(parameters)).ToList();
                _logger.LogInformation
                    ("{Count} entities were successfully extracted from [{Table}]", collection.Count, _tableName);
            
                collection.ForEach(item => this.CreateLinksForEntity(item));
                return Ok(collection);
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    "Error in [{ErrorClassName}]->[{MethodName}] => {ErrorMessage}", 
                    this.GetType().Name, nameof(GetAllBooksAsync), ex.Message);

                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Gets the Book by id
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// GET ef/book/13ce1333-7b7c-4395-8565-0474a6ad05ad
        /// </remarks>
        /// <param name="id">Book id (Guid)</param>
        /// <returns>Returns element of GetDTO_Book</returns>
        /// <response code="200">Success</response>
        /// <response code="404">If the element with such ID is not found in the database</response>
        /// <response code="500">If it was not possible to get element from the database</response>
        [HttpGet("{id:guid}", Name = nameof(GetBookByIdAsync))] // GET: ef/book/id
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<GetDTO_Book>> GetBookByIdAsync(Guid id)
        {
            try
            {
                var entity = await _bookService.GetAsync(id);

                if (entity == null)
                {
                    _logger.LogError("Entity with id: [{EntityId}] from [{Table}] not found", id, _tableName);
                    return StatusCode(StatusCodes.Status404NotFound);
                }
                
                
                _logger.LogInformation
                    ("Entity with id: [{EntityId}] were successfully extracted from [{Table}]", id, _tableName);

                return Ok(this.CreateLinksForEntity(entity));
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    "Error in [{ErrorClassName}]->[{MethodName}] => {ErrorMessage}", 
                    this.GetType().Name, nameof(GetBookByIdAsync), ex.Message);

                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Creates new Book
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     POST: ef/book
        ///     {
        ///         "title": "string",
        ///         "publishingYear": 1990,
        ///         "isbn": "string",
        ///         "PublisherId": "3fa85f64-5717-4562-b3fc-2c963f66afa6"
        ///     }
        /// </remarks>
        /// <param name="newEntity">InsertDTO_Book newEntity</param>
        /// <returns>Returns id (Guid)</returns>
        /// <response code="200">Success</response>
        /// <response code="400">If invalid data entered</response>
        /// <response code="500">If it was not possible to adding element to the database</response>
        [HttpPost(Name = nameof(AddBookAsync))] // POST: ef/book
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Guid>> AddBookAsync(InsertDTO_Book newEntity)
        {
            try
            {
                // Checking whether valid data has been entered
                if (newEntity?.Title == null || newEntity?.ISBN == null)
                {
                    _logger.LogError("Invalid data entered");
                    return StatusCode(StatusCodes.Status400BadRequest);
                }
                
                
                var id = await _bookService.CreateAsync(newEntity);
                _logger.LogInformation
                    ("Entity with id: [{EntityId}] were successfully added to [{Table}]", id, _tableName);

                return Ok(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    "Error in [{ErrorClassName}]->[{MethodName}] => {ErrorMessage}", 
                    this.GetType().Name, nameof(AddBookAsync), ex.Message);

                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Update the Book
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     PUT: ef/book
        ///     {
        ///         "bookId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
        ///         "title": "string",
        ///         "publishingYear": 1990,
        ///         "isbn": "string",
        ///         "PublisherId": "3fa85f64-5717-4562-b3fc-2c963f66afa6"
        ///     }
        /// </remarks>
        /// <param name="updateEntity">UpdateDTO_Book updateEntity</param>
        /// <returns>Returns NoContent</returns>
        /// <response code="204">Success</response>
        /// <response code="400">If invalid data entered</response>
        /// <response code="500">If it was not possible to adding element to the database</response>
        [HttpPut(Name = nameof(UpdateBookAsync))] // PUT: ef/book
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> UpdateBookAsync(UpdateDTO_Book updateEntity)
        {
            try
            {
                // Checking whether valid data has been entered
                if (updateEntity?.Title == null || updateEntity?.ISBN == null)
                {
                    _logger.LogError("Invalid data entered");
                    return StatusCode(StatusCodes.Status400BadRequest);
                }
                
                
                // Whether there is such a record in the database at all
                var findResult = await _bookService.GetAsync(updateEntity.BookId);
                if (findResult == null)
                {
                    _logger.LogError
                        ("Entity with id: [{EntityId}] from [{Table}] not found", updateEntity.BookId, _tableName);

                    return StatusCode(StatusCodes.Status404NotFound);
                }
                
                
                await _bookService.UpdateAsync(updateEntity);
                _logger.LogInformation
                    ("Entity with id: [{EntityId}] were successfully updated from [{Table}]",
                        updateEntity.BookId, _tableName);

                return StatusCode(StatusCodes.Status204NoContent);
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    "Error in [{ErrorClassName}]->[{MethodName}] => {ErrorMessage}", 
                    this.GetType().Name, nameof(UpdateBookAsync), ex.Message);

                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Delete the Book by id
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// DELETE: ef/book/13ce1333-7b7c-4395-8565-0474a6ad05ad
        /// </remarks>
        /// <param name="id">Book id (Guid)</param>
        /// <returns>Returns NoContent</returns>
        /// <response code="204">Success</response>
        /// <response code="400">If invalid data entered</response>
        /// <response code="500">If it was not possible to adding element to the database</response>
        [HttpDelete("{id:guid}", Name = nameof(DeleteBookAsync))] // DELETE: ef/book/id
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> DeleteBookAsync(Guid id)
        {
            try
            {
                // Whether there is such a record in the database at all
                var findResult = await _bookService.GetAsync(id);

                if (findResult == null)
                {
                    _logger.LogError("Entity with id: [{EntityId}] from [{Table}] not found", id, _tableName);
                    return StatusCode(StatusCodes.Status404NotFound);
                }
                
                
                await _bookService.DeleteAsync(id);
                _logger.LogInformation
                    ("Entity with id: [{EntityId}] were successfully deleted from [{Table}]", id, _tableName);

                return StatusCode(StatusCodes.Status204NoContent);
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    "Error in [{ErrorClassName}]->[{MethodName}] => {ErrorMessage}", 
                    this.GetType().Name, nameof(DeleteBookAsync), ex.Message);

                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        

        /// <summary>
        /// Gets the list of all Books
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// GET ef/book/datashaping?Fields=UserId%2C%20FirstName%2C%20LastName%2C%20Password
        /// </remarks>
        /// <returns>Returns list of ExpandoObject(Book)</returns>
        /// <response code="200">Success</response>
        /// <response code="500">If it was not possible to get a list of elements from the database</response>
        [HttpGet("datashaping/", Name = nameof(GetAllBooks_DataShaping_Async))] // ef/book/datashaping?Fields=UserId%2C%20FirstName%2C%20LastName%2C%20Password
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> GetAllBooks_DataShaping_Async([FromQuery] BookParameters parameters)
        {
            try
            {
                var collection = await _bookService.GetAll_DataShaping_Async(parameters);
                _logger.LogInformation
                    ("{Count} entities were successfully extracted from [{Table}]", collection.Count, _tableName);

                return Ok(collection);
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    "Error in [{ErrorClassName}]->[{MethodName}] => {ErrorMessage}", 
                    this.GetType().Name, nameof(GetAllBooks_DataShaping_Async), ex.Message);

                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Gets the Book by id
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// GET ef/book/datashaping/b12c5ca7-ab3f-4d0c-bc58-0512bbb30e69?Fields=UserId%2C%20FirstName%2C%20Email
        /// </remarks>
        /// <param name="id">Book id (Guid)</param>
        /// <param name="parameters">Book parameters for sort/paging/... (BookParameters)</param>
        /// <returns>Returns element of ExpandoObject(Book)</returns>
        /// <response code="200">Success</response>
        /// <response code="404">If the element with such ID is not found in the database</response>
        /// <response code="500">If it was not possible to get element from the database</response>
        [HttpGet("datashaping/{id:guid}", Name = nameof(GetBookById_DataShaping_Async))] // ef/book/datashaping/id
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> GetBookById_DataShaping_Async(Guid id, [FromQuery] BookParameters parameters)
        {
            try
            {
                var entity = await _bookService.GetById_DataShaping_Async(id, parameters);

                if (entity == null)
                {
                    _logger.LogError("Entity with id: [{EntityId}] from [{Table}] not found", id, _tableName);
                    return StatusCode(StatusCodes.Status404NotFound);
                }

                _logger.LogInformation(
                    "Entity with id: [{FirstId}] were successfully extracted from [{Table}]",
                    id, _tableName);
                return Ok(entity);
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    "Error in [{ErrorClassName}]->[{MethodName}] => {ErrorMessage}", 
                    this.GetType().Name, nameof(GetBookById_DataShaping_Async), ex.Message);

                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }



        /// <summary>
        /// Gets the list of all Books with average rating
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// GET ef/book/extension/avg-rating-each-book
        /// </remarks>
        /// <returns>Returns list of GetDTO_AvgRatingBook</returns>
        /// <response code="200">Success</response>
        /// <response code="500">If it was not possible to get a list of elements from the database</response>
        [HttpGet("extension/avg-rating-each-book", Name = nameof(GetAverageRatingForEachBook_Async))]
        [ProducesResponseType(StatusCodes.Status200OK)] // ef/book/extension/avg-rating-each-book
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> GetAverageRatingForEachBook_Async([FromQuery] BookParameters parameters)
        {
            try
            {
                var collection = (await _bookService.GetAvgRatingForBookAsync(parameters)).ToList();
                
                _logger.LogInformation
                    ("{Count} entities were successfully extracted from [{Table}]", collection.Count, _tableName);
                
                return Ok(collection);
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    "Error in [{ErrorClassName}]->[{MethodName}] => {ErrorMessage}", 
                    this.GetType().Name, nameof(GetAverageRatingForEachBook_Async), ex.Message);

                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }



        private GetDTO_Book CreateLinksForEntity(GetDTO_Book entity)
        {
            var idObj = new { id = entity.BookId };
            
            entity.Links.Add(
                new Link(this._urlHelper.Link(nameof(this.GetBookByIdAsync), idObj)!,
                    "self",
                    "GET"));
            
            entity.Links.Add(
                new Link(this._urlHelper.Link(nameof(this.UpdateBookAsync), idObj)!,
                    "update_user",
                    "UPDATE"));
            
            entity.Links.Add(
                new Link(this._urlHelper.Link(nameof(this.DeleteBookAsync), idObj)!,
                    "delete_user",
                    "DELETE"));

            return entity;
        } 
    }
}
