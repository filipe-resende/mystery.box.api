namespace Application.Features.Handlers;

public class ResetPasswordHandler(IUserRepository userRepository) : IRequestHandler<ResetPasswordCommand, ErrorResponseDTO>
{
    private readonly IUserRepository _userRepository = userRepository;

    public async Task<ErrorResponseDTO> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var user = await _userRepository.GetById(request.UserId);
            user.Password = request.Password;
            await _userRepository.Update(user);
        }
        catch (Exception ex)
        {
            throw new Exception($"could not be saved. Error {ex.Message}");
        }

        return new ErrorResponseDTO { Status = 200, Error = "", Message = "Senha Alterada" };
    }
}
