namespace Application.Features.Commands;

public class GetAuthenticationTokenCommand(string email, string password) : IRequest<UserTokenDTO>
{
    [Required]
    public string Email { get; set; } = email;
    [Required]
    public string Password { get; set; } = password;
}
