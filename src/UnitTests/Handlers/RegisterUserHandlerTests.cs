namespace UnitTests.Handlers;

public class RegisterUserHandlerTests
{
    private readonly Mock<IUserRepository> _repositoryMock = new();
    private readonly Mock<ILogger<RegisterUserHandler>> _loggerMock = new();

    private readonly RegisterUserHandler _handler;

    public RegisterUserHandlerTests()
    {
        _handler = new RegisterUserHandler(
            _repositoryMock.Object,
            _loggerMock.Object
        );
    }

    [Fact]
    public async Task Handle_ShouldReturnSuccess_WhenUserIsRegistered()
    {
        // Arrange
        var userId = Guid.NewGuid();

        var command = new RegisterUserCommand
        {
            Name = "Fulano",
            Email = "fulano@email.com",
            Password = "123456",
            IdentificationNumber = "12345678901",
            IdentificationType = "cpf",
            Phone = "31999999999"
        };

        _repositoryMock.Setup(r => r.GetUserByEmail(command.Email))
            .ReturnsAsync((User)null!);

        _repositoryMock.Setup(r => r.Add(It.IsAny<User>()))
            .ReturnsAsync(new User { Id = userId });

        // Act
        Result<User> result = (Result<User>)await _handler.Handle(command, default);

        // Assert
        result.IsSuccess.Should().BeTrue();
        var response = result.Value.As<User>();
        response.Id.Should().Be(userId);
    }

    [Fact]
    public async Task Handle_ShouldReturnFailure_WhenEmailAlreadyExists()
    {
        // Arrange
        var command = new RegisterUserCommand
        {
            Name = "Fulano",
            Email = "ja@existe.com",
            Password = "123456",
            IdentificationNumber = "12345678901",
            IdentificationType = "cpf",
            Phone = "31999999999"
        };

        _repositoryMock.Setup(r => r.GetUserByEmail(command.Email))
            .ReturnsAsync(new User { Id = Guid.NewGuid(), Email = command.Email });

        // Act
        var result = await _handler.Handle(command, default);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Code.Should().Be("REGISTER001");
    }

    [Fact]
    public async Task Handle_ShouldReturnFailure_WhenExceptionIsThrown()
    {
        // Arrange
        var command = new RegisterUserCommand
        {
            Name = "Erro",
            Email = "erro@email.com",
            Password = "123456",
            IdentificationNumber = "12345678901",
            IdentificationType = "cpf",
            Phone = "31999999999"
        };

        _repositoryMock.Setup(r => r.GetUserByEmail(command.Email))
            .ThrowsAsync(new Exception("Erro de banco"));

        // Act
        var result = await _handler.Handle(command, default);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Code.Should().Be("REGISTER999");
        result.Error.Message.Should().Contain("Erro de banco");
    }
}