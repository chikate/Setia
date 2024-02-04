using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Setia.Data;
using Setia.Services.Interfaces;
using Setia.Structs;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Setia.Controllers
{
    [ApiController]
    [AllowAnonymous]
    [Route("/api/[controller]/[action]")]
    public class AuthController : ControllerBase
    {
        private readonly SetiaContext _context;
        private readonly ILogger<AuthController> _logger;
        private readonly IConfiguration _config;
        private readonly IAuth _auth;

        public AuthController
        (
            SetiaContext context,
            ILogger<AuthController> logger,
            IConfiguration config,
            IAuth auth
        )
        {
            _context = context;
            _logger = logger;
            _config = config;
            _auth = auth;
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginDto login)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid credentials");
                }

                if (login.Username == null || login.Username.Length < 6)
                {
                    return BadRequest("Invalid username (too short)");
                }

                if (login.Password == null || login.Password.Length < 6)
                {
                    return BadRequest("Invalid password (too short)");
                }

                var user = await _context.Users
                    .Where(u => u.Username == login.Username && u.Password == login.Password)
                    .FirstOrDefaultAsync();
                if (user != null)
                {
                    List<Claim> claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.NameIdentifier, user.Username),
                        new Claim(ClaimTypes.Email, user.Email),
                        new Claim(ClaimTypes.Name, user.Username),
                        new Claim(ClaimTypes.GivenName, user.Name),
                        new Claim(ClaimTypes.Surname, user.Name),
                        new Claim(ClaimTypes.Role, "Default"),
                    };

                    var token = new JwtSecurityTokenHandler().WriteToken(new JwtSecurityToken(
                        issuer: _config["Jwt:Issuer"],
                        audience: _config["Jwt:Audience"],
                        claims: claims,
                        expires: DateTime.Now.AddMinutes(15),
                        signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"])), SecurityAlgorithms.HmacSha256)));

                    return Ok(token);
                }
                else
                {
                    return NotFound("Invalid credentials / User not found");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, this.GetType().FullName);
                return Unauthorized(ex);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromBody] RegistrationDto registration)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid inputs");
                }

                await _auth.Register(registration);

                return Ok("Account created");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, this.GetType().FullName);
                return Unauthorized(ex);
            }
        }

        [HttpPost]
        public IActionResult GetAllRights()
        {
            return Ok(_auth.GetAllRights());
        }
    }
}