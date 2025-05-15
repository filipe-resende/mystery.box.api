namespace UnitTests.Handlers;

public class ProcessPaymentHandlerTests
{
    private readonly Mock<IPaymentGatewayService> _paymentGatewayMock = new();
    private readonly Mock<IHttpContextAccessor> _httpContextAccessorMock = new();
    private readonly Mock<IUserRepository> _userRepositoryMock = new();
    private readonly Mock<ILogger<ProcessPaymentHandler>> _loggerMock = new();

    private readonly ProcessPaymentHandler _handler;

    public ProcessPaymentHandlerTests()
    {
        _handler = new ProcessPaymentHandler(
            _paymentGatewayMock.Object,
            _httpContextAccessorMock.Object,
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
        _httpContextAccessorMock.Setup(x => x.HttpContext).Returns(httpContext);

        _userRepositoryMock.Setup(x => x.GetById(userId)).ReturnsAsync(user);

        var paymentResponse = new ProcessPaymentResponseDTO
        {
            Status = 201,
            Content = "Pagamento processado com sucesso"
        };

        _paymentGatewayMock.Setup(x => x.ProcessAsync(It.IsAny<ProcessPaymentCommand>(), user))
            .ReturnsAsync(paymentResponse);

        var command = new ProcessPaymentCommand
        {
            Amount = 100,
            PayerEmail = "cliente@teste.com",
            Installments = 1,
            Card = new CreditCardDTO { BrandId = "visa" }
        };

        // Act
        Result<ProcessPaymentResponseDTO> result = (Result<ProcessPaymentResponseDTO>)await _handler.Handle(command, default);

        // Assert
        result.IsSuccess.Should().BeTrue();
        var response = result.Value.As<ProcessPaymentResponseDTO>();
        response.Status.Should().Be(201);
        response.Content.Should().Be("Pagamento processado com sucesso");
    }

    [Fact]
    public async Task Handle_ShouldReturnFailure_WhenSidIsMissing()
    {
        var httpContext = new DefaultHttpContext { User = new ClaimsPrincipal() };
        _httpContextAccessorMock.Setup(x => x.HttpContext).Returns(httpContext);

        var command = new ProcessPaymentCommand();

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
        _httpContextAccessorMock.Setup(x => x.HttpContext).Returns(httpContext);

        _userRepositoryMock.Setup(x => x.GetById(userId)).ReturnsAsync((User)null!);

        var command = new ProcessPaymentCommand();

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
        _httpContextAccessorMock.Setup(x => x.HttpContext).Returns(httpContext);

        _userRepositoryMock.Setup(x => x.GetById(userId))
            .ThrowsAsync(new Exception("Falha interna"));

        var command = new ProcessPaymentCommand();

        var result = await _handler.Handle(command, default);

        result.IsFailure.Should().BeTrue();
        result.Error.Code.Should().Be("PAYMENT999");
        result.Error.Message.Should().Contain("Falha interna");
    }
}