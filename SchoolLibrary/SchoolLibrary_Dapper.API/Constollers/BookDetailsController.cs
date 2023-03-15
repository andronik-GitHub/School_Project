using Microsoft.AspNetCore.Mvc;
using SchoolLibrary_Dapper.BLL.DTO;
using SchoolLibrary_Dapper.BLL.Services.Consracts;

namespace SchoolLibrary_Dapper.API.Constollers
{
    [Route("ado/[controller]")]
    [ApiController]
    public class BookDetailsController : ControllerBase
    {
        IBookDetailsService _bookDetailsService;

        public BookDetailsController(IBookDetailsService bookDetailsService)
        {
            _bookDetailsService = bookDetailsService;
        }



        [HttpGet] // GET: ado/bookdetails
        public async Task<ActionResult<IEnumerable<BookDetailsDTO>>> GetAllAsync()
        {
            try
            {
                var result = await _bookDetailsService.GetAllAsync();
                Console.WriteLine("All BookDetails were successfully extracted from [BookDetails]");

                return Ok(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in [BookDetailsConstoller]->[GetAllAsync]\n " + ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")] // GET: ado/bookdetails/id
        public async Task<ActionResult<BookDetailsDTO>> GetByIdAsync(Guid id)
        {
            try
            {
                var result = await _bookDetailsService.GetAsync(id); // чи взагалі є такий запис в БД

                if (result == null)
                {
                    Console.WriteLine($"BookDetails {id} from [BookDetails] not found");
                    return NotFound();
                }
                else
                {
                    Console.WriteLine($"BookDetails {result.BookDetailId} were successfully extracted from [BookDetails]");
                    return Ok(result);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in [AuthorController]->[GetByIdAsync]\n " + ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpPost] // POST: ado/bookdetails
        public async Task<ActionResult> AddAsync(BookDetailsDTO newBookDetails)
        {
            try
            {
                // Чи введені валідні данні
                if (newBookDetails.Language == null || newBookDetails.Format == null)
                {
                    return BadRequest("Invalid information");
                }
                else
                {
                    var id = await _bookDetailsService.CreateAsync(newBookDetails);
                    Console.WriteLine($"BookDetails {id} successfully added to [BookDetails]");

                    return Ok(id);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in [BookDetailsConstoller]->[AddAsync]\n " + ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpPut] // PUT: ado/bookdetails
        public async Task<ActionResult> UpdateAsync(BookDetailsDTO upBookDetails)
        {
            try
            {
                // Чи введені валідні данні
                if (upBookDetails.Language == null || upBookDetails.Format == null)
                {
                    return BadRequest("Invalid information");
                }
                else
                {
                    var result = await _bookDetailsService.GetAsync(upBookDetails.BookDetailId); // чи взагалі є такий запис в БД

                    if (result == null)
                    {
                        Console.WriteLine($"BookDetails {upBookDetails.BookDetailId} from [BookDetails] not found");
                        return NotFound();
                    }
                    else
                    {
                        await _bookDetailsService.UpdateAsync(upBookDetails);
                        Console.WriteLine($"BookDetails {upBookDetails.BookDetailId} successfully update to [BookDetails]");

                        return Ok();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in [BookDetailsConstoller]->[UpdateAsync]\n " + ex.Message);
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
                    Console.WriteLine($"BookDetails {id} from [BookDetails] not found");
                    return NotFound();
                }
                else
                {
                    await _bookDetailsService.DeleteAsync(id);
                    Console.WriteLine($"BookDetails {id} successfully deleted to [BookDetails]");

                    return Ok();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in [BookDetailsConstoller]->[UpdateAsync]\n " + ex.Message);
                return BadRequest(ex.Message);
            }
        }
    }
}
