﻿namespace Application.Services;

public class MercadoPagoPaymentGatewayService : IPaymentGatewayService
{
    private readonly PaymentClient _client;
    private readonly HttpClient _httpClient;

    public MercadoPagoPaymentGatewayService(
        PaymentClient client,
        HttpClient httpClient,
        IOptions<MercadoPagoSettings> settings)
    {
        _client = client;
        _httpClient = httpClient;

        MercadoPagoConfig.AccessToken = settings.Value.AccessToken;

        _httpClient.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", settings.Value.AccessToken);
    }

    public async Task<MercadoPago.Resource.Payment.Payment> PostCreditCardAsync(PostCreditCardPaymentCommand request)
    {
        var requestOptions = new RequestOptions();
        requestOptions.CustomHeaders.Add("x-idempotency-key", Guid.NewGuid().ToString());

        var paymentRequest = new PaymentCreateRequest
        {
            TransactionAmount = request.TransactionAmount,
            Token = request.Token,
            Description = "Compra de keys para resgate na plataforma steammysterybox.com",
            Installments = request.Installments,
            Payer = new PaymentPayerRequest
            {
                Email = request.Payer.Email,
                Identification = new IdentificationRequest
                {
                    Type = request.Payer.Identification.Type,
                    Number = request.Payer.Identification.Number
                },
            },
            AdditionalInfo = new PaymentAdditionalInfoRequest { Items = request.Itens }
        };

        return await _client.CreateAsync(paymentRequest, requestOptions);
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

    public async Task<MercadoPago.Resource.Payment.Payment> PostPixAsync(PostPixPaymentCommand command)
    {
        var requestOptions = new RequestOptions();
        requestOptions.CustomHeaders.Add("x-idempotency-key", Guid.NewGuid().ToString());

        var request = new PaymentCreateRequest
        {
            TransactionAmount = command.TransactionAmount,
            Description = $"Compras ",
            PaymentMethodId = "pix",
            Payer = new PaymentPayerRequest
            {
                FirstName = command.Payer!.Name,
                LastName = command.Payer.LastName,
                Email = command.Payer.Email,
                Identification = new IdentificationRequest
                {
                    Type = command.Payer.Identification.Type,
                    Number = command.Payer.Identification.Number,
                },
            },
            AdditionalInfo = {
                Items = command.Itens
            },
        };

        return await _client.CreateAsync(request, requestOptions);
    }

    public async Task<MercadoPago.Resource.Payment.Payment> GetPaymentAsync(long paymentId) => await _client.GetAsync(paymentId);
}