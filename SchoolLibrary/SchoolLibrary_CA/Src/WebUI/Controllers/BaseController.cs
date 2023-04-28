using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Controllers
{
    /// <summary>
    /// A base controller class with IMediator 
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
    }
}