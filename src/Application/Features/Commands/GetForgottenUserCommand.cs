namespace Application.Features.Commands;

public class GetForgottenUserCommand(string email) : IRequest<Result>
{
    [Required]
    public string Email { get; set; } = email;
}
