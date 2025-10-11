using Microsoft.AspNetCore.SignalR;

namespace Main.Modules.Comms;

public class CommsHub : Hub
{
    public async Task SendMousePosition(int x, int y) =>
        await Clients.All.SendAsync("ReceiveMousePosition", new { x, y });
}