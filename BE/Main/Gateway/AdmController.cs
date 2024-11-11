using Main.Data.Models;
using Main.Services;
using Main.Standards;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Main.Gateway;

public class AdmController : APIControllerBase
{
    #region Dependency Injection
    private readonly IAuthService _auth;
    private readonly IAdmService _adm;
    private readonly ICRUDService<SettingsModel> _SettingsCRUD;

    public AdmController
    (
        IAuthService auth,
        IAdmService adm,
        ICRUDService<SettingsModel> SettingsCRUD
    )
    {
        _auth = auth;
        _adm = adm;
        _SettingsCRUD = SettingsCRUD;
    }
    #endregion

    [HttpGet]
    [AllowAnonymous]
    public IActionResult Ping() => Ok("Pong");

    [HttpGet]
    public async Task<IActionResult> GetAllAPIs()
    {
        try { return Ok(await _adm.GetAllAPIs()); }
        catch (Exception ex) { return BadRequest(ex.Message); }
    }
}