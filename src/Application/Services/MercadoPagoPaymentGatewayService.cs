namespace Application.Services;

public class MercadoPagoPaymentGatewayService : IPaymentGatewayService
{
    private readonly PaymentClient client = new();
    private readonly HttpClient _httpClient;

    public MercadoPagoPaymentGatewayService()
    {
        MercadoPagoConfig.AccessToken = "TEST-7448109333317481-100923-68f94c20d7c20e0294474ae8371c7f1b-127600031";

        _httpClient = new HttpClient();
        _httpClient.DefaultRequestHeaders.Authorization =
            new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", MercadoPagoConfig.AccessToken);
    }

    public async Task<ProcessPaymentResponseDTO> ProcessAsync(ProcessPaymentCommand request)
    {
        var requestOptions = new RequestOptions();
        requestOptions.CustomHeaders.Add("x-idempotency-key", Guid.NewGuid().ToString());

        var paymentRequest = new PaymentCreateRequest
        {
            TransactionAmount = request.TransactionAmount,
            Token = request.Token,
            Description = $"Compras {request.Cards}",
            Installments = request.Installments,
            PaymentMethodId = "master",
            Payer = new PaymentPayerRequest
            {
                Email = request.Payer.Email,
                Identification = new IdentificationRequest
                {
                    Type = request.Payer.Identification.Type,
                    Number = request.Payer.Identification.Number
                },
            },
        };

        MercadoPago.Resource.Payment.Payment payment = await client.CreateAsync(paymentRequest, requestOptions);

        return new ProcessPaymentResponseDTO
        {
            TransactionId = payment.Id,
            Status = payment.Status,
            Message = payment.StatusDetail
        };
    }

    public async Task<PaymentsMethodsDTO> GetPaymentMethodsByBinAsync(string bin)
    {
        var url = $"https://api.mercadopago.com/v1/payment_methods?bin={bin}";
        var response = await _httpClient.GetAsync(url);

        response.EnsureSuccessStatusCode();

        var content = await response.Content.ReadAsStringAsync();

        IEnumerable<PaymentsMethodsDTO> methods = JsonConvert.DeserializeObject<IEnumerable<PaymentsMethodsDTO>>(content);

        return methods.FirstOrDefault();
    }

    public async Task<ProcessPaymentResponseDTO> GetPaymentAsync(long paymentId)
    {
        MercadoPago.Resource.Payment.Payment payment = await client.GetAsync(paymentId);

        return new ProcessPaymentResponseDTO
        {
            TransactionId = payment.Id,
            Status = payment.Status,
            Message = payment.StatusDetail
        };
    }
}