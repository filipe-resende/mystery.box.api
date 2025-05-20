namespace Application.Features.Handlers;

internal class UpdateUserProfileHandler(IUserRepository repository, ILogger<UpdateUserProfileHandler> logger) : IRequestHandler<UpdateUserProfileCommand, Result>
{
    private readonly IUserRepository _repository = repository;
    private readonly ILogger<UpdateUserProfileHandler> _logger = logger;

    public async Task<Result> Handle(UpdateUserProfileCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Iniciando atualização de perfil para o usuário ID: {UserId}", request.UserId);

        var user = await _repository.GetById(request.UserId);

        if (user == null)
        {
            _logger.LogWarning("Usuário com ID {UserId} não encontrado.", request.UserId);
            return Result.Failure(new Error("USER404", "Usuário não encontrado."));
        }

        user.Name = request.Name;
        user.Email = request.Email;
        user.Phone = request.Phone;

        await _repository.Update(user);

        _logger.LogInformation("Perfil atualizado com sucesso para o usuário ID: {UserId}", request.UserId);

        return Result.Success();
    }
}
