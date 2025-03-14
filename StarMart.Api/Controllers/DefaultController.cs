using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace StarMart.Api.Controllers
{
    [ApiController]
    public abstract class DefaultController : ControllerBase
    {
        protected readonly IMediator Mediator;

        public DefaultController(IMediator mediator)
        {
            Mediator = mediator;
        }
    }
}
