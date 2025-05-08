namespace Application.Features.Commands;

public class GetValidateTokenCommand(string userId) : IRequest<UserTokenDTO>
{
    public Guid UserId { get; } = Guid.Parse(userId);
}

