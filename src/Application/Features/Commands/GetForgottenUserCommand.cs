namespace Application.Features.Commands;

public class GetForgottenUserCommand(string email) : IRequest<ErrorResponseDTO>
{
    [Required]
    public string Email { get; set; } = email;
}
