namespace Domain.Entities;

[Table("Payment")]
public class Payment
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    [StringLength(100)]
    public string ReferenceId { get; set; }

    [Required]
    [StringLength(50)]
    public string Status { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime? PaidAt { get; set; }

    [StringLength(500)]
    public string Description { get; set; }

    [Range(0, int.MaxValue, ErrorMessage = "Amount must be a positive value")]
    public int AmountValue { get; set; }

    [Required]
    [StringLength(10)]
    public string AmountCurrency { get; set; }

    [ForeignKey("PaymentResponse")]
    public Guid? PaymentResponseId { get; set; }  // 👈 Adiciona isso
    public PaymentResponse PaymentResponse { get; set; }

    [ForeignKey("User")]
    public Guid? UserId { get; set; }
    public virtual User User { get; set; }
}
