using Microsoft.AspNetCore.Mvc;
using SchoolLibrary_Dapper.BLL.DTO;
using SchoolLibrary_Dapper.BLL.Services.Consracts;

namespace SchoolLibrary_Dapper.API.Constollers
{
    [Route("ado/[controller]")]
    [ApiController]
    public class LoanController : ControllerBase
    {
        ILoanService _loanService;

        public LoanController(ILoanService loanService)
        {
            _loanService = loanService;
        }


        [HttpGet] // GET: ado/loan
        public async Task<ActionResult<IEnumerable<LoanDTO>>> GetAllAsync()
        {
            try
            {
                var result = await _loanService.GetAllAsync();
                Console.WriteLine("All Loan were successfully extracted from [Loans]");

                return Ok(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in [LoanConstoller]->[GetAllAsync]\n " + ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")] // GET: ado/loan/id
        public async Task<ActionResult<LoanDTO>> GetByIdAsync(Guid id)
        {
            try
            {
                var result = await _loanService.GetAsync(id); // чи взагалі є такий запис в БД

                if (result == null)
                {
                    Console.WriteLine($"Loan {id} from [Loans] not found");
                    return NotFound();
                }
                else
                {
                    Console.WriteLine($"Loan {result.LoanId} were successfully extracted from [Loans]");
                    return Ok(result);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in [LoanConstoller]->[GetAllAsync]\n " + ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpPost] // POST: ado/loan
        public async Task<ActionResult> AddAsync(LoanDTO newLoan)
        {
            try
            {
                var id = await _loanService.CreateAsync(newLoan);
                Console.WriteLine($"Loan {id} successfully added to [Loans]");

                return Ok(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in [LoanConstoller]->[AddAsync]\n " + ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpPut] // PUT: ado/loan
        public async Task<ActionResult> UpdateAsync(LoanDTO upLoan)
        {
            try
            {
                var result = await _loanService.GetAsync(upLoan.LoanId); // чи взагалі є такий запис в БД

                if (result == null)
                {
                    Console.WriteLine($"Loan {upLoan.LoanId} from [Loans] not found");
                    return NotFound();
                }
                else
                {
                    await _loanService.UpdateAsync(upLoan);
                    Console.WriteLine($"Loan {upLoan.LoanId} successfully update to [Loans]");

                    return Ok();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in [LoanConstoller]->[UpdateAsync]\n " + ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")] // DELETE: ado/loan/id
        public async Task<ActionResult> DeleteByIdAsync(Guid id)
        {
            try
            {
                var result = await _loanService.GetAsync(id); // чи взагалі є такий запис в БД

                if (result == null)
                {
                    Console.WriteLine($"Loan {id} from [Loans] not found");
                    return NotFound();
                }
                else
                {
                    await _loanService.DeleteAsync(id);
                    Console.WriteLine($"Loan {id} successfully deleted to [Loans]");

                    return Ok();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in [LoanConstoller]->[UpdateAsync]\n " + ex.Message);
                return BadRequest(ex.Message);
            }
        }
    }
}
