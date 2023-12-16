using Application.MediatR.Commands;
using Domain.DTO;
using Domain.Interfaces.Repositories;
using MediatR;

namespace Application.MediatR.Handlers
{
    public class RegisterSteamCardHandler : IRequestHandler<RegisterSteamCardCommand, Guid>
    {
        private readonly ISteamCardRepository _repository;

        public RegisterSteamCardHandler(ISteamCardRepository repository)
        {
            _repository = repository;
        }

        public async Task<Guid> Handle(RegisterSteamCardCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var steamCard = new SteamCardDTO
                {
                    Name = request.Name,
                    Key = request.Key,
                    Description = request.Description,
                    SteamCardCategory = new SteamCardCategoryDTO 
                    {
                        Id = request.SteamCardCategoryId
                    } ,
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
