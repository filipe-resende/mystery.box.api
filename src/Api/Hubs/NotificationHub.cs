using Microsoft.AspNetCore.SignalR;
namespace Api.Hubs;

public class NotificationHub : Hub
{
    public async Task JoinGroup(string paymentId)
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, paymentId);
    }
}

