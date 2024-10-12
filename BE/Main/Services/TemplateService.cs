using Main.Services.Interfaces;

namespace Main.Services;

public class RegisterService : ITemplate
{
    #region Dependency Injection 
    private readonly ILogger<SenderService> _logger;
    private readonly IConfiguration _config;
    private readonly IAudit _audit;
    private readonly IAuth _auth;

    public RegisterService
    (
        ILogger<SenderService> logger,
        IConfiguration config,
        IAudit audit,
        IAuth auth
    )
    {
        _logger = logger;
        _config = config;
        _audit = audit;
        _auth = auth;
    }
    #endregion

    public async Task TestReturn() => throw new Exception();
    public async Task TestFuntion()
    {
        throw new Exception();
    }
}