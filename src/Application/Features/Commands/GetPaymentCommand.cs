namespace Application.Features.Commands;

public class GetPaymentCommand(Guid userId) : IRequest<Result>
{
    public Guid UserId { get; set; } = userId;

    public string Token { get; set; }

    public string FirstSixDigits { get; set; }

    public int Installments { get; set; } = 1;

    public string? PaymentMethodId { get; set; }

    public decimal TransactionAmount { get; set; }

    public Payer Payer { get; set; }

    public IEnumerable<Guid> Cards { get; set; }
}