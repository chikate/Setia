using Main.Modules.Auth;

namespace Main.Modules.Chat;

public interface IChatService
{
    Task SendMessage(MessageModel messageData);
}

public class ChatService : IChatService
{
    #region Dependency Injection 
    private readonly ILogger<ChatService> _logger;
    private readonly IConfiguration _config;
    private readonly IAuthService _auth;

    public ChatService
    (
        ILogger<ChatService> logger,
        IConfiguration config,
        IAuthService auth
    )
    {
        _logger = logger;
        _config = config;
        _auth = auth;
    }
    #endregion

    public async Task SendMessage(MessageModel messageData)
    {
        messageData.AuthorId = (await _auth.GetCurrentUser())?.Id;
    }
}