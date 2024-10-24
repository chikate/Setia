using Main.Data.Contexts;
using Main.Data.DTOs;
using Main.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Runtime.CompilerServices;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace Main.Services;

public interface IAuthService
{
    Task ChangePassword(string email, string username, string currentPassword, string newPassword);
    Task<List<string>> CheckUserRights(IEnumerable<string> rightsToCeck, Guid? userId = null);
    Task<string> CriptPassword(string password);
    Task<List<string>> GetAllRights();
    Task<UserModel?> GetCurrentUser();
    Task<IEnumerable<string>?> GetUserTags(string? specific = ".", Guid? userId = null);
    Task<object> Login(AuthenticationDTO loginCredentials);
    Task<UserModel> Register(RegistrationDTO registration);
    Task CheckUserRight(Guid? userId = null, [CallerMemberName] string? methodName = "", [CallerFilePath] string? filePath = "");
}

public class AuthService : IAuthService
{
    #region Dependency Injection 
    private readonly BaseContext _context;
    private readonly ILogger<AuthService> _logger;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly ISenderService _sender;
    private readonly IConfiguration _config;

    public AuthService
    (
        BaseContext context,
        ILogger<AuthService> logger,
        IHttpContextAccessor httpContextAccessor,
        ISenderService sender,
        IConfiguration config
    )
    {
        _context = context;
        _logger = logger;
        _httpContextAccessor = httpContextAccessor;
        _sender = sender;
        _config = config;
    }
    #endregion

