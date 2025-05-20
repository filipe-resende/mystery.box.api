namespace Application.Features.Commands;

public class GetPurchaseHistoryCommand(Guid userId) : IRequest<Result>
{
    public Guid UserId { get; set; } = userId;
}