using Application.MediatR.Commands;
using Domain.DTO;
using Domain.Interfaces.Repositories;
using MediatR;

namespace Application.MediatR.Handlers
{
    public class GetSteamCardHandler : IRequestHandler<GetSteamCardCommand, SteamCardDTO>
    {
        private readonly ISteamCardRepository _repository;

        public GetSteamCardHandler(ISteamCardRepository repository)
        {
            _repository = repository;
        }

        public async Task<SteamCardDTO> Handle(GetSteamCardCommand request, CancellationToken cancellationToken)
        {
            try
            {
                return await _repository.GetById(request.Id);
            }
            catch (Exception ex)
            {
                throw new Exception($"could not be saved. Error {ex.Message}");
            }
        }
    }
}
