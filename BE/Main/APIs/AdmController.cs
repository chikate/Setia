using Main.Data.Models;
using Main.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Main.APIs;

[AllowAnonymous]
[ApiController]
[Route("/api/[controller]/[action]")]
public class AdmController : ControllerBase
{
    #region Dependency Injection
    private readonly IAuthService _auth;
    private readonly ICRUDService<SettingsModel> _SettingsCRUD;

    public AdmController
    (
        IAuthService auth,
        ICRUDService<SettingsModel> SettingsCRUD
    )
    {
        _auth = auth;
        _SettingsCRUD = SettingsCRUD;
    }
    #endregion

    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> Ping()
    {
        await Task.CompletedTask;
        try { return Ok("Pong"); }
        catch (Exception ex) { return BadRequest(ex.Message); }
    }

    [HttpGet]
    public async Task<IActionResult> GetServerData()
    {
        await Task.CompletedTask;
        try { return Ok(); }
        catch (Exception ex) { return BadRequest(ex.Message); }
    }
}