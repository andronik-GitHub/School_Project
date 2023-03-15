using Microsoft.AspNetCore.Mvc;
using SchoolLibrary_Rapper.BLL.DTO;
using SchoolLibrary_Rapper.BLL.Services.Consracts;

namespace SchoolLibrary_Dapper.API.Constollers
{
    [Route("ado/[controller]")]
    [ApiController]
    public class BookGenresController : ControllerBase
    {
        IBookGenresService _bookGenresService;

        public BookGenresController(IBookGenresService bookGenresService)
        {
            _bookGenresService = bookGenresService;
        }


        [HttpGet] // GET: ado/bookgenres
        public async Task<ActionResult<IEnumerable<BookGenresDTO>>> GetAllAsync()
        {
            try
            {
                var result = await _bookGenresService.GetAllAsync();
                Console.WriteLine("All BookGenres were successfully extracted from [BookGenres]");

                return Ok(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in [BookGenresConstoller]->[GetAllAsync]\n " + ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")] // GET: ado/bookgenres/id
        public async Task<ActionResult<BookGenresDTO>> GetByIdAsync(Guid id)
        {
            try
            {
                var result = await _bookGenresService.GetAsync(id); // чи взагалі є такий запис в БД

                if (result == null)
                {
                    Console.WriteLine($"BookGenres {id} from [BookGenres] not found");
                    return NotFound();
                }
                else
                {
                    Console.WriteLine(
                        $"BookGenres {result.BookId} {result.GenreId} were successfully extracted from [BookGenres]");
                    return Ok(result);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in [BookGenresController]->[GetByIdAsync]\n " + ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpPost] // POST: ado/bookgenres
        public async Task<ActionResult> AddAsync(BookGenresDTO newBookGenres)
        {
            try
            {
                var id = await _bookGenresService.CreateAsync(newBookGenres);
                Console.WriteLine($"BookGenres {id} successfully added to [BookGenres]");

                return Ok(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in [BookGenresConstoller]->[AddAsync]\n " + ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpPut] // PUT: ado/bookgenres
        public async Task<ActionResult> UpdateAsync(BookGenresDTO upBookGenres)
        {
            try
            {
                var result = await _bookGenresService.GetAsync(upBookGenres.BookId); // чи взагалі є такий запис в БД

                if (result == null)
                {
                    Console.WriteLine($"BookGenres {upBookGenres.BookId} from [BookGenres] not found");
                    return NotFound();
                }
                else
                {
                    await _bookGenresService.UpdateAsync(upBookGenres);
                    Console.WriteLine(
                        $"BookGenres {upBookGenres.BookId} {upBookGenres.GenreId} successfully update to [BookGenres]");

                    return Ok();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in [BookGenresConstoller]->[UpdateAsync]\n " + ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")] // DELETE: ado/bookgenres/id
        public async Task<ActionResult> DeleteByIdAsync(Guid id)
        {
            try
            {
                var result = await _bookGenresService.GetAsync(id); // чи взагалі є такий запис в БД

                if (result == null)
                {
                    Console.WriteLine($"BookGenres {id} from [BookGenres] not found");
                    return NotFound();
                }
                else
                {
                    await _bookGenresService.DeleteAsync(id);
                    Console.WriteLine($"BookGenres {id} successfully deleted to [BookGenres]");

                    return Ok();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in [BookGenresConstoller]->[UpdateAsync]\n " + ex.Message);
                return BadRequest(ex.Message);
            }
        }
    }
}
