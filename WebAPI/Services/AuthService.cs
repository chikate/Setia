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

        public async Task<UserModel> GetCurrentUser()
        {
            UserModel? user = null;
            if (_httpContextAccessor.HttpContext?.User.Identity is ClaimsIdentity identity)
            {
                IEnumerable<Claim> userClaims = identity.Claims;

                string email = userClaims.FirstOrDefault(d => d.Type == ClaimTypes.Email)?.Value ?? "";
                string username = userClaims.FirstOrDefault(d => d.Type == ClaimTypes.NameIdentifier)?.Value ?? "";

                user = await _context.Users
                    .Where(u => u.Email == email && u.Username == username)
                    .SingleOrDefaultAsync();
            }
            return user ?? new UserModel();
        }
        public async Task<IEnumerable<string>> GetUserRights(int idUser)
        {
            UserModel? user = await _context.Users.FindAsync(idUser);
            return [];
        }
        public async Task<IEnumerable<string>> GetUserRoles(int idUser)
        {
            UserModel? user = await _context.Users.FindAsync(idUser);
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
                    .Where(u => u.Username == registration.Username)
                    .SingleOrDefaultAsync();

                registration.Id = 0;
                _context.Users.Add(registration);

                await _context.SaveChangesAsync();

                await _sender.Send(registration.Email, "Account registration / mail validation", "");

                return;
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
                    await _sender.Send(email, "Account registration / mail validation", $"You just changed your password for {username}");
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
        public async Task AssignRoleToUser(int idClaim, int idUser)
        {
            try
            {
                await _context.Users.FindAsync(idUser);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, this.GetType().FullName);
            }
        }
        public async Task RemoveRoleFromUser(int idClaim, int idUser)
        {
            try
            {
                await _context.Users.FindAsync(idUser);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, this.GetType().FullName);
            }
        }
        public async Task AssignClaimToUser(int idClaim, int idUser)
        {
            try
            {
                await _context.Users.FindAsync(idUser);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, this.GetType().FullName);
            }
        }
        public async Task RemoveClaimFromUser(int idClaim, int idUser)
        {
            try
            {
                await _context.Users.FindAsync(idUser);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, this.GetType().FullName);
            }
        }
    }
}