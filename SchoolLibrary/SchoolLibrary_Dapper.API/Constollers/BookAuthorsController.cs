using Microsoft.AspNetCore.Mvc;
using SchoolLibrary_Rapper.BLL.DTO;
using SchoolLibrary_Rapper.BLL.Services.Consracts;

namespace SchoolLibrary_Dapper.API.Constollers
{
    [Route("ado/[controller]")]
    [ApiController]
    public class BookAuthorsController : ControllerBase
    {
        IBookAuthorsService _bookAuthorsService;

        public BookAuthorsController(IBookAuthorsService bookAuthorsService)
        {
            _bookAuthorsService = bookAuthorsService;
        }


        [HttpGet] // GET: ado/bookauthors
        public async Task<ActionResult<IEnumerable<BookAuthorsDTO>>> GetAllAsync()
        {
            try
            {
                var result = await _bookAuthorsService.GetAllAsync();
                Console.WriteLine("All BookAuthors were successfully extracted from [BookAuthors]");

                return Ok(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in [BookAuthorsConstoller]->[GetAllAsync]\n " + ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")] // GET: ado/bookauthors/id
        public async Task<ActionResult<BookAuthorsDTO>> GetByIdAsync(Guid id)
        {
            try
            {
                var result = await _bookAuthorsService.GetAsync(id); // чи взагалі є такий запис в БД

                if (result == null)
                {
                    Console.WriteLine($"BookAuthors {id} from [BookAuthors] not found");
                    return NotFound();
                }
                else
                {
                    Console.WriteLine(
                        $"BookAuthors {result.BookId} {result.AuthorId} were successfully extracted from [BookAuthors]");
                    return Ok(result);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in [BookAuthorsController]->[GetByIdAsync]\n " + ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpPost] // POST: ado/bookauthors
        public async Task<ActionResult> AddAsync(BookAuthorsDTO newBookGenres)
        {
            try
            {
                var id = await _bookAuthorsService.CreateAsync(newBookGenres);
                Console.WriteLine($"BookAuthors {id} successfully added to [BookAuthors]");

                return Ok(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in [BookAuthorsConstoller]->[AddAsync]\n " + ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpPut] // PUT: ado/bookauthors
        public async Task<ActionResult> UpdateAsync(BookAuthorsDTO upBookAuthors)
        {
            try
            {
                var result = await _bookAuthorsService.GetAsync(upBookAuthors.BookId); // чи взагалі є такий запис в БД

                if (result == null)
                {
                    Console.WriteLine($"BookAuthors {upBookAuthors.BookId} from [BookAuthors] not found");
                    return NotFound();
                }
                else
                {
                    await _bookAuthorsService.UpdateAsync(upBookAuthors);
                    Console.WriteLine(
                        $"BookAuthors {upBookAuthors.BookId} {upBookAuthors.AuthorId} successfully update to [BookAuthors]");

                    return Ok();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in [BookAuthorsConstoller]->[UpdateAsync]\n " + ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")] // DELETE: ado/bookauthors/id
        public async Task<ActionResult> DeleteByIdAsync(Guid id)
        {
            try
            {
                var result = await _bookAuthorsService.GetAsync(id); // чи взагалі є такий запис в БД

                if (result == null)
                {
                    Console.WriteLine($"BookAuthors {id} from [BookAuthors] not found");
                    return NotFound();
                }
                else
                {
                    await _bookAuthorsService.DeleteAsync(id);
                    Console.WriteLine($"BookAuthors {id} successfully deleted to [BookAuthors]");

                    return Ok();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in [BookAuthorsConstoller]->[UpdateAsync]\n " + ex.Message);
                return BadRequest(ex.Message);
            }
        }
    }
}
