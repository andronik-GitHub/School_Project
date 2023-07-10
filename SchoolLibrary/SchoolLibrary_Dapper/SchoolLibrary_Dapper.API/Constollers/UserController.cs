using Microsoft.AspNetCore.Mvc;
using SchoolLibrary_Dapper.BLL.DTOs.UserDTOs;
using SchoolLibrary_Dapper.BLL.Services.Consracts;

namespace SchoolLibrary_Dapper.API.Constollers
{
    [Route("ado/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly string TableName = nameof(UserController).Replace("Controller", "");

        public  UserController(IUserService userService)
        {
            _userService = userService;
        }



        [HttpGet] // GET: ado/user
        public async Task<ActionResult<IEnumerable<GetDTO_User>>> GetAllAsync()
        {
            try
            {
                var result = await _userService.GetAllAsync();
                Console.WriteLine($"All entities were successfully extracted from [{TableName}]");

                return Ok(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in [{this.GetType().Name}]->[{nameof(GetAllAsync)}]\n " + ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")] // GET: ado/user/id
        public async Task<ActionResult<GetDTO_User>> GetByIdAsync(Guid id)
        {
            try
            {
                var result = await _userService.GetAsync(id); // чи взагалі є такий запис в БД

                if (result == null)
                {
                    Console.WriteLine($"Entity [{id}] from [{TableName}] not found");
                    return NotFound();
                }
                    
                    
                Console.WriteLine($"Entity [{result.UserId}] were successfully extracted from [{TableName}]");
                return Ok(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in [{this.GetType().Name}]->[{nameof(GetByIdAsync)}]\n " + ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpPost] // POST: ado/user
        public async Task<ActionResult> AddAsync(InsertDTO_User newEntity)
        {
            try
            {
                // Чи введені валідні данні
                if (newEntity?.FirstName == null || newEntity?.LastName == null ||
                    newEntity?.Email == null || newEntity?.Password == null ||
                    newEntity?.Street == null || newEntity?.Country == null ||
                    newEntity?.City == null || newEntity?.PhoneNumber == null)
                {
                    return BadRequest("Invalid information");
                }
                
                
                var id = await _userService.CreateAsync(newEntity);
                Console.WriteLine($"Entity [{id}] successfully added to [{TableName}]");

                return Ok(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in [{this.GetType().Name}]->[{nameof(AddAsync)}]\n " + ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpPut] // PUT: ado/user
        public async Task<ActionResult> UpdateAsync(UpdateDTO_User upEntity)
        {
            try
            {
                // Чи введені валідні данні
                if (upEntity?.FirstName == null || upEntity?.LastName == null ||
                    upEntity?.Email == null || upEntity?.Password == null ||
                    upEntity?.Street == null || upEntity?.Country == null ||
                    upEntity?.City == null || upEntity?.PhoneNumber == null)
                {
                    return BadRequest("Invalid information");
                }
                    
                    
                var result = await _userService.GetAsync(upEntity.UserId); // чи взагалі є такий запис в БД
                if (result == null)
                {
                    Console.WriteLine($"Entity [{upEntity.UserId}] from [{TableName}] not found");
                    return NotFound();
                }
                    
                    
                await _userService.UpdateAsync(upEntity);
                Console.WriteLine($"Entity {upEntity.UserId} successfully update to [{TableName}]");

                return Ok();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in [{this.GetType().Name}]->[{nameof(UpdateAsync)}]\n " + ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")] // DELETE: ado/user/id
        public async Task<ActionResult> DeleteByIdAsync(Guid id)
        {
            try
            {
                var result = await _userService.GetAsync(id); // чи взагалі є такий запис в БД

                if (result == null)
                {
                    Console.WriteLine($"Entity [{id}] from [{TableName}] not found");
                    return NotFound();
                }
                else
                {
                    await _userService.DeleteAsync(id);
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
