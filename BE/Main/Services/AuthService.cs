using Main.Data.Contexts;
using Main.Data.DTOs;
using Main.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace Main.Services;

public interface IAuthService
{
    Task ChangePassword(string email, string username, string currentPassword, string newPassword);
    Task CheckUserRight(Guid? userId = null, [CallerMemberName] string? methodName = "", [CallerFilePath] string? filePath = "");
    Task<List<string>> CheckUserRights(IEnumerable<string> rightsToCeck, Guid? userId = null);
    Task<string> CriptPassword(string password);
    Task<Dictionary<string, List<string>>> GetAllAPIs();
    Task<UserModel?> GetCurrentUser();
    Task<IEnumerable<string>?> GetUserTags(string? specific = ".", Guid? userId = null);
    Task<string> Login(AuthenticationDTO loginCredentials);
    Task<string> RecoverAccount(string email);
    Task<UserModel> Register(RegistrationDTO registration);
    Task<bool> ValidateCredentials(object obj);
}

public class AuthService : IAuthService
{
    #region Dependency Injection 
    private readonly BaseContext _baseContext;
    private readonly ILogger<AuthService> _logger;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly ISenderService _sender;
    private readonly IConfiguration _config;

    public AuthService
    (
        BaseContext baseContext,
        ILogger<AuthService> logger,
        IHttpContextAccessor httpContextAccessor,
        ISenderService sender,
        IConfiguration config
    )
    {
        _baseContext = baseContext;
        _logger = logger;
        _httpContextAccessor = httpContextAccessor;
        _sender = sender;
        _config = config;
    }
    #endregion

