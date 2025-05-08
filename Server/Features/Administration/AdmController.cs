using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Main.Modules.Adm;

[Route("api/[controller]/[action]")]
public class AdmController(IAdmService adm) : ControllerBase
{
    [HttpGet]
    [AllowAnonymous]
    public string Ping() => "Pong";

    [HttpGet]
    public async Task<Dictionary<string, List<string>>> GetAllAPIs() => await adm.GetAllAPIs();
}