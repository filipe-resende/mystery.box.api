namespace Application.Features.Handlers;

public class GetForgottenUserHandler(
    IEmailService emailService,
    IAuthenticationService authenticationService,
    IUserRepository userRepository,
    ILogger<GetForgottenUserHandler> logger
) : IRequestHandler<GetForgottenUserCommand, Result>
{
    private readonly IEmailService _emailService = emailService;
    private readonly IAuthenticationService _authenticationService = authenticationService;
    private readonly IUserRepository _userRepository = userRepository;
    private readonly ILogger<GetForgottenUserHandler> _logger = logger;

    public async Task<Result> Handle(GetForgottenUserCommand request, CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("Solicitação de recuperação de senha iniciada para o email: {Email}", request.Email);

            var user = await _userRepository.GetUsersByEmail(request.Email);

            if (user == null)
            {
                _logger.LogWarning("Usuário não encontrado para o email: {Email}", request.Email);
                return Result.Failure(new Error("FORGOT001", "Usuário não encontrado."));
            }

            var token = _authenticationService.CreateAuthenticationToken(user);

            var resetLink = $"http://localhost:3000/login/reset/{token}";

            _emailService.SendEmail(
                emailTo: request.Email,
                subject: $"[Recuperação de Senha] {request.Email} - {DateTime.Now}",
                body: $@"Clique no link a seguir para redefinir sua senha: <a href='{resetLink}'>Redefinir Senha</a>"
            );

            _logger.LogInformation("Email de recuperação enviado com sucesso para {Email}", request.Email);

            return Result.Success(new ErrorResponseDTO
            {
                Status = 200,
                Error = "",
                Message = "Email enviado!"
            });
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Erro inesperado ao processar solicitação de recuperação de senha para o email: {Email}", request.Email);
            return Result.Failure(new Error("FORGOT999", $"Erro ao enviar e-mail de recuperação: {e.Message}"));
        }
    }
}