using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Main.Modules.Auth;

[Route("api/[controller]/[action]")]
public class AuthController(IAuthService auth) : ControllerBase
{
    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> Login([FromQuery] AuthenticationDTO loginCredentials) => await auth.Login(loginCredentials)
        is var result ? Ok(result) : BadRequest("Error");

    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> Register([FromQuery] RegistrationDTO registration) => await auth.Register(registration)
        is var result ? Ok(result) : BadRequest("Error");

    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> RecoverAccount([FromQuery] string email) => await auth.RecoverAccount(email)
        is var result ? Ok(result) : BadRequest("Error");
}