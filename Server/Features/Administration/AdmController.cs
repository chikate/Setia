using Main.Features.CRUDs;
using Main.Modules.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Main.Modules.Adm;

public class AdmController(IAuthService auth, IAdmService adm, ICRUDService<SettingsModel> SettingsCRUD) : ControllerBase
{
    [HttpGet]
    [AllowAnonymous]
    public IActionResult Ping() => Ok("Pong");

    [HttpGet]
    public async Task<IActionResult> GetAllAPIs() => await adm.GetAllAPIs()
        is var result ? Ok(result) : BadRequest("Error");
}