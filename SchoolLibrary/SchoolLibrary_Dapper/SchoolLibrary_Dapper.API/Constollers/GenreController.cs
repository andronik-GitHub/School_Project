using Microsoft.AspNetCore.Mvc;
using SchoolLibrary_Dapper.BLL.DTOs.GenreDTOs;
using SchoolLibrary_Dapper.BLL.Services.Consracts;

namespace SchoolLibrary_Dapper.API.Constollers
{
    [Route("ado/[controller]")]
    [ApiController]
    public class GenreController : ControllerBase
    {
        private readonly IGenreService _genreService;
        private readonly string TableName = nameof(GenreController).Replace("Controller", "");

        public GenreController(IGenreService genreService)
        {
            _genreService = genreService;
        }


        [HttpGet] // GET: ado/genre
        public async Task<ActionResult<IEnumerable<GetDTO_Genre>>> GetAllAsync()
        {
            try
            {
                var result = await _genreService.GetAllAsync();
                Console.WriteLine($"All entities were successfully extracted from [{TableName}]");

                return Ok(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in [{this.GetType().Name}]->[{nameof(GetAllAsync)}]\n " + ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")] // GET: ado/genre/id
        public async Task<ActionResult<GetDTO_Genre>> GetByIdAsync(Guid id)
        {
            try
            {
                var result = await _genreService.GetAsync(id); // чи взагалі є такий запис в БД

                if (result == null)
                {
                    Console.WriteLine($"Entity [{id}] from [{TableName}] not found");
                    return NotFound();
                }
                
                
                Console.WriteLine(
                    $"Entity [{result.GenreId}] were successfully extracted from [{TableName}]");
                return Ok(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in [{this.GetType().Name}]->[{nameof(GetByIdAsync)}]\n " + ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpPost] // POST: ado/genre
        public async Task<ActionResult> AddAsync(InsertDTO_Genre newEntity)
        {
            try
            {
                // Чи введені валідні данні
                if (newEntity?.Name == null)
                {
                    return BadRequest("Invalid information");
                }


                var id = await _genreService.CreateAsync(newEntity);
                Console.WriteLine($"Entity [{id}] successfully added to [{TableName}]");

                return Ok(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in [{this.GetType().Name}]->[{nameof(AddAsync)}]\n " + ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpPut] // PUT: ado/genre
        public async Task<ActionResult> UpdateAsync(UpdateDTO_Genre upEntity)
        {
            try
            {
                // Чи введені валідні данні
                if (upEntity?.Name == null)
                {
                    return BadRequest("Invalid information");
                }
                else
                {
                    var result = await _genreService.GetAsync(upEntity.GenreId); // чи взагалі є такий запис в БД

                    if (result == null)
                    {
                        Console.WriteLine($"Entity [{upEntity.GenreId}] from [{TableName}] not found");
                        return NotFound();
                    }
                    else
                    {
                        await _genreService.UpdateAsync(upEntity);
                        Console.WriteLine($"Entity {upEntity.GenreId} successfully update to [{TableName}]");

                        return Ok();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in [{this.GetType().Name}]->[{nameof(UpdateAsync)}]\n " + ex.Message);
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
                    Console.WriteLine($"Entity [{id}] from [{TableName}] not found");
                    return NotFound();
                }


                await _genreService.DeleteAsync(id);
                Console.WriteLine($"Entity [{id}] successfully deleted to [{TableName}]");

                return Ok();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in [{this.GetType().Name}]->[{nameof(DeleteByIdAsync)}]\n " + ex.Message);
                return BadRequest(ex.Message);
            }
        }
    }
}