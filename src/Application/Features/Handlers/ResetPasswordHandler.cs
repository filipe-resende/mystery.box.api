namespace Application.Features.Handlers;

public class ResetPasswordHandler(
    IUserRepository userRepository,
    ILogger<ResetPasswordHandler> logger
) : IRequestHandler<ResetPasswordCommand, Result>
{
    private readonly IUserRepository _userRepository = userRepository;
    private readonly ILogger<ResetPasswordHandler> _logger = logger;

    public async Task<Result> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("Solicitação de redefinição de senha recebida para o usuário: {UserId}", request.UserId);

            var user = await _userRepository.GetById(request.UserId);

            if (user == null)
            {
                _logger.LogWarning("Usuário não encontrado para o ID: {UserId}", request.UserId);
                return Result.Failure(new Error("RESET001", "Usuário não encontrado."));
            }

            user.Password = request.Password;
            await _userRepository.Update(user);

            _logger.LogInformation("Senha redefinida com sucesso para o usuário: {UserId}", request.UserId);

            return Result.Success(new ErrorResponseDTO
            {
                Status = 200,
                Error = "",
                Message = "Senha alterada com sucesso."
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao redefinir a senha para o usuário: {UserId}", request.UserId);
            return Result.Failure(new Error("RESET999", $"Erro ao redefinir a senha: {ex.Message}"));
        }
    }
}
