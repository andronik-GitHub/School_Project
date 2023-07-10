using Microsoft.AspNetCore.Mvc;
using SchoolLibrary_Dapper.BLL.DTOs.BookGenreDTOs;
using SchoolLibrary_Dapper.BLL.Services.Consracts;

namespace SchoolLibrary_Dapper.API.Constollers
{
    [Route("ado/[controller]")]
    [ApiController]
    public class BookGenresController : ControllerBase
    {
        private readonly IBookGenresService _bookGenresService;
        private readonly string TableName = nameof(BookGenresController).Replace("Controller", "");

        public BookGenresController(IBookGenresService bookGenresService)
        {
            _bookGenresService = bookGenresService;
        }


        [HttpGet] // GET: ado/bookgenres
        public async Task<ActionResult<IEnumerable<GetDTO_BookGenres>>> GetAllAsync()
        {
            try
            {
                var result = await _bookGenresService.GetAllAsync();
                Console.WriteLine($"All entities were successfully extracted from [{TableName}]");

                return Ok(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in [{this.GetType().Name}]->[{nameof(GetAllAsync)}]\n " + ex.Message);
                return BadRequest(ex.Message);
            }
        }
        
        [HttpGet("include")] // GET: ado/bookgenres/include
        public async Task<ActionResult<IEnumerable<GetDTOInclude_BookGenres>>> Include_GetAllAsync()
        {
            try
            {
                var result = await _bookGenresService.Include_GetAllAsync();
                Console.WriteLine($"All entities were successfully extracted from [{TableName}]");

                return Ok(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in [{this.GetType().Name}]->[{nameof(Include_GetAllAsync)}]\n " + ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{BookId}/{GenreId}")] // GET: ado/bookgenres/bookid/genreid
        public async Task<ActionResult<GetDTO_BookGenres>> GetByIdAsync(Guid BookId, Guid GenreId)
        {
            try
            {
                var result = await _bookGenresService.GetAsync(BookId, GenreId); // чи взагалі є такий запис в БД

                if (result == null)
                {
                    Console.WriteLine($"Entity [{BookId} - {GenreId}] from [{TableName}] not found");
                    return NotFound();
                }
                
                
                Console.WriteLine(
                    $"Entity [{result.BookId} - {result.GenreId}] were successfully extracted from [{TableName}]");
                return Ok(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in [{this.GetType().Name}]->[{nameof(GetByIdAsync)}]\n " + ex.Message);
                return BadRequest(ex.Message);
            }
        }
        
        [HttpGet("include/{BookId}/{GenreId}")] // GET: ado/bookgenres/bookid/genreid
        public async Task<ActionResult<GetDTO_BookGenres>> Include_GetByIdAsync(Guid BookId, Guid GenreId)
        {
            try
            {
                var result = await _bookGenresService.Include_GetAsync(BookId, GenreId); // чи взагалі є такий запис в БД

                if (result == null)
                {
                    Console.WriteLine($"Entity [{BookId} - {GenreId}] from [{TableName}] not found");
                    return NotFound();
                }
                
                
                Console.WriteLine(
                    $"Entity [{result.BookId} - {result.GenreId}] were successfully extracted from [{TableName}]");
                return Ok(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in [{this.GetType().Name}]->[{nameof(Include_GetByIdAsync)}]\n " + ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpPost] // POST: ado/bookgenres
        public async Task<ActionResult> AddAsync(InsertDTO_BookGenres newGetDtoBookGenres)
        {
            try
            {
                var ids = await _bookGenresService.CreateAsync(newGetDtoBookGenres);
                Console.WriteLine($"Entity [{ids.Item1} - {ids.Item2}] successfully added to [{TableName}]");

                return Ok(ids);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in [{this.GetType().Name}]->[{nameof(AddAsync)}]\n " + ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpPut] // PUT: ado/bookgenres
        public async Task<ActionResult> UpdateAsync(UpdateDTO_BookGenres upGetDtoBookGenres)
        {
            try
            {
                var result = await _bookGenresService.GetAsync(upGetDtoBookGenres.BookId, upGetDtoBookGenres.GenreId); // чи взагалі є такий запис в БД

                if (result == null)
                {
                    Console.WriteLine
                        ($"Entity [{upGetDtoBookGenres.BookId} - {upGetDtoBookGenres.GenreId}] from [{TableName}] not found");
                    return NotFound();
                }
                
                
                await _bookGenresService.UpdateAsync(upGetDtoBookGenres);
                Console.WriteLine(
                    $"BookGenres {upGetDtoBookGenres.BookId} {upGetDtoBookGenres.GenreId} successfully update to [BookGenres]");

                return Ok();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in [BookGenresConstoller]->[UpdateAsync]\n " + ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{BookId}/{GenreId}")] // DELETE: ado/bookgenres/bookid/genreid
        public async Task<ActionResult> DeleteByIdAsync(Guid BookId, Guid GenreId)
        {
            try
            {
                var result = await _bookGenresService.GetAsync(BookId, GenreId); // чи взагалі є такий запис в БД

                if (result == null)
                {
                    Console.WriteLine($"Entity [{BookId} - {GenreId}] from [{TableName}] not found");
                    return NotFound();
                }
                
                
                await _bookGenresService.DeleteAsync(BookId, GenreId);
                Console.WriteLine($"Entity [{BookId} - {GenreId}] successfully deleted to [{TableName}]");

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
