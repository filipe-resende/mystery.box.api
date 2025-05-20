namespace Application.Features.Handlers;

public class PostCreditCardPaymentHandler(
    IPaymentGatewayService paymentGatewayService,
    IUserRepository repository,
    IPaymentRepository paymentRepository,
    IMapper mapper,
    ILogger<PostCreditCardPaymentHandler> logger
) : IRequestHandler<PostCreditCardPaymentCommand, Result>
{
    private readonly IPaymentGatewayService _paymentGateway = paymentGatewayService;
    private readonly IUserRepository _repository = repository;
    private readonly IPaymentRepository _paymentRepository = paymentRepository;
    private readonly IMapper _mapper = mapper;
    private readonly ILogger<PostCreditCardPaymentHandler> _logger = logger;

    public async Task<Result> Handle(PostCreditCardPaymentCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var user = await _repository.GetById(request.UserId);

            if (user == null)
            {
                _logger.LogWarning("Usuário não encontrado no banco para o ID: {UserId}", request.UserId);
                return Result.Failure(new Error("PAYMENT001", "Usuário não autenticado."));
            }

            request.Payer.Email = user.Email;

            _logger.LogInformation("Iniciando processamento de pagamento para o usuário: {UserId}", user.Id);

            MercadoPago.Resource.Payment.Payment mpPayment = await _paymentGateway.PostCreditCardAsync(request);

            var entity = _mapper.Map<Payment>(mpPayment);
            entity.UserId = user.Id;

            foreach (var purchaseHistory in entity.PurchaseHistories)
            {
                purchaseHistory.PaymentId = entity.Id;
                purchaseHistory.UserId = request.UserId;
            }

            await _paymentRepository.Add(entity);

            return Result.Success(new ProcessPaymentResponseDTO
            {
                TransactionId = mpPayment.Id,
                Status = mpPayment.Status,
                Message = mpPayment.StatusDetail
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao processar pagamento.");
            return Result.Failure(new Error("PAYMENT999", $"Erro ao processar pagamento: {ex.Message}"));
        }
    }
}

