using Application.Features.AuthorFeatures.Commands;
using Application.Features.AuthorFeatures.Commands.CreateAuthor;
using Application.Features.AuthorFeatures.Commands.DeleteAuthor;
using Application.Features.AuthorFeatures.Commands.UpdateAuthor;
using Application.Features.AuthorFeatures.Queries;
using Application.Features.AuthorFeatures.Queries.GetAllAuthors;
using Application.Features.AuthorFeatures.Queries.GetAuthor;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Controllers
{
    /// <summary>
    /// AuthorController
    /// </summary>
    public class AuthorController : BaseController
    {
        /// <summary>
        /// Get list of Authors
        /// </summary>
        /// <returns>Returns list of Authors</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> GetAllAuthorAsync()
        {
            return Ok(await Mediator.Send(new GetAllAuthorsQuery()));
        }

        /// <summary>
        /// Get Author by id
        /// </summary>
        /// <param name="id">Author id</param>
        /// <returns>Returns entity by id</returns>
        [HttpGet("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> GetAuthorByIdAsync(Guid id)
        {
            return Ok(await Mediator.Send(new GetAuthorByIdQuery { Id = id }));
        }
        
        /// <summary>
        /// Craete new Author
        /// </summary>
        /// <param name="command">Cteate command</param>
        /// <returns>Returns id created entity</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> CreateAuthorAsync(CreateAuthorCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        /// <summary>
        /// Update Author by id
        /// </summary>
        /// <param name="command">Update command</param>
        /// <returns>Returns id updated entity</returns>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> UpdateAuthorAsync(UpdateAuthorCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        /// <summary>
        /// Delete Author by id
        /// </summary>
        /// <param name="id">Author Id</param>
        /// <returns>Return deleted entity id</returns>
        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> DeleteAuthorAsync(Guid id)
        {
            return Ok(await Mediator.Send(new DeleteAuthorCommand { Id = id }));
        }
    }
}