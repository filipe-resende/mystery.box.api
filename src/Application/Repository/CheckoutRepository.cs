using Domain.Entities.CheckoutPayload;
using Domain.Interfaces.Repositories;
using Newtonsoft.Json;
using System.Text;

namespace Application.Repository
{
    public class CheckoutRepository : ICheckoutRepository
    {
        private readonly HttpClient _httpClient;

        public CheckoutRepository(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<CheckoutResponsePayload> ProcessPayment(CheckoutPayload checkout)
        {
            var requestBody = JsonConvert.SerializeObject(checkout);

            var requestContent = new StringContent(requestBody, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("orders",requestContent);

            response.EnsureSuccessStatusCode();

            var responseContent = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<CheckoutResponsePayload>(responseContent)!;
        }
    }
}
