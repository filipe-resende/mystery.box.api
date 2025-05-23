﻿namespace Application.Features.Handlers;

public class RegisterUserHandler(
    IUserRepository repository,
    ILogger<RegisterUserHandler> logger
) : IRequestHandler<RegisterUserCommand, Result>
{
    private readonly IUserRepository _repository = repository;
    private readonly ILogger<RegisterUserHandler> _logger = logger;

    public async Task<Result> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("Tentando registrar novo usuário com email: {Email}", request.Email);

            var existingUser = await _repository.GetUserByEmail(request.Email);

            if (existingUser != null)
            {
                _logger.LogWarning("Registro falhou - usuário já existe com email: {Email}", request.Email);
                return Result.Failure(new Error("REGISTER001", "Usuário já existe."));
            }

            var user = new User
            {
                Name = request.Name,
                LastName = request.LastName,
                Email = request.Email,
                Password = BCrypt.Net.BCrypt.HashPassword(request.Password),
                Identification = new Domain.Entities.Identification() { Type = request.IdentificationType, Number = request.IdentificationNumber },
                Role = Role.Registered,
                Phone = request.Phone
            };

            var result = await _repository.Add(user);

            _logger.LogInformation("Usuário registrado com sucesso - ID: {UserId}", result.Id);

            return Result.Success(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro inesperado ao registrar usuário com email: {Email}", request.Email);
            return Result.Failure(new Error("REGISTER999", $"Erro ao registrar usuário: {ex.Message}"));
        }
    }
}
