using Domain.Interfaces;

namespace Domain.DTO
{
    public class SteamCardDTO : IEntityDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Key { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool Active { get; set; }
        public SteamCardCategoryDTO SteamCardCategory { get; set; }
    }
}
