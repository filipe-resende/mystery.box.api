using Domain.DTO;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Application.MediatR.Commands
{
    public class GetForgottenUserCommand : IRequest<ErrorResponseDTO>
    {
        public GetForgottenUserCommand(string email)
        {
            Email = email;
        }
        [Required]
        public string Email { get; set; }
    }
}