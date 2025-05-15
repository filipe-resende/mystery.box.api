namespace Api.Controllers;

[ApiController]
[Route("[controller]")]
public class PaymentController(IMediator mediator, ILogger<UserController> logger) : ControllerBase
{
    private readonly IMediator _mediator = mediator;
    private readonly ILogger<UserController> _logger = logger;

    [HttpPost]
    [Authorize(Roles = "registered")]
    public async Task<Result> Post(ProcessPaymentCommand command)
    {
        var userId = User.FindFirst(ClaimTypes.Sid)?.Value;

        if (userId == null)
        {
            _logger.LogWarning("Usuário não encontrado para o TraceId: {TraceIdentifier}", HttpContext.TraceIdentifier);
            return Result.Failure(new Error("TOKEN001", "Usuário não encontrado."));
        }

        command.UserId = Guid.Parse(userId);

        return await _mediator.Send(command);
    }


    [HttpGet]
    [Authorize(Roles = "registered")]
    public async Task<Result> Get(GetPaymentCommand command)
    {
        //TODO
        var userId = User.FindFirst(ClaimTypes.Sid)?.Value;

        if (userId == null)
        {
            _logger.LogWarning("Usuário não encontrado para o TraceId: {TraceIdentifier}", HttpContext.TraceIdentifier);
            return Result.Failure(new Error("TOKEN001", "Usuário não encontrado."));
        }

        command.UserId = Guid.Parse(userId);

        return await _mediator.Send(command);
    }
}