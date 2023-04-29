using Application.Features.BookFeatures.Commands;
using Application.Features.BookFeatures.Commands.CreateBook;
using Application.Features.BookFeatures.Commands.DeleteBook;
using Application.Features.BookFeatures.Commands.UpdateBook;
using Application.Features.BookFeatures.Queries;
using Application.Features.BookFeatures.Queries.GetAllBooks;
using Application.Features.BookFeatures.Queries.GetBook;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Controllers
{
    /// <summary>
    /// BookController
    /// </summary>
    public class BookController : BaseController
    {
        /// <summary>
        /// Get list of Books
        /// </summary>
        /// <returns>Returns list of Books</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> GetAllBookAsync()
        {
            return Ok(await Mediator.Send(new GetAllBooksQuery()));
        }

        /// <summary>
        /// Get Book by id
        /// </summary>
        /// <param name="id">Book id</param>
        /// <returns>Returns entity by id</returns>
        [HttpGet("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> GetBookByIdAsync(Guid id)
        {
            return Ok(await Mediator.Send(new GetBookByIdQuery { Id = id }));
        }
        
        /// <summary>
        /// Craete new Book
        /// </summary>
        /// <param name="command">Cteate command</param>
        /// <returns>Returns id created entity</returns>
        [HttpPost]
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
        [HttpPut]
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
        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> DeleteBookAsync(Guid id)
        {
            return Ok(await Mediator.Send(new DeleteBookCommand { Id = id }));
        }
    }
}