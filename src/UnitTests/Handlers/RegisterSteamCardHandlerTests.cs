namespace UnitTests.Handlers;

public class RegisterSteamCardHandlerTests
{
    private readonly Mock<ISteamCardRepository> _repositoryMock = new();
    private readonly Mock<ILogger<RegisterSteamCardHandler>> _loggerMock = new();

    private readonly RegisterSteamCardHandler _handler;

    public RegisterSteamCardHandlerTests()
    {
        _handler = new RegisterSteamCardHandler(
            _repositoryMock.Object,
            _loggerMock.Object
        );
    }

    [Fact]
    public async Task Handle_ShouldReturnSuccess_WhenSteamCardIsCreated()
    {
        // Arrange
        var steamCardEntity = new SteamCard
        {
            Id = Guid.NewGuid(),
            Name = "Steam 100",
            Key = "ABC-123-XYZ",
            Description = "Cartão de R$100",
            SteamCardCategory = new SteamCardCategory { Id = Guid.NewGuid() }
        };

        var command = new RegisterSteamCardCommand
        {
            Name = steamCardEntity.Name,
            Key = steamCardEntity.Key,
            Description = steamCardEntity.Description,
            SteamCardCategoryId = steamCardEntity.SteamCardCategory.Id
        };

        _repositoryMock.Setup(r => r.Add(It.IsAny<SteamCard>()))
            .ReturnsAsync(steamCardEntity);

        // Act
        Result<SteamCard> result = (Result<SteamCard>)await _handler.Handle(command, default);

        // Assert
        result.IsSuccess.Should().BeTrue();
        var response = result.Value.As<SteamCard>();
        response.Name.Should().Be(response.Name);
        response.Key.Should().Be(response.Key);
        response.Description.Should().Be(response.Description);
    }

    [Fact]
    public async Task Handle_ShouldReturnFailure_WhenExceptionIsThrown()
    {
        // Arrange
        var command = new RegisterSteamCardCommand
        {
            Name = "Falha",
            Key = "INVALID",
            Description = "Erro",
            SteamCardCategoryId = Guid.NewGuid()
        };

        _repositoryMock.Setup(r => r.Add(It.IsAny<SteamCard>()))
            .ThrowsAsync(new Exception("Erro no banco"));

        // Act
        var result = await _handler.Handle(command, default);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Code.Should().Be("STEAMREG999");
        result.Error.Message.Should().Contain("Erro no banco");
    }
}
