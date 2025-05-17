namespace UnitTests.Handlers;

public class ProcessPaymentHandlerTests
{
    private readonly Mock<IPaymentGatewayService> _paymentGatewayMock = new();
    private readonly Mock<IUserRepository> _userRepositoryMock = new();
    private readonly Mock<ILogger<ProcessPaymentHandler>> _loggerMock = new();

    private readonly ProcessPaymentHandler _handler;

    public ProcessPaymentHandlerTests()
    {
        _handler = new ProcessPaymentHandler(
            _paymentGatewayMock.Object,
            _userRepositoryMock.Object,
            _loggerMock.Object
        );
    }

    [Fact]
    public async Task Handle_ShouldReturnSuccess_WhenUserAndTokenAreValid()
    {
        // Arrange
        var userId = Guid.NewGuid();

        var user = new User
        {
            Id = userId,
            Name = "Cliente",
            Email = "cliente@teste.com",
            Role = Role.Registered
        };

        var claims = new ClaimsPrincipal(new ClaimsIdentity(new[]
        {
            new Claim(ClaimTypes.Sid, userId.ToString())
        }));

        var httpContext = new DefaultHttpContext { User = claims };

        _userRepositoryMock.Setup(x => x.GetById(userId)).ReturnsAsync(user);

        var paymentResponse = new ProcessPaymentResponseDTO
        {
            TransactionId= 123456,
            Status = "201",
            Message = "Pagamento processado com sucesso"
        };

        _paymentGatewayMock.Setup(x => x.ProcessAsync(It.IsAny<ProcessPaymentCommand>()))
            .ReturnsAsync(paymentResponse);

        var command = new ProcessPaymentCommand(userId)
        {
            TransactionAmount = 100,
            Payer = new Payer { Email = "cliente@teste.com" },
            Installments = 1,
            Token = "token_card"
        };

        // Act
        Result<ProcessPaymentResponseDTO> result = (Result<ProcessPaymentResponseDTO>)await _handler.Handle(command, default);

        // Assert
        result.IsSuccess.Should().BeTrue();
        var response = result.Value.As<ProcessPaymentResponseDTO>();
        response.Status.Should().Be("201");
        response.Message.Should().Be("Pagamento processado com sucesso");
    }

    [Fact]
    public async Task Handle_ShouldReturnFailure_WhenSidIsMissing()
    {
        var httpContext = new DefaultHttpContext { User = new ClaimsPrincipal() };

        var command = new ProcessPaymentCommand(Guid.NewGuid());

        var result = await _handler.Handle(command, default);

        result.IsFailure.Should().BeTrue();
        result.Error.Code.Should().Be("PAYMENT001");
    }

    [Fact]
    public async Task Handle_ShouldReturnFailure_WhenUserNotFound()
    {
        var userId = Guid.NewGuid();

        var claims = new ClaimsPrincipal(new ClaimsIdentity(new[]
        {
            new Claim(ClaimTypes.Sid, userId.ToString())
        }));

        var httpContext = new DefaultHttpContext { User = claims };

        _userRepositoryMock.Setup(x => x.GetById(userId)).ReturnsAsync((User)null!);

        var command = new ProcessPaymentCommand(Guid.NewGuid());

        var result = await _handler.Handle(command, default);

        result.IsFailure.Should().BeTrue();
        result.Error.Code.Should().Be("PAYMENT001");
    }

    [Fact]
    public async Task Handle_ShouldReturnFailure_WhenExceptionIsThrown()
    {
        var userId = Guid.NewGuid();

        var claims = new ClaimsPrincipal(new ClaimsIdentity(new[]
        {
            new Claim(ClaimTypes.Sid, userId.ToString())
        }));

        var httpContext = new DefaultHttpContext { User = claims };

        _userRepositoryMock.Setup(x => x.GetById(userId))
            .ThrowsAsync(new Exception("Falha interna"));

        var command = new ProcessPaymentCommand(userId);

        var result = await _handler.Handle(command, default);

        result.IsFailure.Should().BeTrue();
        result.Error.Code.Should().Be("PAYMENT999");
        result.Error.Message.Should().Contain("Falha interna");
    }
}