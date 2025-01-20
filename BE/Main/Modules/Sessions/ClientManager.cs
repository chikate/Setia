public class SSEClientManager
{
    private readonly List<HttpResponse> _clients = new();
    private readonly object _lock = new();

    public void AddClient(HttpResponse response)
    {
        lock (_lock)
        {
            _clients.Add(response);
        }
    }

    public void RemoveClient(HttpResponse response)
    {
        lock (_lock)
        {
            _clients.Remove(response);
        }
    }

    public List<HttpResponse> GetClients()
    {
        lock (_lock)
        {
            return new List<HttpResponse>(_clients);
        }
    }
}
