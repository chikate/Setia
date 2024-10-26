using Main.Data.DTOs;
using Main.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Main.APIs;

[AllowAnonymous]
[ApiController]
[Route("/api/[controller]/[action]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _auth;
    public AuthController(IAuthService auth) { _auth = auth; }

    [HttpGet]
    public async Task<IActionResult> Login([FromQuery] AuthenticationDTO loginCredentials)
    {
        try { return Ok(await _auth.Login(loginCredentials)); }
        catch (Exception ex) { return BadRequest(ex.Message); }
    }

    [HttpGet]
    public async Task<IActionResult> Register([FromQuery] RegistrationDTO registration)
    {
        try { return Ok(await _auth.Register(registration)); }
        catch (Exception ex) { return BadRequest(ex.Message); }
    }

    [HttpGet]
    public async Task<IActionResult> RecoverAccount([FromQuery] string email)
    {
        try { return Ok(await _auth.RecoverAccount(email)); }
        catch (Exception ex) { return BadRequest(ex.Message); }
    }
}