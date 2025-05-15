using Microsoft.AspNetCore.SignalR;

namespace Api.Controllers;
[ApiController]
[Route("webhook/mercadopago")]
public class MercadoPagoWebhookController(IHubContext<NotificationHub> hubContext) : ControllerBase
{
    private readonly IHubContext<NotificationHub> _hubContext = hubContext;

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] WebhookPayload payload)
    {

        await _hubContext.Clients.Group(payload.Id).SendAsync("PagamentoAtualizado", new
        {
            status = payload.Action,
            id = payload.Id
        });

        return Ok();
    }
}

public class WebhookPayload
{
    public string Action { get; set; }

    [JsonPropertyName("api_version")]
    public string ApiVersion { get; set; }

    public WebhookData Data { get; set; }

    [JsonPropertyName("date_created")]
    public DateTime DateCreated { get; set; }

    public string Id { get; set; }

    [JsonPropertyName("live_mode")]
    public bool LiveMode { get; set; }

    public string Type { get; set; }

    [JsonPropertyName("user_id")]
    public long UserId { get; set; }
}

public class WebhookData
{
    public string Id { get; set; }
}

