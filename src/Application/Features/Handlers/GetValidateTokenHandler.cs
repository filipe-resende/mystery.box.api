namespace Application.Features.Handlers;

public class GetValidateTokenHandler(
    IUserRepository repository,
    IAuthenticationService authenticationService,
    IMapper mapper,
    ILogger<GetValidateTokenHandler> logger
) : IRequestHandler<GetValidateTokenCommand, Result>
{
    private readonly IAuthenticationService _authenticationService = authenticationService;
    private readonly IUserRepository _repository = repository;
    private readonly IMapper _mapper = mapper;
    private readonly ILogger<GetValidateTokenHandler> _logger = logger;

    public async Task<Result> Handle(GetValidateTokenCommand request, CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("Iniciando validação de token para o usuário: {UserId}", request.UserId);

            var user = await _repository.GetById(request.UserId);

            if (user == null)
            {
                _logger.LogWarning("Usuário não encontrado para o ID: {UserId}", request.UserId);
                return Result.Failure(new Error("TOKEN001", "Usuário não encontrado."));
            }

            var token = _authenticationService.CreateAuthenticationToken(user);

            _logger.LogInformation("Token revalidado com sucesso para o usuário: {UserId}", request.UserId);

            return Result.Success(new UserTokenDTO
            {
                User = _mapper.Map<UserDTO>(user),
                Token = token
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro inesperado ao validar token para o usuário: {UserId}", request.UserId);
            return Result.Failure(new Error("TOKEN999", $"Erro inesperado ao validar o token: {ex.Message}"));
        }
    }
}
