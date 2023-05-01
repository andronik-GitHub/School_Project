using System.Text.Json;
using System.Text.Json.Serialization;
using Application.Common.Pagging.Entities;
using Application.Features.BookAuthorFeatures.Commands.CreateBookAuthor;
using Application.Features.BookAuthorFeatures.Commands.DeleteBookAuthor;
using Application.Features.BookAuthorFeatures.Commands.UpdateBookAuthor;
using Application.Features.BookAuthorFeatures.Queries.GetAllBookAuthors;
using Application.Features.BookAuthorFeatures.Queries.GetAllBookAuthors_DataShaping;
using Application.Features.BookAuthorFeatures.Queries.GetBookAuthor;
using Application.Features.BookAuthorFeatures.Queries.GetBookAuthor_DataShaping;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Controllers
{
    /// <summary>
    /// BookAuthorsAuthorController
    /// </summary>
    public class BookAuthorController : BaseController
    {
        /// <summary>
        /// BookAuthorController constructor for initialisation ILogger
        /// </summary>
        /// <param name="loggerFactory"></param>
        public BookAuthorController(ILoggerFactory loggerFactory) : base(loggerFactory)
        {
        }
        
        
        /// <summary>
        /// Get list of BookAuthors
        /// </summary>
        /// <returns>Returns list of BookAuthorsAuthors</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> GetAllBookAuthorsAsync([FromQuery] BookAuthorParameter parameters)
        {
            var list = await Mediator.Send(new GetAllBookAuthorsQuery(parameters));
            
            _logger.LogInformation(
                "{Count} entities were successfully extracted from [{Table}]", 
                list.Count(), 
                this.GetType().Name.Substring(0, this.GetType().Name.IndexOf("Controller", StringComparison.Ordinal)));

            return Ok(list);
        }

        /// <summary>
        /// Get BookAuthor by ids
        /// </summary>
        /// <param name="bookId">Book Id</param>
        /// <param name="AuthorId">Author Id</param>
        /// <returns>Returns entity by id</returns>
        [HttpGet("{bookId:guid}&{AuthorId:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> GetBookAuthorsByIdAsync(Guid bookId, Guid AuthorId)
        {
            return Ok(await Mediator.Send(new GetBookAuthorByIdsQuery { BookId = bookId, AuthorId = AuthorId }));
        }
        
        /// <summary>
        /// Craete new BookAuthor
        /// </summary>
        /// <param name="command">Cteate command</param>
        /// <returns>Returns ids created entity</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> CreateBookAuthorsAsync(CreateBookAuthorCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        /// <summary>
        /// Update BookAuthor by ids
        /// </summary>
        /// <param name="command">Update command</param>
        /// <returns>Returns ids updated entity</returns>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> UpdateBookAuthorsAsync(UpdateBookAuthorCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        /// <summary>
        /// Delete BookAuthor by ids
        /// </summary>
        /// <param name="bookId">Book Id</param>
        /// <param name="AuthorId">Author Id</param>
        /// <returns>Return deleted entity ids</returns>
        [HttpDelete("{bookId:guid}&{AuthorId:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> DeleteBookAuthorsAsync(Guid bookId, Guid AuthorId)
        {
            return Ok(await Mediator.Send(new DeleteBookAuthorCommand { BookId = bookId, AuthorId = AuthorId }));
        }


        /// <summary>
        /// Get list of BookAuthors with selected fields
        /// </summary>
        /// <param name="parameters">Fields, that need select</param>
        /// <returns>Returns list of BookAuthors with selected fields</returns>
        [HttpGet("datashaping/", Name = nameof(GetAllBookAuthors_DataShapingAsync))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> GetAllBookAuthors_DataShapingAsync([FromQuery] BookAuthorParameter parameters)
        {
            var list = await Mediator.Send(new GetAllBookAuthors_DataShapingQuery(parameters));
            
            _logger.LogInformation(
                "{Count} entities were successfully extracted from [{Table}]",
                list.Count(), 
                this.GetType().Name.Substring(0, this.GetType().Name.IndexOf("Controller", StringComparison.Ordinal)));
            
            
            string json = JsonSerializer.Serialize(list, new JsonSerializerOptions
            {
                WriteIndented = true, // spaces are included in json (relatively speaking, for beauty)
                ReferenceHandler = ReferenceHandler.Preserve // "Preserve" to avoid circular references
            });
            
            return Ok(json);
        }

        
        /// <summary>
        /// Get BookAuthor by ids with selected fields
        /// </summary>
        /// <param name="bookId">Book Id</param>
        /// <param name="AuthorId">Author Id</param>
        /// <param name="parameters">Fields, that need select</param>
        /// <returns>Returns entity by id with selected fields</returns>
        [HttpGet("datashaping/{bookId:guid}&{AuthorId:guid}", Name = nameof(GetBookAuthorByIds_DataShapingAsync))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> GetBookAuthorByIds_DataShapingAsync
            (Guid bookId, Guid AuthorId, [FromQuery] BookAuthorParameter parameters)
        {
            var entity = await Mediator.Send(
                new GetBookAuthorByIds_DataShapingQuery(parameters) { BookId = bookId, AuthorId = AuthorId });
            
            _logger.LogInformation(
                "Entity were successfully extracted from [{Table}]",
                this.GetType().Name.Substring(0, this.GetType().Name.IndexOf("Controller", StringComparison.Ordinal)));
            
            
            string json = JsonSerializer.Serialize(entity, new JsonSerializerOptions
            {
                WriteIndented = true, // spaces are included in json (relatively speaking, for beauty)
                ReferenceHandler = ReferenceHandler.Preserve // "Preserve" to avoid circular references
            });
            
            return Ok(json);
        }
    }
}