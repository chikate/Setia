using Main.Data.Contexts;
using Main.Data.DTOs;
using Main.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Main.APIs.Base;

[ApiController]
[Route("/api/[controller]/[action]")]
public class AuthController : ControllerBase
{
    #region Dependency Injection
    private readonly BaseContext _context;
    private readonly IConfiguration _config;
    private readonly IAuth _auth;

    public AuthController
    (
        BaseContext context,
        IConfiguration config,
        IAuth auth
    )
    {
        _context = context;
        _config = config;
        _auth = auth;
    }
    #endregion

    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> Login([FromBody] AuthenticationDTO loginCredentials)
    {
        try { return Ok(await _auth.Login(loginCredentials)); }
        catch (Exception ex) { return BadRequest("Error message: " + ex.Message); }
    }

    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> Register([FromBody] RegistrationDTO registration)
    {
        try { return Ok(await _auth.Register(registration)); }
        catch (Exception ex) { return BadRequest("Error message: " + ex.Message); }
    }

    [HttpGet]
    public async Task<IActionResult> CheckUserRights([FromQuery] IEnumerable<string> rightsToCeck, Guid? user = null)
    {
        try { return Ok(await _auth.CheckUserRights(rightsToCeck, user)); }
        catch (Exception ex) { return BadRequest("Error message: " + ex.Message); }
    }
}