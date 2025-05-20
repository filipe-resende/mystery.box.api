namespace Application.Features.Commands;

public class GetUserProfileCommand(Guid UserId) : IRequest<Result>
{
    public Guid UserId { get; set; } = UserId;
}
