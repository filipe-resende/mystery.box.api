using Domain.DTO;
using MediatR;

namespace Application.Controllers
{
    public class ResetPasswordCommand: IRequest<ErrorResponseDTO>
    {
        public ResetPasswordCommand(string password, string userId)
        {
            Password = password;
            UserId = Guid.Parse(userId);
        }

        public string Password { get; set; }
        public Guid UserId { get; set; }
    }
}