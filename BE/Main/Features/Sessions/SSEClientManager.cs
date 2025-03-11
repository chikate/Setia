namespace Main.Modules.Sessions;

public class SSEClientManager
{
    private readonly List<HttpResponse> _connections = new();
    private readonly object _lock = new();

    public void AddClient(HttpResponse response)
    {
        lock (_lock)
        {
            _connections.Add(response);
        }
    }

    public void RemoveClient(HttpResponse response)
    {
        lock (_lock)
        {
            _connections.Remove(response);
        }
    }

    public List<HttpResponse> GetClients()
    {
        lock (_lock)
        {
            return new List<HttpResponse>(_connections);
        }
    }
}
