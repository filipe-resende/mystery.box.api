using MediatR;

namespace Application.MediatR.Commands
{
    public class GetAuthenticationTokenCommand : IRequest<string>
    {
        public GetAuthenticationTokenCommand(string email, string password)
        {
            Email = email;
            Password = password;
        }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}