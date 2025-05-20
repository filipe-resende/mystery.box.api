namespace Application.Features.Handlers;

internal class GetUserProfileHandler(IUserRepository repository, ILogger<GetUserProfileHandler> logger) : IRequestHandler<GetUserProfileCommand, Result>
{
    private readonly IUserRepository _repository = repository;
    private readonly ILogger<GetUserProfileHandler> _logger = logger;

    public async Task<Result> Handle(GetUserProfileCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Buscando dados do perfil para o usuário ID: {UserId}", request.UserId);

        var user = await _repository.GetById(request.UserId);

        if (user == null)
        {
            _logger.LogWarning("Usuário com ID {UserId} não encontrado.", request.UserId);
            return Result.Failure(new Error("USER404", "Usuário não encontrado."));
        }

        var dto = new UserProfileDto
        {
            Name = user.Name,
            Email = user.Email,
            Phone = user.Phone
        };

        return Result.Success(dto);
    }
}
