namespace Api.Controllers;

[ApiController]
[Route("[controller]")]
public class PaymentController(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    [HttpPost]
    [Authorize(Roles = "registered")]
    public async Task<IActionResult> Post(ProcessPaymentCommand command)
    {
        var response = await _mediator.Send(command);
        return Ok(response);
    }
}