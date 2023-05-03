using System.Dynamic;
using System.Text.Json;
using System.Text.Json.Serialization;
using Application.Common.Pagging.Entities;
using Application.Features.BookGenreFeatures.Commands.CreateBookGenre;
using Application.Features.BookGenreFeatures.Commands.DeleteBookGenre;
using Application.Features.BookGenreFeatures.Commands.UpdateBookGenre;
using Application.Features.BookGenreFeatures.Queries.GetAllBookGenres;
using Application.Features.BookGenreFeatures.Queries.GetAllBookGenres_DataShaping;
using Application.Features.BookGenreFeatures.Queries.GetBookGenre;
using Application.Features.BookGenreFeatures.Queries.GetBookGenre_DataShaping;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Controllers
{
    /// <summary>
    /// BookGenresGenreController
    /// </summary>
    public class BookGenreController : BaseController
    {
        /// <summary>
        /// BookGenreController constructor for initialisation ILogger and IUrlHelper
        /// </summary>
        /// <param name="loggerFactory">ILoggerFactory</param>
        /// <param name="urlHelper">IUrlHelper</param>
        public BookGenreController(ILoggerFactory loggerFactory, IUrlHelper urlHelper) 
            : base(loggerFactory, urlHelper)
        {
        }


        /// <summary>
        /// Get list of BookGenres
        /// </summary>
        /// <returns>Returns list of BookGenresGenres</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> GetAllBookGenresAsync([FromQuery] BookGenreParameter parameters)
        {
            var list = await Mediator.Send(new GetAllBookGenresQuery(parameters));
            
            _logger.LogInformation(
                "{Count} entities were successfully extracted from [{Table}]", 
                list.Count(), 
                this.GetType().Name.Substring(0, this.GetType().Name.IndexOf("Controller", StringComparison.Ordinal)));

            return Ok(list);
        }

        /// <summary>
        /// Get BookGenre by ids
        /// </summary>
        /// <param name="bookId">Book Id</param>
        /// <param name="genreId">Genre Id</param>
        /// <returns>Returns entity by id</returns>
        [HttpGet("{bookId:guid}&{genreId:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> GetBookGenresByIdAsync(Guid bookId, Guid genreId)
        {
            return Ok(await Mediator.Send(new GetBookGenreByIdsQuery { BookId = bookId, GenreId = genreId }));
        }
        
        /// <summary>
        /// Craete new BookGenre
        /// </summary>
        /// <param name="command">Cteate command</param>
        /// <returns>Returns ids created entity</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> CreateBookGenresAsync(CreateBookGenreCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        /// <summary>
        /// Update BookGenre by ids
        /// </summary>
        /// <param name="command">Update command</param>
        /// <returns>Returns ids updated entity</returns>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> UpdateBookGenresAsync(UpdateBookGenreCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        /// <summary>
        /// Delete BookGenre by ids
        /// </summary>
        /// <param name="bookId">Book Id</param>
        /// <param name="genreId">Genre Id</param>
        /// <returns>Return deleted entity ids</returns>
        [HttpDelete("{bookId:guid}&{genreId:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> DeleteBookGenresAsync(Guid bookId, Guid genreId)
        {
            return Ok(await Mediator.Send(new DeleteBookGenreCommand { BookId = bookId, GenreId = genreId }));
        }


        /// <summary>
        /// Get list of BookGenres with selected fields
        /// </summary>
        /// <param name="parameters">Fields, that need select</param>
        /// <returns>Returns list of BookGenres with selected fields</returns>
        [HttpGet("datashaping/", Name = nameof(GetAllBookGenres_DataShapingAsync))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> GetAllBookGenres_DataShapingAsync([FromQuery] BookGenreParameter parameters)
        {
            var list = await Mediator.Send(new GetAllBookGenres_DataShapingQuery(parameters));
            
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
        /// Get BookGenre by ids with selected fields
        /// </summary>
        /// <param name="bookId">Book Id</param>
        /// <param name="GenreId">Genre Id</param>
        /// <param name="parameters">Fields, that need select</param>
        /// <returns>Returns entity by id with selected fields</returns>
        [HttpGet("datashaping/{bookId:guid}&{GenreId:guid}", Name = nameof(GetBookGenreByIds_DataShapingAsync))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> GetBookGenreByIds_DataShapingAsync
            (Guid bookId, Guid GenreId, [FromQuery] BookGenreParameter parameters)
        {
            var entity = await Mediator.Send(
                new GetBookGenreByIds_DataShapingQuery(parameters) { BookId = bookId, GenreId = GenreId });
            
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