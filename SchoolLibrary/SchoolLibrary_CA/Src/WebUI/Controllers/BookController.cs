using System.Text.Json;
using System.Text.Json.Serialization;
using Application.Common.Pagging.Entities;
using Application.Features.BookFeatures.Commands.CreateBook;
using Application.Features.BookFeatures.Commands.DeleteBook;
using Application.Features.BookFeatures.Commands.UpdateBook;
using Application.Features.BookFeatures.Queries.GetAllBooks;
using Application.Features.BookFeatures.Queries.GetAllBooks_DataShaping;
using Application.Features.BookFeatures.Queries.GetAvgRatingForBook;
using Application.Features.BookFeatures.Queries.GetBook;
using Application.Features.BookFeatures.Queries.GetBook_DataShaping;
using Application.Features.BookFeatures.Queries.GetBooksWithoutReviews;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Controllers
{
    /// <summary>
    /// BookController
    /// </summary>
    public class BookController : BaseController
    {
        /// <summary>
        /// BookController constructor for initialisation ILogger and IUrlHelper
        /// </summary>
        /// <param name="loggerFactory">ILoggerFactory</param>
        /// <param name="urlHelper">IUrlHelper</param>
        public BookController(ILoggerFactory loggerFactory, IUrlHelper urlHelper) 
            : base(loggerFactory, urlHelper)
        {
        }


        /// <summary>
        /// Get list of Books
        /// </summary>
        /// <returns>Returns list of Books</returns>
        [HttpGet(Name = nameof(GetAllBookAsync))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> GetAllBookAsync([FromQuery] BookParameter parameters)
        {
            var list = (await Mediator.Send(new GetAllBooksQuery(parameters))).ToList();
            
            _logger.LogInformation(
                "{Count} entities were successfully extracted from [{Table}]", 
                list.Count, 
                this.GetType().Name.Substring(0, this.GetType().Name.IndexOf("Controller", StringComparison.Ordinal)));

            list.ForEach(item => this.CreateLinksForEntity(
                item, 
                item.BookId,
                nameof(GetBookByIdAsync), 
                nameof(UpdateBookAsync), 
                nameof(DeleteBookAsync))); // HATEOAS
            return Ok(list);
        }

        /// <summary>
        /// Get Book by id
        /// </summary>
        /// <param name="id">Book id</param>
        /// <returns>Returns entity by id</returns>
        [HttpGet("{id:guid}", Name = nameof(GetBookByIdAsync))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> GetBookByIdAsync(Guid id)
        {
            var entity = await Mediator.Send(new GetBookByIdQuery { Id = id });
            return Ok(this.CreateLinksForEntity(
                entity,
                entity.BookId,
                nameof(GetBookByIdAsync),
                nameof(UpdateBookAsync),
                nameof(DeleteBookAsync))); // HATEOAS
        }
        
        /// <summary>
        /// Craete new Book
        /// </summary>
        /// <param name="command">Cteate command</param>
        /// <returns>Returns id created entity</returns>
        [HttpPost(Name = nameof(CreateBookAsync))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> CreateBookAsync(CreateBookCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        /// <summary>
        /// Update Book by id
        /// </summary>
        /// <param name="command">Update command</param>
        /// <returns>Returns id updated entity</returns>
        [HttpPut(Name = nameof(UpdateBookAsync))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> UpdateBookAsync(UpdateBookCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        /// <summary>
        /// Delete Book by id
        /// </summary>
        /// <param name="id">Book Id</param>
        /// <returns>Return deleted entity id</returns>
        [HttpDelete("{id:guid}", Name = nameof(DeleteBookAsync))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> DeleteBookAsync(Guid id)
        {
            return Ok(await Mediator.Send(new DeleteBookCommand { Id = id }));
        }
 

        /// <summary>
        /// Get list of Book with selected fields
        /// </summary>
        /// <param name="parameters">Fields, that need select</param>
        /// <returns>Returns list of Book with selected fields</returns>
        [HttpGet("datashaping/", Name = nameof(GetAllBook_DataShapingAsync))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> GetAllBook_DataShapingAsync([FromQuery] BookParameter parameters)
        {
            var list = await Mediator.Send(new GetAllBooks_DataShapingQuery(parameters));
            
            _logger.LogInformation(
                "{Count} entities were successfully extracted from [{Table}]",
                list.Count, 
                this.GetType().Name.Substring(0, this.GetType().Name.IndexOf("Controller", StringComparison.Ordinal)));
            
            list.ForEach(item => this.CreateLinksForEntity(
                item,
                item.Id,
                nameof(GetBookByIdAsync), 
                nameof(UpdateBookAsync), 
                nameof(DeleteBookAsync))); // HATEOAS
            
            string json = JsonSerializer.Serialize(list, new JsonSerializerOptions
            {
                WriteIndented = true, // spaces are included in json (relatively speaking, for beauty)
                ReferenceHandler = ReferenceHandler.Preserve // "Preserve" to avoid circular references
            });
            
            return Ok(json);
        }

        /// <summary>
        /// Get Book by id with selected fields
        /// </summary>
        /// <param name="id">Book id</param>
        /// <param name="parameters">Fields, that need select</param>
        /// <returns>Returns entity by id with selected fields</returns>
        [HttpGet("datashaping/{id:guid}", Name = nameof(GetBookById_DataShapingAsync))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> GetBookById_DataShapingAsync
            (Guid id, [FromQuery] BookParameter parameters)
        {
            var entity = await Mediator.Send(new GetBookById_DataShapingQuery(parameters) { Id = id });
            
            _logger.LogInformation(
                "Entity were successfully extracted from [{Table}]",
                this.GetType().Name.Substring(0, this.GetType().Name.IndexOf("Controller", StringComparison.Ordinal)));

            return Ok(this.CreateLinksForEntity(
                    entity,
                    entity.Id,
                    nameof(GetBookByIdAsync), 
                    nameof(UpdateBookAsync), 
                    nameof(DeleteBookAsync))
                .ToString()); // HATEOAS
        }
        
        
        /// <summary>
        /// Gets the list of all Books with average rating
        /// </summary>
        /// <returns>Returns list of GetDTO_AvgRatingBook</returns>
        [HttpGet("extension/avg-rating-each-book", Name = nameof(GetAverageRatingForEachBook_Async))]
        public async Task<ActionResult> GetAverageRatingForEachBook_Async([FromQuery] BookParameter parameters)
        {
            var collection = (await Mediator.Send(new GetAvgRatingForBookQuery(parameters))).ToList();
            
            _logger.LogInformation
                ("{Count} entities were successfully extracted from [{Table}]", collection.Count, _tableName);
            
            return Ok(collection);
        }
        
        /// <summary>
        /// Gets the list of all books without reviews
        /// </summary>
        /// <returns>Returns list of GetDTO_BookWithoutReviews</returns>
        [HttpGet("extension/books-without-reviews", Name = nameof(GetBooksWithoutReviews_Async))]
        public async Task<ActionResult> GetBooksWithoutReviews_Async([FromQuery] BookParameter parameters)
        {
            var collection = (await Mediator.Send(new GetBooksWithoutReviewsQuery(parameters))).ToList();
            
            _logger.LogInformation
                ("{Count} entities were successfully extracted from [{Table}]", collection.Count, _tableName);
            
            return Ok(collection);
        }
    }
}