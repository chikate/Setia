using Microsoft.AspNetCore.Mvc;

namespace Main.Standards;

[ApiController]
[Route("api/[controller]/[action]")]

public abstract class APIControllerBase : ControllerBase { }
