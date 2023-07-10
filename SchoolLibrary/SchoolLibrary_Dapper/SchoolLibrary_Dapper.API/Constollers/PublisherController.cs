using Microsoft.AspNetCore.Mvc;
using SchoolLibrary_Dapper.BLL.DTOs.PublisherDTOs;
using SchoolLibrary_Dapper.BLL.Services.Consracts;

namespace SchoolLibrary_Dapper.API.Constollers
{
    [Route("ado/[controller]")]
    [ApiController]
    public class PublisherController : ControllerBase
    {
        private readonly IPublisherService _publisherService;
        private readonly string TableName = nameof(PublisherController).Replace("Controller", "");

        public PublisherController(IPublisherService publisherService)
        {
            _publisherService = publisherService;
        }



        [HttpGet] // GET: ado/publisher
        public async Task<ActionResult<IEnumerable<GetDTO_Publisher>>> GetAllAsync()
        {
            try
            {
                var result = await _publisherService.GetAllAsync();
                Console.WriteLine($"All entities were successfully extracted from [{TableName}]");

                return Ok(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in [{this.GetType().Name}]->[{nameof(GetAllAsync)}]\n " + ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")] // GET: ado/publisher/id
        public async Task<ActionResult<GetDTO_Publisher>> GetByIdAsync(Guid id)
        {
            try
            {
                var result = await _publisherService.GetAsync(id); // чи взагалі є такий запис в БД

                if (result == null)
                {
                    Console.WriteLine($"Entity [{id}] from [{TableName}] not found");
                    return NotFound();
                }


                Console.WriteLine(
                    $"Entity [{result.PublisherId}] were successfully extracted from [{TableName}]");
                return Ok(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in [{this.GetType().Name}]->[{nameof(GetByIdAsync)}]\n " + ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpPost] // POST: ado/publisher
        public async Task<ActionResult> AddAsync(InsertDTO_Publisher newEntity)
        {
            try
            {
                // Чи введені валідні данні
                if (newEntity?.Name == null || newEntity?.Location == null)
                {
                    return BadRequest("Invalid information");
                }
                    
                    
                var id = await _publisherService.CreateAsync(newEntity);
                Console.WriteLine($"Entity [{id}] successfully added to [{TableName}]");

                return Ok(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in [{this.GetType().Name}]->[{nameof(AddAsync)}]\n " + ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpPut] // PUT: ado/publisher
        public async Task<ActionResult> UpdateAsync(UpdateDTO_Publisher upEntity)
        {
            try
            {
                // Чи введені валідні данні
                if (upEntity?.Name == null || upEntity?.Location == null)
                {
                    return BadRequest("Invalid information");
                }
                    
                    
                var result = await _publisherService.GetAsync(upEntity.PublisherId); // чи взагалі є такий запис в БД
                if (result == null)
                {
                    Console.WriteLine($"Entity [{upEntity.PublisherId}] from [{TableName}] not found");
                    return NotFound();
                }


                await _publisherService.UpdateAsync(upEntity);
                Console.WriteLine($"Entity {upEntity.PublisherId} successfully update to [{TableName}]");

                return Ok();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in [{this.GetType().Name}]->[{nameof(UpdateAsync)}]\n " + ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")] // DELETE: ado/publisher/id
        public async Task<ActionResult> DeleteByIdAsync(Guid id)
        {
            try
            {
                var result = await _publisherService.GetAsync(id); // чи взагалі є такий запис в БД

                if (result == null)
                {
                    Console.WriteLine($"Entity [{id}] from [{TableName}] not found");
                    return NotFound();
                }
                    
                    
                await _publisherService.DeleteAsync(id);
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
