namespace Application.Features.Handlers;

public class GetSteamCardHandler(
    ISteamCardRepository repository,
    IMapper mapper,
    ILogger<GetSteamCardHandler> logger
) : IRequestHandler<GetSteamCardCommand, Result>
{
    private readonly ISteamCardRepository _repository = repository;
    private readonly IMapper _mapper = mapper;
    private readonly ILogger<GetSteamCardHandler> _logger = logger;

    public async Task<Result> Handle(GetSteamCardCommand request, CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("Buscando SteamCard com ID: {Id}", request.Id);

            var steamcard = await _repository.GetById(request.Id);

            if (steamcard == null)
            {
                _logger.LogWarning("SteamCard não encontrado para o ID: {Id}", request.Id);
                return Result.Failure(new Error("STEAM001", "Cartão não encontrado."));
            }

            var dto = _mapper.Map<SteamCardDTO>(steamcard);

            _logger.LogInformation("SteamCard encontrado com sucesso: {Id}", request.Id);

            return Result.Success(dto);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao buscar SteamCard com ID: {Id}", request.Id);
            return Result.Failure(new Error("STEAM999", $"Erro inesperado ao buscar cartão: {ex.Message}"));
        }
    }
}
