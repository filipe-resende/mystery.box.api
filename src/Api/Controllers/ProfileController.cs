namespace Api.Controllers;

[ApiController]
[Route("[controller]")]
public class ProfileController(IMediator mediator, ILogger<ProfileController> logger) : ControllerBase
{
    private readonly IMediator _mediator = mediator;
    private readonly ILogger<ProfileController> _logger = logger;

    [HttpPut]
    [Authorize(Roles = "registered")]
    public async Task<Result> UpdateProfile([FromBody] UpdateUserProfileRequest request)
    {
        var userId = User.FindFirst(ClaimTypes.Sid)?.Value;

        if (userId == null)
        {
            _logger.LogWarning("Usuário não encontrado para o TraceId: {TraceIdentifier}", HttpContext.TraceIdentifier);
            return Result.Failure(new Error("TOKEN001", "Usuário não encontrado."));
        }

        var command = new UpdateUserProfileCommand
        {
            UserId = Guid.Parse(userId),
            Name = request.Name,
            Phone = request.Phone,
            Email = request.Email
        };

        return await _mediator.Send(command);
    }


    [HttpGet]
    [Authorize(Roles = "registered")]
    public async Task<Result> GetProfile()
    {
        var userId = User.FindFirst(ClaimTypes.Sid)?.Value;

        if (userId == null)
        {
            _logger.LogWarning("Usuário não encontrado para o TraceId: {TraceIdentifier}", HttpContext.TraceIdentifier);
            return Result.Failure(new Error("TOKEN001", "Usuário não encontrado."));
        }

        return await _mediator.Send(new GetUserProfileCommand(Guid.Parse(userId)));
    }
}

public record UpdateUserProfileRequest(string Name, string Email, string Phone);
