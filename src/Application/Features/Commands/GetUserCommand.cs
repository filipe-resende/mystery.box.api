namespace Application.Features.Commands;

public class GetUserCommand(Guid id) : IRequest<UserDTO>
{
    public Guid Id { get; set; } = id;
}
