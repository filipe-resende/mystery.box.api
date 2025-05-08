namespace Domain.DTO;

public class CreditCardDTO
{
    [Required]
    public string Token { get; set; }

    [Required]
    public string BrandId { get; set; }
}