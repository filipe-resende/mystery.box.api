namespace Application.Features.Handlers;

public class RegisterSteamCardHandlerCategoryHandler(
    ISteamCardCategoryRepository repository,
    ILogger<RegisterSteamCardHandlerCategoryHandler> logger
) : IRequestHandler<RegisterSteamCardCategoryCommand, Result>
{
    private readonly ISteamCardCategoryRepository _repository = repository;
    private readonly ILogger<RegisterSteamCardHandlerCategoryHandler> _logger = logger;

    public async Task<Result> Handle(RegisterSteamCardCategoryCommand request, CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("Iniciando registro de categoria SteamCard: {Name}", request.Name);

            var steamCardCategory = new SteamCardCategory
            {
                Title = request.Name,
                UnitPrice = request.Price,
                PictureUrl = request.Thumb,
                Description = request.Description,
            };

            var result = await _repository.Add(steamCardCategory);

            _logger.LogInformation("Categoria registrada com sucesso. ID: {Id}", result.Id);

            return Result.Success(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao registrar categoria SteamCard: {Name}", request.Name);
            return Result.Failure(new Error("STEAMCATREG999", $"Erro ao registrar categoria: {ex.Message}"));
        }
    }
}
