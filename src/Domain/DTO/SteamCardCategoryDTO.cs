using Domain.Interfaces;

namespace Domain.DTO
{
    public class SteamCardCategoryDTO : IEntityDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Quantity { get; set; }
        public float Price { get; set; }
        public string Thumb { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool Active { get; set; }
    }
}
