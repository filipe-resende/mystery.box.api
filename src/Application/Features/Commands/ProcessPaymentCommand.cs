namespace Application.Features.Commands;

public class ProcessPaymentCommand : IRequest<ProcessPaymentResponseDTO>
{
    [Required]
    public IEnumerable<ItemDTO> Item { get; set; }
    [Required]
    public CreditCardDTO Card { get; set; }
}
