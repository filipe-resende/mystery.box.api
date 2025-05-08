namespace Application.Features.Handlers;

public class RegisterSteamCardHandlerCategoryHandler(ISteamCardCategoryRepository repository) : IRequestHandler<RegisterSteamCardCategoryCommand, Guid>
{
    private readonly ISteamCardCategoryRepository _repository = repository;

    public async Task<Guid> Handle(RegisterSteamCardCategoryCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var steamCard = new SteamCardCategory
            {
                Name = request.Name,
                Price = request.Price,
                Thumb = request.Thumb,
                Description = request.Description,
            };

            var result = await _repository.Add(steamCard);
            return result.Id;
        }
        catch (Exception ex)
        {
            throw new Exception($"could not be saved. Error {ex.Message}");
        }
    }
}
