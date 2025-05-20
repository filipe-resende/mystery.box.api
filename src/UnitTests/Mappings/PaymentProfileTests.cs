namespace UnitTests.Mappings;

public class PaymentProfileTests
{
    private readonly IMapper _mapper;

    public PaymentProfileTests()
    {
        var config = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<PaymentProfile>();
        });

        _mapper = config.CreateMapper();
    }

    [Fact]
    public void Should_Map_MercadoPagoPayment_To_EntityPayment()
    {

        // Arrange
        var mpPayment = new MercadoPago.Resource.Payment.Payment
        {
            Id = 999999999,
            Status = "approved",
            StatusDetail = "accredited",
            ExternalReference = "REF-1234",
            TransactionAmount = 50,
            DateCreated = DateTime.UtcNow,
            DateApproved = DateTime.UtcNow.AddMinutes(1),
            MoneyReleaseDate = DateTime.UtcNow.AddDays(2),
            Installments = 1,
            PaymentMethodId = "visa",
            PaymentTypeId = "credit_card",
            Card = new PaymentCard
            {
                FirstSixDigits = "411111",
                LastFourDigits = "1111",
                Cardholder = new PaymentCardholder
                {
                    Name = "TEST USER",
                    Identification = new MercadoPago.Resource.Common.Identification
                    {
                        Type = "CPF",
                        Number = "12345678900"
                    }
                }
            },
            Payer = new PaymentPayer
            {
                Email = "cliente@teste.com",
                Identification = new MercadoPago.Resource.Common.Identification
                {
                    Type = "CPF",
                    Number = "12345678900"
                }
            },
            AdditionalInfo = new PaymentAdditionalInfo
            {
                Items =
                    [
                        new PaymentItem
                        {
                            Title = "Produto Teste",
                            Quantity = 1,
                            UnitPrice = 50
                        }
                    ]
            }
        };

        // Act
        var result = _mapper.Map<Domain.Entities.Payment>(mpPayment);

        // Assert
        result.MercadoPagoPaymentId.Should().Be(mpPayment.Id);
        result.Status.Should().Be(mpPayment.Status);
        result.PayerEmail.Should().Be(mpPayment.Payer.Email);
        result.CardFirstSix.Should().Be("411111");
        result.CardLastFour.Should().Be("1111");
        result.FullResponseJson.Should().Be(JsonSerializer.Serialize(mpPayment));
    }
}
