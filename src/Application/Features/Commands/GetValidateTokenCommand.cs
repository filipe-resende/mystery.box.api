namespace Application.Features.Commands;

public class GetValidateTokenCommand(string userId) : IRequest<Result>
{
    public Guid UserId { get; } = Guid.Parse(userId);
}

