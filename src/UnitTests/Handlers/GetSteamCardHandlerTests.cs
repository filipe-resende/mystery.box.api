namespace UnitTests.Handlers;

public class GetSteamCardHandlerTests
{
    private readonly Mock<ISteamCardRepository> _repositoryMock = new();
    private readonly Mock<IMapper> _mapperMock = new();
    private readonly Mock<ILogger<GetSteamCardHandler>> _loggerMock = new();

    private readonly GetSteamCardHandler _handler;

    public GetSteamCardHandlerTests()
    {
        _handler = new GetSteamCardHandler(
            _repositoryMock.Object,
            _mapperMock.Object,
            _loggerMock.Object
        );
    }

    [Fact]
    public async Task Handle_ShouldReturnSuccess_WhenCardExists()
    {
        // Arrange
        var id = Guid.NewGuid();

        var categoryEntity = new SteamCardCategory
        {
            Id = Guid.NewGuid(),
            Title = "Games",
            Active = true,
            CreatedAt = DateTime.UtcNow
        };

        var cardEntity = new SteamCard
        {
            Id = id,
            Name = "Steam Gift Card",
            Key = "ABC-123",
            Description = "Cartão de presente Steam",
            Active = true,
            CreatedAt = DateTime.UtcNow,
            SteamCardCategory = categoryEntity
        };

        var categoryDTO = new SteamCardCategoryDTO
        {
            Id = categoryEntity.Id,
            Title = categoryEntity.Title,
            Active = categoryEntity.Active,
            CreatedAt = categoryEntity.CreatedAt
        };

        var cardDTO = new SteamCardDTO
        {
            Id = cardEntity.Id,
            Name = cardEntity.Name,
            Key = cardEntity.Key,
            Description = cardEntity.Description,
            Active = cardEntity.Active,
            CreatedAt = cardEntity.CreatedAt,
            SteamCardCategory = categoryDTO
        };

        _repositoryMock.Setup(r => r.GetById(id)).ReturnsAsync(cardEntity);
        _mapperMock.Setup(m => m.Map<SteamCardDTO>(cardEntity)).Returns(cardDTO);

        var command = new GetSteamCardCommand (id: id);

        // Act
        Result<SteamCardDTO> result = (Result<SteamCardDTO>)await _handler.Handle(command, default);

        // Assert
        result.IsSuccess.Should().BeTrue();

        var returned = result.Value.Should().BeOfType<SteamCardDTO>().Subject;
        returned.Name.Should().Be("Steam Gift Card");
        returned.Key.Should().Be("ABC-123");
        returned.Description.Should().Be("Cartão de presente Steam");
        returned.SteamCardCategory.Should().NotBeNull();
        returned.SteamCardCategory.Title.Should().Be("Games");
    }


    [Fact]
    public async Task Handle_ShouldReturnFailure_WhenCardDoesNotExist()
    {
        // Arrange
        var id = Guid.NewGuid();

        _repositoryMock.Setup(r => r.GetById(id)).ReturnsAsync((SteamCard)null);

        var command = new GetSteamCardCommand(id: id);

        // Act
        var result = await _handler.Handle(command, default);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Code.Should().Be("STEAM001");
    }

    [Fact]
    public async Task Handle_ShouldReturnFailure_WhenExceptionIsThrown()
    {
        // Arrange
        var id = Guid.NewGuid();

        _repositoryMock.Setup(r => r.GetById(id)).ThrowsAsync(new Exception("Falha crítica"));

        var command = new GetSteamCardCommand (id: id);

        // Act
        var result = await _handler.Handle(command, default);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Code.Should().Be("STEAM999");
        result.Error.Message.Should().Contain("Falha crítica");
    }
}
