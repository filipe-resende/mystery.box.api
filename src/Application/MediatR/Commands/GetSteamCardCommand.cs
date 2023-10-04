using Domain.DTO;
using MediatR;

namespace Application.MediatR.Commands
{
    public class GetSteamCardCommand : IRequest<SteamCardDTO>
    {
        public GetSteamCardCommand(Guid id)
        {
            Id = id;
        }
        public Guid Id { get; set; }
    }
}