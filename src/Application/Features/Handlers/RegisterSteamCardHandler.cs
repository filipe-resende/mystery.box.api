namespace Application.Features.Handlers;
    public class RegisterSteamCardHandler(ISteamCardRepository repository) : IRequestHandler<RegisterSteamCardCommand, Guid>
    {
        private readonly ISteamCardRepository _repository = repository;

        public async Task<Guid> Handle(RegisterSteamCardCommand request, CancellationToken cancellationToken)
        {
            try
            {
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
                return result.Id;
            }
            catch (Exception ex)
            {
                throw new Exception($"could not be saved. Error {ex.Message}");
            }
        }
    }

