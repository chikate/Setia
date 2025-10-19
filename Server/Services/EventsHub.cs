using Microsoft.AspNetCore.SignalR;

namespace Main.Services;

public class EventsHub : Hub
{
    public async Task SendNotification(object data) =>
        await Clients.All.SendAsync("ReceiveNotification", data);
}