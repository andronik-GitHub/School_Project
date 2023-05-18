using Application.Features.UserIdentityFeatures.Queries.GetUsersIdentity;
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
        [HttpGet(Name = nameof(GetUsersIdentityAsync))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> GetUsersIdentityAsync()
        {
            var list = (await Mediator.Send(new GetUsersIdentityQuery())).ToList();
            
            _logger.LogInformation(
                "{Count} entities were successfully extracted from [{Table}]", 
                list.Count, 
                this.GetType().Name.Substring(0, this.GetType().Name.IndexOf("Controller", StringComparison.Ordinal)));

            return Ok(list);
        } 
    }
}