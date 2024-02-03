using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Setia.Data;
using Setia.Models;
using Setia.Services.Interfaces;
using System.Security.Claims;
using System.Text.RegularExpressions;

namespace Setia.Services
{
    public class AuthenticationService : IAuth
    {
        private readonly string regexEmailValidation = @"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$";

        private readonly SetiaContext _context;
        private readonly ILogger<AuthenticationService> _logger;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuthenticationService
        (
            SetiaContext context,
            ILogger<AuthenticationService> logger,
            IMapper mapper,
            IHttpContextAccessor httpContextAccessor
        )
        {
            _context = context;
            _logger = logger;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public UserModel GetCurrentUser()
        {
            var identity = _httpContextAccessor.HttpContext.User.Identity as ClaimsIdentity;

            if (identity != null)
            {
                var userClaims = identity.Claims;
                return new UserModel
                {
                    Email = userClaims.FirstOrDefault(d => d.Type == ClaimTypes.Email)?.Value,
                    Username = userClaims.FirstOrDefault(d => d.Type == ClaimTypes.NameIdentifier)?.Value,
                };
            }
            return new UserModel();
        }

        public async Task RegisterUser(string email, string username, string password)
        {
            try
            {
                if (email == null || !Regex.IsMatch(email, regexEmailValidation))
                {
                    return;
                }

                if (username == null || username.Length < 3)
                {
                    return;
                }

                var user = await _context.Users
                    .Where(u => u.Username == username)
                    .SingleOrDefaultAsync();
                if (user != null)
                {
                    return;
                }

                if (password == null || password.Length < 6)
                {
                    return;
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

                if (username == null || username.Length < 3)
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

        //public async Task ForgotPassword(string email, string username)
        //{
        //    try
        //    {
        //        if (email == null || !Regex.IsMatch(email, regexEmailValidation)
        //        {
        //            return;
        //        }

        //        if (username == null || username.Length < 3)
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