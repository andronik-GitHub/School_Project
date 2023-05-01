using System.Text.Json;
using System.Text.Json.Serialization;
using Application.Common.Pagging.Entities;
using Application.Features.PublisherFeatures.Commands.CreatePublisher;
using Application.Features.PublisherFeatures.Commands.DeletePublisher;
using Application.Features.PublisherFeatures.Commands.UpdatePublisher;
using Application.Features.PublisherFeatures.Queries.GetAllPublishers;
using Application.Features.PublisherFeatures.Queries.GetAllPublishers_DataShaping;
using Application.Features.PublisherFeatures.Queries.GetPublisher;
using Application.Features.PublisherFeatures.Queries.GetPublisher_DataShaping;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Controllers
{
    /// <summary>
    /// PublisherController
    /// </summary>
    public class PublisherController : BaseController
    {
        /// <summary>
        /// PublisherController constructor for initialisation ILogger
        /// </summary>
        /// <param name="loggerFactory"></param>
        public PublisherController(ILoggerFactory loggerFactory) : base(loggerFactory)
        {
        }


        /// <summary>
        /// Get list of Publishers
        /// </summary>
        /// <returns>Returns list of Publishers</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> GetAllPublisherAsync([FromQuery] PublisherParameter parameters)
        {
            var list = await Mediator.Send(new GetAllPublishersQuery(parameters));
            
            _logger.LogInformation(
                "{Count} entities were successfully extracted from [{Table}]", 
                list.Count(), 
                this.GetType().Name.Substring(0, this.GetType().Name.IndexOf("Controller", StringComparison.Ordinal)));

            return Ok(list);
        }

        /// <summary>
        /// Get Publisher by id
        /// </summary>
        /// <param name="id">Publisher id</param>
        /// <returns>Returns entity by id</returns>
        [HttpGet("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> GetPublisherByIdAsync(Guid id)
        {
            return Ok(await Mediator.Send(new GetPublisherByIdQuery { Id = id }));
        }
        
        /// <summary>
        /// Craete new Publisher
        /// </summary>
        /// <param name="command">Cteate command</param>
        /// <returns>Returns id created entity</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> CreatePublisherAsync(CreatePublisherCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        /// <summary>
        /// Update Publisher by id
        /// </summary>
        /// <param name="command">Update command</param>
        /// <returns>Returns id updated entity</returns>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> UpdatePublisherAsync(UpdatePublisherCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        /// <summary>
        /// Delete Publisher by id
        /// </summary>
        /// <param name="id">Publisher Id</param>
        /// <returns>Return deleted entity id</returns>
        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> DeletePublisherAsync(Guid id)
        {
            return Ok(await Mediator.Send(new DeletePublisherCommand { Id = id }));
        }
 

        /// <summary>
        /// Get list of Publisher with selected fields
        /// </summary>
        /// <param name="parameters">Fields, that need select</param>
        /// <returns>Returns list of Publisher with selected fields</returns>
        [HttpGet("datashaping/", Name = nameof(GetAllPublisher_DataShapingAsync))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> GetAllPublisher_DataShapingAsync([FromQuery] PublisherParameter parameters)
        {
            var list = await Mediator.Send(new GetAllPublishers_DataShapingQuery(parameters));
            
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
        /// Get Publisher by id with selected fields
        /// </summary>
        /// <param name="id">Publisher id</param>
        /// <param name="parameters">Fields, that need select</param>
        /// <returns>Returns entity by id with selected fields</returns>
        [HttpGet("datashaping/{id:guid}", Name = nameof(GetPublisherById_DataShapingAsync))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> GetPublisherById_DataShapingAsync
            (Guid id, [FromQuery] PublisherParameter parameters)
        {
            var entity = await Mediator.Send(new GetPublisherById_DataShapingQuery(parameters) { Id = id });
            
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