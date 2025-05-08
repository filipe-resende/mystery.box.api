namespace Application.Features.Commands;

public class GetAuthenticationTokenCommand(string email, string password) : IRequest<Result>
{
    [Required]
    public string Email { get; set; } = email;
    [Required]
    public string Password { get; set; } = password;
}