    public async Task<object> Login(AuthenticationDTO loginCredentials)
    {
        try
        {
            if (string.IsNullOrEmpty(loginCredentials.Password) || loginCredentials.Password.Length < 6 ||
                string.IsNullOrEmpty(loginCredentials.Username) || loginCredentials.Username.Length < 3)
                throw new Exception("Invalid Credentials");

            loginCredentials.Password = await CriptPassword(loginCredentials.Password);

            UserModel? user = await _context.Set<UserModel>()
                .SingleOrDefaultAsync(u =>
                    u.Username == loginCredentials.Username &&
                    u.Password == loginCredentials.Password);

            if (user == null) throw new Exception("User Not Found");

            return new
            {
                Token = new JwtSecurityTokenHandler().WriteToken(new JwtSecurityToken(
                issuer: _config["Server"],
                audience: _config["Origin"],
                claims: new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.Name),
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.NameIdentifier, user.Username),
                    new Claim(ClaimTypes.Role, "Default"),
                },
                expires: DateTime.SpecifyKind(DateTime.Now.AddDays(1)!, DateTimeKind.Utc),
                signingCredentials: new SigningCredentials(
                    new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(_config["CryptKey"] ?? "")),
                        SecurityAlgorithms.HmacSha256))),
                User = user
            };
        }
        catch (Exception ex) { _logger.LogError(ex.Message, GetType().FullName); throw; }
    }
    public async Task<UserModel> Register(RegistrationDTO registration)
    {
        try
        {
            if (registration.Email == null || !Regex.IsMatch(registration.Email, _config["RegexValidator:Email"] ?? ""))
                throw new Exception("Invalid email");

            if (registration.Username == null || registration.Username.Length < 6)
                throw new Exception("Invalid username");

            if (registration.Password == null || registration.Password.Length < 6)
                throw new Exception("Invalid password");

            if (_context.Set<UserModel>().Any(u => u.Username == registration.Username))
                throw new Exception("Username already exists");

            registration.Password = await CriptPassword(registration.Password);

            UserModel createdUser = (await _context.Set<UserModel>().AddAsync(new UserModel
            {
                Username = registration.Username,
                Password = registration.Password,
                Email = registration.Email,
                Signiture = registration.Signiture,
                BirthDay = registration.BirthDay,
                Name = registration.Name,
                Avatar = registration.Avatar,
            })).Entity;

            await _context.SaveChangesAsync();

            return createdUser;

            // _sender.SendMail(registration.Email, "Email validation", $"Here is the confirmation link: {"https://www.google.ro"}");
        }
        catch (Exception ex) { _logger.LogError(ex.Message, GetType().FullName); throw; }
    }
    public async Task<UserModel?> GetCurrentUser() =>
        _httpContextAccessor.HttpContext?.User.Identity is ClaimsIdentity identity
        ? await _context.Set<UserModel>().SingleOrDefaultAsync(u =>
            u.Email == identity.FindFirst(ClaimTypes.Email).Value &&
            u.Username == identity.FindFirst(ClaimTypes.NameIdentifier).Value)
        : null;
    public async Task<List<string>> GetAllRights()
    {
        // Simulate async operation
        await Task.CompletedTask;
        return [];
    }
    public async Task<IEnumerable<string>?> GetUserTags(string? specific = ".", Guid? userId = null)
    {
        try
        {
            userId = userId ?? (await GetCurrentUser())?.Id;
            return await _context.Set<UserModel>()
                .Where(u => u.Id == userId)
                .Select(p => p.Tags)
                .SingleOrDefaultAsync();
        }
        catch (Exception ex) { _logger.LogError(ex.Message, GetType().FullName); throw; }
    }
    public async Task ChangePassword(string email, string username, string currentPassword, string newPassword)
    {
        try
        {
            if (email == null || !Regex.IsMatch(email, _config["RegexValidator:Email"] ?? "")) throw new Exception("Invalid Email");
            if (username == null || username.Length < 6) throw new Exception("Invalid Username");
            if (currentPassword == null || currentPassword.Length < 6) throw new Exception("Invalid Password");
            if (newPassword == null || newPassword.Length < 6) throw new Exception("Invalid new Passowrd");

            UserModel? user = await _context.Set<UserModel>()
                .Where(u => u.Email == email && u.Username == username && u.Password == currentPassword)
                .SingleOrDefaultAsync();
            if (user != null)
            {
                user.Password = newPassword;
                await _context.SaveChangesAsync();
                string recoveryLink = "https://www.google.com";
                _sender.SendMail(new EmailDTO
                {
                    To = email,
                    Subject = "Password changed",
                    Body = $"You just changed your password for {username}. If it was ot you access this link {recoveryLink}"
                });
                return;
            }
            return;
        }
        catch (Exception ex) { _logger.LogError(ex.Message, GetType().FullName); throw; }
    }
    public async Task<string> CriptPassword(string password)
    {
        // Simulate async operation
        await Task.CompletedTask;
        return Convert.ToHexString(SHA256.HashData(Encoding.UTF8.GetBytes(password)));
    }
    public async Task<List<string>> CheckUserRights(IEnumerable<string> rightsToCeck, Guid? userId = null)
    {
        try
        {
            userId = userId ?? (await GetCurrentUser())?.Id;
            UserModel? user = await _context.Set<UserModel>().Where(p => p.Id == userId).SingleOrDefaultAsync();
            if (user == null) throw new Exception("User Not Found");
            List<string> userRights = new();
            foreach (string rightToCheck in rightsToCeck) if (user.Tags.Any(t => t.ToString().Contains(rightToCheck))) userRights.Add(rightToCheck);
            return userRights;
        }
        catch (Exception ex) { _logger.LogError(ex.Message, GetType().FullName); throw; }
    }
    public async Task CheckUserRight(Guid? userId = null, [CallerMemberName] string? methodName = "", [CallerFilePath] string? filePath = "")
    {
        UserModel? user = await _context.Set<UserModel>().FindAsync(userId ?? (await GetCurrentUser())?.Id);
        if (user?.Tags.Any(t => t.Contains("Dragos") || t.Contains("Role.Admin") || t.Contains($"{Path.GetFileNameWithoutExtension(filePath)}.{methodName}")) == false)
            throw new UnauthorizedAccessException($"User: {user?.Name}, does not have the required right: {Path.GetFileNameWithoutExtension(filePath)}.{methodName}");
    }
}