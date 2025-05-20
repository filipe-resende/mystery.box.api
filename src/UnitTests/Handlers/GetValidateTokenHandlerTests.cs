namespace UnitTests.Handlers;

public class GetValidateTokenHandlerTests
{
    private readonly Mock<IUserRepository> _repositoryMock = new();
    private readonly Mock<IAuthenticationService> _authServiceMock = new();
    private readonly Mock<IMapper> _mapperMock = new();
    private readonly Mock<ILogger<GetValidateTokenHandler>> _loggerMock = new();

    private readonly GetValidateTokenHandler _handler;

    public GetValidateTokenHandlerTests()
    {
        _handler = new GetValidateTokenHandler(
            _repositoryMock.Object,
            _authServiceMock.Object,
            _mapperMock.Object,
            _loggerMock.Object
        );
    }

    [Fact]
    public async Task Handle_ShouldReturnSuccess_WhenUserExists()
    {
        // Arrange
        var userId = Guid.NewGuid();

        var user = new User
        {
            Id = userId,
            Name = "Usuário",
            Email = "usuario@email.com",
            Role = Role.Registered,
            Identification = new Domain.Entities.Identification() { Type = "cpf", Number = "12345678901" },
            Phone = "31999999999",
            Active = true,
            CreatedAt = DateTime.UtcNow
        };

        var token = "jwt-token-fake";

        var userDto = new UserDTO
        {
            Id = user.Id,
            Name = user.Name,
            Role = user.Role,
            Active = true,
            CreatedAt = user.CreatedAt
        };

        _repositoryMock.Setup(r => r.GetById(userId)).ReturnsAsync(user);
        _authServiceMock.Setup(a => a.CreateAuthenticationToken(user)).Returns(token);
        _mapperMock.Setup(m => m.Map<UserDTO>(user)).Returns(userDto);

        var command = new GetValidateTokenCommand(userId: userId.ToString());

        // Act
        Result<UserTokenDTO> result = (Result<UserTokenDTO>)await _handler.Handle(command, default);

        // Assert
        result.IsSuccess.Should().BeTrue();

        var dto = result.Value.Should().BeOfType<UserTokenDTO>().Subject;
        
        dto.Token.Should().Be(token);
        dto.User.Id.Should().Be(user.Id);
        dto.User.Name.Should().Be(user.Name);
        dto.User.Role.Should().Be(user.Role);
    }

    [Fact]
    public async Task Handle_ShouldReturnFailure_WhenUserNotFound()
    {
        // Arrange
        var userId = Guid.NewGuid();

        _repositoryMock.Setup(r => r.GetById(userId)).ReturnsAsync((User)null!);

        var command = new GetValidateTokenCommand(userId: userId.ToString());

        // Act
        var result = await _handler.Handle(command, default);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Code.Should().Be("TOKEN001");
    }

    [Fact]
    public async Task Handle_ShouldReturnFailure_WhenExceptionThrown()
    {
        // Arrange
        var userId = Guid.NewGuid();

        _repositoryMock.Setup(r => r.GetById(userId)).ThrowsAsync(new Exception("Falha inesperada"));

        var command = new GetValidateTokenCommand (userId: userId.ToString());

        // Act
        var result = await _handler.Handle(command, default);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Code.Should().Be("TOKEN999");
        result.Error.Message.Should().Contain("Falha inesperada");
    }
}
