using Microsoft.AspNetCore.Mvc;
using SchoolLibrary_Dapper.BLL.DTOs.ReviewDTOs;
using SchoolLibrary_Dapper.BLL.Services.Consracts;

namespace SchoolLibrary_Dapper.API.Constollers
{
    [Route("ado/[controller]")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private readonly IReviewService _reviewService;
        private readonly string TableName = nameof(ReviewController).Replace("Controller", "");

        public ReviewController(IReviewService reviewService)
        {
            _reviewService = reviewService;
        }


        [HttpGet] // GET: ado/review
        public async Task<ActionResult<IEnumerable<GetDTO_Review>>> GetAllAsync()
        {
            try
            {
                var result = await _reviewService.GetAllAsync();
                Console.WriteLine($"All entities were successfully extracted from [{TableName}]");

                return Ok(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in [{this.GetType().Name}]->[{nameof(GetAllAsync)}]\n " + ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")] // GET: ado/review/id
        public async Task<ActionResult<GetDTO_Review>> GetByIdAsync(Guid id)
        {
            try
            {
                var result = await _reviewService.GetAsync(id); // чи взагалі є такий запис в БД

                if (result == null)
                {
                    Console.WriteLine($"Entity [{id}] from [{TableName}] not found");
                    return NotFound();
                }
                
                
                Console.WriteLine(
                    $"Entity [{result.ReviewId}] were successfully extracted from [{TableName}]");
                return Ok(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in [{this.GetType().Name}]->[{nameof(GetByIdAsync)}]\n " + ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpPost] // POST: ado/review
        public async Task<ActionResult> AddAsync(InsertDTO_Review newEntity)
        {
            try
            {
                // Чи введені валідні данні
                if (newEntity?.ReviewText == null)
                {
                    return BadRequest("Invalid information");
                }
                else
                {
                    var id = await _reviewService.CreateAsync(newEntity);
                    Console.WriteLine($"Entity [{id}] successfully added to [{TableName}]");

                    return Ok(id);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in [{this.GetType().Name}]->[{nameof(AddAsync)}]\n " + ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpPut] // PUT: ado/review
        public async Task<ActionResult> UpdateAsync(UpdateDTO_Review upEntity)
        {
            try
            {
                // Чи введені валідні данні
                if (upEntity?.ReviewText == null)
                {
                    return BadRequest("Invalid information");
                }


                var result = await _reviewService.GetAsync(upEntity.ReviewId); // чи взагалі є такий запис в БД
                if (result == null)
                {
                    Console.WriteLine($"Entity [{upEntity.ReviewId}] from [{TableName}] not found");
                    return NotFound();
                }
                    
                    
                await _reviewService.UpdateAsync(upEntity);
                Console.WriteLine($"Entity {upEntity.ReviewId} successfully update to [{TableName}]");

                return Ok();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in [{this.GetType().Name}]->[{nameof(UpdateAsync)}]\n " + ex.Message);
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
                    Console.WriteLine($"Entity [{id}] from [{TableName}] not found");
                    return NotFound();
                }
                else
                {
                    await _reviewService.DeleteAsync(id);
                    Console.WriteLine($"Entity [{id}] successfully deleted to [{TableName}]");

                    return Ok();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in [{this.GetType().Name}]->[{nameof(DeleteByIdAsync)}]\n " + ex.Message);
                return BadRequest(ex.Message);
            }
        }
        
        
        
        [HttpGet("reviews-with-users-and-books")] // GET: ado/review/reviews-with-users-and-books
        public async Task<ActionResult<IEnumerable<GetDTO_Review>>> GetReviewsWithUsersAndBooksAsync()
        {
            try
            {
                var result = await _reviewService.GetReviewsWithUsersAndBooksAsync();
                Console.WriteLine($"All entities were successfully extracted from [{TableName}]");

                return Ok(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine
                    ($"Error in [{this.GetType().Name}]->[{nameof(GetReviewsWithUsersAndBooksAsync)}]\n " + ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("reviews-with-users-and-books/{id}")] // GET: ado/review/reviews-with-users-and-books/id
        public async Task<ActionResult<GetDTO_Review>> GetByIdReviewWithUserAndBookAsync(Guid id)
        {
            try
            {
                var result = await _reviewService.GetReviewWithUserAndBookAsync(id); // чи взагалі є такий запис в БД

                if (result == null)
                {
                    Console.WriteLine($"Entity [{id}] from [{TableName}] not found");
                    return NotFound();
                }
                
                
                Console.WriteLine($"Entity [{result.ReviewId}] were successfully extracted from [{TableName}]");
                return Ok(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine
                    ($"Error in [{this.GetType().Name}]->[{nameof(GetByIdReviewWithUserAndBookAsync)}]\n " + ex.Message);
                return BadRequest(ex.Message);
            }
        }
    }
}
