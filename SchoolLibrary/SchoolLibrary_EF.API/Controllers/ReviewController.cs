using Microsoft.AspNetCore.Mvc;
using SchoolLibrary_EF.BLL.DTO;
using SchoolLibrary_EF.BLL.Services.Contracts;
using SchoolLibrary_EF.DAL.Pagging;

namespace SchoolLibrary_EF.API.Controllers
{
    [Produces("application/json")]
    [Route("ef/[controller]")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private readonly IReviewService _reviewService;
        private readonly ILogger _logger;

        public ReviewController(IReviewService reviewService, ILoggerFactory loggerFactory)
        {
            _reviewService = reviewService;
            _logger = loggerFactory.CreateLogger($"{this.GetType().Name}_Logger");
        }


        /// <summary>
        /// Gets the list of all Reviews
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// GET ef/review
        /// </remarks>
        /// <returns>Returns list of ReviewDTO</returns>
        /// <response code="200">Success</response>
        /// <response code="500">If it was not possible to get a list of elements from the database</response>
        [HttpGet] // GET: ef/review
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<ReviewDTO>>> GetAllAsync([FromQuery] AuthorParameters parameters)
        {
            try
            {
                var collection = await _reviewService.GetAllAsync(parameters);
                _logger.LogInformation
                    ("{Count} entities were successfully extracted from [Reviews]", collection.Count());

                return Ok(collection);
            }
            catch (Exception ex)
            {
                _logger.LogError
                    ("Error in [{ErrorClassName}]->[GetAllAsync] => {ErrorMessage}", this.GetType().Name, ex.Message);

                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Gets the Review by id
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// GET ef/review/13ce1333-7b7c-4395-8565-0474a6ad05ad
        /// </remarks>
        /// <param name="id">Review id (Guid)</param>
        /// <returns>Returns element of ReviewDTO</returns>
        /// <response code="200">Success</response>
        /// <response code="404">If the element with such ID is not found in the database</response>
        /// <response code="500">If it was not possible to get element from the database</response>
        [HttpGet("{id}")] // GET: ef/review/id
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ReviewDTO>> GetByIdAsync(Guid id)
        {
            try
            {
                var entity = await _reviewService.GetAsync(id);

                if (entity == null)
                {
                    _logger.LogError("Entity with id: [{EntityId}] from [Reviews] not found", id);

                    return StatusCode(StatusCodes.Status404NotFound);
                    //return NotFound();
                }
                else
                {
                    _logger.LogInformation
                        ("Entity with id: [{EntityId}] were successfully extracted from [Reviews]", id);

                    return Ok(entity);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError
                    ("Error in [{ErrorClassName}]->[GetAllAsync] => {ErrorMessage}", this.GetType().Name, ex.Message);

                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Creates new Review
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     POST: ef/review
        ///     {
        ///         "reviewId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
        ///         "rating": 0,
        ///         "reviewText": "string",
        ///         "userFullName": "string",
        ///         "bookTitle": "string"
        ///     }
        /// </remarks>
        /// <param name="newReview">ReviewDTO newEntity</param>
        /// <returns>Returns id (Guid)</returns>
        /// <response code="200">Success</response>
        /// <response code="400">If invalid data entered</response>
        /// <response code="500">If it was not possible to adding element to the database</response>
        [HttpPost] // POST: ef/review
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Guid>> AddAsync(ReviewDTO newReview)
        {
            try
            {
                // Checking whether valid data has been entered
                if (newReview.ReviewText == null || newReview.UserFullName == null || newReview.BookTitle == null)
                {
                    _logger.LogError("Invalid data entered");

                    return StatusCode(StatusCodes.Status400BadRequest);
                    //return BadRequest("Invalid data entered");
                }
                else
                {
                    var id = await _reviewService.CreateAsync(newReview);
                    _logger.LogInformation
                        ("Entity with id: [{EntityId}] were successfully added to [Reviews]", id);

                    return Ok(id);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError
                    ("Error in [{ErrorClassName}]->[GetAllAsync] => {ErrorMessage}", this.GetType().Name, ex.Message);

                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Update the Review
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     PUT: ef/review
        ///     {
        ///         "reviewId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
        ///         "rating": 0,
        ///         "reviewText": "string",
        ///         "userFullName": "string",
        ///         "bookTitle": "string"
        ///     }
        /// </remarks>
        /// <param name="updateReview">ReviewDTO updateEntity</param>
        /// <returns>Returns NoContent</returns>
        /// <response code="204">Success</response>
        /// <response code="400">If invalid data entered</response>
        /// <response code="500">If it was not possible to adding element to the database</response>
        [HttpPut] // PUT: ef/review
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> UpdateAsync(ReviewDTO updateReview)
        {
            try
            {
                // Checking whether valid data has been entered
                if (updateReview.ReviewText == null || updateReview.UserFullName == null || updateReview.BookTitle == null)
                {
                    _logger.LogError("Invalid data entered");

                    return StatusCode(StatusCodes.Status400BadRequest);
                    //return BadRequest("Invalid data entered");
                }
                else
                {
                    // Whether there is such a record in the database at all
                    var findResult = await _reviewService.GetAsync(updateReview.ReviewId);

                    if (findResult == null)
                    {
                        _logger.LogError
                            ("Entity with id: [{EntityId}] from [Reviews] not found",
                                updateReview.ReviewId);

                        return StatusCode(StatusCodes.Status404NotFound);
                        //return NotFound();
                    }
                    else
                    {
                        await _reviewService.UpdateAsync(updateReview);
                        _logger.LogInformation
                            ("Entity with id: [{EntityId}] were successfully updated from [Reviews]",
                                updateReview.ReviewId);

                        return StatusCode(StatusCodes.Status204NoContent);
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError
                    ("Error in [{ErrorClassName}]->[GetAllAsync] => {ErrorMessage}", this.GetType().Name, ex.Message);

                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Delete the Review by id
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// DELETE: ef/review/13ce1333-7b7c-4395-8565-0474a6ad05ad
        /// </remarks>
        /// <param name="id">Review id (Guid)</param>
        /// <returns>Returns NoContent</returns>
        /// <response code="204">Success</response>
        /// <response code="400">If invalid data entered</response>
        /// <response code="500">If it was not possible to adding element to the database</response>
        [HttpDelete("{id}")] // DELETE: ef/review/id
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> DeleteAsync(Guid id)
        {
            try
            {
                // Whether there is such a record in the database at all
                var findResult = await _reviewService.GetAsync(id);

                if (findResult == null)
                {
                    _logger.LogError("Entity with id: [{EntityId}] from [Reviews] not found", id);

                    return StatusCode(StatusCodes.Status404NotFound);
                    //return NotFound();
                }
                else
                {
                    await _reviewService.DeleteAsync(id);
                    _logger.LogInformation
                        ("Entity with id: [{EntityId}] were successfully deleted from [Reviews]", id);

                    return StatusCode(StatusCodes.Status204NoContent);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError
                    ("Error in [{ErrorClassName}]->[GetAllAsync] => {ErrorMessage}", this.GetType().Name, ex.Message);

                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
