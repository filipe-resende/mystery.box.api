namespace Application.Features.Handlers;

public class PostPixPaymentHandler(
    IPaymentGatewayService paymentGatewayService,
    IUserRepository repository,
    IPaymentRepository paymentRepository,
    IMapper mapper,
    ILogger<PostPixPaymentHandler> logger
) : IRequestHandler<PostPixPaymentCommand, Result>
{
    private readonly IPaymentGatewayService _paymentGateway = paymentGatewayService;
    private readonly IUserRepository _repository = repository;
    private readonly IPaymentRepository _paymentRepository = paymentRepository;
    private readonly IMapper _mapper = mapper;
    private readonly ILogger<PostPixPaymentHandler> _logger = logger;

    public async Task<Result> Handle(PostPixPaymentCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var user = await _repository.GetById(request.UserId);

            if (user == null)
            {
                _logger.LogWarning("Usuário não encontrado no banco para o ID: {UserId}", request.UserId);
                return Result.Failure(new Error("PAYMENT001", "Usuário não autenticado."));
            }

            request.Payer = new Payer()
            {
                Name = user.Name,
                LastName = user.LastName,
                Email = user.Email,
                Identification = new Commands.Identification(
                    Type: user.Identification.Type,
                    Number: user.Identification.Number
                )
            };

            _logger.LogInformation("Iniciando processamento de pagamento via PIX para o usuário: {UserId}", user.Id);

            MercadoPago.Resource.Payment.Payment mpPayment = await _paymentGateway.PostPixAsync(request);

            var entity = _mapper.Map<Payment>(mpPayment);
            entity.UserId = user.Id;

            await _paymentRepository.Add(entity);

            return Result.Success(new ProcessPaymentResponseDTO
            {
                TransactionId = mpPayment.Id,
                Status = mpPayment.Status,
                Message = mpPayment.StatusDetail,
                QRCode = mpPayment.PointOfInteraction.TransactionData.QrCode,
                QRCodeBase64 = mpPayment.PointOfInteraction.TransactionData.QrCodeBase64,
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao processar pagamento via PIX.");
            return Result.Failure(new Error("PAYMENT999", $"Erro ao processar pagamento: {ex.Message}"));
        }
    }
}