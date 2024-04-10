using Base;
using Microsoft.EntityFrameworkCore;
using Setia.Models;
using Setia.Services.Interfaces;
using System.Security.Claims;
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

        public UserModel GetCurrentUser()
        {
            if (_httpContextAccessor.HttpContext?.User.Identity is ClaimsIdentity identity)
            {
                IEnumerable<Claim> userClaims = identity.Claims;

                string email = userClaims.FirstOrDefault(d => d.Type == ClaimTypes.Email)?.Value ?? "";
                string username = userClaims.FirstOrDefault(d => d.Type == ClaimTypes.NameIdentifier)?.Value ?? "";

                UserModel? user = _context.Users.Where(u => u.Email == email && u.Username == username).SingleOrDefault();
                return user ?? new UserModel();
            }
            return new UserModel();
        }
        public async Task<IEnumerable<string>> GetUserRights(string username)
        {
            UserModel? user = await _context.Users.FindAsync(username);
            return [];
        }
        public async Task<IEnumerable<string>> GetUserRoles(string username)
        {
            UserModel? user = await _context.Users.FindAsync(username);
            return [];
        }
        public async Task Register(UserModel registration)
        {
            try
            {
                if (registration.Email == null || !Regex.IsMatch(registration.Email, _config["RegexValidator:Email"] ?? "")) return;
                if (registration.Username == null || registration.Username.Length < 6) return;
                if (registration.Password == null || registration.Password.Length < 6) return;

                await _context.Users
                    .Where(u => u.Username != registration.Username)
                    .SingleOrDefaultAsync();

                _context.Users.Add(registration);
                await _context.SaveChangesAsync();
                string link = "https://www.google.ro";
                await _sender.SendMail(registration.Email, "Email validation", $"Here is the confirmation link: {link}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, this.GetType().FullName);
                return;
            }
        }
        public async Task ChangePassword(string email, string username, string currentPassword, string newPassword)
        {
            try
            {
                if (email == null || !Regex.IsMatch(email, _config["RegexValidator:Email"] ?? "")) return;
                if (username == null || username.Length < 6) return;
                if (currentPassword == null || currentPassword.Length < 6) return;
                if (newPassword == null || newPassword.Length < 6) return;

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
                return;
            }
        }
        public async Task GiveUserTag(Guid tag, string username)
        {
            try
            {
                await _context.UserTags.AddAsync(new UserTagModel() { User = username, Tag = tag });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, this.GetType().FullName);
            }
        }
        public async Task RemoveUserTag(Guid Tag, string username)
        {
            try
            {
                UserTagModel? userTag = await _context.UserTags.Where(t => t.User == username).FirstOrDefaultAsync();
                if (userTag != null) _context.UserTags.Remove(userTag);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, this.GetType().FullName);
            }
        }
        public async Task GetUserTags(Guid Tag, string username)
        {
            try
            {
                UserTagModel? userTag = await _context.UserTags.Where(t => t.User == username).FirstOrDefaultAsync();
                if (userTag != null) _context.UserTags.Remove(userTag);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, this.GetType().FullName);
            }
        }
    }
}