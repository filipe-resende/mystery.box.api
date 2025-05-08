namespace Application.Features.Commands;

public class PasswordRequest(string password)
{
    public string Password { get; set; } = password;
}

