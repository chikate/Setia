using Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Setia.Models;
using Setia.Services.Interfaces;
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
        private readonly BaseContext _context;
        private readonly ILogger<AuthController> _logger;
        private readonly IConfiguration _config;
        private readonly IAuth _auth;

        public AuthController
        (
            BaseContext context,
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
        public async Task<IActionResult> Login([FromBody] UserModel loginCredentials)
        {
            try
            {
                if (loginCredentials.Password == null || loginCredentials.Password.Length < 6) return BadRequest("Invalid password (too short)");

                UserModel? user = await _context.Users
                    .Where(u => u.Username == loginCredentials.Username && u.Password == loginCredentials.Password)
                    .FirstOrDefaultAsync();
                if (user != null)
                {
                    List<Claim> claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, user.Name),
                        new Claim(ClaimTypes.Email, user.Email),
                        new Claim(ClaimTypes.NameIdentifier, user.Username),
                        new Claim(ClaimTypes.Role, "Default"),
                    };

                    string token = new JwtSecurityTokenHandler().WriteToken(new JwtSecurityToken(
                        issuer: _config["JWT:Issuer"],
                        audience: _config["JWT:Audience"],
                        claims,
                        expires: DateTime.Now.AddDays(1),
                        signingCredentials: new SigningCredentials(
                            new SymmetricSecurityKey(
                                Encoding.UTF8.GetBytes(_config["JWT:Key"] ?? "")),
                                SecurityAlgorithms.HmacSha256)));

                    return Ok(new LoginResultDto { Token = token, User = user });
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
        public async Task<IActionResult> Register([FromBody] UserModel registration)
        {
            try
            {
                await _auth.Register(registration);
                return Ok("Account created");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, this.GetType().FullName);
                return Unauthorized(ex);
            }
        }
    }
}