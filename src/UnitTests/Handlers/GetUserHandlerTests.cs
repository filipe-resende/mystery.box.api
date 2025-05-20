namespace UnitTests.Handlers;

public class GetUserHandlerTests
{
    private readonly Mock<IUserRepository> _repositoryMock = new();
    private readonly Mock<IMapper> _mapperMock = new();
    private readonly Mock<ILogger<GetUserHandler>> _loggerMock = new();

    private readonly GetUserHandler _handler;

    public GetUserHandlerTests()
    {
        _handler = new GetUserHandler(
            _repositoryMock.Object,
            _mapperMock.Object,
            _loggerMock.Object
        );
    }

    [Fact]
    public async Task Handle_ShouldReturnSuccess_WhenUserExists()
    {
        // Arrange
        var userId = Guid.NewGuid();


        var userEntity = new User
        {
            Id = userId,
            Name = "João Teste",
            Email = "joao@email.com",
            Identification = new Domain.Entities.Identification() { Type = "cpf", Number= "12345678901" },
            Phone = "31999999999",
            Role = Role.Registered,
            Active = true,
            CreatedAt = DateTime.UtcNow
        };

        var userDTO = new UserDTO
        {
            Id = userEntity.Id,
            Name = userEntity.Name,
            Role = userEntity.Role,
            Active = true,
            CreatedAt = userEntity.CreatedAt
        };

        _repositoryMock.Setup(r => r.GetById(userId)).ReturnsAsync(userEntity);
        _mapperMock.Setup(m => m.Map<UserDTO>(userEntity)).Returns(userDTO);

        var command = new GetUserCommand(id: userId);

        // Act
        Result<UserDTO> result = (Result<UserDTO>)await _handler.Handle(command, default);

        // Assert
        result.IsSuccess.Should().BeTrue();
        var dto = result.Value.Should().BeOfType<UserDTO>().Subject;

        dto.Id.Should().Be(userDTO.Id);
        dto.Name.Should().Be(userDTO.Name);
        dto.Role.Should().Be(userDTO.Role);
    }

    [Fact]
    public async Task Handle_ShouldReturnFailure_WhenUserNotFound()
    {
        // Arrange
        var userId = Guid.NewGuid();

        _repositoryMock.Setup(r => r.GetById(userId)).ReturnsAsync((User)null!);

        var command = new GetUserCommand(id: userId);

        // Act
        var result = await _handler.Handle(command, default);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Code.Should().Be("GETUSER001");
    }

    [Fact]
    public async Task Handle_ShouldReturnFailure_WhenExceptionThrown()
    {
        // Arrange
        var userId = Guid.NewGuid();

        _repositoryMock.Setup(r => r.GetById(userId))
            .ThrowsAsync(new Exception("Erro inesperado"));

        var command = new GetUserCommand(id: userId);

        // Act
        var result = await _handler.Handle(command, default);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Code.Should().Be("GETUSER999");
        result.Error.Message.Should().Contain("Erro inesperado");
    }
}
