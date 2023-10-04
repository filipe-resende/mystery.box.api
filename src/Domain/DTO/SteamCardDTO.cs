using Domain.Interfaces;

namespace Domain.DTO
{
    public class SteamCardDTO : IEntityDTO
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool Active { get; set; }
    }
}
