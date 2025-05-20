namespace Domain.Entities;

[Table("PurchaseHistory")]
public class PurchaseHistory
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();

    [Required]
    public decimal? Quantity { get; set; }

    [Required]
    public decimal? UnitPrice { get; set; }

    public SteamCardStatus? Status { get; set; } = SteamCardStatus.Pending;

    [ForeignKey("Payment")]
    public Guid PaymentId { get; set; }
    public virtual Payment Payment { get; set; }

    [ForeignKey("User")]
    public Guid UserId { get; set; }
    public virtual User User { get; set; }

    [ForeignKey("SteamCardCategory")]
    public Guid SteamCardCategoryId { get; set; }
    public virtual SteamCardCategory SteamCardCategory { get; set; }   
}
