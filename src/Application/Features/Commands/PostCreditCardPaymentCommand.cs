namespace Application.Features.Commands;

public class PostCreditCardPaymentCommand(Guid userId) : IRequest<Result>
{
    public Guid UserId { get; set; } = userId;
    public string Token { get; set; }
    public string FirstSixDigits { get; set; }
    public int Installments { get; set; } = 1;
    public string? PaymentMethodId { get; set; }
    public decimal TransactionAmount { get; set; }
    public Payer Payer { get; set; }
    public IList<PaymentItemRequest> Itens { get; set; }
}

public class Payer()
{ 
    public string? Email { get; set; }
    public string? Name { get; set; }
    public string? LastName { get; set; }
    public Identification Identification {  get; set; } 
};

public record Identification(string Type, string Number);

