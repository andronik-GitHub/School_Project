using System.Text.Json;
using System.Text.Json.Serialization;
using Application.Common.Pagging.Entities;
using Application.Features.AuthorFeatures.Commands.CreateAuthor;
using Application.Features.AuthorFeatures.Commands.DeleteAuthor;
using Application.Features.AuthorFeatures.Commands.UpdateAuthor;
using Application.Features.AuthorFeatures.Queries.GetAllAuthors;
using Application.Features.AuthorFeatures.Queries.GetAllAuthors_DataShaping;
using Application.Features.AuthorFeatures.Queries.GetAuthor;
using Application.Features.AuthorFeatures.Queries.GetAuthor_DataShaping;
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
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> GetAllAuthorAsync([FromQuery] AuthorParameter parameters)
        {
            if (!parameters.ValidYearRand) // if invalid filtering data is entered
                return StatusCode(StatusCodes.Status400BadRequest);
            
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


        /// <summary>
        /// Get list of Authors with selected fields
        /// </summary>
        /// <param name="parameters">Fields, that need select</param>
        /// <returns>Returns list of Authors with selected fields</returns>
        [HttpGet("datashaping/", Name = nameof(GetAllAuthors_DataShapingAsync))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> GetAllAuthors_DataShapingAsync([FromQuery] AuthorParameter parameters)
        {
            if (!parameters.ValidYearRand) // if invalid filtering data is entered
                return StatusCode(StatusCodes.Status400BadRequest);

            var list = await Mediator.Send(new GetAllAuthors_DataShapingQuery(parameters));
            
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
        /// Get Author by id with selected fields
        /// </summary>
        /// <param name="id">Author id</param>
        /// <param name="parameters">Fields, that need select</param>
        /// <returns>Returns entity by id with selected fields</returns>
        [HttpGet("datashaping/{id:guid}", Name = nameof(GetAuthorById_DataShapingAsync))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> GetAuthorById_DataShapingAsync(Guid id, [FromQuery] AuthorParameter parameters)
        {
            if (!parameters.ValidYearRand) // if invalid filtering data is entered
                return StatusCode(StatusCodes.Status400BadRequest);
            
            var entity = await Mediator.Send(new GetAuthorById_DataShapingQuery(parameters) { Id = id });
            
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