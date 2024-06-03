using Microsoft.EntityFrameworkCore;
using Setia.Contexts.Base;
using Setia.Models.Base;
using Setia.Services.Interfaces;
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
        public async Task<IEnumerable<string>> GetUserTags(string specific, string? username = null)
        {
            try
            {
                if (username == null) username = GetCurrentUser()?.Username;
                return (await _context.UserTags.Where(p => p.User == username && p.Tag.MatchesLQuery(specific)).ToListAsync()).Select(p => p.Tag.ToString());
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
        public async Task<List<string>> CheckUserRights(IEnumerable<string> rightsToCeck, string? user = null)
        {
            try
            {
                if (string.IsNullOrEmpty(user)) user = GetCurrentUser()?.Username;

                IEnumerable<UserTagModel> userTags = await _context.UserTags.Where(p => p.User == user).ToListAsync();
                List<string> userRights = new List<string>();
                foreach (string rightToCheck in rightsToCeck)
                {
                    if (userTags.Any(t => t.Tag.ToString().Contains(rightToCheck))) userRights.Add(rightToCheck);
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