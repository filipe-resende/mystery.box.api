using Application.MediatR.Commands;
using Domain.DTO;
using Domain.Interfaces.Repositories;
using MediatR;

namespace Application.MediatR.Handlers
{
    public class GetAllSteamCardHandler : IRequestHandler<GetAllSteamCardCommand, IEnumerable<SteamCardCategoryDTO>>
    {
        private readonly ISteamCardCategoryRepository _repository;

        public GetAllSteamCardHandler(ISteamCardCategoryRepository repository) => _repository = repository;

        public async Task<IEnumerable<SteamCardCategoryDTO>> Handle(GetAllSteamCardCommand request, CancellationToken cancellationToken)
        {
            var steamCardsCategory = _repository.GetAll();
            return steamCardsCategory;
        }
    }
}
