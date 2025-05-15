namespace Application.Features.Handlers;

public class ProcessPaymentHandler(
    IPaymentGatewayService paymentGatewayService,
    IUserRepository repository,
    ILogger<ProcessPaymentHandler> logger
) : IRequestHandler<ProcessPaymentCommand, Result>
{
    private readonly IPaymentGatewayService _paymentGateway = paymentGatewayService;
    private readonly ILogger<ProcessPaymentHandler> _logger = logger;
    private readonly IUserRepository _repository = repository;

    public async Task<Result> Handle(ProcessPaymentCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var user = await _repository.GetById(request.UserId);

            if (user == null)
            {
                _logger.LogWarning("Usuário não encontrado no banco para o ID: {UserId}", user);
                return Result.Failure(new Error("PAYMENT001", "Usuário não autenticado."));
            }

            request.Payer.Email = user.Email;

            _logger.LogInformation("Iniciando processamento de pagamento para o usuário: {UserId}", user.Id);

            //var response = await _paymentGateway.ProcessAsync(request);

            var mock = new ProcessPaymentResponseDTO() {TransactionId = 123456, Message = "approved", Status = "approved" };

            return Result.Success(mock);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao processar pagamento.");
            return Result.Failure(new Error("PAYMENT999", $"Erro ao processar pagamento: {ex.Message}"));
        }
    }
}