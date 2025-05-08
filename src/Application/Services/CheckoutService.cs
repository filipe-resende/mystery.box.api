using Domain.Entities;

namespace Application.Services;

public class CheckoutService(HttpClient httpClient) : ICheckoutRepository
{
    private readonly HttpClient _httpClient = httpClient;

    public async Task<CheckoutResponsePayload> ProcessPayment(CheckoutPayload checkout)
    {
        var requestBody = JsonConvert.SerializeObject(checkout);

        var requestContent = new StringContent(requestBody, Encoding.UTF8, "application/json");

        var response = await _httpClient.PostAsync("orders", requestContent);

        response.EnsureSuccessStatusCode();

        var responseContent = await response.Content.ReadAsStringAsync();

        return JsonConvert.DeserializeObject<CheckoutResponsePayload>(responseContent)!;
    }
}