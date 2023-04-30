using Application.Common.Pagging.Entities;
using Application.Features.AuthorFeatures.Commands.CreateAuthor;
using Application.Features.AuthorFeatures.Commands.DeleteAuthor;
using Application.Features.AuthorFeatures.Commands.UpdateAuthor;
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
        /// AuthorController constructor for initialisation ILogger
        /// </summary>
        /// <param name="loggerFactory"></param>
        public AuthorController(ILoggerFactory loggerFactory) 
            : base(loggerFactory)
        {
        }
    
    
        /// <summary>
        /// Get list of Authors
        /// </summary>
        /// <returns>Returns list of Authors</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> GetAllAuthorAsync([FromQuery] AuthorParameter parameters)
        {
            var list = await Mediator.Send(new GetAllAuthorsQuery(parameters));
            
            _logger.LogInformation(
                "{Count} entities were successfully extracted from [{Table}]", 
                list.Count(), 
                this.GetType().Name.Substring(0, this.GetType().Name.IndexOf("Controller", StringComparison.Ordinal)));
            
            return Ok(list);
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