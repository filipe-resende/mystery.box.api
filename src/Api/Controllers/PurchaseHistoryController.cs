namespace Api.Controllers;

[ApiController]
[Route("[controller]")]
public class PurchaseHistoryController(IMediator mediator, ILogger<PurchaseHistoryController> logger) : ControllerBase
{
    private readonly IMediator _mediator = mediator;
    private readonly ILogger<PurchaseHistoryController> _logger = logger;

    [HttpGet]
    [Authorize(Roles = "registered")]
    public async Task<Result> Get()
    {
        var userId = User.FindFirst(ClaimTypes.Sid)?.Value;

        if (userId == null)
        {
            _logger.LogWarning("Usuário não encontrado para o TraceId: {TraceIdentifier}", HttpContext.TraceIdentifier);
            return Result.Failure(new Error("TOKEN001", "Usuário não encontrado."));
        }

        return await _mediator.Send(new GetPurchaseHistoryCommand(Guid.Parse(userId)));
    }
}