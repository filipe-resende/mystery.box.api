namespace UnitTests.Services;

public class AuthenticationServiceTests
{
    private readonly IConfiguration _config;

    public AuthenticationServiceTests()
    {
        var inMemorySettings = new Dictionary<string, string>
        {
            { "Jwt:Secret", "lY63cLF5xea1wmtbesD1KGsaEJhyqwe88pKzaqKpFGZHu22mPmeFLfjaxyxhufGgRodljS8SUhlwBYx6KvZUwEef4qkBU814D72xgJfUttBlgFEB6Ddu9IayRAWy3r12KEORCkvTxmcXalue0xoC0lYDa3db5Yx5COcjz9GPBos1atEaomZlp9PFAZ1uRwWCfUDuMCalmZwmt3uo1BHbBlISuv5IkOLtFyhvpDcpGR9XTGh2rBwkffxuBzHJkbXJ" }
        };

        _config = new ConfigurationBuilder()
            .AddInMemoryCollection(inMemorySettings)
            .Build();
    }

    [Fact]
    public void CreateAuthenticationToken_ShouldReturnValidJwt_WhenUserIsValid()
    {
        // Arrange
        var service = new AuthenticationService(_config);
        var user = new User
        {
            Id = Guid.NewGuid(),
            Name = "Fulano",
            Role = Role.Registered
        };

        // Act
        var token = service.CreateAuthenticationToken(user);

        // Assert
        token.Should().NotBeNullOrEmpty();

        var handler = new JwtSecurityTokenHandler();
        var jwtToken = handler.ReadJwtToken(token);

        jwtToken.Claims.Should().Contain(c =>
            c.Type == "unique_name" &&
            c.Value == "Fulano");

        jwtToken.Claims.Should().Contain(c =>
            c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/sid" &&
            c.Value == user.Id.ToString());

        jwtToken.Claims.Should().Contain(c =>
            c.Type == "role" &&
            c.Value == "registered");
    }

    [Fact]
    public void CreateAuthenticationToken_ShouldThrowException_WhenSecretIsMissing()
    {
        // Arrange
        var emptyConfig = new ConfigurationBuilder().Build();
        var service = new AuthenticationService(emptyConfig);

        var user = new User
        {
            Id = Guid.NewGuid(),
            Name = "Fulano",
            Role = Role.Registered
        };

        // Act
        Action act = () => service.CreateAuthenticationToken(user);

        // Assert
        act.Should().Throw<ApplicationException>()
            .WithMessage("JwtSecret não está definida como uma variável de ambiente.");
    }
}
