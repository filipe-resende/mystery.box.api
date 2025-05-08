namespace Application.Features.Handlers;

public class GetAuthenticationTokenHandler(
    IUserRepository repository,
    IAuthenticationService authenticationService,
    IMapper mapper,
    ILogger<GetAuthenticationTokenHandler> logger
) : IRequestHandler<GetAuthenticationTokenCommand, Result>
{
    private readonly IUserRepository _repository = repository;
    private readonly IAuthenticationService _authenticationService = authenticationService;
    private readonly IMapper _mapper = mapper;
    private readonly ILogger<GetAuthenticationTokenHandler> _logger = logger;

    public async Task<Result> Handle(GetAuthenticationTokenCommand request, CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("Iniciando autenticação para o email: {Email}", request.Email);

            var user = await _repository.GetUserByLogin(request.Email, request.Password);

            if (user == null)
            {
                _logger.LogWarning("Usuário ou senha inválidos para o email: {Email}", request.Email);
                return Result.Failure(new Error("AUTH001", "Usuário ou senha inválidos."));
            }

            var token = _authenticationService.CreateAuthenticationToken(user);

            _logger.LogInformation("Token gerado com sucesso para o usuário: {UserId}", user.Id);

            return Result.Success(new UserTokenDTO
            {
                User = _mapper.Map<UserDTO>(user),
                Token = token
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro inesperado durante o processo de autenticação para o email: {Email}", request.Email);
            return Result.Failure(new Error("AUTH999", $"Erro inesperado ao autenticar: {ex.Message}"));
        }
    }
}
