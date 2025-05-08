namespace Application.Features.Commands;

public class GetSteamCardCommand(Guid id) : IRequest<Result>
{
    public Guid Id { get; set; } = id;
}
