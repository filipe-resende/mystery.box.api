namespace Application.Features.Commands;

public class PasswordCommand(string password)
{
    public string Password { get; set; } = password;
}

