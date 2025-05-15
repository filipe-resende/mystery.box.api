namespace Domain.DTO;

public class ProcessPaymentResponseDTO
{
    public long? TransactionId { get; set; }
    public string Status { get; set; }
    public string Message { get; set; }
}
