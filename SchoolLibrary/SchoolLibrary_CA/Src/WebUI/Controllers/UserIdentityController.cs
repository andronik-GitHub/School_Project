using Application.Features.UserIdentityFeatures.Commands.RegisterUserIdentity;
using Application.Features.UserIdentityFeatures.Commands.RevokeRefreshToken;
using Application.Features.UserIdentityFeatures.Queries.AddRole;
using Application.Features.UserIdentityFeatures.Queries.Common;
using Application.Features.UserIdentityFeatures.Queries.GetJwtToken;
using Application.Features.UserIdentityFeatures.Queries.GetRefreshToken;
using Application.Features.UserIdentityFeatures.Queries.GetRefreshTokensByUserId;
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
        
        /// <summary>
        /// Get tokens by user id
        /// </summary>
        /// <param name="id">User id</param>
        /// <returns>Returns tokens by user id</returns>
        /// <response code="200">All database data retrieved successfully</response>
        /// <response code="400">If incorrect data is entered or retrieved</response>
        /// <response code="401">If the user is not authorized</response>
        /// <response code="404">If the user is not found in the database</response>
        /// <response code="500">
        /// If it was not possible to get a list of elements from the database or anothers errors
        /// </response>
        [HttpGet("tokens/{id:guid}", Name = nameof(GetTokensByIdAsync))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> GetTokensByIdAsync(Guid id)
        {
            var entity = await Mediator.Send(new GetRefreshTokensByUserIdQuery { Id = id });
            
            _logger.LogInformation
                ("Entity were successfully extracted. Count Refresh Tokens: {refreshTokensCount}", entity.Count());
            
            return Ok(entity);
        }
        
        /// <summary>
        /// Register user with role "USER"
        /// </summary>
        /// <param name="command">Register models</param>
        /// <returns>Returns id registered user</returns>
        /// <response code="200">All data has been successfully entered into the database</response>
        /// <response code="400">If incorrect data is entered or retrieved</response>
        /// <response code="401">If the user is not authorized</response>
        /// <response code="422">If the input data is invalid</response>
        /// <response code="500">
        /// If it was not possible to get a list of elements from the database or anothers errors
        /// </response>
        [HttpPost("register-user", Name = nameof(RegisterUserAsync))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> RegisterUserAsync(RegisterUserIdentityCommand command)
        {
            var id = await Mediator.Send(command);
            _logger.LogInformation("User with id [{id}] were successfully register", id);
            return Ok(id);
        }

        

        /// <summary>Get secured data to user</summary>
        /// <returns>Returns secured data to user</returns>
        [HttpGet("secured-user", Name = nameof(GetSecuredData_User))]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> GetSecuredData_User()
        {
            _logger.LogInformation("Secure data only for Authenticated Users successfully extracted");
            return await Task.Run(() => Ok("This Secure Data is available only for Authenticated Users"));
        }
        
        /// <summary>Get secured data to administrator</summary>
        /// <returns>Returns secured data to administrator</returns>
        [HttpGet("secured-administrator", Name = nameof(GetSecuredData_Administrator))]
        [Authorize(Roles = "Administrator")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> GetSecuredData_Administrator()
        {
            _logger.LogInformation("Secure data only for Administrators successfully extracted");
            return await Task.Run(() => Ok("This Secured Data is available only for Administrators"));
        }

        
        /// <summary>
        /// Adds role to user
        /// </summary>
        /// <param name="query">AddRole models</param>
        /// <returns>Returns id user was successfully assigned role</returns>
        /// <response code="200">All data has been successfully entered into the database</response>
        /// <response code="400">If incorrect data is entered or retrieved</response>
        /// <response code="401">If the user is not authorized</response>
        /// <response code="422">If it was not possible to add a role to a user</response>
        /// <response code="500">
        /// If it was not possible to get a list of elements from the database or anothers errors
        /// </response>
        [HttpPost("add-role", Name = nameof(AddRoleAsync))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> AddRoleAsync(AddRoleQuery query)
        {
            var id = await Mediator.Send(query);
            _logger.LogInformation("User with id [{id}] was successfully assigned role [{role}]", id, query.Role);
            return Ok(id);
        }

        /// <summary>
        /// Revokes refresh token
        /// </summary>
        /// <param name="token">Refresh token</param>
        /// <returns>Returns the result of token revoke(true or false)</returns>
        /// <response code="200">All data has been successfully entered into the database</response>
        /// <response code="400">If incorrect data is entered or retrieved</response>
        /// <response code="401">If the user is not authorized</response>
        /// <response code="422">If it was not possible to add a role to a user</response>
        /// <response code="500">
        /// If it was not possible to get a list of elements from the database or anothers errors
        /// </response>
        [HttpPost("revoke-refresh-token", Name = nameof(RevokeRefreshTokenAsync))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> RevokeRefreshTokenAsync(string? token)
        {
            var result = await Mediator.Send
                (new RevokeRefreshTokenCommand { Token = token ?? Request.Cookies["refreshToken"] });
            
            if (result) _logger.LogInformation("Refresh Token successfully revoked");
            else _logger.LogInformation("Refresh Token was not successfully revoked");
            
            return Ok(result);
        }



        /// <summary>
        /// Get token
        /// </summary>
        /// <returns>Returns JWT Security token</returns>
        /// <response code="200">All database data retrieved successfully</response>
        /// <response code="400">If incorrect data is entered or retrieved</response>
        /// <response code="500">
        /// If it was not possible to get a list of elements from the database or anothers errors
        /// </response>
        [HttpPost("token", Name = nameof(GetTokenAsync))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> GetTokenAsync(TokenRequestModel model)
        {
            var result = await Mediator.Send(new GetJwtTokenQuery { Email = model.Email, Password = model.Password });
            SetRefreshTokenInCookie(result.RefreshToken);
            _logger.LogInformation("JWT Token successfully extracted");
            return Ok(result);
        }

        /// <summary>
        /// Get RefreshToken
        /// </summary>
        /// <returns>Returns JWT Security token and RefreshToken</returns>
        /// <response code="200">All database data retrieved successfully</response>
        /// <response code="400">If incorrect data is entered or retrieved</response>
        /// <response code="500">
        /// If it was not possible to get a list of elements from the database or anothers errors
        /// </response>
        [HttpPost("refresh-token", Name = nameof(GetRefreshTokenAsync))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> GetRefreshTokenAsync()
        {
            var refreshToken = Request.Cookies["refreshToken"];
            var response = await Mediator.Send(new GetRefreshTokenQuery { Token = refreshToken });

            if (!string.IsNullOrEmpty(response.RefreshToken))
                SetRefreshTokenInCookie(response.RefreshToken);

            _logger.LogInformation("Refresh Token successfully extracted");
            return Ok(response);
        }




        /// <summary>
        /// Takes a refresh token as a parameter and stores it as a browser cookie
        /// </summary>
        /// <param name="refreshToken">RefreshToken</param>
        private void SetRefreshTokenInCookie(string refreshToken)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = DateTime.UtcNow.AddDays(10),
            };
            Response.Cookies.Append("refreshToken", refreshToken, cookieOptions);
            
            _logger.LogInformation("The refresh token was successfully saved as a browser cookie");
        }
    }
}