using System.Text.Json;
using System.Text.Json.Serialization;
using Application.Common.Pagging.Entities;
using Application.Features.UserFeatures.Commands;
using Application.Features.UserFeatures.Commands.CreateUser;
using Application.Features.UserFeatures.Commands.UpdateUser;
using Application.Features.UserFeatures.Queries.GetAllUsers;
using Application.Features.UserFeatures.Queries.GetAllUsers_DataShaping;
using Application.Features.UserFeatures.Queries.GetUser;
using Application.Features.UserFeatures.Queries.GetUser_DataShaping;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Controllers
{
    /// <summary>
    /// UserController
    /// </summary>
    public class UserController : BaseController
    {
        /// <summary>
        /// UserController constructor for initialisation ILogger and IUrlHelper
        /// </summary>
        /// <param name="loggerFactory">ILoggerFactory</param>
        /// <param name="urlHelper">IUrlHelper</param>
        public UserController(ILoggerFactory loggerFactory, IUrlHelper urlHelper) 
            : base(loggerFactory, urlHelper)
        {
        }


        /// <summary>
        /// Get list of Users
        /// </summary>
        /// <returns>Returns list of Users</returns>
        [HttpGet(Name = nameof(GetAllUserAsync))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> GetAllUserAsync([FromQuery] UserParameter parameters)
        {
            var list = (await Mediator.Send(new GetAllUsersQuery(parameters))).ToList();
            
            _logger.LogInformation(
                "{Count} entities were successfully extracted from [{Table}]", 
                list.Count, 
                this.GetType().Name.Substring(0, this.GetType().Name.IndexOf("Controller", StringComparison.Ordinal)));

            list.ForEach(item => this.CreateLinksForEntity(
                item,
                item.UserId,
                nameof(GetUserByIdAsync), 
                nameof(UpdateUserAsync), 
                nameof(DeleteUserAsync))); // HATEOAS
            return Ok(list);
        }

        /// <summary>
        /// Get User by id
        /// </summary>
        /// <param name="id">User id</param>
        /// <returns>Returns entity by id</returns>
        [HttpGet("{id:guid}", Name = nameof(GetUserByIdAsync))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> GetUserByIdAsync(Guid id)
        {
            var entity = await Mediator.Send(new GetUserByIdQuery { Id = id });
            return Ok(this.CreateLinksForEntity(
                entity,
                entity.UserId,
                nameof(GetUserByIdAsync), 
                nameof(UpdateUserAsync), 
                nameof(DeleteUserAsync))); // HATEOAS
        }
        
        /// <summary>
        /// Craete new User
        /// </summary>
        /// <param name="command">Cteate command</param>
        /// <returns>Returns id created entity</returns>
        [HttpPost(Name = nameof(CreateUserAsync))]
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
        [HttpPut(Name = nameof(UpdateUserAsync))]
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
        [HttpDelete("{id:guid}", Name = nameof(DeleteUserAsync))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> DeleteUserAsync(Guid id)
        {
            return Ok(await Mediator.Send(new DeleteUserCommand { Id = id }));
        }
  

        /// <summary>
        /// Get list of User with selected fields
        /// </summary>
        /// <param name="parameters">Fields, that need select</param>
        /// <returns>Returns list of User with selected fields</returns>
        [HttpGet("datashaping/", Name = nameof(GetAllUser_DataShapingAsync))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> GetAllUser_DataShapingAsync([FromQuery] UserParameter parameters)
        {
            var list = await Mediator.Send(new GetAllUsers_DataShapingQuery(parameters));
            
            _logger.LogInformation(
                "{Count} entities were successfully extracted from [{Table}]",
                list.Count, 
                this.GetType().Name.Substring(0, this.GetType().Name.IndexOf("Controller", StringComparison.Ordinal)));
            
            
            list.ForEach(item => this.CreateLinksForEntity(
                item,
                item.Id,
                nameof(GetUserByIdAsync), 
                nameof(UpdateUserAsync), 
                nameof(DeleteUserAsync))); // HATEOAS
            
            string json = JsonSerializer.Serialize(list, new JsonSerializerOptions
            {
                WriteIndented = true, // spaces are included in json (relatively speaking, for beauty)
                ReferenceHandler = ReferenceHandler.Preserve // "Preserve" to avoid circular references
            });
            
            return Ok(json);
        }

        /// <summary>
        /// Get User by id with selected fields
        /// </summary>
        /// <param name="id">User id</param>
        /// <param name="parameters">Fields, that need select</param>
        /// <returns>Returns entity by id with selected fields</returns>
        [HttpGet("datashaping/{id:guid}", Name = nameof(GetUserById_DataShapingAsync))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> GetUserById_DataShapingAsync
            (Guid id, [FromQuery] UserParameter parameters)
        {
            var entity = await Mediator.Send(new GetUserById_DataShapingQuery(parameters) { Id = id });
            
            _logger.LogInformation(
                "Entity were successfully extracted from [{Table}]",
                this.GetType().Name.Substring(0, this.GetType().Name.IndexOf("Controller", StringComparison.Ordinal)));

            
            return Ok(this.CreateLinksForEntity(
                    entity,
                    entity.Id,
                    nameof(GetUserByIdAsync), 
                    nameof(UpdateUserAsync), 
                    nameof(DeleteUserAsync))
                .ToString()); // HATEOAS
        }
    }
}