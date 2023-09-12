using System.Dynamic;
using Application.Common.HATEOS;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Controllers
{
    /// <summary>
    /// A base controller class for an MVC controller without view support. With IMediator, ILogger, IUrlHelper
    /// </summary>
    [ApiController]
    [Route("ca/[controller]")]
    public abstract class BaseController : ControllerBase
    {
        private IMediator? _mediator;
        /// <summary>
        /// Defines a mediator to encapsulate request/response and publishing interaction patterns
        /// </summary>
        protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>()!;
        
        /// <summary>
        /// Represents a type used to perform logging.
        /// </summary>
        protected readonly ILogger _logger;
        /// <summary>
        /// Defines the contract for the helper to build URLs for ASP.NET MVC within an application
        /// </summary>
        private readonly IUrlHelper _urlHelper;

        /// <summary>
        /// A property containing the name of the table with which the used controller interacts
        /// </summary>
        protected readonly string _tableName;

        /// <summary>
        /// Base constructor for initialisation ILogger and IUrlHelper
        /// </summary>
        /// <param name="loggerFactory">ILoggerFactory</param>
        /// <param name="urlHelper">IUrlHelper</param>
        protected BaseController(ILoggerFactory loggerFactory, IUrlHelper urlHelper)
        {
            _urlHelper = urlHelper;
            _logger = loggerFactory.CreateLogger($"{this.GetType().Name}_Logger");
            
            _tableName = this.GetType().Name.Replace("Controller", "");
            _tableName = _tableName is "BookDetails" or "BookAuthors" or "BookGenres" ? _tableName : _tableName + "s";
        }


        /// <summary>
        /// Create links for implementation HATEOAS in entity
        /// </summary>
        /// <param name="entity">Essential entity(DTO)</param>
        /// <param name="entityId">Entity Id</param>
        /// <param name="selfLinkName">Self link name</param>
        /// <param name="updateLinkName">Update link name</param>
        /// <param name="deleteLinkName">Delete link name</param>
        /// <typeparam name="TEntity">Type essential entity(DTO)</typeparam>
        /// <returns>Returns entity with generated links</returns>
        protected virtual TEntity CreateLinksForEntity<TEntity>
            (TEntity entity, Guid entityId, string selfLinkName, string updateLinkName, string deleteLinkName) 
            where TEntity : LinkBaseEntity
        {
            var idObj = new { id = entityId };
            
            entity.Links.Add(
                new Link(this._urlHelper.Link(selfLinkName, idObj)!,
                    "get_self",
                    "GET"));
            
            entity.Links.Add(
                new Link(this._urlHelper.Link(updateLinkName, idObj)!,
                    "update_self",
                    "UPDATE"));
            
            entity.Links.Add(
                new Link(this._urlHelper.Link(deleteLinkName, idObj)!,
                    "delete_self",
                    "DELETE"));

            return entity;
        }
    }
}