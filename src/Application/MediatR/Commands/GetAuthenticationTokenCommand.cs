using Domain.DTO;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Application.MediatR.Commands
{
    public class GetAuthenticationTokenCommand : IRequest<UserTokenDTO>
    {
        public GetAuthenticationTokenCommand(string email, string password)
        {
            Email = email;
            Password = password;
        }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}