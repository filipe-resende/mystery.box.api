namespace UnitTests.Handlers;

public class ProcessPaymentHandlerTests
{
    private readonly Mock<IPaymentGatewayService> _paymentGatewayMock = new();
    private readonly Mock<IUserRepository> _userRepositoryMock = new();
    private readonly Mock<IPaymentRepository> _paymentRepositoryMock = new();
    private readonly IMapper _mapper;
    private readonly Mock<ILogger<PostCreditCardPaymentHandler>> _loggerMock = new();

    private readonly PostCreditCardPaymentHandler _handler;

    public ProcessPaymentHandlerTests()
    {

        var config = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<PaymentProfile>();
        });

        _mapper = config.CreateMapper();

        _handler = new PostCreditCardPaymentHandler(
                   _paymentGatewayMock.Object,
                   _userRepositoryMock.Object,
                   _paymentRepositoryMock.Object,
                   _mapper,
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

        var mpPayment = new MercadoPago.Resource.Payment.Payment
        {
            Id = 123456789,
            Status = "approved",
            ExternalReference = "REF-001",
            StatusDetail = "Pagamento processado com sucesso",
            TransactionAmount = 100,
            Payer = new PaymentPayer
            {
                Email = "cliente@teste.com",
                Identification = new MercadoPago.Resource.Common.Identification
                {
                    Type = "CPF",
                    Number = "12345678900"
                }
            }
        };

        _paymentGatewayMock.Setup(x => x.PostCreditCardAsync(It.IsAny<PostCreditCardPaymentCommand>()))
            .ReturnsAsync(mpPayment);

        var command = new PostCreditCardPaymentCommand(userId)
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

        response.TransactionId.Should().Be(123456789);
        response.Status.Should().Be("approved");
        response.Message.Should().Be("Pagamento processado com sucesso");
    }

    [Fact]
    public async Task Handle_ShouldReturnFailure_WhenSidIsMissing()
    {
        var httpContext = new DefaultHttpContext { User = new ClaimsPrincipal() };

        var command = new PostCreditCardPaymentCommand(Guid.NewGuid());

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

        var command = new PostCreditCardPaymentCommand(Guid.NewGuid());

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

        var command = new PostCreditCardPaymentCommand(userId);

        var result = await _handler.Handle(command, default);

        result.IsFailure.Should().BeTrue();
        result.Error.Code.Should().Be("PAYMENT999");
        result.Error.Message.Should().Contain("Falha interna");
    }
}