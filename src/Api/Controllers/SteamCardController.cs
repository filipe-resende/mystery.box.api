namespace Api.Controllers;

[ApiController]
[Route("[controller]")]
public class SteamCardController(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    [HttpGet("{id}")]
    public async Task<Result> Get(Guid id) => await _mediator.Send(new GetSteamCardCommand(id));

    [AllowAnonymous]
    [HttpGet("category")]
    public async Task<Result> GetAll() => await _mediator.Send(new GetAllSteamCardCommand());

    [HttpPost]
    public async Task<Result> Post(RegisterSteamCardCommand command) => await _mediator.Send(command);

    [HttpPost("category")]
    public async Task<Result> PostCategory(RegisterSteamCardCategoryCommand command) => await _mediator.Send(command);
}

