using Application.Common.Pagging.Entities;
using Application.Features.BookDetailsFeatures.Commands.CreateBookDetails;
using Application.Features.BookDetailsFeatures.Commands.DeleteBookDetails;
using Application.Features.BookDetailsFeatures.Commands.UpdateBookDetails;
using Application.Features.BookDetailsFeatures.Queries.GetAllBookDetails;
using Application.Features.BookDetailsFeatures.Queries.GetAllBookDetails_DataShaping;
using Application.Features.BookDetailsFeatures.Queries.GetBookDetails;
using Application.Features.BookDetailsFeatures.Queries.GetBookDetails_DataShaping;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Controllers
{
    /// <summary>
    /// BookDetailsController
    /// </summary>
    public class BookDetailsController : BaseController
    {
        /// <summary>
        /// BookDetailsController constructor for initialisation ILogger
        /// </summary>
        /// <param name="loggerFactory"></param>
        public BookDetailsController(ILoggerFactory loggerFactory) : base(loggerFactory)
        {
        }


        /// <summary>
        /// Get list of BookDetails
        /// </summary>
        /// <returns>Returns list of BookDetails</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> GetAllBookDetailsAsync([FromQuery] BookDetailsParameter parameters)
        {
            var list = await Mediator.Send(new GetAllBookDetailsQuery(parameters));
            
            _logger.LogInformation(
                "{Count} entities were successfully extracted from [{Table}]", 
                list.Count(), 
                this.GetType().Name.Substring(0, this.GetType().Name.IndexOf("Controller", StringComparison.Ordinal)));

            return Ok(list);
        }

        /// <summary>
        /// Get BookDetails by id
        /// </summary>
        /// <param name="id">BookDetails id</param>
        /// <returns>Returns entity by id</returns>
        [HttpGet("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> GetBookDetailsByIdAsync(Guid id)
        {
            return Ok(await Mediator.Send(new GetBookDetailsByIdQuery { Id = id }));
        }
        
        /// <summary>
        /// Craete new BookDetails
        /// </summary>
        /// <param name="command">Cteate command</param>
        /// <returns>Returns id created entity</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> CreateBookDetailsAsync(CreateBookDetailsCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        /// <summary>
        /// Update BookDetails by id
        /// </summary>
        /// <param name="command">Update command</param>
        /// <returns>Returns id updated entity</returns>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> UpdateBookDetailsAsync(UpdateBookDetailsCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        /// <summary>
        /// Delete BookDetails by id
        /// </summary>
        /// <param name="id">BookDetails Id</param>
        /// <returns>Return deleted entity id</returns>
        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> DeleteBookDetailsAsync(Guid id)
        {
            return Ok(await Mediator.Send(new DeleteBookDetailsCommand { Id = id }));
        }


        /// <summary>
        /// Get list of BookDetails with selected fields
        /// </summary>
        /// <param name="parameters">Fields, that need select</param>
        /// <returns>Returns list of BookDetails with selected fields</returns>
        [HttpGet("datashaping/", Name = nameof(GetAllBookDetails_DataShapingAsync))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> GetAllBookDetails_DataShapingAsync([FromQuery] BookDetailsParameter parameters)
        {
            var list = await Mediator.Send(new GetAllBookDetails_DataShapingQuery(parameters));
            
            _logger.LogInformation(
                "{Count} entities were successfully extracted from [{Table}]",
                list.Count(), 
                this.GetType().Name.Substring(0, this.GetType().Name.IndexOf("Controller", StringComparison.Ordinal)));
            
            return Ok(list);
        }

        /// <summary>
        /// Get BookDetails by id with selected fields
        /// </summary>
        /// <param name="id">BookDetails id</param>
        /// <param name="parameters">Fields, that need select</param>
        /// <returns>Returns entity by id with selected fields</returns>
        [HttpGet("datashaping/{id:guid}", Name = nameof(GetBookDetailsById_DataShapingAsync))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> GetBookDetailsById_DataShapingAsync
            (Guid id, [FromQuery] BookDetailsParameter parameters)
        {
            var entity = await Mediator.Send(new GetBookDetailsById_DataShapingQuery(parameters) { Id = id });
            
            _logger.LogInformation(
                "Entity were successfully extracted from [{Table}]",
                this.GetType().Name.Substring(0, this.GetType().Name.IndexOf("Controller", StringComparison.Ordinal)));
            
            return Ok(entity);
        }
    }
}