namespace Application.Features.Commands;

public class GetPaymentCommand(Guid userId, long paymentId) : IRequest<Result>
{
    public Guid UserId { get; set; } = userId;
    public long PaymentId { get; set; } = paymentId;
}