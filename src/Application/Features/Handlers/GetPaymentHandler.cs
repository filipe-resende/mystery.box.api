namespace Application.Features.Handlers;

public class GetPaymentHandler(
    IPaymentGatewayService paymentGatewayService,
    IUserRepository repository,
    IPaymentRepository paymentRepository,
    IMapper mapper,
    ILogger<GetPaymentHandler> logger
) : IRequestHandler<GetPaymentCommand, Result>
{
    private readonly IPaymentGatewayService _paymentGateway = paymentGatewayService;
    private readonly IUserRepository _repository = repository;
    private readonly IPaymentRepository _paymentRepository = paymentRepository;
    private readonly IMapper _mapper = mapper;
    private readonly ILogger<GetPaymentHandler> _logger = logger;

    public async Task<Result> Handle(GetPaymentCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var user = await _repository.GetById(request.UserId);

            if (user == null)
            {
                _logger.LogWarning("Usuário não encontrado no banco para o ID: {UserId}", request.UserId);
                return Result.Failure(new Error("PAYMENT001", "Usuário não autenticado."));
            }

            _logger.LogInformation("Buscando pagamento no MercadoPago para o ID: {PaymentId}", request.PaymentId);

            MercadoPago.Resource.Payment.Payment mpPayment = await _paymentGateway.GetPaymentAsync(request.PaymentId);

            if (mpPayment.Id == null)
            {
                _logger.LogWarning("Pagamento retornado do Mercado Pago sem ID.");
                return Result.Failure(new Error("PAYMENT003", "ID do pagamento não informado na resposta do Mercado Pago."));
            }

            var entity = await _paymentRepository.GetByMercadoPagoIdAsync(mpPayment?.Id);

            if (entity == null)
            {
                _logger.LogWarning("Pagamento com MercadoPagoPaymentId {PaymentId} não encontrado localmente.", mpPayment!.Id);
                return Result.Failure(new Error("PAYMENT002", "Pagamento não encontrado localmente."));
            }

            _mapper.Map(mpPayment, entity);

            await _paymentRepository.Update(entity);

            return Result.Success(new ProcessPaymentResponseDTO
            {
                TransactionId = mpPayment!.Id,
                Status = mpPayment.Status,
                Message = mpPayment.StatusDetail
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao consultar e atualizar pagamento.");
            return Result.Failure(new Error("PAYMENT999", $"Erro ao consultar pagamento: {ex.Message}"));
        }
    }
}
