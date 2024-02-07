using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Setia.Data;
using Setia.Models;
using Setia.Services.Interfaces;
using Setia.Structs;
using System.Reflection;
using System.Security.Claims;
using System.Text.RegularExpressions;

namespace Setia.Services
{
    public class AuthService : IAuth
    {
        private readonly string regexEmailValidation = @"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$";

        private readonly SetiaContext _context;
        private readonly ILogger<AuthService> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ISender _sender;

        public AuthService
        (
            SetiaContext context,
            ILogger<AuthService> logger,
            IHttpContextAccessor httpContextAccessor,
            ISender sender
        )
        {
            _context = context;
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
            _sender = sender;
        }

        public async Task<int> GetCurrentUserId()
        {
            var identity = _httpContextAccessor.HttpContext.User.Identity as ClaimsIdentity;

            if (identity != null)
            {
                var userClaims = identity.Claims;

                string email = userClaims.FirstOrDefault(d => d.Type == ClaimTypes.Email)?.Value;
                string username = userClaims.FirstOrDefault(d => d.Type == ClaimTypes.NameIdentifier)?.Value;

                var user = await _context.Users
                    .Where(u => u.Email == email && u.Username == username)
                    .SingleOrDefaultAsync();
                if (user != null)
                {
                    return user.Id;
                }
            }
            return 0;
        }
        public async Task<IEnumerable<string>> GetUserRights(int id_user)
        {
            var user = await _context.Users.FindAsync(id_user);
            return [];
        }
        public async Task<IEnumerable<string>> GetUserRoles(int id_user)
        {
            var user = await _context.Users.FindAsync(id_user);
            return [];
        }
        public IEnumerable<string> GetAllActions()
        {
            List<string> actions = new List<string>();
            foreach (var controller in Assembly.GetExecutingAssembly().GetTypes().Where(type => typeof(ControllerBase).IsAssignableFrom(type)))
            {
                foreach (var method in controller.GetMethods(BindingFlags.Public | BindingFlags.Instance))
                {
                    if (method.DeclaringType == controller)
                    {
                        string right = $"{controller.Name}.{method.Name}";
                    }
                }
            }
            return actions;
        }
        public async Task Register(RegistrationDto registration)
        {
            try
            {
                if (registration.Email == null || !Regex.IsMatch(registration.Email, regexEmailValidation)) return;
                if (registration.Login.Username == null || registration.Login.Username.Length < 6) return;

                await _context.Users
                    .Where(u => u.Username == registration.Login.Username)
                    .SingleOrDefaultAsync();

                if (registration.Login.Password == null || registration.Login.Password.Length < 6) return;

                _context.Users.Add(new UseRoleModel()
                {
                    Email = registration.Email,
                    Username = registration.Login.Username,
                    Password = registration.Login.Password,
                });

                await _context.SaveChangesAsync();

                await _sender.SendEmail(registration.Email, "Account registration / mail validation", "");
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
                if (email == null || !Regex.IsMatch(email, regexEmailValidation)) return;
                if (username == null || username.Length < 6) return;
                if (currentPassword == null || currentPassword.Length < 6) return;
                if (newPassword == null || newPassword.Length < 6) return;

                var user = await _context.Users
                    .Where(u => u.Email == email && u.Username == username && u.Password == currentPassword)
                    .SingleOrDefaultAsync();
                if (user != null)
                {
                    user.Password = newPassword;
                    await _context.SaveChangesAsync();
                    await _sender.SendEmail(email, "Account registration / mail validation", $"You just changed your password for {username}");
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
        public async Task AssignRoleToUser(int id_claim, int id_user)
        {
            try
            {
                await _context.Users.FindAsync(id_user);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, this.GetType().FullName);
            }
        }
        public async Task RemoveRoleFromUser(int id_claim, int id_user)
        {
            try
            {
                await _context.Users.FindAsync(id_user);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, this.GetType().FullName);
            }
        }
        public async Task AssignClaimToUser(int id_claim, int id_user)
        {
            try
            {
                await _context.Users.FindAsync(id_user);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, this.GetType().FullName);
            }
        }
        public async Task RemoveClaimFromUser(int id_claim, int id_user)
        {
            try
            {
                await _context.Users.FindAsync(id_user);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, this.GetType().FullName);
            }
        }
    }
}