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

        public AuthService
        (
            SetiaContext context,
            ILogger<AuthService> logger,
            IHttpContextAccessor httpContextAccessor
        )
        {
            _context = context;
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
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

        public async Task Register(RegistrationDto registration)
        {
            try
            {
                if (registration.Email == null || !Regex.IsMatch(registration.Email, regexEmailValidation))
                {
                    return;
                }

                if (registration.Login.Username == null || registration.Login.Username.Length < 6)
                {
                    return;
                }

                var user = await _context.Users
                    .Where(u => u.Username == registration.Login.Username)
                    .SingleOrDefaultAsync();
                if (user != null)
                {
                    return;
                }

                if (registration.Login.Password == null || registration.Login.Password.Length < 6)
                {
                    return;
                }

                _context.Users.Add(new UserModel()
                {
                    Email = registration.Email,
                    Username = registration.Login.Username,
                    Password = registration.Login.Password,
                });
                await _context.SaveChangesAsync();

                //send email verification
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
                if (email == null || !Regex.IsMatch(email, regexEmailValidation))
                {
                    return;
                }

                if (username == null || username.Length < 6)
                {
                    return;
                }

                if (currentPassword == null || currentPassword.Length < 6)
                {
                    return;
                }

                if (newPassword == null || newPassword.Length < 6)
                {
                    return;
                }

                var user = await _context.Users
                    .Where(u => u.Email == email && u.Username == username && u.Password == currentPassword)
                    .SingleOrDefaultAsync();
                if (user != null)
                {
                    user.Password = newPassword;
                    await _context.SaveChangesAsync();
                    return;
                }
                else
                {
                    return;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, this.GetType().FullName);
                return;
            }
        }

        public IEnumerable<string> GetAllRights()
        {
            List<string> allRights = new List<string>();
            foreach (var controller in Assembly.GetExecutingAssembly().GetTypes().Where(type => typeof(ControllerBase).IsAssignableFrom(type)))
            {
                foreach (var method in controller.GetMethods(BindingFlags.Public | BindingFlags.Instance))
                {
                    if (method.DeclaringType == controller)
                    {
                        allRights.Add($"{controller.Name}.{method.Name}");
                    }
                }
            }
            return allRights;
        }

        //public async Task ForgotPassword(string email, string username)
        //{
        //    try
        //    {
        //        if (email == null || !Regex.IsMatch(email, regexEmailValidation)
        //        {
        //            return;
        //        }

        //        if (username == null || username.Length < 6)
        //        {
        //            return;
        //        }

        //        var user = await _context.Users
        //            .Where(u => u.Email == email && u.Username == username)
        //            .SingleOrDefaultAsync();
        //        if (user != null)
        //        {
        //            //generate api change link
        //            //send mail with new password link
        //            return;
        //        }
        //        else
        //        {
        //            return;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(ex, this.GetType().FullName);
        //        return;
        //    }
        //}

        //public async Task ForgotUsername(string email)
        //{
        //    try
        //    {
        //        if (email == null || !Regex.IsMatch(email, regexEmailValidation))
        //        {
        //            return;
        //        }

        //        List<string> usernames = await _context.Users
        //            .Where(u => u.Email == email)
        //            .Select(u => u.Username)
        //            .ToListAsync();
        //        if (usernames != null)
        //        {
        //            // send email with users
        //            return;
        //        }
        //        else
        //        {
        //            return;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(ex, this.GetType().FullName);
        //        return;
        //    }
        //}

    }
}