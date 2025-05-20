namespace Application.Features.Commands;

public class PostPixPaymentCommand(Guid userId) : IRequest<Result>
{
    public Guid UserId { get; set; } = userId;
    public Payer? Payer { get; set; }
    public decimal TransactionAmount { get; set; }
    public IList<PaymentItemRequest> Itens { get; set; }
}

