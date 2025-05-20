namespace UnitTests.Handlers;

public class GetAllSteamCardHandlerTests
{
    private readonly Mock<ISteamCardCategoryRepository> _repositoryMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly Mock<ILogger<GetAllSteamCardHandler>> _loggerMock;
    private readonly GetAllSteamCardHandler _handler;

    public GetAllSteamCardHandlerTests()
    {
        _repositoryMock = new Mock<ISteamCardCategoryRepository>();
        _mapperMock = new Mock<IMapper>();
        _loggerMock = new Mock<ILogger<GetAllSteamCardHandler>>();

        _handler = new GetAllSteamCardHandler(
            _repositoryMock.Object,
            _mapperMock.Object,
            _loggerMock.Object
        );
    }

    [Fact]
    public async Task Handle_ShouldReturnSuccess_WhenCategoriesExist()
    {
        // Arrange
        var fakeEntities = new List<SteamCardCategory>
        {
            new() { Id = Guid.NewGuid(), Title = "Ação", Active = true, CreatedAt = DateTime.UtcNow },
            new() { Id = Guid.NewGuid(), Title = "Aventura", Active = true, CreatedAt = DateTime.UtcNow }
        };

        var fakeDTOs = new List<SteamCardCategoryDTO>
        {
            new() { Id = fakeEntities[0].Id, Title = "Ação", Active = true },
            new() { Id = fakeEntities[1].Id, Title = "Aventura", Active = true }
        };

        _repositoryMock.Setup(r => r.GetAll()).ReturnsAsync(fakeEntities);
        _mapperMock.Setup(m => m.Map<IEnumerable<SteamCardCategoryDTO>>(fakeEntities)).Returns(fakeDTOs);

        // Act
        var result = await _handler.Handle(new GetAllSteamCardCommand(), default);

        // Assert
        result.IsSuccess.Should().BeTrue();
        //result.Value.Should().BeEquivalentTo(fakeDTOs);
    }

    [Fact]
    public async Task Handle_ShouldReturnFailure_WhenNoCategoriesFound()
    {
        // Arrange
        _repositoryMock.Setup(r => r.GetAll()).ReturnsAsync(new List<SteamCardCategory>());

        // Act
        var result = await _handler.Handle(new GetAllSteamCardCommand(), default);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Code.Should().Be("STEAMCAT001");
    }

    [Fact]
    public async Task Handle_ShouldReturnFailure_WhenExceptionIsThrown()
    {
        // Arrange
        _repositoryMock.Setup(r => r.GetAll()).ThrowsAsync(new Exception("Banco fora do ar"));

        // Act
        var result = await _handler.Handle(new GetAllSteamCardCommand(), default);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Code.Should().Be("STEAMCAT999");
        result.Error.Message.Should().Contain("Banco fora do ar");
    }
}
