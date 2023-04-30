using Application.Features.BookAuthorFeatures.Commands;
using Application.Features.BookAuthorFeatures.Commands.CreateBookAuthor;
using Application.Features.BookAuthorFeatures.Commands.DeleteBookAuthor;
using Application.Features.BookAuthorFeatures.Commands.UpdateBookAuthor;
using Application.Features.BookAuthorFeatures.Queries;
using Application.Features.BookAuthorFeatures.Queries.GetAllBookAuthors;
using Application.Features.BookAuthorFeatures.Queries.GetBookAuthor;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Controllers
{
    /// <summary>
    /// BookAuthorsAuthorController
    /// </summary>
    public class BookAuthorController : BaseController
    {
        public BookAuthorController(ILoggerFactory loggerFactory) : base(loggerFactory)
        {
        }
        
        
        /// <summary>
        /// Get list of BookAuthors
        /// </summary>
        /// <returns>Returns list of BookAuthorsAuthors</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> GetAllBookAuthorsAsync()
        {
            return Ok(await Mediator.Send(new GetAllBookAuthorsQuery()));
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
    }
}