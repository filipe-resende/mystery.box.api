namespace Application.MediatR.Commands
{
    public class PasswordCommand
    {
        public PasswordCommand(string password)
        {
            Password = password;
        }

        public string Password { get; set; }
    }
}
