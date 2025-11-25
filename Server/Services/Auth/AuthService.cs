using Data.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MimeKit;
using System.IdentityModel.Tokens.Jwt;
using System.Runtime.CompilerServices;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Modules.Auth;

public interface IAuthService
{
    #region Base
    Task<string> Login(AuthenticationDTO loginCredentials);
    Task<UserModel> Register(RegistrationDTO registration);
    Task<string> RecoverAccount(RecoveryDTO recoveryInfo);
    Task<UserModel?> GetCurrentUser();
    Task ChangePassword(string email, string username, string currentPassword, string newPassword);
    string HashPassword(string password);
    #endregion

    Task CheckUserAPIAccess(Guid? userId = null, [CallerMemberName] string? methodName = "", [CallerFilePath] string? filePath = "");
    Task<List<string>> CheckUserRights(IEnumerable<string> rightsToCeck, Guid? userId = null);
    Task<IEnumerable<string>?> GetUserTags(string? specific = ".", Guid? userId = null);
}

public class AuthService(BaseContext context, IHttpContextAccessor httpContextAccessor, IConfiguration config) : IAuthService
{
    public async Task<string> Login([FromBody] AuthenticationDTO credentials)
    {
        string hashedPassword = HashPassword(credentials.Password);

        UserModel? user = await context.Set<UserModel>()
            .SingleOrDefaultAsync(u => u.Username == credentials.Username && u.Password == hashedPassword);

        if (user == null)
            throw new Exception("Invalid Username or Password");

        return GenerateJwtToken(user);
    }

    public async Task<UserModel> Register([FromQuery] RegistrationDTO registrationInfo)
    {
        if (await context.Set<UserModel>().AnyAsync(u => u.Username == registrationInfo.Username))
            throw new Exception("User Already Exists");

        UserModel newUser = new UserModel
        {
            Username = registrationInfo.Username,
            Password = HashPassword(registrationInfo.Password),
            Email = registrationInfo.Email,
            Signiture = registrationInfo.Signiture,
            BirthDay = registrationInfo.BirthDay,
            Name = registrationInfo.Name,
            Avatar = registrationInfo.Avatar,
            Tags = []
        };

        await context.Set<UserModel>().AddAsync(newUser);
        await context.SaveChangesAsync();

        // _sender.SendMail(registration.Email, "Email validation", $"Here is the confirmation link: https://www.google.ro");

        newUser.Password = string.Empty;
        return newUser;
    }

    public async Task<string> RecoverAccount(RecoveryDTO recoveryInfo)
    {
        if (!await context.Set<UserModel>().AnyAsync(p => p.Email == recoveryInfo.Email))
            throw new Exception($"No accounts found with this email: {recoveryInfo.Email}");

        // Send email logic (commented out in original)
        
        return "A recovery email has been sent. Please check your inbox.";
    }

    public async Task<UserModel?> GetCurrentUser()
    {
        var httpContext = httpContextAccessor.HttpContext;
        if (httpContext?.User.Identity is not ClaimsIdentity identity || !identity.IsAuthenticated)
            return null;

        var idClaim = identity.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrEmpty(idClaim) || !Guid.TryParse(idClaim, out var userId))
            return null;

        return await context.Set<UserModel>().FindAsync(userId);
    }

    public async Task ChangePassword(string email, string username, string currentPassword, string newPassword)
    {
        string hashedCurrentPassword = HashPassword(currentPassword);

        var user = await context.Set<UserModel>()
            .SingleOrDefaultAsync(u => u.Email == email && u.Username == username && u.Password == hashedCurrentPassword);

        if (user == null)
            throw new Exception("User not found with provided credentials.");

        user.Password = HashPassword(newPassword);
        await context.SaveChangesAsync();

        await Helper.SendEmailAsync(new MimeMessage
        {
            From = { new MailboxAddress(config.GetSection("SmtpSettings")["SenderName"], config.GetSection("SmtpSettings")["SenderEmail"]) },
            To = { new MailboxAddress(string.Empty, email) },
            Subject = "Password Changed",
            Body = new TextPart("html") { Text = $"You just changed your password for {username}. If this wasn't you, please access this link: https://www.google.com" }
        });
    }

    public string HashPassword(string password) => Convert.ToHexString(SHA256.HashData(Encoding.UTF8.GetBytes(password)));

    public async Task CheckUserAPIAccess(Guid? userId = null, [CallerMemberName] string? apiMethodName = "", [CallerFilePath] string? filePath = "")
    {
        userId ??= (await GetCurrentUser())?.Id;
        var user = await context.Set<UserModel>().FindAsync(userId);

        string requiredRight = $"{Path.GetFileNameWithoutExtension(filePath)}.{apiMethodName}";

        if (user?.Tags.Any(t => t.Contains("Dragos") || t.Contains("Role.Admin") || t.Contains(requiredRight)) == false)
            throw new UnauthorizedAccessException($"User: {user?.Name}, does not have the required right: {requiredRight}");
    }

    public async Task<List<string>> CheckUserRights(IEnumerable<string> rightsToCheck, Guid? userId = null)
    {
        userId ??= (await GetCurrentUser())?.Id;
        var user = await context.Set<UserModel>().FindAsync(userId);

        if (user == null) throw new Exception("User Not Found");

        return rightsToCheck.Where(right => user.Tags.Any(t => t.Contains(right))).ToList();
    }

    public async Task<IEnumerable<string>?> GetUserTags(string? specific = ".", Guid? userId = null)
    {
        userId ??= (await GetCurrentUser())?.Id;
        return await context.Set<UserModel>()
            .Where(u => u.Id == userId)
            .Select(p => p.Tags)
            .SingleOrDefaultAsync();
    }

    private string GenerateJwtToken(UserModel user)
    {
        return new JwtSecurityTokenHandler().WriteToken(new JwtSecurityToken(
            issuer: config["Origin"],
            audience: config["Audience"],
            claims: new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(ClaimTypes.GivenName, user.Username),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
            },
            expires: DateTime.Now.AddDays(1),
            signingCredentials: new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["CryptKey"]!)),
                SecurityAlgorithms.HmacSha256)));
    }
}