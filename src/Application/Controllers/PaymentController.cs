using Application.MediatR.Commands;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Application.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PaymentController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PaymentController(IMediator mediator) => _mediator = mediator;

        [HttpPost]
        [Authorize(Roles = "registered")]
        public async Task<IActionResult> Post(ProcessPaymentCommand command)
        {
            var response = await _mediator.Send(command);
            return Ok(response);
        }
    }
}
