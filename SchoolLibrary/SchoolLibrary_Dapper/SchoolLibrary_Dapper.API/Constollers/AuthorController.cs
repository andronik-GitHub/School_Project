using Microsoft.AspNetCore.Mvc;
using SchoolLibrary_Dapper.BLL.DTOs.AuthorDTOs;
using SchoolLibrary_Dapper.BLL.Services.Consracts;

namespace SchoolLibrary_Dapper.API.Constollers
{
    [Route("ado/[controller]")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        private readonly IAuthorService _authorService;
        private readonly string TableName = nameof(AuthorController).Replace("Controller", "");

        public AuthorController(IAuthorService authorService)
        {
            _authorService = authorService;
        }



        [HttpGet] // GET: ado/author
        public async Task<ActionResult<IEnumerable<GetDTO_Author>>> GetAllAsync()
        {
            try
            {
                var result = await _authorService.GetAllAsync();
                Console.WriteLine($"All entities were successfully extracted from [{TableName}]");

                return Ok(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in [{this.GetType().Name}]->[{nameof(GetAllAsync)}]\n " + ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")] // GET: ado/author/id
        public async Task<ActionResult<GetDTO_Author>> GetByIdAsync(Guid id)
        {
            try
            {
                var result = await _authorService.GetAsync(id); // чи взагалі є такий запис в БД

                if (result == null)
                {
                    Console.WriteLine($"Entity [{id}] from [{TableName}] not found");
                    return NotFound();
                }
                else
                {
                    Console.WriteLine(
                        $"Entity [{result.AuthorId}] were successfully extracted from [{TableName}]");
                    return Ok(result);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in [{this.GetType().Name}]->[{nameof(GetByIdAsync)}]\n " + ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpPost] // POST: ado/author
        public async Task<ActionResult> AddAsync(InsertDTO_Author newEntity)
        {
            try
            {
                // Чи введені валідні данні
                if (newEntity?.FirstName == null || newEntity?.LastName == null || newEntity?.Nationality == null)
                {
                    return BadRequest("Invalid information");
                }
                
                
                var id = await _authorService.CreateAsync(newEntity);
                Console.WriteLine($"Entity [{id}] successfully added to [{TableName}]");

                return Ok(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in [{this.GetType().Name}]->[{nameof(AddAsync)}]\n " + ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpPut] // PUT: ado/author
        public async Task<ActionResult> UpdateAsync(UpdateDTO_Author upEntity)
        {
            try
            {
                // Чи введені валідні данні
                if (upEntity?.FirstName == null || upEntity?.LastName == null || upEntity?.Nationality == null)
                    return BadRequest("Invalid information");
                
                
                var result = await _authorService.GetAsync(upEntity.AuthorId); // чи взагалі є такий запис в БД
                if (result == null)
                {
                    Console.WriteLine($"Entity [{upEntity.AuthorId}] from [{TableName}] not found");
                    return NotFound();
                }
                
                
                await _authorService.UpdateAsync(upEntity);
                Console.WriteLine($"Entity {upEntity.AuthorId} successfully update to [{TableName}]");

                return Ok();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in [{this.GetType().Name}]->[{nameof(UpdateAsync)}]\n " + ex.Message);
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
                    Console.WriteLine($"Entity [{id}] from [{TableName}] not found");
                    return NotFound();
                }
                else
                {
                    await _authorService.DeleteAsync(id);
                    Console.WriteLine($"Entity [{id}] successfully deleted to [{TableName}]");

                    return Ok();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in [{this.GetType().Name}]->[{nameof(DeleteByIdAsync)}]\n " + ex.Message);
                return BadRequest(ex.Message);
            }
        }
    }
}
