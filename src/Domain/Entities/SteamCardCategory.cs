namespace Domain.Entities;

[Table("SteamCardCategory")]
public class SteamCardCategory
{
    [Key]
    public Guid Id { get; set; }

    [Required(ErrorMessage = "Name is required")]
    [StringLength(100, ErrorMessage = "Name can't exceed 100 characters")]
    public string Name { get; set; }

    [Range(0, double.MaxValue, ErrorMessage = "Price must be a positive value")]
    public float Price { get; set; }

    [StringLength(255, ErrorMessage = "Thumbnail path can't exceed 255 characters")]
    public string Thumb { get; set; }

    [StringLength(1000, ErrorMessage = "Description can't exceed 1000 characters")]
    public string Description { get; set; }

    public bool Active { get; set; } = true;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
