namespace Application.Features.Handlers;

public class GetPurchaseHistoryHandler(
    IUserRepository repository,
    IPurchaseHistoryRepository purchaseHistoryRepository,
    IMapper mapper,
    ILogger<GetPurchaseHistoryHandler> logger
) : IRequestHandler<GetPurchaseHistoryCommand, Result>
{
    private readonly IUserRepository _repository = repository;
    private readonly IPurchaseHistoryRepository _purchaseHistoryRepository = purchaseHistoryRepository;
    private readonly IMapper _mapper = mapper;
    private readonly ILogger<GetPurchaseHistoryHandler> _logger = logger;

    public async Task<Result> Handle(GetPurchaseHistoryCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var user = await _repository.GetById(request.UserId);

            if (user is null)
            {
                _logger.LogWarning("Usuário não encontrado no banco para o ID: {UserId}", request.UserId);
                return Result.Failure(new Error("PURCHASEHISTORY001", "Usuário não autenticado."));
            }

            var historyList = await _purchaseHistoryRepository.GetByUserId(user.Id);

            if (!historyList.Any())
            {
                _logger.LogInformation("Nenhum histórico de compras encontrado para o usuário: {UserId}", request.UserId);
                return Result.Success(Enumerable.Empty<PurchaseHistoryResponseDTO>());
            }

            var dtoList = _mapper.Map<List<PurchaseHistoryResponseDTO>>(historyList);

            return Result.Success(dtoList);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao buscar histórico de compras.");
            return Result.Failure(new Error("PURCHASEHISTORY999", $"Erro ao buscar histórico: {ex.Message}"));
        }
    }
}
