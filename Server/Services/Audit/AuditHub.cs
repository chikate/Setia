using Microsoft.AspNetCore.SignalR;

namespace Main.Modules.Audit;

public class AuditHub : Hub
{
    public override async Task OnConnectedAsync() =>
        await base.OnConnectedAsync();

    public override async Task OnDisconnectedAsync(Exception? exception) =>
        await base.OnDisconnectedAsync(exception);
}
