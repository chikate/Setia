using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Setia.Contexts.Base;
using Setia.Models.Base;
using Setia.Services.Interfaces;

namespace Setia.Controllers
{
    [ApiController]
    [Authorize]
    [Route("/api/[controller]/[action]")]
    public class AuthController : ControllerBase
    {
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

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] UserModel loginCredentials)
        {
            try { return Ok(await _auth.Login(loginCredentials)); }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] UserModel registration)
        {
            try
            {
                await _auth.Register(registration);
                return Ok("Account created");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> CheckUserRights([FromQuery] IEnumerable<string> rightsToCeck, Guid? user = null)
        {
            try { return Ok(await _auth.CheckUserRights(rightsToCeck, user)); }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}