using Microsoft.AspNetCore.Mvc;
using SchoolLibrary_Dapper.BLL.DTO;
using SchoolLibrary_Dapper.BLL.Services.Consracts;

namespace SchoolLibrary_Dapper.API.Constollers
{
    [Route("ado/[controller]")]
    [ApiController]
    public class BookConstoller : ControllerBase
    {
        IBookService _bookService;

        public BookConstoller(IBookService bookService)
        {
            _bookService = bookService;
        }


        [HttpGet] // GET: ado/book
        public async Task<ActionResult<IEnumerable<BookDTO>>> GetAllAsync()
        {
            try
            {
                var result = await _bookService.GetAllAsync();
                Console.WriteLine("All Books were successfully extracted from [Books]");

                return Ok(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in [BookConstoller]->[GetAllAsync]\n " + ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")] // GET: ado/book/id
        public async Task<ActionResult<BookDTO>> GetByIdAsync(Guid id)
        {
            try
            {
                var result = await _bookService.GetAsync(id); // чи взагалі є такий запис в БД

                if (result == null)
                {
                    Console.WriteLine($"Book {id} from [Books] not found");
                    return NotFound();
                }
                else
                {
                    Console.WriteLine($"Book {result.BookId} were successfully extracted from [Books]");
                    return Ok(result);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in [BookController]->[GetByIdAsync]\n " + ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpPost] // POST: ado/book
        public async Task<ActionResult> AddAsync(BookDTO newBook)
        {
            try
            {
                // Чи введені валідні данні
                if (newBook.Title == null || newBook.ISBN == null)
                {
                    return BadRequest("Invalid information");
                }
                else
                {
                    var id = await _bookService.CreateAsync(newBook);
                    Console.WriteLine($"Book {id} successfully added to [Books]");

                    return Ok(id);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in [BookConstoller]->[AddAsync]\n " + ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpPut] // PUT: ado/book
        public async Task<ActionResult> UpdateAsync(BookDTO upBook)
        {
            try
            {
                // Чи введені валідні данні
                if (upBook.Title == null || upBook.ISBN == null)
                {
                    return BadRequest("Invalid information");
                }
                else
                {
                    var result = await _bookService.GetAsync(upBook.BookId); // чи взагалі є такий запис в БД

                    if (result == null)
                    {
                        Console.WriteLine($"Book {upBook.BookId} from [Books] not found");
                        return NotFound();
                    }
                    else
                    {
                        await _bookService.UpdateAsync(upBook);
                        Console.WriteLine($"Book {upBook.BookId} successfully update to [Books]");

                        return Ok();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in [BookConstoller]->[UpdateAsync]\n " + ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")] // DELETE: ado/book/id
        public async Task<ActionResult> DeleteByIdAsync(Guid id)
        {
            try
            {
                var result = await _bookService.GetAsync(id); // чи взагалі є такий запис в БД

                if (result == null)
                {
                    Console.WriteLine($"Book {id} from [Books] not found");
                    return NotFound();
                }
                else
                {
                    await _bookService.DeleteAsync(id);
                    Console.WriteLine($"Book {id} successfully deleted to [Users]");

                    return Ok();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in [BookConstoller]->[UpdateAsync]\n " + ex.Message);
                return BadRequest(ex.Message);
            }
        }
    }
}
