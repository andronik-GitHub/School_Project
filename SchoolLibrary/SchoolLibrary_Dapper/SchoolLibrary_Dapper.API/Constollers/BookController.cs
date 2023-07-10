using Microsoft.AspNetCore.Mvc;
using SchoolLibrary_Dapper.BLL.DTOs.BookDTOs;
using SchoolLibrary_Dapper.BLL.Services.Consracts;

namespace SchoolLibrary_Dapper.API.Constollers
{
    [Route("ado/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookService _bookService;
        private readonly string TableName = nameof(BookController).Replace("Controller", "");

        public BookController(IBookService bookService)
        {
            _bookService = bookService;
        }


        [HttpGet] // GET: ado/book
        public async Task<ActionResult<IEnumerable<GetDTO_Book>>> GetAllAsync()
        {
            try
            {
                var result = await _bookService.GetAllAsync();
                Console.WriteLine($"All entities were successfully extracted from [{TableName}]");

                return Ok(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in [{this.GetType().Name}]->[{nameof(GetAllAsync)}]\n " + ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("{id}")] // GET: ado/book/id
        public async Task<ActionResult<GetDTO_Book>> GetByIdAsync(Guid id)
        {
            try
            {
                var result = await _bookService.GetAsync(id); // чи взагалі є такий запис в БД

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

        [HttpPost] // POST: ado/book
        public async Task<ActionResult> AddAsync(InsertDTO_Book newEntity)
        {
            try
            {
                // Чи введені валідні данні
                if (newEntity?.Title == null || newEntity?.ISBN == null)
                {
                    return BadRequest("Invalid information");
                }
                
                
                var id = await _bookService.CreateAsync(newEntity);
                Console.WriteLine($"Entity [{id}] successfully added to [{TableName}]");

                return Ok(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in [{this.GetType().Name}]->[{nameof(AddAsync)}]\n " + ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpPut] // PUT: ado/book
        public async Task<ActionResult> UpdateAsync(UpdateDTO_Book upEntity)
        {
            try
            {
                // Чи введені валідні данні
                if (upEntity?.Title == null || upEntity?.ISBN == null)
                {
                    return BadRequest("Invalid information");
                }                    


                var result = await _bookService.GetAsync(upEntity.BookId); // чи взагалі є такий запис в БД

                if (result == null)
                {
                    Console.WriteLine($"Entity [{upEntity.BookId}] from [{TableName}] not found");
                    return NotFound();
                }
                
                
                await _bookService.UpdateAsync(upEntity);
                Console.WriteLine($"Entity {upEntity.BookId} successfully update to [{TableName}]");

                return Ok();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in [{this.GetType().Name}]->[{nameof(UpdateAsync)}]\n " + ex.Message);
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
                    Console.WriteLine($"Entity [{id}] from [{TableName}] not found");
                    return NotFound();
                }
                
                
                await _bookService.DeleteAsync(id);
                Console.WriteLine($"Entity [{id}] successfully deleted to [{TableName}]");

                return Ok();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in [{this.GetType().Name}]->[{nameof(DeleteByIdAsync)}]\n " + ex.Message);
                return BadRequest(ex.Message);
            }
        }
        
        
        
        [HttpGet("books-with-authors-and-genres")] // GET: ado/book/books-with-authors-and-genres
        public async Task<ActionResult<IEnumerable<GetDTO_Book>>> GetBooksWithAthorsAndGenresAsync()
        {
            try
            {
                var result = await _bookService.GetBooksWithAuthorsAndGenresAsync();
                Console.WriteLine($"All entities were successfully extracted from [{TableName}]");

                return Ok(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine
                    ($"Error in [{this.GetType().Name}]->[{nameof(GetBooksWithAthorsAndGenresAsync)}]\n " + ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("book-with-authors-and-genres/{id}")] // GET: ado/book/book-with-authors-and-genres/id
        public async Task<ActionResult<GetDTO_Book>> GetByIdBookWithAthorAndGenreAsync(Guid id)
        {
            try
            {
                var result = await _bookService.GetBookWithAuthorAndGenreAsync(id); // чи взагалі є такий запис в БД

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
                Console.WriteLine
                    ($"Error in [{this.GetType().Name}]->[{nameof(GetByIdBookWithAthorAndGenreAsync)}]\n " + ex.Message);
                return BadRequest(ex.Message);
            }
        }
    }
}
