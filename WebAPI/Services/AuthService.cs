using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Setia.Data;
using Setia.Models;
using Setia.Services.Interfaces;
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
        public IEnumerable<object> GetAllActions()
        {
            List<object> actions = new List<object>();

            foreach (var controller in Assembly.GetExecutingAssembly().GetTypes().Where(type => typeof(ControllerBase).IsAssignableFrom(type)))
            {
                if (!controller.IsDefined(typeof(AllowAnonymousAttribute), inherit: true))
                {
                    List<object> children = new List<object>();

                    foreach (var parameter in Assembly.GetExecutingAssembly().GetTypes().Where(type => typeof(ControllerBase).IsAssignableFrom(type)))
                    {
                        children.Add(new { key = $"{controller.Name}", label = $"{controller.Name.Replace("Controller", "")}" });
                    }

                    foreach (var method in controller.GetMethods(BindingFlags.Public | BindingFlags.Instance))
                    {
                        if (method.DeclaringType == controller)
                        {
                            actions.Add(new { key = $"{controller.Name}.{method.Name}", label = $"{controller.Name.Replace("Controller", "")} {method.Name}", children });
                        }
                    }
                }
            }
            return actions;
        }

        public async Task Register(UserModel registration)
        {
            try
            {
                if (registration.Email == null || !Regex.IsMatch(registration.Email, regexEmailValidation)) return;
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