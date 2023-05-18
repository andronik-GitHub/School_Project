using Application.Features.UserIdentityFeatures.Queries.GetUserIdentity;
using Application.Features.UserIdentityFeatures.Queries.GetUsersIdentity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Controllers
{
    /// <summary>
    /// UserIdentityController
    /// </summary>
    public class UserIdentityController : BaseController
    {
        /// <summary>
        /// UserIdentityController constructor for initialisation ILogger and IUrlHelper
        /// </summary>
        /// <param name="loggerFactory">ILoggerFactory</param>
        /// <param name="urlHelper">IUrlHelper</param>
        public UserIdentityController(ILoggerFactory loggerFactory, IUrlHelper urlHelper) 
            : base(loggerFactory, urlHelper)
        {
        }


        /// <summary>
        /// Get list of UsersIdentity
        /// </summary>
        /// <returns>Returns list of UsersIdentity</returns>
        /// <response code="200">All database data retrieved successfully</response>
        /// <response code="400">If incorrect data is entered or retrieved</response>
        /// <response code="401">If the user is not authorized</response>
        /// <response code="500">
        /// If it was not possible to get a list of elements from the database or anothers errors
        /// </response>
        [HttpGet(Name = nameof(GetUsersIdentityAsync))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> GetUsersIdentityAsync()
        {
            var list = (await Mediator.Send(new GetUsersIdentityQuery())).ToList();
            
            _logger.LogInformation(
                "{Count} entities were successfully extracted from [{Table}]", 
                list.Count, 
                this.GetType().Name.Substring(0, this.GetType().Name.IndexOf("Controller", StringComparison.Ordinal)));

            return Ok(list);
        }

        /// <summary>
        /// Get UserIdentity by id
        /// </summary>
        /// <param name="id">UserIdentity id</param>
        /// <returns>Returns entity by id</returns>
        /// <response code="200">All database data retrieved successfully</response>
        /// <response code="400">If incorrect data is entered or retrieved</response>
        /// <response code="401">If the user is not authorized</response>
        /// <response code="404">If the user is not found in the database</response>
        /// <response code="500">
        /// If it was not possible to get a list of elements from the database or anothers errors
        /// </response>
        [HttpGet("{id:guid}", Name = nameof(GetUserIdentityByIdAsync))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> GetUserIdentityByIdAsync(Guid id)
        {
            var entity = await Mediator.Send(new GetUserIdentityQuery { Id = id });
            
            _logger.LogInformation(
                "Entity were successfully extracted from [{Table}]", 
                this.GetType().Name.Substring(0, this.GetType().Name.IndexOf("Controller", StringComparison.Ordinal)));

            return Ok(entity);
        }


        /// 
        [HttpGet(Name = nameof(GetSecuredData))]
        [Authorize]
        public async Task<ActionResult> GetSecuredData()
        {
            return await Task.Run(() => Ok("This Secured Data is available only for Authenticated Users"));
        }
    }
}