using cheburechnaya_core.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace cheburechnaya_core.Controllers {
    public class BaseController : ControllerBase {
        private IMediator _mediator;
        protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();

        protected async Task<IActionResult> Run<T>(IRunnableAction<T> command) => Ok(await Mediator.Send(command));
        protected async Task<IActionResult> Run<T>(IRunnableAction<T> command, int id) {
            // todo: тут надо быть сделать строго типизированный вариант
            (command as dynamic).Id = id;
            return Ok(await Mediator.Send(command));
        }
    }
}
