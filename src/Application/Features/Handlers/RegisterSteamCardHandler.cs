namespace Application.Features.Handlers;

public class RegisterSteamCardHandler(
    ISteamCardRepository repository,
    ILogger<RegisterSteamCardHandler> logger
) : IRequestHandler<RegisterSteamCardCommand, Result>
{
    private readonly ISteamCardRepository _repository = repository;
    private readonly ILogger<RegisterSteamCardHandler> _logger = logger;

    public async Task<Result> Handle(RegisterSteamCardCommand request, CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("Iniciando registro de SteamCard: {Name}", request.Name);

            var steamCard = new SteamCard
            {
                Name = request.Name,
                Key = request.Key,
                Description = request.Description,
                SteamCardCategory = new SteamCardCategory
                {
                    Id = request.SteamCardCategoryId
                },
            };

            var result = await _repository.Add(steamCard);

            _logger.LogInformation("SteamCard registrado com sucesso. ID: {Id}", result.Id);

            return Result.Success(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao registrar SteamCard: {Name}", request.Name);
            return Result.Failure(new Error("STEAMREG999", $"Erro ao registrar SteamCard: {ex.Message}"));
        }
    }
}
