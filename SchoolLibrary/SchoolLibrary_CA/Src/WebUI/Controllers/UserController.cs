﻿using Application.Common.Pagging.Entities;
using Application.Features.UserFeatures.Commands;
using Application.Features.UserFeatures.Commands.CreateUser;
using Application.Features.UserFeatures.Commands.UpdateUser;
using Application.Features.UserFeatures.Queries.GetAllUsers;
using Application.Features.UserFeatures.Queries.GetUser;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Controllers
{
    /// <summary>
    /// UserController
    /// </summary>
    public class UserController : BaseController
    {
        /// <summary>
        /// UserController constructor for initialisation ILogger
        /// </summary>
        /// <param name="loggerFactory"></param>
        public UserController(ILoggerFactory loggerFactory) : base(loggerFactory)
        {
        }


        /// <summary>
        /// Get list of Users
        /// </summary>
        /// <returns>Returns list of Users</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> GetAllUserAsync([FromQuery] UserParameter parameters)
        {
            var list = await Mediator.Send(new GetAllUsersQuery(parameters));
            
            _logger.LogInformation(
                "{Count} entities were successfully extracted from [{Table}]", 
                list.Count(), 
                this.GetType().Name.Substring(0, this.GetType().Name.IndexOf("Controller", StringComparison.Ordinal)));

            return Ok(list);
        }

        /// <summary>
        /// Get User by id
        /// </summary>
        /// <param name="id">User id</param>
        /// <returns>Returns entity by id</returns>
        [HttpGet("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> GetUserByIdAsync(Guid id)
        {
            return Ok(await Mediator.Send(new GetUserByIdQuery { Id = id }));
        }
        
        /// <summary>
        /// Craete new User
        /// </summary>
        /// <param name="command">Cteate command</param>
        /// <returns>Returns id created entity</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> CreateUserAsync(CreateUserCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        /// <summary>
        /// Update User by id
        /// </summary>
        /// <param name="command">Update command</param>
        /// <returns>Returns id updated entity</returns>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> UpdateUserAsync(UpdateUserCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        /// <summary>
        /// Delete User by id
        /// </summary>
        /// <param name="id">User Id</param>
        /// <returns>Return deleted entity id</returns>
        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> DeleteUserAsync(Guid id)
        {
            return Ok(await Mediator.Send(new DeleteUserCommand { Id = id }));
        }
    }
}