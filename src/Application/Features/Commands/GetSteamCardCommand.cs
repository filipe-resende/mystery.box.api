namespace Application.Features.Commands;

public class GetSteamCardCommand(Guid id) : IRequest<SteamCardDTO>
{
    public Guid Id { get; set; } = id;
}
