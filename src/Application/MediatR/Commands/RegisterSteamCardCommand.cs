using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Application.MediatR.Commands
{
    public class RegisterSteamCardCommand : IRequest<Guid>
    {
        public Guid? UserId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Key { get; set; }
        [Required]
        public float Price { get; set; }
        [Required]
        public string Thumb { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public Guid SteamCardCategoryId { get; set; }
    }
}