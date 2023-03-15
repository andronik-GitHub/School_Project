using Microsoft.AspNetCore.Mvc;
using SchoolLibrary_Rapper.BLL.DTO;
using SchoolLibrary_Rapper.BLL.Services.Consracts;

namespace SchoolLibrary_Dapper.API.Constollers
{
    [Route("ado/[controller]")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        IReviewService _reviewService;

        public ReviewController(IReviewService reviewService)
        {
            _reviewService = reviewService;
        }


        [HttpGet] // GET: ado/review
        public async Task<ActionResult<IEnumerable<ReviewDTO>>> GetAllAsync()
        {
            try
            {
                var result = await _reviewService.GetAllAsync();
                Console.WriteLine("All Review were successfully extracted from [Review]");

                return Ok(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in [ReviewConstoller]->[GetAllAsync]\n " + ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")] // GET: ado/review/id
        public async Task<ActionResult<ReviewDTO>> GetByIdAsync(Guid id)
        {
            try
            {
                var result = await _reviewService.GetAsync(id); // чи взагалі є такий запис в БД

                if (result == null)
                {
                    Console.WriteLine($"Review {id} from [Reviews] not found");
                    return NotFound();
                }
                else
                {
                    Console.WriteLine($"Review {result.ReviewId} were successfully extracted from [Reviews]");
                    return Ok(result);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in [ReviewController]->[GetByIdAsync]\n " + ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpPost] // POST: ado/review
        public async Task<ActionResult> AddAsync(ReviewDTO newReview)
        {
            try
            {
                // Чи введені валідні данні
                if (newReview.ReviewText == null)
                {
                    return BadRequest("Invalid information");
                }
                else
                {
                    var id = await _reviewService.CreateAsync(newReview);
                    Console.WriteLine($"Review {id} successfully added to [Reviews]");

                    return Ok(id);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in [ReviewConstoller]->[AddAsync]\n " + ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpPut] // PUT: ado/review
        public async Task<ActionResult> UpdateAsync(ReviewDTO upReview)
        {
            try
            {
                // Чи введені валідні данні
                if (upReview.ReviewText == null)
                {
                    return BadRequest("Invalid information");
                }
                else
                {
                    var result = await _reviewService.GetAsync(upReview.ReviewId); // чи взагалі є такий запис в БД

                    if (result == null)
                    {
                        Console.WriteLine($"Review {upReview.ReviewId} from [Reviews] not found");
                        return NotFound();
                    }
                    else
                    {
                        await _reviewService.UpdateAsync(upReview);
                        Console.WriteLine($"Review {upReview.ReviewId} successfully update to [Reviews]");

                        return Ok();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in [ReviewConstoller]->[UpdateAsync]\n " + ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")] // DELETE: ado/review/id
        public async Task<ActionResult> DeleteByIdAsync(Guid id)
        {
            try
            {
                var result = await _reviewService.GetAsync(id); // чи взагалі є такий запис в БД

                if (result == null)
                {
                    Console.WriteLine($"Review {id} from [Reviews] not found");
                    return NotFound();
                }
                else
                {
                    await _reviewService.DeleteAsync(id);
                    Console.WriteLine($"Review {id} successfully deleted to [Reviews]");

                    return Ok();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in [ReviewConstoller]->[UpdateAsync]\n " + ex.Message);
                return BadRequest(ex.Message);
            }
        }
    }
}
