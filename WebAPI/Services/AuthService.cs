using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Setia.Contexts.Base;
using Setia.Models.Base;
using Setia.Services.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace Setia.Services
{
    public class AuthService : IAuth
    {
        private readonly BaseContext _context;
        private readonly ILogger<AuthService> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ISender _sender;
        private readonly IConfiguration _config;

        public AuthService
        (
            BaseContext context,
            ILogger<AuthService> logger,
            IHttpContextAccessor httpContextAccessor,
            ISender sender,
            IConfiguration config
        )
        {
            _context = context;
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
            _sender = sender;
            _config = config;
        }

        public async Task<object> Login(UserModel loginCredentials)
        {
            try
            {
                if (loginCredentials.Password == null || loginCredentials.Password.Length < 6) throw new Exception();

                UserModel? user = await _context.Users
                    .FirstOrDefaultAsync(u =>
                    u.Username == loginCredentials.Username &&
                    u.Password == CriptPassword(loginCredentials.Password));

                if (user != null)
                {
                    IEnumerable<Claim> claims = new List<Claim>
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
                        expires: DateTime.SpecifyKind((DateTime)DateTime.Now.AddDays(1)!, DateTimeKind.Utc),
                        signingCredentials: new SigningCredentials(
                            new SymmetricSecurityKey(
                                Encoding.UTF8.GetBytes(_config["JWT:Key"] ?? "")),
                                SecurityAlgorithms.HmacSha256)));

                    return new { Token = token, User = user };
                }
                else { throw new Exception(); }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, this.GetType().FullName);
                throw new Exception();
            }
        }
        public UserModel? GetCurrentUser()
        {
            if (_httpContextAccessor.HttpContext?.User.Identity is ClaimsIdentity identity)
            {
                IEnumerable<Claim> userClaims = identity.Claims;

                string email = userClaims.FirstOrDefault(d => d.Type == ClaimTypes.Email)?.Value ?? "";
                string username = userClaims.FirstOrDefault(d => d.Type == ClaimTypes.NameIdentifier)?.Value ?? "";

                UserModel? user = _context.Users.Where(u => u.Email == email && u.Username == username).SingleOrDefault();
                return user;
            }
            return null;
        }
        public async Task<IEnumerable<string>> GetUserTags(string? specific = ".", Guid? userId = null)
        {
            try
            {
                if (userId == null) { userId = GetCurrentUser()?.Id; }
                return await _context.Users.Where(u => u.Id == userId).Select(p => p.Tags).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, this.GetType().FullName);
                throw new Exception();
            }
        }
        public async Task Register(UserModel registration)
        {
            try
            {
                if (registration.Email == null || !Regex.IsMatch(registration.Email, _config["RegexValidator:Email"] ?? "")) throw new Exception();
                if (registration.Username == null || registration.Username.Length < 6) throw new Exception();
                if (registration.Password == null || registration.Password.Length < 6) throw new Exception();

                _context.Users.Any(u => u.Username != registration.Username);
                registration.Password = CriptPassword(registration.Password);
                _context.Users.Add(registration);
                await _context.SaveChangesAsync();
                string link = "https://www.google.ro";
                await _sender.SendMail(registration.Email, "Email validation", $"Here is the confirmation link: {link}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, this.GetType().FullName);
                throw new Exception();
            }
        }
        public async Task ChangePassword(string email, string username, string currentPassword, string newPassword)
        {
            try
            {
                if (email == null || !Regex.IsMatch(email, _config["RegexValidator:Email"] ?? "")) throw new Exception();
                if (username == null || username.Length < 6) throw new Exception();
                if (currentPassword == null || currentPassword.Length < 6) throw new Exception();
                if (newPassword == null || newPassword.Length < 6) throw new Exception();

                UserModel? user = await _context.Users
                    .Where(u => u.Email == email && u.Username == username && u.Password == currentPassword)
                    .SingleOrDefaultAsync();
                if (user != null)
                {
                    user.Password = newPassword;
                    await _context.SaveChangesAsync();
                    string recoveryLink = "https://www.google.com";
                    await _sender.SendMail(email, "Password changed", $"You just changed your password for {username}. If it was ot you access this link {recoveryLink}");
                    return;
                }
                return;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, this.GetType().FullName);
                throw new Exception();
            }
        }
        public string CriptPassword(string password) => Convert.ToHexString(SHA256.HashData(Encoding.Default.GetBytes(password)));
        public async Task<List<string>> CheckUserRights(IEnumerable<string> rightsToCeck, Guid? userId = null)
        {
            try
            {
                if (userId == null) userId = GetCurrentUser().Id;

                UserModel user = await _context.Users.Where(p => p.Id == userId).SingleOrDefaultAsync();
                List<string> userRights = new List<string>();
                foreach (string rightToCheck in rightsToCeck)
                {
                    if (user.Tags.Any(t => t.ToString().Contains(rightToCheck))) userRights.Add(rightToCheck);
                }

                return userRights;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, this.GetType().FullName);
                throw new Exception();
            }
        }
    }
}