using System.Text.Json;
using System.Text.Json.Serialization;
using Application.Common.Pagging.Entities;
using Application.Features.ReviewFeatures.Commands.CreateReview;
using Application.Features.ReviewFeatures.Commands.DeleteReview;
using Application.Features.ReviewFeatures.Commands.UpdateReview;
using Application.Features.ReviewFeatures.Queries.GetAllReviews;
using Application.Features.ReviewFeatures.Queries.GetAllReviews_DataShaping;
using Application.Features.ReviewFeatures.Queries.GetReview;
using Application.Features.ReviewFeatures.Queries.GetReview_DataShaping;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Controllers
{
    /// <summary>
    /// ReviewController
    /// </summary>
    public class ReviewController : BaseController
    {
        /// <summary>
        /// ReviewController constructor for initialisation ILogger and IUrlHelper
        /// </summary>
        /// <param name="loggerFactory">ILoggerFactory</param>
        /// <param name="urlHelper">IUrlHelper</param>
        public ReviewController(ILoggerFactory loggerFactory, IUrlHelper urlHelper) 
            : base(loggerFactory, urlHelper)
        {
        }


        /// <summary>
        /// Get list of Reviews
        /// </summary>
        /// <returns>Returns list of Reviews</returns>
        [HttpGet(Name = nameof(GetAllReviewAsync))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> GetAllReviewAsync([FromQuery] ReviewParameter parameters)
        {
            var list = (await Mediator.Send(new GetAllReviewsQuery(parameters))).ToList();
            
            _logger.LogInformation(
                "{Count} entities were successfully extracted from [{Table}]", 
                list.Count, 
                this.GetType().Name.Substring(0, this.GetType().Name.IndexOf("Controller", StringComparison.Ordinal)));

            list.ForEach(item => this.CreateLinksForEntity(
                item,
                item.ReviewId,
                nameof(GetReviewByIdAsync), 
                nameof(UpdateReviewAsync), 
                nameof(DeleteReviewAsync))); // HATEOAS
            return Ok(list);
        }

        /// <summary>
        /// Get Review by id
        /// </summary>
        /// <param name="id">Review id</param>
        /// <returns>Returns entity by id</returns>
        [HttpGet("{id:guid}", Name = nameof(GetReviewByIdAsync))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> GetReviewByIdAsync(Guid id)
        {
            var entity = await Mediator.Send(new GetReviewByIdQuery { Id = id });
            return Ok(this.CreateLinksForEntity(
                entity,
                entity.ReviewId,
                nameof(GetReviewByIdAsync), 
                nameof(UpdateReviewAsync), 
                nameof(DeleteReviewAsync))); // HATEOAS
        }
        
        /// <summary>
        /// Craete new Review
        /// </summary>
        /// <param name="command">Cteate command</param>
        /// <returns>Returns id created entity</returns>
        [HttpPost(Name = nameof(CreateReviewAsync))]
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
        [HttpPut(Name = nameof(UpdateReviewAsync))]
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
        [HttpDelete("{id:guid}", Name = nameof(DeleteReviewAsync))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> DeleteReviewAsync(Guid id)
        {
            return Ok(await Mediator.Send(new DeleteReviewCommand { Id = id }));
        }
  

        /// <summary>
        /// Get list of Review with selected fields
        /// </summary>
        /// <param name="parameters">Fields, that need select</param>
        /// <returns>Returns list of Review with selected fields</returns>
        [HttpGet("datashaping/", Name = nameof(GetAllReview_DataShapingAsync))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> GetAllReview_DataShapingAsync([FromQuery] ReviewParameter parameters)
        {
            var list = await Mediator.Send(new GetAllReviews_DataShapingQuery(parameters));
            
            _logger.LogInformation(
                "{Count} entities were successfully extracted from [{Table}]",
                list.Count, 
                this.GetType().Name.Substring(0, this.GetType().Name.IndexOf("Controller", StringComparison.Ordinal)));
            
            
            list.ForEach(item => this.CreateLinksForEntity(
                item,
                item.Id,
                nameof(GetReviewByIdAsync), 
                nameof(UpdateReviewAsync), 
                nameof(DeleteReviewAsync))); // HATEOAS
            
            string json = JsonSerializer.Serialize(list, new JsonSerializerOptions
            {
                WriteIndented = true, // spaces are included in json (relatively speaking, for beauty)
                ReferenceHandler = ReferenceHandler.Preserve // "Preserve" to avoid circular references
            });
            
            return Ok(json);
        }

        /// <summary>
        /// Get Review by id with selected fields
        /// </summary>
        /// <param name="id">Review id</param>
        /// <param name="parameters">Fields, that need select</param>
        /// <returns>Returns entity by id with selected fields</returns>
        [HttpGet("datashaping/{id:guid}", Name = nameof(GetReviewById_DataShapingAsync))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> GetReviewById_DataShapingAsync
            (Guid id, [FromQuery] ReviewParameter parameters)
        {
            var entity = await Mediator.Send(new GetReviewById_DataShapingQuery(parameters) { Id = id });
            
            _logger.LogInformation(
                "Entity were successfully extracted from [{Table}]",
                this.GetType().Name.Substring(0, this.GetType().Name.IndexOf("Controller", StringComparison.Ordinal)));

            
            return Ok(this.CreateLinksForEntity(
                    entity,
                    entity.Id,
                    nameof(GetReviewByIdAsync), 
                    nameof(UpdateReviewAsync), 
                    nameof(DeleteReviewAsync))
                .ToString()); // HATEOAS
        }
    }
}