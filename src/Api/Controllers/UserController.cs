namespace Api.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController(IMediator mediator, ILogger<UserController> logger) : ControllerBase
{
    private readonly IMediator _mediator = mediator;
    private readonly ILogger<UserController> _logger = logger;


    [HttpPost]
    [AllowAnonymous]
    [Route("signIn")]
    public async Task<Result> Post([FromBody] GetAuthenticationTokenCommand command) => await _mediator.Send(command);

    [HttpPost]
    [Route("validate")]
    [Authorize(Roles = "registered")]
    public async Task<Result> Validate()
    {
        var user = User.FindFirst(ClaimTypes.Sid)?.Value;

        if (user == null)
        {
            _logger.LogWarning("Usuário não encontrado para o TraceId: {TraceIdentifier}", HttpContext.TraceIdentifier);
            return Result.Failure(new Error("TOKEN001", "Usuário não encontrado."));
        }

        var command = new GetValidateTokenCommand(user);

        return await _mediator.Send(command);
    }

    [HttpPost]
    [Route("forgotten")]
    public async Task<Result> ResetPassword([FromBody] GetForgottenUserCommand command) => await _mediator.Send(command);


    [HttpGet]
    [Route("active/{token}")]
    public async Task<Result> ActiverUser(string token)
    {
        var command = new ActiverUserCommand();
        return await _mediator.Send(command);
    }

    [HttpPatch]
    [Route("reset")]
    [Authorize(Roles = "registered")]
    public async Task<Result> ResetPassword([FromBody] PasswordRequest request)
    {

        var user = User.FindFirst(ClaimTypes.Sid)?.Value;

        if (user == null)
        {
            _logger.LogWarning("Usuário não encontrado para o TraceId: {TraceIdentifier}", HttpContext.TraceIdentifier);
            return Result.Failure(new Error("TOKEN001", "Usuário não encontrado."));
        }

        var command = new ResetPasswordCommand(request.Password, user);
        return await _mediator.Send(command);
    }

    [HttpGet("{id}")]
    public async Task<Result> GetUser(Guid id)
    {
        var command = new GetUserCommand(id);
        return await _mediator.Send(command);
    }

    [HttpPost]
    public async Task<Result> PostUser(RegisterUserCommand command) =>  await _mediator.Send(command);
}
