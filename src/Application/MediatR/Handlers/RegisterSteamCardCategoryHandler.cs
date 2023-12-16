using Application.MediatR.Commands;
using Domain.DTO;
using Domain.Interfaces.Repositories;
using MediatR;

namespace Application.MediatR.Handlers
{
    public class RegisterSteamCardHandlerCategoryHandler : IRequestHandler<RegisterSteamCardCategoryCommand, Guid>
    {
        private readonly ISteamCardCategoryRepository _repository;

        public RegisterSteamCardHandlerCategoryHandler(ISteamCardCategoryRepository repository)
        {
            _repository = repository;
        }

        public async Task<Guid> Handle(RegisterSteamCardCategoryCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var steamCard = new SteamCardCategoryDTO
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
}
