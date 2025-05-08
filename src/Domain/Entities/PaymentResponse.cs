namespace Domain.Entities;

[Table("PaymentResponse")]
public class PaymentResponse
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    [StringLength(20)]
    public string Code { get; set; }

    [StringLength(255)]
    public string Message { get; set; }

    [StringLength(100)]
    public string Reference { get; set; }

    [StringLength(50)]
    public string AuthorizationCode { get; set; }

    [StringLength(50)]
    public string Nsu { get; set; }

    [StringLength(20)]
    public string ReasonCode { get; set; }
}
