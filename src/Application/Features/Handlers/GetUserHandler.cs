namespace Application.Features.Handlers;

public class GetUserHandler(
    IUserRepository repository,
    IMapper mapper,
    ILogger<GetUserHandler> logger
) : IRequestHandler<GetUserCommand, Result>
{
    private readonly IUserRepository _repository = repository;
    private readonly IMapper _mapper = mapper;
    private readonly ILogger<GetUserHandler> _logger = logger;

    public async Task<Result> Handle(GetUserCommand request, CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("Buscando dados do usuário com ID: {UserId}", request.Id);

            var user = await _repository.GetById(request.Id);

            if (user == null)
            {
                _logger.LogWarning("Usuário não encontrado com ID: {UserId}", request.Id);
                return Result.Failure(new Error("GETUSER001", "Usuário não encontrado."));
            }

            var userDto = _mapper.Map<UserDTO>(user);

            _logger.LogInformation("Usuário encontrado com sucesso: {UserId}", request.Id);
            return Result.Success(userDto);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao buscar usuário com ID: {UserId}", request.Id);
            return Result.Failure(new Error("GETUSER999", $"Erro ao buscar usuário: {ex.Message}"));
        }
    }
}
