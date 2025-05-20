namespace Application.Features.Commands;

public class UpdateUserProfileCommand : IRequest<Result>
{
    public Guid UserId { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
}


