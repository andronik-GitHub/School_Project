using Application.Common.Pagging.Entities;
using Application.Features.ReviewFeatures.Commands.CreateReview;
using Application.Features.ReviewFeatures.Commands.DeleteReview;
using Application.Features.ReviewFeatures.Commands.UpdateReview;
using Application.Features.ReviewFeatures.Queries.GetAllReviews;
using Application.Features.ReviewFeatures.Queries.GetReview;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Controllers
{
    /// <summary>
    /// ReviewController
    /// </summary>
    public class ReviewController : BaseController
    {
        /// <summary>
        /// ReviewController constructor for initialisation ILogger
        /// </summary>
        /// <param name="loggerFactory"></param>
        public ReviewController(ILoggerFactory loggerFactory) : base(loggerFactory)
        {
        }


        /// <summary>
        /// Get list of Reviews
        /// </summary>
        /// <returns>Returns list of Reviews</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> GetAllReviewAsync([FromQuery] ReviewParameter parameters)
        {
            var list = await Mediator.Send(new GetAllReviewsQuery(parameters));
            
            _logger.LogInformation(
                "{Count} entities were successfully extracted from [{Table}]", 
                list.Count(), 
                this.GetType().Name.Substring(0, this.GetType().Name.IndexOf("Controller", StringComparison.Ordinal)));

            return Ok(list);
        }

        /// <summary>
        /// Get Review by id
        /// </summary>
        /// <param name="id">Review id</param>
        /// <returns>Returns entity by id</returns>
        [HttpGet("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> GetReviewByIdAsync(Guid id)
        {
            return Ok(await Mediator.Send(new GetReviewByIdQuery { Id = id }));
        }
        
        /// <summary>
        /// Craete new Review
        /// </summary>
        /// <param name="command">Cteate command</param>
        /// <returns>Returns id created entity</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> CreateReviewAsync(CreateReviewCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        /// <summary>
        /// Update Review by id
        /// </summary>
        /// <param name="command">Update command</param>
        /// <returns>Returns id updated entity</returns>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> UpdateReviewAsync(UpdateReviewCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        /// <summary>
        /// Delete Review by id
        /// </summary>
        /// <param name="id">Review Id</param>
        /// <returns>Return deleted entity id</returns>
        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> DeleteReviewAsync(Guid id)
        {
            return Ok(await Mediator.Send(new DeleteReviewCommand { Id = id }));
        }
    }
}