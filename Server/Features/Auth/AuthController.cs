using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Main.Modules.Auth;

[Route("api/[controller]/[action]")]
public class AuthController(IAuthService auth) : ControllerBase
{
    [HttpGet]
    [AllowAnonymous]
    public Task<string> Login([FromQuery] AuthenticationDTO credentials)
        => auth.Login(credentials);

    [HttpGet]
    [AllowAnonymous]
    public Task<UserModel> Register([FromQuery] RegistrationDTO registrationInfo)
        => auth.Register(registrationInfo);

    [HttpGet]
    [AllowAnonymous]
    public Task RecoverAccount([FromQuery] RecoveryDTO recoveryInfo)
        => auth.RecoverAccount(recoveryInfo);
}