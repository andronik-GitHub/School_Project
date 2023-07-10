using Microsoft.AspNetCore.Mvc;
using SchoolLibrary_Dapper.BLL.DTOs.BookAuthorDTOs;
using SchoolLibrary_Dapper.BLL.Services.Consracts;

namespace SchoolLibrary_Dapper.API.Constollers
{
    [Route("ado/[controller]")]
    [ApiController]
    public class BookAuthorsController : ControllerBase
    {
        private readonly IBookAuthorsService _bookAuthorsService;
        private readonly string TableName = nameof(BookAuthorsController).Replace("Controller", "");

        public BookAuthorsController(IBookAuthorsService bookAuthorsService)
        {
            _bookAuthorsService = bookAuthorsService;
        }


        [HttpGet] // GET: ado/bookauthors
        public async Task<ActionResult<IEnumerable<GetDTO_BookAuthors>>> GetAllAsync()
        {
            try
            {
                var result = await _bookAuthorsService.GetAllAsync();
                Console.WriteLine($"All entities were successfully extracted from [{TableName}]");

                return Ok(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in [{this.GetType().Name}]->[{nameof(GetAllAsync)}]\n " + ex.Message);
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("include")] // GET: ado/bookauthors/include
        public async Task<ActionResult<IEnumerable<GetDTOInclude_BookAuthors>>> Include_GetAllAsync()
        {
            try
            {
                var result = await _bookAuthorsService.Include_GetAllAsync();
                Console.WriteLine($"All entities were successfully extracted from [{TableName}]");

                return Ok(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in [{this.GetType().Name}]->[{nameof(Include_GetAllAsync)}]\n " + ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{BookId}/{AuthorId}")] // GET: ado/bookauthors/bookid/authorid
        public async Task<ActionResult<GetDTO_BookAuthors>> GetByIdAsync(Guid BookId, Guid AuthorId)
        {
            try
            {
                var result = await _bookAuthorsService.GetAsync(BookId, AuthorId); // чи взагалі є такий запис в БД

                if (result == null)
                {
                    Console.WriteLine($"Entity [{BookId} - {AuthorId}] from [{TableName}] not found");
                    return NotFound();
                }
                
                
                Console.WriteLine(
                    $"Entity [{result.BookId} - {result.AuthorId}] were successfully extracted from [{TableName}]");
                return Ok(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in [{this.GetType().Name}]->[{nameof(GetByIdAsync)}]\n " + ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("include/{BookId}/{AuthorId}")] // GET: ado/bookauthors/include/bookid/authorid
        public async Task<ActionResult<GetDTOInclude_BookAuthors>> Include_GetByIdAsync(Guid BookId, Guid AuthorId)
        {
            try
            {
                var result = await _bookAuthorsService.Include_GetAsync(BookId, AuthorId); // чи взагалі є такий запис в БД

                if (result == null)
                {
                    Console.WriteLine($"Entity [{BookId} - {AuthorId}] from [{TableName}] not found");
                    return NotFound();
                }
                
                
                Console.WriteLine(
                    $"Entity [{result.BookId} - {result.AuthorId}] were successfully extracted from [{TableName}]");
                return Ok(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in [{this.GetType().Name}]->[{nameof(Include_GetByIdAsync)}]\n " + ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpPost] // POST: ado/bookauthors
        public async Task<ActionResult> AddAsync(InsertDTO_BookAuthors newGetDtoBookGenres)
        {
            try
            {
                var ids = await _bookAuthorsService.CreateAsync(newGetDtoBookGenres);
                Console.WriteLine($"Entity [{ids.Item1} - {ids.Item2}] successfully added to [{TableName}]");

                return Ok(ids);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in [{this.GetType().Name}]->[{nameof(AddAsync)}]\n " + ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpPut] // PUT: ado/bookauthors
        public async Task<ActionResult> UpdateAsync(UpdateDTO_BookAuthors upGetDtoBookAuthors)
        {
            try
            {
                var result = await _bookAuthorsService.GetAsync(upGetDtoBookAuthors.BookId, upGetDtoBookAuthors.AuthorId); // чи взагалі є такий запис в БД

                if (result == null)
                {
                    Console.WriteLine
                        ($"Entity [{upGetDtoBookAuthors.BookId} - {upGetDtoBookAuthors.AuthorId}] from [{TableName}] not found");
                    return NotFound();
                }
                
                
                await _bookAuthorsService.UpdateAsync(upGetDtoBookAuthors);
                Console.WriteLine(
                    $"Entity {upGetDtoBookAuthors.BookId} {upGetDtoBookAuthors.AuthorId} successfully update to [{TableName}]");

                return Ok();
                
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in [{this.GetType().Name}]->[{nameof(UpdateAsync)}]\n " + ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{BookId}/{AuthorId}")] // DELETE: ado/bookauthors/bookid/authorid
        public async Task<ActionResult> DeleteByIdAsync(Guid BookId, Guid AuthorId)
        {
            try
            {
                var result = await _bookAuthorsService.GetAsync(BookId, AuthorId); // чи взагалі є такий запис в БД

                if (result == null)
                {
                    Console.WriteLine($"Entity [{BookId} - {AuthorId}] from [{TableName}] not found");
                    return NotFound();
                }
                
                await _bookAuthorsService.DeleteAsync(BookId, AuthorId);
                Console.WriteLine($"Entity [{BookId} - {AuthorId}] successfully deleted to [{TableName}]");

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
