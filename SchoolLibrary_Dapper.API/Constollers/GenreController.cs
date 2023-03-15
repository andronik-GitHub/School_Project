using Microsoft.AspNetCore.Mvc;
using SchoolLibrary_Rapper.BLL.DTO;
using SchoolLibrary_Rapper.BLL.Services.Consracts;

namespace SchoolLibrary_Dapper.API.Constollers
{
    [Route("ado/[controller]")]
    [ApiController]
    public class GenreController : ControllerBase
    {
        IGenreService _genreService;

        public GenreController(IGenreService genreService)
        {
            _genreService = genreService;
        }


        [HttpGet] // GET: ado/genre
        public async Task<ActionResult<IEnumerable<GenreDTO>>> GetAllAsync()
        {
            try
            {
                var result = await _genreService.GetAllAsync();
                Console.WriteLine("All Genre were successfully extracted from [Genre]");

                return Ok(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in [GenreConstoller]->[GetAllAsync]\n " + ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")] // GET: ado/genre/id
        public async Task<ActionResult<GenreDTO>> GetByIdAsync(Guid id)
        {
            try
            {
                var result = await _genreService.GetAsync(id); // чи взагалі є такий запис в БД

                if (result == null)
                {
                    Console.WriteLine($"Genre {id} from [Genres] not found");
                    return NotFound();
                }
                else
                {
                    Console.WriteLine($"Genre {result.GenreId} were successfully extracted from [Genres]");
                    return Ok(result);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in [GenreController]->[GetByIdAsync]\n " + ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpPost] // POST: ado/genre
        public async Task<ActionResult> AddAsync(GenreDTO newGenre)
        {
            try
            {
                // Чи введені валідні данні
                if (newGenre.Name == null)
                {
                    return BadRequest("Invalid information");
                }
                else
                {
                    var id = await _genreService.CreateAsync(newGenre);
                    Console.WriteLine($"Genre {id} successfully added to [Genres]");

                    return Ok(id);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in [GenreConstoller]->[AddAsync]\n " + ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpPut] // PUT: ado/genre
        public async Task<ActionResult> UpdateAsync(GenreDTO upGenre)
        {
            try
            {
                // Чи введені валідні данні
                if (upGenre.Name == null)
                {
                    return BadRequest("Invalid information");
                }
                else
                {
                    var result = await _genreService.GetAsync(upGenre.GenreId); // чи взагалі є такий запис в БД

                    if (result == null)
                    {
                        Console.WriteLine($"Genre {upGenre.GenreId} from [Genres] not found");
                        return NotFound();
                    }
                    else
                    {
                        await _genreService.UpdateAsync(upGenre);
                        Console.WriteLine($"Genre {upGenre.GenreId} successfully update to [Genres]");

                        return Ok();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in [GenreConstoller]->[UpdateAsync]\n " + ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")] // DELETE: ado/genre/id
        public async Task<ActionResult> DeleteByIdAsync(Guid id)
        {
            try
            {
                var result = await _genreService.GetAsync(id); // чи взагалі є такий запис в БД

                if (result == null)
                {
                    Console.WriteLine($"Genre {id} from [Genres] not found");
                    return NotFound();
                }
                else
                {
                    await _genreService.DeleteAsync(id);
                    Console.WriteLine($"Genre {id} successfully deleted to [Genres]");

                    return Ok();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in [GenreConstoller]->[UpdateAsync]\n " + ex.Message);
                return BadRequest(ex.Message);
            }
        }
    }
}