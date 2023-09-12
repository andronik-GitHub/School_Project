using System.Text.Json;
using System.Text.Json.Serialization;
using Application.Common.Pagging.Entities;
using Application.Features.GenreFeatures.Commands.CreateGenre;
using Application.Features.GenreFeatures.Commands.DeleteGenre;
using Application.Features.GenreFeatures.Commands.UpdateGenre;
using Application.Features.GenreFeatures.Queries.GetAllGenres;
using Application.Features.GenreFeatures.Queries.GetAllGenres_DataShaping;
using Application.Features.GenreFeatures.Queries.GetCountOfBooksEachGenre;
using Application.Features.GenreFeatures.Queries.GetGenre;
using Application.Features.GenreFeatures.Queries.GetGenre_DataShaping;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Controllers
{
    /// <summary>
    /// GenreController
    /// </summary>
    public class GenreController : BaseController
    {
        /// <summary>
        /// GenreController constructor for initialisation ILogger and IUrlHelper
        /// </summary>
        /// <param name="loggerFactory">ILoggerFactory</param>
        /// <param name="urlHelper">IUrlHelper</param>
        public GenreController(ILoggerFactory loggerFactory, IUrlHelper urlHelper) 
            : base(loggerFactory, urlHelper)
        {
        }


        /// <summary>
        /// Get list of Genres
        /// </summary>
        /// <returns>Returns list of Genres</returns>
        [HttpGet(Name = nameof(GetAllGenreAsync))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> GetAllGenreAsync([FromQuery] GenreParameter parameters)
        {
            var list = (await Mediator.Send(new GetAllGenresQuery(parameters))).ToList();
            
            _logger.LogInformation(
                "{Count} entities were successfully extracted from [{Table}]", 
                list.Count, 
                this.GetType().Name.Substring(0, this.GetType().Name.IndexOf("Controller", StringComparison.Ordinal)));

            list.ForEach(item => this.CreateLinksForEntity(
                item,
                item.GenreId,
                nameof(GetGenreByIdAsync), 
                nameof(UpdateGenreAsync), 
                nameof(DeleteGenreAsync))); // HATEOAS
            return Ok(list);
        }

        /// <summary>
        /// Get Genre by id
        /// </summary>
        /// <param name="id">Genre id</param>
        /// <returns>Returns entity by id</returns>
        [HttpGet("{id:guid}", Name = nameof(GetGenreByIdAsync))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> GetGenreByIdAsync(Guid id)
        {
            var entity = await Mediator.Send(new GetGenreByIdQuery { Id = id });
            return Ok(this.CreateLinksForEntity(
                entity,
                entity.GenreId,
                nameof(GetGenreByIdAsync), 
                nameof(UpdateGenreAsync), 
                nameof(DeleteGenreAsync))); // HATEOAS
        }
        
        /// <summary>
        /// Craete new Genre
        /// </summary>
        /// <param name="command">Cteate command</param>
        /// <returns>Returns id created entity</returns>
        [HttpPost(Name = nameof(CreateGenreAsync))]
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
        [HttpPut(Name = nameof(UpdateGenreAsync))]
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
        [HttpDelete("{id:guid}", Name = nameof(DeleteGenreAsync))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> DeleteGenreAsync(Guid id)
        {
            return Ok(await Mediator.Send(new DeleteGenreCommand { Id = id }));
        }
 

        /// <summary>
        /// Get list of Genre with selected fields
        /// </summary>
        /// <param name="parameters">Fields, that need select</param>
        /// <returns>Returns list of Genre with selected fields</returns>
        [HttpGet("datashaping/", Name = nameof(GetAllGenre_DataShapingAsync))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> GetAllGenre_DataShapingAsync([FromQuery] GenreParameter parameters)
        {
            var list = await Mediator.Send(new GetAllGenres_DataShapingQuery(parameters));
            
            _logger.LogInformation(
                "{Count} entities were successfully extracted from [{Table}]",
                list.Count, 
                this.GetType().Name.Substring(0, this.GetType().Name.IndexOf("Controller", StringComparison.Ordinal)));
            
            
            list.ForEach(item => this.CreateLinksForEntity(
                item,
                item.Id,
                nameof(GetGenreByIdAsync), 
                nameof(UpdateGenreAsync), 
                nameof(DeleteGenreAsync))); // HATEOAS
            
            string json = JsonSerializer.Serialize(list, new JsonSerializerOptions
            {
                WriteIndented = true, // spaces are included in json (relatively speaking, for beauty)
                ReferenceHandler = ReferenceHandler.Preserve // "Preserve" to avoid circular references
            });
            
            return Ok(json);
        }

        /// <summary>
        /// Get Genre by id with selected fields
        /// </summary>
        /// <param name="id">Genre id</param>
        /// <param name="parameters">Fields, that need select</param>
        /// <returns>Returns entity by id with selected fields</returns>
        [HttpGet("datashaping/{id:guid}", Name = nameof(GetGenreById_DataShapingAsync))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> GetGenreById_DataShapingAsync
            (Guid id, [FromQuery] GenreParameter parameters)
        {
            var entity = await Mediator.Send(new GetGenreById_DataShapingQuery(parameters) { Id = id });
            
            _logger.LogInformation(
                "Entity were successfully extracted from [{Table}]",
                this.GetType().Name.Substring(0, this.GetType().Name.IndexOf("Controller", StringComparison.Ordinal)));

            
            return Ok(this.CreateLinksForEntity(
                    entity,
                    entity.Id,
                    nameof(GetGenreByIdAsync), 
                    nameof(UpdateGenreAsync), 
                    nameof(DeleteGenreAsync))
                .ToString()); // HATEOAS
        }
        
        
        /// <summary>
        /// Gets count the number of books of each genre
        /// </summary>
        /// <returns>Returns list of GetDTO_CountOfBooksEachGenre</returns>
        [HttpGet("extension/count-of-book-each-genre", Name = nameof(GetCountOfBooksEachGenre_Async))]
        public async Task<ActionResult> GetCountOfBooksEachGenre_Async([FromQuery] GenreParameter parameters)
        {
            var list = (await Mediator.Send(new GetCountOfBooksEachGenreQuery(parameters))).ToList();

            _logger.LogInformation
                ("{Count} entities were successfully extracted from [{Table}]", list.Count, _tableName);
            
            return Ok(list);
        }
    }
}