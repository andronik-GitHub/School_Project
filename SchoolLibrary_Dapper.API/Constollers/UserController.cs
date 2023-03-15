using Microsoft.AspNetCore.Mvc;
using SchoolLibrary_Rapper.BLL.DTO;
using SchoolLibrary_Rapper.BLL.Services.Consracts;

namespace SchoolLibrary_Dapper.API.Constollers
{
    [Route("ado/[controller]")]
    public class UserController : ControllerBase
    {
        IUserService _userService;

        public  UserController(IUserService userService)
        {
            _userService = userService;
        }



        [HttpGet] // GET: ado/publisher
        public async Task<ActionResult<IEnumerable<UserDTO>>> GetAllAsync()
        {
            try
            {
                var result = await _userService.GetAllAsync();
                Console.WriteLine("All User were successfully extracted from [Users]");

                return Ok(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in [UserConstoller]->[GetAllAsync]\n " + ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")] // GET: ado/publisher/id
        public async Task<ActionResult<UserDTO>> GetByIdAsync(Guid id)
        {
            try
            {
                var result = await _userService.GetAsync(id); // чи взагалі є такий запис в БД

                if (result == null)
                {
                    Console.WriteLine($"User {id} from [Users] not found");
                    return NotFound();
                }
                else
                {
                    Console.WriteLine($"User {result.UserId} were successfully extracted from [Users]");
                    return Ok(result);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in [UserController]->[GetByIdAsync]\n " + ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpPost] // POST: ado/publisher
        public async Task<ActionResult> AddAsync(UserDTO newUser)
        {
            try
            {
                // Чи введені валідні данні
                if (newUser.FirstName == null || newUser.LastName == null ||
                    newUser.Email == null || newUser.Password == null ||
                    newUser.Address == null || newUser.Phone == null)
                {
                    return BadRequest("Invalid information");
                }
                else
                {
                    var id = await _userService.CreateAsync(newUser);
                    Console.WriteLine($"User {id} successfully added to [Users]");

                    return Ok(id);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in [UserConstoller]->[AddAsync]\n " + ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpPut] // PUT: ado/publisher
        public async Task<ActionResult> UpdateAsync(UserDTO upUser)
        {
            try
            {
                // Чи введені валідні данні
                if (upUser.FirstName == null || upUser.LastName == null ||
                    upUser.Email == null || upUser.Password == null ||
                    upUser.Address == null || upUser.Phone == null)
                {
                    return BadRequest("Invalid information");
                }
                else
                {
                    var result = await _userService.GetAsync(upUser.UserId); // чи взагалі є такий запис в БД

                    if (result == null)
                    {
                        Console.WriteLine($"User {upUser.UserId} from [Users] not found");
                        return NotFound();
                    }
                    else
                    {
                        await _userService.UpdateAsync(upUser);
                        Console.WriteLine($"User {upUser.UserId} successfully update to [Users]");

                        return Ok();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in [UserConstoller]->[UpdateAsync]\n " + ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")] // DELETE: ado/publisher/id
        public async Task<ActionResult> DeleteByIdAsync(Guid id)
        {
            try
            {
                var result = await _userService.GetAsync(id); // чи взагалі є такий запис в БД

                if (result == null)
                {
                    Console.WriteLine($"User {id} from [Users] not found");
                    return NotFound();
                }
                else
                {
                    await _userService.DeleteAsync(id);
                    Console.WriteLine($"User {id} successfully deleted to [Users]");

                    return Ok();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in [UserConstoller]->[UpdateAsync]\n " + ex.Message);
                return BadRequest(ex.Message);
            }
        }
    }
}
