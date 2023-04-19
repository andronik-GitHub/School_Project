using Microsoft.AspNetCore.Mvc;
using SchoolLibrary_Dapper.BLL.DTO;
using SchoolLibrary_Dapper.BLL.Services.Consracts;

namespace SchoolLibrary_Dapper.API.Constollers
{
    [Route("ado/[controller]")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        IAuthorService _authorService;

        public AuthorController(IAuthorService authorService)
        {
            _authorService = authorService;
        }



        [HttpGet] // GET: ado/author
        public async Task<ActionResult<IEnumerable<AuthorDTO>>> GetAllAsync()
        {
            try
            {
                var result = await _authorService.GetAllAsync();
                Console.WriteLine("All Authors were successfully extracted from [Authors]");

                return Ok(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in [AuthorConstoller]->[GetAllAsync]\n " + ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")] // GET: ado/author/id
        public async Task<ActionResult<AuthorDTO>> GetByIdAsync(Guid id)
        {
            try
            {
                var result = await _authorService.GetAsync(id); // чи взагалі є такий запис в БД

                if (result == null)
                {
                    Console.WriteLine($"Author {id} from [Authors] not found");
                    return NotFound();
                }
                else
                {
                    Console.WriteLine($"Author {result.AuthorId} were successfully extracted from [Authors]");
                    return Ok(result);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in [AuthorController]->[GetByIdAsync]\n " + ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpPost] // POST: ado/author
        public async Task<ActionResult> AddAsync(AuthorDTO newAuthor)
        {
            try
            {
                // Чи введені валідні данні
                if (newAuthor.FirstName == null || newAuthor.LastName == null)
                {
                    return BadRequest("Invalid information");
                }
                else
                {
                    var id = await _authorService.CreateAsync(newAuthor);
                    Console.WriteLine($"Author {id} successfully added to [Authors]");

                    return Ok(id);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in [AuthorConstoller]->[AddAsync]\n " + ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpPut] // PUT: ado/author
        public async Task<ActionResult> UpdateAsync(AuthorDTO upAuthor)
        {
            try
            {
                // Чи введені валідні данні
                if (upAuthor.FirstName == null || upAuthor.LastName == null)
                {
                    return BadRequest("Invalid information");
                }
                else
                {
                    var result = await _authorService.GetAsync(upAuthor.AuthorId); // чи взагалі є такий запис в БД

                    if (result == null)
                    {
                        Console.WriteLine($"Author {upAuthor.AuthorId} from [Authors] not found");
                        return NotFound();
                    }
                    else
                    {
                        await _authorService.UpdateAsync(upAuthor);
                        Console.WriteLine($"Author {upAuthor.AuthorId} successfully update to [Authors]");

                        return Ok();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in [AuthorConstoller]->[UpdateAsync]\n " + ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")] // DELETE: ado/author/id
        public async Task<ActionResult> DeleteByIdAsync(Guid id)
        {
            try
            {
                var result = await _authorService.GetAsync(id); // чи взагалі є такий запис в БД

                if (result == null)
                {
                    Console.WriteLine($"Author {id} from [Authors] not found");
                    return NotFound();
                }
                else
                {
                    await _authorService.DeleteAsync(id);
                    Console.WriteLine($"Author {id} successfully deleted to [Authors]");

                    return Ok();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in [AuthorConstoller]->[UpdateAsync]\n " + ex.Message);
                return BadRequest(ex.Message);
            }
        }
    }
}
