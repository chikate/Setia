using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Modules.Auth;

[ApiController]
[Route("api/[controller]/[action]")]
[AllowAnonymous]
public class AuthController(IAuthService auth) : ControllerBase
{
    [HttpPost]
    public Task<string> Login([FromBody] AuthenticationDTO credentials) => auth.Login(credentials);

    [HttpGet]
    public Task<UserModel> Register([FromQuery] RegistrationDTO registrationInfo) => auth.Register(registrationInfo);

    [HttpGet]
    public Task RecoverAccount([FromQuery] RecoveryDTO recoveryInfo) => auth.RecoverAccount(recoveryInfo);
}