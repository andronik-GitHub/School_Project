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
        /// PublisherController constructor for initialisation ILogger and IUrlHelper
        /// </summary>
        /// <param name="loggerFactory">ILoggerFactory</param>
        /// <param name="urlHelper">IUrlHelper</param>
        public PublisherController(ILoggerFactory loggerFactory, IUrlHelper urlHelper) 
            : base(loggerFactory, urlHelper)
        {
        }


        /// <summary>
        /// Get list of Publishers
        /// </summary>
        /// <returns>Returns list of Publishers</returns>
        [HttpGet(Name = nameof(GetAllPublisherAsync))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> GetAllPublisherAsync([FromQuery] PublisherParameter parameters)
        {
            var list = (await Mediator.Send(new GetAllPublishersQuery(parameters))).ToList();
            
            _logger.LogInformation(
                "{Count} entities were successfully extracted from [{Table}]", 
                list.Count, 
                this.GetType().Name.Substring(0, this.GetType().Name.IndexOf("Controller", StringComparison.Ordinal)));

            list.ForEach(item => this.CreateLinksForEntity(
                item,
                item.PublisherId,
                nameof(GetPublisherByIdAsync), 
                nameof(UpdatePublisherAsync), 
                nameof(DeletePublisherAsync))); // HATEOAS
            return Ok(list);
        }

        /// <summary>
        /// Get Publisher by id
        /// </summary>
        /// <param name="id">Publisher id</param>
        /// <returns>Returns entity by id</returns>
        [HttpGet("{id:guid}", Name = nameof(GetPublisherByIdAsync))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> GetPublisherByIdAsync(Guid id)
        {
            var entity = await Mediator.Send(new GetPublisherByIdQuery { Id = id });
            return Ok(this.CreateLinksForEntity(
                entity,
                entity.PublisherId,
                nameof(GetPublisherByIdAsync), 
                nameof(UpdatePublisherAsync), 
                nameof(DeletePublisherAsync))); // HATEOAS
        }
        
        /// <summary>
        /// Craete new Publisher
        /// </summary>
        /// <param name="command">Cteate command</param>
        /// <returns>Returns id created entity</returns>
        [HttpPost(Name = nameof(CreatePublisherAsync))]
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
        [HttpPut(Name = nameof(UpdatePublisherAsync))]
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
        [HttpDelete("{id:guid}", Name = nameof(DeletePublisherAsync))]
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
                list.Count, 
                this.GetType().Name.Substring(0, this.GetType().Name.IndexOf("Controller", StringComparison.Ordinal)));
            
            
            list.ForEach(item => this.CreateLinksForEntity(
                item,
                item.Id,
                nameof(GetPublisherByIdAsync), 
                nameof(UpdatePublisherAsync), 
                nameof(DeletePublisherAsync))); // HATEOAS
            
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

            
            return Ok(this.CreateLinksForEntity(
                    entity,
                    entity.Id,
                    nameof(GetPublisherByIdAsync), 
                    nameof(UpdatePublisherAsync), 
                    nameof(DeletePublisherAsync))
                .ToString()); // HATEOAS
        }
    }
}