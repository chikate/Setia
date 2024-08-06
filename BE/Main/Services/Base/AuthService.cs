using Main.Data.Contexts;
using Main.Data.Models.Base;
using Main.Services.Base.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace Main.Services.Base
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

        public async Task<object?> Login(UserModel loginCredentials)
        {
            try
            {
                if (loginCredentials.Password == null || loginCredentials.Password.Length < 6) throw new Exception("Invalid Credentials");

                loginCredentials.Password = await CriptPassword(loginCredentials.Password);

                UserModel? user = await _context.Users
                    .SingleOrDefaultAsync(u =>
                    u.Username == loginCredentials.Username &&
                    u.Password == loginCredentials.Password);

                if (user == null) return null;

                IEnumerable<Claim> claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, user.Name),
                        new Claim(ClaimTypes.Email, user.Email),
                        new Claim(ClaimTypes.NameIdentifier, user.Username),
                        new Claim(ClaimTypes.Role, "Default"),
                    };

                string token = new JwtSecurityTokenHandler().WriteToken(new JwtSecurityToken(
                    issuer: _config["Server"],
                    audience: _config["Origin"],
                    claims,
                    expires: DateTime.SpecifyKind(DateTime.Now.AddDays(1)!, DateTimeKind.Utc),
                    signingCredentials: new SigningCredentials(
                        new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(_config["Key"] ?? "")),
                            SecurityAlgorithms.HmacSha256)));

                return new { Token = token, User = user };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, GetType().FullName);
                throw;
            }
        }
        public async Task<bool> Register(UserModel registration)
        {
            try
            {
                if (registration.Email == null ||
                    !Regex.IsMatch(registration.Email, _config["RegexValidator:Email"] ?? ""))
                    throw new Exception("Invalid email");

                if (registration.Username == null ||
                    registration.Username.Length < 6) throw new Exception("Invalid username");

                if (registration.Password == null ||
                    registration.Password.Length < 6) throw new Exception("Invalid password");

                if (_context.Users.Any(u =>
                        u.Username == registration.Username)) throw new Exception("Username already exists");

                registration.Password = await CriptPassword(registration.Password);

                await _context.Users.AddAsync(registration);
                await _context.SaveChangesAsync();
                // generate confirmation link
                await _sender.SendMail(registration.Email, "Email validation", $"Here is the confirmation link: {"https://www.google.ro"}");
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, GetType().FullName);
                return false;
                throw;
            }
        }
        public async Task<UserModel?> GetCurrentUser()
        {
            if (_httpContextAccessor.HttpContext?.User.Identity is ClaimsIdentity identity)
            {
                IEnumerable<Claim> userClaims = identity.Claims;

                string email = userClaims.FirstOrDefault(d => d.Type == ClaimTypes.Email)?.Value ?? "";
                string username = userClaims.FirstOrDefault(d => d.Type == ClaimTypes.NameIdentifier)?.Value ?? "";

                UserModel? user = await _context.Users.Where(u => u.Email == email && u.Username == username).SingleOrDefaultAsync();
                return user;
            }
            return null;
        }

        public async Task<List<string>> GetAllRights()
        {
            return [];
        }
        public async Task<IEnumerable<string>?> GetUserTags(string? specific = ".", Guid? userId = null)
        {
            try
            {
                userId = userId ?? (await GetCurrentUser())?.Id;
                return await _context.Users
                    .Where(u => u.Id == userId)
                    .Select(p => p.Tags)
                    .SingleOrDefaultAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, GetType().FullName);
                throw;
            }
        }
        public async Task ChangePassword(string email, string username, string currentPassword, string newPassword)
        {
            try
            {
                if (email == null || !Regex.IsMatch(email, _config["RegexValidator:Email"] ?? "")) throw new Exception("Invalid Email");
                if (username == null || username.Length < 6) throw new Exception("Invalid Username");
                if (currentPassword == null || currentPassword.Length < 6) throw new Exception("Invalid Password");
                if (newPassword == null || newPassword.Length < 6) throw new Exception("Invalid new Passowrd");

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
            catch (Exception ex) { _logger.LogError(ex.Message, GetType().FullName); throw; }
        }
        public async Task<string> CriptPassword(string password) => Convert.ToHexString(SHA256.HashData(Encoding.Default.GetBytes(password)));
        public async Task<List<string>> CheckUserRights(IEnumerable<string> rightsToCeck, Guid? userId = null)
        {
            try
            {
                userId = userId ?? (await GetCurrentUser())?.Id;

                UserModel? user = await _context.Users.Where(p => p.Id == userId).SingleOrDefaultAsync();
                List<string> userRights = new List<string>();
                foreach (string rightToCheck in rightsToCeck)
                {
                    if (user.Tags.Any(t => t.ToString().Contains(rightToCheck))) userRights.Add(rightToCheck);
                }

                return userRights;
            }
            catch (Exception ex) { _logger.LogError(ex.Message, GetType().FullName); throw; }
        }
    }
}