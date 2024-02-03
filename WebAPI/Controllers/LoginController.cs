using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Setia.Data;
using Setia.Models;
using Setia.Services.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.RegularExpressions;

namespace Setia.Controllers
{
    [ApiController]
    [Route("/api/[controller]/[action]")]
    public class LoginController : ControllerBase
    {
        private readonly string regexEmailValidation = @"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$";

        private readonly SetiaContext _context;
        private readonly ILogger<LoginController> _logger;
        private readonly IConfiguration _config;
        private readonly IMapper _mapper;
        private readonly IAudit _audit;
        private readonly IAuth _auth;

        public LoginController
        (
            SetiaContext context,
            ILogger<LoginController> logger,
            IConfiguration config,
            IMapper mapper,
            IAudit audit,
            IAuth auth
        )
        {
            _context = context;
            _logger = logger;
            _config = config;
            _mapper = mapper;
            _audit = audit;
            _auth = auth;
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login(string username, string password)
        {
            try
            {
                if (username == null || username.Length < 3)
                {
                    return Unauthorized("Invalid username (too short)");
                }

                if (password == null || password.Length < 6)
                {
                    return Unauthorized("Invalid password (too short)");
                }

                var user = await _context.Users
                    .Where(u => u.Username == username && u.Password == password)
                    .FirstOrDefaultAsync();
                if (user != null)
                {
                    var claims = new[]
                    {
                        new Claim(ClaimTypes.NameIdentifier, user.Username),
                        new Claim(ClaimTypes.Email, user.Email),
                        new Claim(ClaimTypes.GivenName, user.Name),
                        new Claim(ClaimTypes.Surname, user.Name),
                        new Claim(ClaimTypes.Role, user.AuthorityCode.ToString()),
                    };

                    var token = new JwtSecurityTokenHandler().WriteToken(new JwtSecurityToken(_config["Jwt:Issuer"], _config["Jwt:Audience"], claims, expires: DateTime.Now.AddMinutes(15), signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"])), SecurityAlgorithms.HmacSha256)));
                    return Ok(token);
                }
                else
                {
                    return NotFound("Invalid credentials / User not found");
                }
            }
            catch (Exception ex)
            {
                return Unauthorized(ex);
            }
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Register(string email, string username, string password)
        {
            try
            {
                if (email == null || !Regex.IsMatch(email, regexEmailValidation))
                {
                    return Unauthorized("Invalid email");
                }

                if (username == null || username.Length < 3)
                {
                    return Unauthorized("Invalid username (too short)");
                }

                var user = await _context.Users
                    .Where(u => u.Username == username)
                    .SingleOrDefaultAsync();
                if (user != null)
                {
                    return Unauthorized("Username already exists");
                }

                if (password == null || password.Length < 6)
                {
                    return Unauthorized("Invalid password (too short)");
                }

                _context.Users.Add(new UserModel()
                {
                    Email = email,
                    Username = username,
                    Password = password,
                    CreationDate = DateTime.UtcNow,
                });
                await _context.SaveChangesAsync();

                //send email verification
                return Ok("Account created");

            }
            catch (Exception ex)
            {
                return Unauthorized(ex);
            }
        }
    }
}