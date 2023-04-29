using Application.Features.PublisherFeatures.Commands;
using Application.Features.PublisherFeatures.Commands.CreatePublisher;
using Application.Features.PublisherFeatures.Commands.DeletePublisher;
using Application.Features.PublisherFeatures.Commands.UpdatePublisher;
using Application.Features.PublisherFeatures.Queries;
using Application.Features.PublisherFeatures.Queries.GetAllPublishers;
using Application.Features.PublisherFeatures.Queries.GetPublisher;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Controllers
{
    /// <summary>
    /// PublisherController
    /// </summary>
    public class PublisherController : BaseController
    {
        /// <summary>
        /// Get list of Publishers
        /// </summary>
        /// <returns>Returns list of Publishers</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> GetAllPublisherAsync()
        {
            return Ok(await Mediator.Send(new GetAllPublishersQuery()));
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
    }
}