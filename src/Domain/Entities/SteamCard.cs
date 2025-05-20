namespace Domain.Entities;

[Table("SteamCard")]
public class SteamCard
{
    [Key]
    public Guid Id { get; set; }

    [Required(ErrorMessage = "Name is required")]
    [StringLength(100, ErrorMessage = "Name can't exceed 100 characters")]
    public string Name { get; set; }

    public string Key { get; set; }

    [StringLength(1000, ErrorMessage = "Description can't exceed 1000 characters")]
    public string Description { get; set; }

    [ForeignKey("User")]
    public Guid? UserId { get; set; }

    public virtual User User { get; set; }

    [ForeignKey("SteamCardCategory")]
    public Guid? SteamCardCategoryId { get; set; }

    public virtual SteamCardCategory SteamCardCategory { get; set; }

    public bool Active { get; set; } = true;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public SteamCardStatus Status { get; set; } = SteamCardStatus.Available;
}