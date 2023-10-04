using Application.MediatR.Commands;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Application.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator) => _mediator = mediator;

        [HttpPost]
        [AllowAnonymous]
        [Route("auth")]
        public async Task<IActionResult> Post([FromBody] GetAuthenticationTokenCommand command)
        {
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var command = new GetUserCommand(id);
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> Post(RegisterUserCommand command)
        {
            var response = await _mediator.Send(command);
            return Ok(response);
        }

    }
}
