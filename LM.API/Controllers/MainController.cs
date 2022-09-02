using LM.Domain.Common;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace LM.API.Controllers
{
    /// <summary>
    /// This is the base controller of this application that other controllers inherits.
    /// It helps in versioning the application. The Mediator is the messenger/middle man
    /// between this base controller and other controllers in this application.
    /// </summary>
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class MainController : BaseController
    {
        //Field declaration.
        private IMediator _mediator;

        /// <summary>
        /// This is the entry point to all the controllers in  this application.
        /// It send http request through the mediator messenger to any controller that is invoked.
        /// </summary>
        protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();
    }
}