    public async Task<string> Login(AuthenticationDTO loginCredentials)
    {
        try
        {
            await ValidateCredentials(loginCredentials);

            loginCredentials.Password = await CriptPassword(loginCredentials.Password);

            UserModel? user = await _baseContext.Set<UserModel>()
                .SingleOrDefaultAsync(u =>
                    u.Username == loginCredentials.Username &&
                    u.Password == loginCredentials.Password);

            if (user == null) 
                throw new Exception("User Not Found");

            return new JwtSecurityTokenHandler().WriteToken(new JwtSecurityToken(
                issuer: _config["Server"],
                audience: _config["Origin"],
                claims: new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.Name),
                    new Claim(ClaimTypes.GivenName, user.Username),
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Email, user.Email),
                    //new Claim(ClaimTypes.Role, user.Tags.Find("Dragos") ? "Admin" : "Default"),
                },
                expires: DateTime.Now.ToUniversalTime().AddDays(1)!,
                signingCredentials: new SigningCredentials(
                    new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(_config["CryptKey"]!)),
                        SecurityAlgorithms.HmacSha256)));
        }
        catch (Exception ex) { _logger.LogError(ex, GetType().FullName, ex.Message); throw; }
    }
    public async Task<UserModel> Register(RegistrationDTO registration)
    {
        try
        {
            await ValidateCredentials(registration);

            registration.Password = await CriptPassword(registration.Password);

            UserModel? userCheck = await _baseContext.Set<UserModel>()
                .SingleOrDefaultAsync(u => u.Username == registration.Username);

            if (userCheck != null) 
                throw new Exception("User Already Exists");

            UserModel newUser = new UserModel
            {
                Username = registration.Username,
                Password = registration.Password,
                Email = registration.Email,
                Signiture = registration.Signiture,
                BirthDay = registration.BirthDay,
                Name = registration.Name,
                Avatar = registration.Avatar,
            };

            await _baseContext.Set<UserModel>().AddAsync(newUser);
            await _baseContext.SaveChangesAsync();

            // _sender.SendMail(registration.Email, "Email validation", $"Here is the confirmation link: https://www.google.ro");

            return newUser;
        }
        catch (Exception ex) { _logger.LogError(ex, GetType().FullName, ex.Message); throw; }
    }
    public async Task<string> RecoverAccount(string email)
    {
        try
        {
            if (!Regex.IsMatch(email, _config["RegexValidator:Email"]!)) 
                throw new Exception("Invalid email format.");

            List<UserModel> usersWithThisEmail = await _baseContext.Set<UserModel>().Where(p => p.Email == email).ToListAsync();

            if (usersWithThisEmail.Count == 0) 
                throw new EntryPointNotFoundException($"No account found for email: {email}");

            //_sender.SendMail(new EmailDTO
            //{
            //    To = email.First(),
            //    Subject = "Account Recovery",
            //    Body = $"Click here to reset your password: https://www.yourapp.com/reset-password?token={Guid.NewGuid().ToString()}"
            //});

            await _baseContext.SaveChangesAsync(); // Save any changes, if applicable.

            return "A recovery email has been sent. Please check your inbox.";
        }
        catch (Exception ex) { _logger.LogError(ex, GetType().FullName, ex.Message); throw; }
    }
    public async Task<UserModel?> GetCurrentUser() =>
        _httpContextAccessor.HttpContext?.User.Identity is ClaimsIdentity identity
        ? await _baseContext.Set<UserModel>().SingleOrDefaultAsync(u =>
            u.Email == identity.FindFirst(ClaimTypes.Email)!.Value &&
            u.Username == identity.FindFirst(ClaimTypes.NameIdentifier)!.Value)
        : null;
    public async Task<IEnumerable<string>?> GetUserTags(string? specific = ".", Guid? userId = null)
    {
        try
        {
            userId = userId ?? (await GetCurrentUser())?.Id;
            return await _baseContext.Set<UserModel>()
                .Where(u => u.Id == userId)
                .Select(p => p.Tags)
                .SingleOrDefaultAsync();
        }
        catch (Exception ex) { _logger.LogError(ex, GetType().FullName, ex.Message); throw; }
    }
    public async Task ChangePassword(string email, string username, string currentPassword, string newPassword)
    {
        try
        {
            if (string.IsNullOrEmpty(email) || !Regex.IsMatch(email, _config["RegexValidator:Email"] ?? "^\\S+@\\S+\\.\\S+$"))
                throw new Exception("Invalid Email format.");

            if (string.IsNullOrEmpty(username) || username.Length < 6)
                throw new Exception("Invalid Username. Must be at least 6 characters long.");

            if (string.IsNullOrEmpty(currentPassword) || currentPassword.Length < 6)
                throw new Exception("Invalid Current Password. Must be at least 6 characters long.");

            if (string.IsNullOrEmpty(newPassword) || newPassword.Length < 6)
                throw new Exception("Invalid New Password. Must be at least 6 characters long.");

            // Fetch user with matching credentials
            var user = await _baseContext.Set<UserModel>()
                .Where(u => u.Email == email && u.Username == username && u.Password == currentPassword)
                .SingleOrDefaultAsync();

            if (user == null) 
                throw new Exception("User not found with provided credentials.");

            user.Password = await CriptPassword(newPassword);
            await _baseContext.SaveChangesAsync();

            // Send Confirmation Email
            string recoveryLink = "https://www.google.com";
            await _sender.SendMail(new EmailDTO
            {
                To = new List<string> { email },
                Subject = "Password Changed",
                Body = $"You just changed your password for {username}. If this wasn't you, please access this link: {recoveryLink}"
            });
        }
        catch (Exception ex) { _logger.LogError(ex, GetType().FullName, ex.Message); throw; }
    }
    public async Task<List<string>> CheckUserRights(IEnumerable<string> rightsToCeck, Guid? userId = null)
    {
        try
        {
            userId = userId ?? (await GetCurrentUser())?.Id;
            UserModel? user = await _baseContext.Set<UserModel>().Where(p => p.Id == userId).SingleOrDefaultAsync();

            if (user == null) 
                throw new Exception("User Not Found");

            List<string> userRights = new();
            foreach (string rightToCheck in rightsToCeck) 
                if (user.Tags.Any(t => t.ToString().Contains(rightToCheck))) 
                    userRights.Add(rightToCheck);

            return userRights;
        }
        catch (Exception ex) { _logger.LogError(ex, GetType().FullName, ex.Message); throw; }
    }
    public async Task CheckUserRight(Guid? userId = null, [CallerMemberName] string? methodName = "", [CallerFilePath] string? filePath = "")
    {
        UserModel? user = await _baseContext.Set<UserModel>().FindAsync(userId ?? (await GetCurrentUser())?.Id);
        if (user?.Tags.Any(t => t.Contains("Dragos") || t.Contains("Role.Admin") || t.Contains($"{Path.GetFileNameWithoutExtension(filePath)}.{methodName}")) == false)
            throw new UnauthorizedAccessException($"User: {user?.Name}, does not have the required right: {Path.GetFileNameWithoutExtension(filePath)}.{methodName}");
    }
    public async Task<string> CriptPassword(string password) => await Task.Run(() => Convert.ToHexString(SHA256.HashData(Encoding.UTF8.GetBytes(password))));
    public async Task<Dictionary<string, List<string>>> GetAllAPIs() => await Task.Run(() =>
        Assembly.GetExecutingAssembly()
                .GetTypes()
                .Where(t => t.Namespace == "Main.Gateway" && t.IsClass && !t.Name.Contains("<") && !t.GetCustomAttributes(typeof(AllowAnonymousAttribute), true).Any())
                .ToDictionary(
                    t => t.Name,
                    t => t.GetMethods(BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static | BindingFlags.FlattenHierarchy)
                            .Where(m => !m.GetCustomAttributes(typeof(AllowAnonymousAttribute), true).Any())
                            .Select(m => m.Name)
                            .Concat(t.BaseType?.GetMethods(BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static)
                                .Where(m => !m.GetCustomAttributes(typeof(AllowAnonymousAttribute), true).Any()) // Exclude base class methods with [AllowAnonymous]
                                .Select(m => m.Name) ?? Enumerable.Empty<string>())
                            .ToList()));
    public async Task<bool> ValidateCredentials(object obj)
    {
        // Check if 'obj' has the 'Email' property
        PropertyInfo? emailProperty = obj.GetType().GetProperty("Email");
        if (emailProperty != null && (emailProperty?.GetValue(obj) == null || !Regex.IsMatch(emailProperty.GetValue(obj)?.ToString()!, _config["RegexValidator:Email"]!)))
            throw new Exception("Invalid email");

        // Check if 'obj' has the 'Username' property
        PropertyInfo? usernameProperty = obj.GetType().GetProperty("Username");
        if (usernameProperty != null && (usernameProperty?.GetValue(obj) == null || usernameProperty.GetValue(obj)?.ToString()?.Length < 6))
            throw new Exception("Invalid username");

        // Check if 'obj' has the 'Password' property
        PropertyInfo? passwordProperty = obj.GetType().GetProperty("Password");
        if (passwordProperty != null && (passwordProperty?.GetValue(obj) == null || passwordProperty.GetValue(obj)?.ToString()?.Length < 6))
            throw new Exception("Invalid password");

        await Task.CompletedTask;
        return true;
    }
}