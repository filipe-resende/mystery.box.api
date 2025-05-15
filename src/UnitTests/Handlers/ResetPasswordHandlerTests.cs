namespace UnitTests.Handlers;

public class ResetPasswordHandlerTests
{
    private readonly Mock<IUserRepository> _userRepositoryMock = new();
    private readonly Mock<ILogger<ResetPasswordHandler>> _loggerMock = new();

    private readonly ResetPasswordHandler _handler;

    public ResetPasswordHandlerTests()
    {
        _handler = new ResetPasswordHandler(
            _userRepositoryMock.Object,
            _loggerMock.Object
        );
    }

    [Fact]
    public async Task Handle_ShouldResetPassword_WhenUserExists()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var user = new User
        {
            Id = userId,
            Name = "Fulano",
            Email = "fulano@email.com",
            Password = "oldPassword"
        };

        var command = new ResetPasswordCommand(userId: userId.ToString(), password: "newPassword");

        _userRepositoryMock.Setup(r => r.GetById(userId)).ReturnsAsync(user);
        _userRepositoryMock.Setup(r => r.Update(It.IsAny<User>())).Returns(Task.CompletedTask);

        // Act
        Result<ErrorResponseDTO> result = (Result<ErrorResponseDTO>)await _handler.Handle(command, default);

        // Assert
        result.IsSuccess.Should().BeTrue();
        var dto = result.Value.As<ErrorResponseDTO>();
        dto.Status.Should().Be(200);
        dto.Message.Should().Be("Senha alterada com sucesso.");

        user.Password.Should().Be("newPassword");
    }

    [Fact]
    public async Task Handle_ShouldReturnFailure_WhenUserNotFound()
    {
        // Arrange
        var command = new ResetPasswordCommand(userId: "3f2504e0-4f89-11d3-9a0c-0305e82c3301", password: "novaSenha");

        _userRepositoryMock.Setup(r => r.GetById(command.UserId)).ReturnsAsync((User)null!);

        // Act
        var result = await _handler.Handle(command, default);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Code.Should().Be("RESET001");
    }

    [Fact]
    public async Task Handle_ShouldReturnFailure_WhenExceptionIsThrown()
    {
        // Arrange
        var command = new ResetPasswordCommand(userId: "3f2504e0-4f89-11d3-9a0c-0305e82c3301", password: "qualquerSenha");

        _userRepositoryMock.Setup(r => r.GetById(command.UserId)).ThrowsAsync(new Exception("Erro de banco"));

        // Act
        var result = await _handler.Handle(command, default);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Code.Should().Be("RESET999");
        result.Error.Message.Should().Contain("Erro de banco");
    }
}
