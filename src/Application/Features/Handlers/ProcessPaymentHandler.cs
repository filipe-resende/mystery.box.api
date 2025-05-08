namespace Application.Features.Handlers;

public class ProcessPaymentHandler(
    IGetUserFromToken getUserFromToken,
    ILogger<ProcessPaymentHandler> logger
) : IRequestHandler<ProcessPaymentCommand, Result>
{
    private readonly IGetUserFromToken _getUserFromToken = getUserFromToken;
    private readonly ILogger<ProcessPaymentHandler> _logger = logger;

    public async Task<Result> Handle(ProcessPaymentCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var user = await _getUserFromToken.GetUserIdFromToken();

            if (user == null)
            {
                _logger.LogWarning("Usuário não encontrado no token.");
                return Result.Failure(new Error("PAYMENT001", "Usuário não autenticado."));
            }

            _logger.LogInformation("Iniciando processamento de pagamento para o usuário: {UserId}", user.Id);

            MercadoPagoConfig.AccessToken = "SUA_ACCESS_TOKEN_AQUI";

            var orderRequest = new OrderCreateRequest
            {
                Type = "online",
                TotalAmount = request.Amount.ToString("F2", System.Globalization.CultureInfo.InvariantCulture),
                ExternalReference = $"order_{Guid.NewGuid()}",
                ProcessingMode = "manual",
                Payer = new OrderPayerRequest
                {
                    Email = request.PayerEmail
                }
            };

            var client = new OrderClient();

            // Cria o pedido
            var order = await client.CreateAsync(orderRequest);

            // Cria a transação de pagamento
            var transactionRequest = new OrderTransactionRequest
            {
                Payments = new List<OrderPaymentRequest>
                {
                    new OrderPaymentRequest
                    {
                        Amount = request.Amount.ToString("F2", System.Globalization.CultureInfo.InvariantCulture),
                        PaymentMethod = new OrderPaymentMethodRequest
                        {
                            Id = request.Card.BrandId,
                            Type = "credit_card",
                            Token = request.Card.BrandId,
                            Installments = request.Installments
                        }
                    }
                }
            };

            var transaction = await client.CreateTransactionAsync(order.Id, transactionRequest);


            return Result.Success(new ProcessPaymentResponseDTO
            {
                Status = transaction.ApiResponse.StatusCode,
                Content = transaction.ApiResponse.Content
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao processar pagamento.");
            return Result.Failure(new Error("PAYMENT999", $"Erro ao processar pagamento: {ex.Message}"));
        }
    }
}
