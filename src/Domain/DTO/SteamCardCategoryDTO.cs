namespace Domain.DTO;

public class SteamCardCategoryDTO : IEntityDTO
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Quantity { get; set; }
    public float UnitPrice { get; set; }
    public string PictureUrl { get; set; }
    public string Description { get; set; }
    public string CategoryId { get; set; }
    public DateTime CreatedAt { get; set; }
    public bool Active { get; set; }
}