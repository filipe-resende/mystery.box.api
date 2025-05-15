using Domain.Shared;

namespace UnitTests.Handlers;

public class GetForgottenUserHandlerTests
{
    private readonly Mock<IEmailService> _emailServiceMock = new();
    private readonly Mock<IAuthenticationService> _authServiceMock = new();
    private readonly Mock<IUserRepository> _userRepositoryMock = new();
    private readonly Mock<ILogger<GetForgottenUserHandler>> _loggerMock = new();

    private readonly GetForgottenUserHandler _handler;

    public GetForgottenUserHandlerTests()
    {
        _handler = new GetForgottenUserHandler(
            _emailServiceMock.Object,
            _authServiceMock.Object,
            _userRepositoryMock.Object,
            _loggerMock.Object
        );
    }

    [Fact]
    public async Task Handle_ShouldReturnSuccess_WhenEmailExists()
    {
        // Arrange
        var email = "user@example.com";
        var user = new User
        {
            Id = Guid.NewGuid(),
            Name = "Test",
            Email = email,
            Role = Role.Registered
        };

        _userRepositoryMock.Setup(r => r.GetUserByEmail(email))
            .ReturnsAsync(user);

        _authServiceMock.Setup(a => a.CreateAuthenticationToken(user))
            .Returns("fake-token");

        _emailServiceMock
          .Setup(e => e.SendEmail(
              email,
              It.Is<string>(s => s.Contains("[Recuperação de Senha]")),
              It.Is<string>(body => body.Contains("fake-token"))
          ))
          .Verifiable();


        var command = new GetForgottenUserCommand(email: email);

        // Act
        Result<ErrorResponseDTO> result = (Result<ErrorResponseDTO>)await _handler.Handle(command, default);

        // Assert
        result.IsSuccess.Should().BeTrue();

        var dto = result.Value.As<ErrorResponseDTO>();
        dto.Message.Should().Be("Email enviado!");
        dto.Status.Should().Be(200);

        _emailServiceMock.Verify();
    }

    [Fact]
    public async Task Handle_ShouldReturnFailure_WhenUserDoesNotExist()
    {
        // Arrange
        _userRepositoryMock.Setup(r => r.GetUserByEmail("notfound@example.com"))
            .ReturnsAsync((User)null);

        var command = new GetForgottenUserCommand(email: "notfound@example.com");

        // Act
        var result = await _handler.Handle(command, default);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Code.Should().Be("FORGOT001");
    }

    [Fact]
    public async Task Handle_ShouldReturnFailure_WhenExceptionIsThrown()
    {
        // Arrange
        var email = "crash@example.com";

        _userRepositoryMock.Setup(r => r.GetUserByEmail(email))
            .ThrowsAsync(new Exception("Falha grave"));

        var command = new GetForgottenUserCommand(email: email);

        // Act
        var result = await _handler.Handle(command, default);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Code.Should().Be("FORGOT999");
        result.Error.Message.Should().Contain("Falha grave");
    }
}
