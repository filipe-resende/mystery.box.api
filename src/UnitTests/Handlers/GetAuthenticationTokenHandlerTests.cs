using Domain.Shared;

namespace Application.Features.Handlers;

public class GetAuthenticationTokenHandlerTests
{
    private readonly Mock<IUserRepository> _repositoryMock = new();
    private readonly Mock<IMapper> _mapperMock = new();
    private readonly Mock<IConfiguration> _configMock = new();
    private readonly Mock<IHttpContextAccessor> _httpContextMock = new();
    private readonly Mock<ILogger<GetAuthenticationTokenHandler>> _loggerMock = new();

    private readonly GetAuthenticationTokenHandler _handler;

    public GetAuthenticationTokenHandlerTests()
    {
        _handler = new GetAuthenticationTokenHandler(
            _repositoryMock.Object,
            _mapperMock.Object,
            _configMock.Object,
            _httpContextMock.Object,
            _loggerMock.Object
        );

        // Default Jwt config
        _configMock.Setup(c => c["Jwt:Secret"]).Returns("wQwj15E9ZgfRuAdkLc3F3eH3SeS9y1KFhWtWStP3fMzAHdmL1Qai1Xk4lLflR3xcutHQ4D35JMfTOc3r9QzmUp1k28sO64dckpSyZ7XoZSJU5J4UzC6TJ1AcUeOY5CP11ilD93yb6jjyAQDDp00tCWMuuttU28UJBMw7K8wWJ3tw4Gj5PpqRyAsywzzTaFFP9ZOJwUOjVYWvPOf9sy7c4xZZWblR1qtosHoL7CX0meh3o4Fvcmtc3AdVffJUPQ0a");
        _configMock.Setup(c => c["Jwt:Issuer"]).Returns("test-issuer");
        _configMock.Setup(c => c["Jwt:Audience"]).Returns("test-audience");
    }

    [Fact]
    public async Task Handle_ShouldReturnToken_WhenCredentialsAreValid()
    {
        // Arrange
        var user = new User
        {
            Id = Guid.NewGuid(),
            Name = "Test User",
            Email = "test@example.com",
            Role = Role.Registered,
            Password = BCrypt.Net.BCrypt.HashPassword("validPassword")
        };

        var command = new GetAuthenticationTokenCommand(email: "test@example.com", password: "validPassword");

        var userDto = new UserDTO
        {
            Id = user.Id,
            Name = user.Name,
            Role = user.Role,
        };

        _repositoryMock.Setup(r => r.GetUserByEmail(user.Email)).ReturnsAsync(user);
        _mapperMock.Setup(m => m.Map<UserDTO>(user)).Returns(userDto);

        var httpContext = new DefaultHttpContext();
        httpContext.Response.Body = new MemoryStream(); 

        _httpContextMock.Setup(h => h.HttpContext).Returns(httpContext);

        // Act
        Result<UserTokenDTO> result = (Result<UserTokenDTO>)await _handler.Handle(command, default);

        // Assert
        result.IsSuccess.Should().BeTrue();
   
        result.Value.Token.Should().NotBeNullOrEmpty();
        result.Value.User.Id.Should().Be(user.Id);
        result.Value.User.Name.Should().Be(user.Name);
        result.Value.User.Role.Should().Be(user.Role);

        var cookies = httpContext.Response.Headers["Set-Cookie"].ToString();
        cookies.Should().Contain("access_token");
    }

    [Fact]
    public async Task Handle_ShouldReturnFailure_WhenUserNotFound()
    {
        // Arrange
        var command = new GetAuthenticationTokenCommand(email: "nonexistent@example.com", password: "any");

        _repositoryMock.Setup(r => r.GetUserByEmail(command.Email)).ReturnsAsync((User)null);

        // Act
        var result = await _handler.Handle(command, default);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Code.Should().Be("AUTH001");
    }

    [Fact]
    public async Task Handle_ShouldReturnFailure_WhenPasswordIsIncorrect()
    {
        // Arrange
        var user = new User
        {
            Id = Guid.NewGuid(),
            Name = "Wrong Pass",
            Email = "wrong@example.com",
            Role = Role.Registered,
            Password = BCrypt.Net.BCrypt.HashPassword("correctPassword")
        };

        _repositoryMock.Setup(r => r.GetUserByEmail(user.Email)).ReturnsAsync(user);

        var command = new GetAuthenticationTokenCommand(email: user.Email, password: "wrongPassword");

        // Act
        var result = await _handler.Handle(command, default);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Code.Should().Be("AUTH003");
    }

    [Fact]
    public async Task Handle_ShouldReturnFailure_WhenPasswordIsMissing()
    {
        // Arrange
        var user = new User
        {
            Id = Guid.NewGuid(),
            Name = "No Password",
            Email = "nopass@example.com",
            Role = Role.Registered,
            Password = null
        };

        _repositoryMock.Setup(r => r.GetUserByEmail(user.Email)).ReturnsAsync(user);

        var command = new GetAuthenticationTokenCommand(email: user.Email, password: null);

        // Act
        var result = await _handler.Handle(command, default);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Code.Should().Be("AUTH004");
    }
}
