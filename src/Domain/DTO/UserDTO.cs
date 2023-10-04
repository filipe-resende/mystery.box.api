using Domain.Entities;
using Domain.Interfaces;

namespace Domain.DTO
{
    public class UserDTO : IEntityDTO
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public virtual IEnumerable<SteamCard> SteamCard { get; set; } = Enumerable.Empty<SteamCard>();
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool Active { get; set; }
    }
}
