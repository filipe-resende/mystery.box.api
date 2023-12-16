using Domain.DTO;
using MediatR;

namespace Application.MediatR.Commands
{
    public class GetAllSteamCardCommand : IRequest<IEnumerable<SteamCardCategoryDTO>>
    {
    }
}
