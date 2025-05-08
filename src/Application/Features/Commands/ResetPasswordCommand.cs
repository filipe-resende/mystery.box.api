namespace Application.Features.Commands;

public class ResetPasswordCommand(string password, string userId) : IRequest<Result>
{
    public string Password { get; set; } = password;
    public Guid UserId { get; set; } = Guid.Parse(userId);
};
