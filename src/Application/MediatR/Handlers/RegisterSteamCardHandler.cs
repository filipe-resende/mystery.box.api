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
                var post = new SteamCardDTO { Title = request.Title };
                var result = await _repository.Add(post);
                return result.Id;
            }
            catch (Exception ex)
            {
                throw new Exception($"could not be saved. Error {ex.Message}");
            }
        }
    }
}
