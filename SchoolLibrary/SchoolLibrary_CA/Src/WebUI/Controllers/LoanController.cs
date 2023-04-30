using Application.Common.Pagging.Entities;
using Application.Features.LoanFeatures.Commands;
using Application.Features.LoanFeatures.Commands.CreateLoan;
using Application.Features.LoanFeatures.Commands.DeleteLoan;
using Application.Features.LoanFeatures.Commands.UpdateLoan;
using Application.Features.LoanFeatures.Queries;
using Application.Features.LoanFeatures.Queries.GetAllLoans;
using Application.Features.LoanFeatures.Queries.GetLoan;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Controllers
{
    /// <summary>
    /// LoanController
    /// </summary>
    public class LoanController : BaseController
    {
        /// <summary>
        /// LoanController constructor for initialisation ILogger
        /// </summary>
        /// <param name="loggerFactory"></param>
        public LoanController(ILoggerFactory loggerFactory) : base(loggerFactory)
        {
        }


        /// <summary>
        /// Get list of Loans
        /// </summary>
        /// <returns>Returns list of Loans</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> GetAllLoanAsync([FromQuery] LoanParameter parameters)
        {
            var list = await Mediator.Send(new GetAllLoansQuery(parameters));
            
            _logger.LogInformation(
                "{Count} entities were successfully extracted from [{Table}]", 
                list.Count(), 
                this.GetType().Name.Substring(0, this.GetType().Name.IndexOf("Controller", StringComparison.Ordinal)));

            return Ok(list);
        }

        /// <summary>
        /// Get Loan by id
        /// </summary>
        /// <param name="id">Loan id</param>
        /// <returns>Returns entity by id</returns>
        [HttpGet("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> GetLoanByIdAsync(Guid id)
        {
            return Ok(await Mediator.Send(new GetLoanByIdQuery { Id = id }));
        }
        
        /// <summary>
        /// Craete new Loan
        /// </summary>
        /// <param name="command">Cteate command</param>
        /// <returns>Returns id created entity</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> CreateLoanAsync(CreateLoanCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        /// <summary>
        /// Update Loan by id
        /// </summary>
        /// <param name="command">Update command</param>
        /// <returns>Returns id updated entity</returns>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> UpdateLoanAsync(UpdateLoanCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        /// <summary>
        /// Delete Loan by id
        /// </summary>
        /// <param name="id">Loan Id</param>
        /// <returns>Return deleted entity id</returns>
        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> DeleteLoanAsync(Guid id)
        {
            return Ok(await Mediator.Send(new DeleteLoanCommand { Id = id }));
        }
    }
}