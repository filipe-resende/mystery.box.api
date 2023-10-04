using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Application.MediatR.Commands
{
    public class RegisterSteamCardCommand : IRequest<Guid>
    {
        [Required]
        public Guid UserId { get; set; }
        public string Title { get; set; }
    }
}