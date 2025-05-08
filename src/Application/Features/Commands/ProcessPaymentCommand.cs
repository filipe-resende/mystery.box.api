using System.ComponentModel.DataAnnotations;
using MediatR;

namespace Application.Features.Commands;

public class ProcessPaymentCommand : IRequest<Result>
{
    [Required]
    public IEnumerable<ItemDTO> Item { get; set; }

    [Required]
    public CreditCardDTO Card { get; set; }

    [Required]
    [Range(0.01, double.MaxValue, ErrorMessage = "O valor deve ser maior que zero.")]
    public float Amount { get; set; }

    [Required]
    [EmailAddress]
    public string PayerEmail { get; set; }

    [Range(1, 12)]
    public int Installments { get; set; } = 1;
}
