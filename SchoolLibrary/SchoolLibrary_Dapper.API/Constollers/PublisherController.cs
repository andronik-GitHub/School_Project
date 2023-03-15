using Microsoft.AspNetCore.Mvc;
using SchoolLibrary_Dapper.BLL.DTO;
using SchoolLibrary_Dapper.BLL.Services.Consracts;

namespace SchoolLibrary_Dapper.API.Constollers
{
    [Route("ado/[controller]")]
    [ApiController]
    public class PublisherController : ControllerBase
    {
        IPublisherService _publisherService;

        public PublisherController(IPublisherService publisherService)
        {
            _publisherService = publisherService;
        }



        [HttpGet] // GET: ado/publisher
        public async Task<ActionResult<IEnumerable<PublisherDTO>>> GetAllAsync()
        {
            try
            {
                var result = await _publisherService.GetAllAsync();
                Console.WriteLine("All Publisher were successfully extracted from [Publishers]");

                return Ok(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in [PublisherConstoller]->[GetAllAsync]\n " + ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")] // GET: ado/publisher/id
        public async Task<ActionResult<PublisherDTO>> GetByIdAsync(Guid id)
        {
            try
            {
                var result = await _publisherService.GetAsync(id); // чи взагалі є такий запис в БД

                if (result == null)
                {
                    Console.WriteLine($"Publisher {id} from [Publishers] not found");
                    return NotFound();
                }
                else
                {
                    Console.WriteLine($"Publisher {result.PublisherId} were successfully extracted from [Publishers]");
                    return Ok(result);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in [PublisherController]->[GetByIdAsync]\n " + ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpPost] // POST: ado/publisher
        public async Task<ActionResult> AddAsync(PublisherDTO newPublisher)
        {
            try
            {
                // Чи введені валідні данні
                if (newPublisher.Name == null || newPublisher.Location == null)
                {
                    return BadRequest("Invalid information");
                }
                else
                {
                    var id = await _publisherService.CreateAsync(newPublisher);
                    Console.WriteLine($"Publisher {id} successfully added to [Publishers]");

                    return Ok(id);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in [PublisherConstoller]->[AddAsync]\n " + ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpPut] // PUT: ado/publisher
        public async Task<ActionResult> UpdateAsync(PublisherDTO upPublisher)
        {
            try
            {
                // Чи введені валідні данні
                if (upPublisher.Name == null || upPublisher.Location == null)
                {
                    return BadRequest("Invalid information");
                }
                else
                {
                    var result = await _publisherService.GetAsync(upPublisher.PublisherId); // чи взагалі є такий запис в БД

                    if (result == null)
                    {
                        Console.WriteLine($"Publisher {upPublisher.PublisherId} from [Publishers] not found");
                        return NotFound();
                    }
                    else
                    {
                        await _publisherService.UpdateAsync(upPublisher);
                        Console.WriteLine($"Publisher {upPublisher.PublisherId} successfully update to [Publishers]");

                        return Ok();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in [PublisherConstoller]->[UpdateAsync]\n " + ex.Message);
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
                    Console.WriteLine($"Publisher {id} from [Publishers] not found");
                    return NotFound();
                }
                else
                {
                    await _publisherService.DeleteAsync(id);
                    Console.WriteLine($"Publisher {id} successfully deleted to [Publishers]");

                    return Ok();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in [PublisherConstoller]->[UpdateAsync]\n " + ex.Message);
                return BadRequest(ex.Message);
            }
        }
    }
}
