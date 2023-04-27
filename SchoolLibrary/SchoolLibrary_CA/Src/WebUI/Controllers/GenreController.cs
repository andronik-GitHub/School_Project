using Application.Features.GenreFeatures.Commands;
using Application.Features.GenreFeatures.Queries;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Controllers
{
    /// <summary>
    /// GenreController
    /// </summary>
    public class GenreController : BaseController
    {
        /// <summary>
        /// Get list of Genres
        /// </summary>
        /// <returns>Returns list of Genres</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> GetAllGenreAsync()
        {
            return Ok(await Mediator.Send(new GetAllGenresQuery()));
        }

        /// <summary>
        /// Get Genre by id
        /// </summary>
        /// <param name="id">Genre id</param>
        /// <returns>Returns entity by id</returns>
        [HttpGet("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> GetGenreByIdAsync(Guid id)
        {
            return Ok(await Mediator.Send(new GetGenreByIdQuery { Id = id }));
        }
        
        /// <summary>
        /// Craete new Genre
        /// </summary>
        /// <param name="command">Cteate command</param>
        /// <returns>Returns id created entity</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> CreateGenreAsync(CreateGenreCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        /// <summary>
        /// Update Genre by id
        /// </summary>
        /// <param name="command">Update command</param>
        /// <returns>Returns id updated entity</returns>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> UpdateGenreAsync(UpdateGenreCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        /// <summary>
        /// Delete Genre by id
        /// </summary>
        /// <param name="id">Genre Id</param>
        /// <returns>Return deleted entity id</returns>
        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> DeleteGenreAsync(Guid id)
        {
            return Ok(await Mediator.Send(new DeleteGenreCommand { Id = id }));
        }
    }
}