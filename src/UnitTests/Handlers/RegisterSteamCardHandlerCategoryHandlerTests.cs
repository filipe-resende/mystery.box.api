namespace UnitTests.Handlers;

public class RegisterSteamCardHandlerCategoryHandlerTests
{
    private readonly Mock<ISteamCardCategoryRepository> _repositoryMock = new();
    private readonly Mock<ILogger<RegisterSteamCardHandlerCategoryHandler>> _loggerMock = new();

    private readonly RegisterSteamCardHandlerCategoryHandler _handler;

    public RegisterSteamCardHandlerCategoryHandlerTests()
    {
        _handler = new RegisterSteamCardHandlerCategoryHandler(
            _repositoryMock.Object,
            _loggerMock.Object
        );
    }

    [Fact]
    public async Task Handle_ShouldReturnSuccess_WhenCategoryIsCreated()
    {
        // Arrange
        var category = new SteamCardCategory
        {
            Id = Guid.NewGuid(),
            Name = "Aventura",
            Price = 99.90f,
            Thumb = "img.jpg",
            Description = "Categoria de jogos de aventura"
        };

        var command = new RegisterSteamCardCategoryCommand
        {
            Name = category.Name,
            Price = category.Price,
            Thumb = category.Thumb,
            Description = category.Description
        };

        _repositoryMock.Setup(r => r.Add(It.IsAny<SteamCardCategory>()))
            .ReturnsAsync(category);

        // Act
        Result<SteamCardCategory> result = (Result<SteamCardCategory>)await _handler.Handle(command, default);

        // Assert
        result.IsSuccess.Should().BeTrue();
        var response = result.Value.As<SteamCardCategory>();
        response.Name.Should().Be(category.Name);
        response.Price.Should().Be(category.Price);
        response.Thumb.Should().Be(category.Thumb);
        response.Description.Should().Be(category.Description);
    }

    [Fact]
    public async Task Handle_ShouldReturnFailure_WhenExceptionIsThrown()
    {
        // Arrange
        var command = new RegisterSteamCardCategoryCommand
        {
            Name = "Erro",
            Price = 10,
            Thumb = "fail.jpg",
            Description = "Descrição"
        };

        _repositoryMock.Setup(r => r.Add(It.IsAny<SteamCardCategory>()))
            .ThrowsAsync(new Exception("Falha no banco"));

        // Act
        var result = await _handler.Handle(command, default);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Code.Should().Be("STEAMCATREG999");
        result.Error.Message.Should().Contain("Falha no banco");
    }
}
