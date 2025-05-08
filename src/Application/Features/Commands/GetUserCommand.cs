namespace Application.Features.Commands;

public class GetUserCommand(Guid id) : IRequest<Result>
{
    public Guid Id { get; set; } = id;
}
