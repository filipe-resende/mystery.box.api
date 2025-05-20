namespace Api.Controllers;

[ApiController]
[Route("[controller]")]
public class PaymentController(IMediator mediator, ILogger<UserController> logger) : ControllerBase
{
    private readonly IMediator _mediator = mediator;
    private readonly ILogger<UserController> _logger = logger;

    [HttpPost]
    [Authorize(Roles = "registered")]
    public async Task<Result> Post(PostCreditCardPaymentCommand command)
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


    [HttpGet("{id}")]
    [Authorize(Roles = "registered")]
    public async Task<Result> Get(long id)
    {
        var userId = User.FindFirst(ClaimTypes.Sid)?.Value;

        if (userId == null)
        {
            _logger.LogWarning("Usuário não encontrado para o TraceId: {TraceIdentifier}", HttpContext.TraceIdentifier);
            return Result.Failure(new Error("TOKEN001", "Usuário não encontrado."));
        }

        var command = new GetPaymentCommand(Guid.Parse(userId), id);

        return await _mediator.Send(command);
    }

    [HttpPost("pix")]
    [Authorize(Roles = "registered")]
    public async Task<Result> PostPix(PostPixPaymentCommand command)
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
}