using Microsoft.AspNetCore.Mvc;
using SchoolLibrary_EF.BLL.DTO;
using SchoolLibrary_EF.BLL.Services.Contracts;

namespace SchoolLibrary_EF.API.Controllers
{
    [Produces("application/json")]
    [Route("ef/[controller]")]
    [ApiController]
    public class LoanController : ControllerBase
    {
        private readonly ILoanService _loanService;
        private readonly ILogger _logger;

        public LoanController(ILoanService loanService, ILoggerFactory loggerFactory)
        {
            _loanService = loanService;
            _logger = loggerFactory.CreateLogger($"{this.GetType().Name}_Logger");
        }


        /// <summary>
        /// Gets the list of all Loans
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// GET ef/loan
        /// </remarks>
        /// <returns>Returns list of LoanDTO</returns>
        /// <response code="200">Success</response>
        /// <response code="500">If it was not possible to get a list of elements from the database</response>
        [HttpGet] // GET: ef/loan
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<LoanDTO>>> GetAllAsync()
        {
            try
            {
                var collection = await _loanService.GetAllAsync();
                _logger.LogInformation("All entities were successfully extracted from [Loans]");

                return Ok(collection);
            }
            catch (Exception ex)
            {
                _logger.LogError
                    ("Error in [{ErrorClassName}]->[GetAllAsync] => {ErrorMessage}", this.GetType().Name, ex.Message);

                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Gets the Loan by id
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// GET ef/loan/13ce1333-7b7c-4395-8565-0474a6ad05ad
        /// </remarks>
        /// <param name="id">Loan id (Guid)</param>
        /// <returns>Returns element of LoanDTO</returns>
        /// <response code="200">Success</response>
        /// <response code="404">If the element with such ID is not found in the database</response>
        /// <response code="500">If it was not possible to get element from the database</response>
        [HttpGet("{id}")] // GET: ef/loan/id
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<LoanDTO>> GetByIdAsync(Guid id)
        {
            try
            {
                var entity = await _loanService.GetAsync(id);

                if (entity == null)
                {
                    _logger.LogError("Entity with id: [{EntityId}] from [Loans] not found", id);

                    return StatusCode(StatusCodes.Status404NotFound);
                    //return NotFound();
                }
                else
                {
                    _logger.LogInformation
                        ("Entity with id: [{EntityId}] were successfully extracted from [Loans]", id);

                    return Ok(entity);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError
                    ("Error in [{ErrorClassName}]->[GetAllAsync] => {ErrorMessage}", this.GetType().Name, ex.Message);

                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Creates new Loan
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     POST: ef/loan
        ///     {
        ///         "loanId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
        ///         "loanDate": "2023-03-21T07:55:11.461Z",
        ///         "returnDate": "2023-03-21T07:55:11.461Z",
        ///         "userFullName": "string",
        ///         "bookTitle": "string",
        ///         "bookISBN": "string"
        ///     }
        /// </remarks>
        /// <param name="newLoan">LoanDTO newEntity</param>
        /// <returns>Returns id (Guid)</returns>
        /// <response code="200">Success</response>
        /// <response code="400">If invalid data entered</response>
        /// <response code="500">If it was not possible to adding element to the database</response>
        [HttpPost] // POST: ef/loan
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Guid>> AddAsync(LoanDTO newLoan)
        {
            try
            {
                // Checking whether valid data has been entered
                if (newLoan.UserFullName == null || newLoan.BookTitle == null || newLoan.BookISBN == null)
                {
                    _logger.LogError("Invalid data entered");

                    return StatusCode(StatusCodes.Status400BadRequest);
                    //return BadRequest("Invalid data entered");
                }
                else
                {
                    var id = await _loanService.CreateAsync(newLoan);
                    _logger.LogInformation
                        ("Entity with id: [{EntityId}] were successfully added to [Loan]", id);

                    return Ok(id);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError
                    ("Error in [{ErrorClassName}]->[GetAllAsync] => {ErrorMessage}", this.GetType().Name, ex.Message);

                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Update the Loan
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     PUT: ef/loan
        ///     {
        ///         "loanId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
        ///         "loanDate": "2023-03-21T07:55:11.461Z",
        ///         "returnDate": "2023-03-21T07:55:11.461Z",
        ///         "userFullName": "string",
        ///         "bookTitle": "string",
        ///         "bookISBN": "string"
        ///     }
        /// </remarks>
        /// <param name="updateLoan">LoanDTO updateEntity</param>
        /// <returns>Returns NoContent</returns>
        /// <response code="204">Success</response>
        /// <response code="400">If invalid data entered</response>
        /// <response code="500">If it was not possible to adding element to the database</response>
        [HttpPut] // PUT: ef/loan
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> UpdateAsync(LoanDTO updateLoan)
        {
            try
            {
                // Checking whether valid data has been entered
                if (updateLoan.UserFullName == null || updateLoan.BookTitle == null || updateLoan.BookISBN == null)
                {
                    _logger.LogError("Invalid data entered");

                    return StatusCode(StatusCodes.Status400BadRequest);
                    //return BadRequest("Invalid data entered");
                }
                else
                {
                    // Whether there is such a record in the database at all
                    var findResult = await _loanService.GetAsync(updateLoan.LoanId);

                    if (findResult == null)
                    {
                        _logger.LogError
                            ("Entity with id: [{EntityId}] from [Loans] not found",
                                updateLoan.LoanId);

                        return StatusCode(StatusCodes.Status404NotFound);
                        //return NotFound();
                    }
                    else
                    {
                        await _loanService.UpdateAsync(updateLoan);
                        _logger.LogInformation
                            ("Entity with id: [{EntityId}] were successfully updated from [Loans]",
                                updateLoan.LoanId);

                        return StatusCode(StatusCodes.Status204NoContent);
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError
                    ("Error in [{ErrorClassName}]->[GetAllAsync] => {ErrorMessage}", this.GetType().Name, ex.Message);

                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Delete the Loan by id
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// DELETE: ef/loan/13ce1333-7b7c-4395-8565-0474a6ad05ad
        /// </remarks>
        /// <param name="id">Loan id (Guid)</param>
        /// <returns>Returns NoContent</returns>
        /// <response code="204">Success</response>
        /// <response code="400">If invalid data entered</response>
        /// <response code="500">If it was not possible to adding element to the database</response>
        [HttpDelete("{id}")] // DELETE: ef/loan/id
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> DeleteAsync(Guid id)
        {
            try
            {
                // Whether there is such a record in the database at all
                var findResult = await _loanService.GetAsync(id);

                if (findResult == null)
                {
                    _logger.LogError("Entity with id: [{EntityId}] from [Loans] not found", id);

                    return StatusCode(StatusCodes.Status404NotFound);
                    //return NotFound();
                }
                else
                {
                    await _loanService.DeleteAsync(id);
                    _logger.LogInformation
                        ("Entity with id: [{EntityId}] were successfully deleted from [Loans]", id);

                    return StatusCode(StatusCodes.Status204NoContent);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError
                    ("Error in [{ErrorClassName}]->[GetAllAsync] => {ErrorMessage}", this.GetType().Name, ex.Message);

                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
