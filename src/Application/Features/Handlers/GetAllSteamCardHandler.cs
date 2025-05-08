namespace Application.Features.Handlers;

public class GetAllSteamCardHandler(
    ISteamCardCategoryRepository repository,
    IMapper mapper,
    ILogger<GetAllSteamCardHandler> logger
) : IRequestHandler<GetAllSteamCardCommand, Result>
{
    private readonly ISteamCardCategoryRepository _repository = repository;
    private readonly IMapper _mapper = mapper;
    private readonly ILogger<GetAllSteamCardHandler> _logger = logger;

    public async Task<Result> Handle(GetAllSteamCardCommand request, CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("Buscando todas as categorias de Steam Cards");

            var categories = await _repository.GetAll();

            if (categories == null || !categories.Any())
            {
                _logger.LogWarning("Nenhuma categoria de Steam Card encontrada.");
                return Result.Failure(new Error("STEAMCAT001", "Nenhuma categoria encontrada."));
            }

            var result = _mapper.Map<IEnumerable<SteamCardCategoryDTO>>(categories);

            _logger.LogInformation("Categorias de Steam Cards retornadas com sucesso. Total: {Count}", result.Count());

            return Result.Success(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao buscar categorias de Steam Cards.");
            return Result.Failure(new Error("STEAMCAT999", $"Erro ao buscar categorias: {ex.Message}"));
        }
    }
}
