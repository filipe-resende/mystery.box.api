namespace Application.Features.Handlers;

public class GetPaymentHandler(
    IPaymentGatewayService paymentGatewayService,
    IUserRepository repository,
    ILogger<GetPaymentHandler> logger
) : IRequestHandler<GetPaymentCommand, Result>
{
    private readonly IPaymentGatewayService _paymentGateway = paymentGatewayService;
    private readonly ILogger<GetPaymentHandler> _logger = logger;
    private readonly IUserRepository _repository = repository;

    public async Task<Result> Handle(GetPaymentCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var user = await _repository.GetById(request.UserId);

            if (user == null)
            {
                _logger.LogWarning("Usuário não encontrado no banco para o ID: {UserId}", user);
                return Result.Failure(new Error("PAYMENT001", "Usuário não autenticado."));
            }

            _logger.LogInformation("Iniciando processamento de pagamento para o usuário: {UserId}", user.Id);

            var response = await _paymentGateway.GetPaymentAsync(request.PaymentId);

            return Result.Success(response);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao processar pagamento.");
            return Result.Failure(new Error("PAYMENT999", $"Erro ao processar pagamento: {ex.Message}"));
        }
    }
}