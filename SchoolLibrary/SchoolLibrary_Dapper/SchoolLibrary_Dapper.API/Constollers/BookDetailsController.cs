using Microsoft.AspNetCore.Mvc;
using SchoolLibrary_Dapper.BLL.DTOs.BookDetailsDTOs;
using SchoolLibrary_Dapper.BLL.Services.Consracts;

namespace SchoolLibrary_Dapper.API.Constollers
{
    [Route("ado/[controller]")]
    [ApiController]
    public class BookDetailsController : ControllerBase
    {
        private readonly IBookDetailsService _bookDetailsService;
        private readonly string TableName = nameof(BookDetailsController).Replace("Controller", "");

        public BookDetailsController(IBookDetailsService bookDetailsService)
        {
            _bookDetailsService = bookDetailsService;
        }



        [HttpGet] // GET: ado/bookdetails
        public async Task<ActionResult<IEnumerable<GetDTO_BookDetails>>> GetAllAsync()
        {
            try
            {
                var result = await _bookDetailsService.GetAllAsync();
                Console.WriteLine($"All entities were successfully extracted from [{TableName}]");

                return Ok(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in [{this.GetType().Name}]->[{nameof(GetAllAsync)}]\n " + ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("{id}")] // GET: ado/bookdetails/id
        public async Task<ActionResult<GetDTO_BookDetails>> GetByIdAsync(Guid id)
        {
            try
            {
                var result = await _bookDetailsService.GetAsync(id); // чи взагалі є такий запис в БД

                if (result == null)
                {
                    Console.WriteLine($"Entity [{id}] from [{TableName}] not found");
                    return NotFound();
                }
                    
                    
                Console.WriteLine(
                    $"Entity [{result.BookId}] were successfully extracted from [{TableName}]");
                return Ok(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in [{this.GetType().Name}]->[{nameof(GetByIdAsync)}]\n " + ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpPost] // POST: ado/bookdetails
        public async Task<ActionResult> AddAsync(InsertDTO_BookDetails newEntity)
        {
            try
            {
                // Чи введені валідні данні
                if (newEntity?.Language == null || newEntity?.Format == null)
                {
                    return BadRequest("Invalid information");
                }
                
                
                var id = await _bookDetailsService.CreateAsync(newEntity);
                Console.WriteLine($"Entity [{id}] successfully added to [{TableName}]");

                return Ok(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in [{this.GetType().Name}]->[{nameof(AddAsync)}]\n " + ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpPut] // PUT: ado/bookdetails
        public async Task<ActionResult> UpdateAsync(UpdateDTO_BookDetails upEntity)
        {
            try
            {
                // Чи введені валідні данні
                if (upEntity?.Language == null || upEntity?.Format == null)
                {
                    return BadRequest("Invalid information");
                }
                    
                    
                var result = await _bookDetailsService.GetAsync(upEntity.BookDetailId); // чи взагалі є такий запис в БД

                if (result == null)
                {
                    Console.WriteLine($"Entity [{upEntity.BookDetailId}] from [{TableName}] not found");
                    return NotFound();
                }
                    
                    
                await _bookDetailsService.UpdateAsync(upEntity);
                Console.WriteLine($"Entity {upEntity.BookDetailId} successfully update to [{TableName}]");

                return Ok();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in [{this.GetType().Name}]->[{nameof(UpdateAsync)}]\n " + ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")] // DELETE: ado/bookdetails/id
        public async Task<ActionResult> DeleteByIdAsync(Guid id)
        {
            try
            {
                var result = await _bookDetailsService.GetAsync(id); // чи взагалі є такий запис в БД

                if (result == null)
                {
                    Console.WriteLine($"Entity [{id}] from [{TableName}] not found");
                    return NotFound();
                }
                else
                {
                    await _bookDetailsService.DeleteAsync(id);
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
    }
}
