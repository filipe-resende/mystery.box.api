namespace Application.DTO;

public class PurchaseHistoryResponseDTO
{
    public Guid Id { get; set; }

    public string ProductName { get; set; }

    public DateTime PurchaseDate { get; set; }

    public string Status { get; set; }

    public string Email { get; set; }

    public decimal UnitPrice { get; set; }

    public int Quantity { get; set; }

    public string Key { get; set; }
}
