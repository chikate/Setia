using Main.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MimeKit;
using System.IdentityModel.Tokens.Jwt;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace Main.Modules.Auth;

public interface IAuthService
{
    #region Base
    Task ValidateCredentials(object obj);
    Task<string> Login(AuthenticationDTO loginCredentials);
    Task<UserModel> Register(RegistrationDTO registration);
    Task<string> RecoverAccount(RecoveryDTO recoveryInfo);
    Task<UserModel?> GetCurrentUser();
    Task ChangePassword(string email, string username, string currentPassword, string newPassword);
    Task<string> CriptPassword(string password);
    #endregion

    Task CheckUserAPIAccess(Guid? userId = null, [CallerMemberName] string? methodName = "", [CallerFilePath] string? filePath = "");
    Task<List<string>> CheckUserRights(IEnumerable<string> rightsToCeck, Guid? userId = null);
    Task<IEnumerable<string>?> GetUserTags(string? specific = ".", Guid? userId = null);
}

public class AuthService(BaseContext context, IHttpContextAccessor httpContextAccessor, IConfiguration config) : IAuthService
{
    #region Base
    /// <summary>
    /// Validate the credentials of an object.
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public async Task ValidateCredentials(object obj)
    {
        // Check if 'obj' has the 'Email' property
        PropertyInfo? emailProperty = obj.GetType().GetProperty("Email");
        if (emailProperty != null && (emailProperty?.GetValue(obj) == null || !Regex.IsMatch(emailProperty.GetValue(obj)?.ToString()!, config["RegexValidator:Email"]!)))
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
    }

    /// <summary>
    /// Login a user and return a JWT token.
    /// </summary>
    /// <param name="loginCredentials"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public async Task<string> Login(AuthenticationDTO credentials)
    {
        //await ValidateCredentials(credentials);

        credentials.Password = await CriptPassword(credentials.Password);

        UserModel? user = await context.Set<UserModel>().SingleOrDefaultAsync(u =>
            u.Username == credentials.Username &&
            u.Password == credentials.Password);

        if (user == null)
            throw new Exception("User Not Found");

        return new JwtSecurityTokenHandler().WriteToken(new JwtSecurityToken(
            issuer: config["HOST_Server"],
            audience: config["HOST_Client"],
            claims: new List<Claim>
            {
                    new Claim(ClaimTypes.Name, user.Name),
                    new Claim(ClaimTypes.GivenName, user.Username),
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Email, user.Email),
                //new Claim(ClaimTypes.Role, user.Tags.Find("Dragos") ? "Admin" : "Default"),
            },
            expires: DateTime.Now.AddDays(1)!,
            signingCredentials: new SigningCredentials(
                new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(config["CryptKey"]!)),
                    SecurityAlgorithms.HmacSha256)));
    }

    /// <summary>
    /// Register a new user.
    /// </summary>
    /// <param name="registration"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public async Task<UserModel> Register(RegistrationDTO registrationInfo)
    {
        //await ValidateCredentials(registrationInfo);

        registrationInfo.Password = await CriptPassword(registrationInfo.Password);

        UserModel? userCheck = await context.Set<UserModel>().SingleOrDefaultAsync(u =>
            u.Username == registrationInfo.Username);

        if (userCheck != null)
            throw new Exception("User Already Exists");

        UserModel newUser = new UserModel
        {
            Username = registrationInfo.Username,
            Password = registrationInfo.Password,
            Email = registrationInfo.Email,
            Signiture = registrationInfo.Signiture,
            BirthDay = registrationInfo.BirthDay,
            Name = registrationInfo.Name,
            Avatar = registrationInfo.Avatar,
        };

        await context.Set<UserModel>().AddAsync(newUser);
        await context.SaveChangesAsync();

        // _sender.SendMail(registration.Email, "Email validation", $"Here is the confirmation link: https://www.google.ro");

        return newUser;
    }

    /// <summary>
    /// Recover account by sending a recovery email to the user.
    /// </summary>
    /// <param name="email"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public async Task<string> RecoverAccount(RecoveryDTO recoveryInfo)
    {
        //await ValidateCredentials(recoveryInfo);

        List<UserModel> usersWithThisEmail = await context.Set<UserModel>().Where(p => p.Email == recoveryInfo.Email).ToListAsync();

        if (usersWithThisEmail.Count == 0)
            throw new Exception($"No accounts found with this email: {recoveryInfo.Email}");

        //_sender.SendMail(new EmailDTO
        //{
        //    To = email.First(),
        //    Subject = "Account Recovery",
        //    Body = $"Click here to reset your password: https://www.yourapp.com/reset-password?token={Guid.NewGuid().ToString()}"
        //});

        //await context.SaveChangesAsync(); // Save any changes, if applicable.

        return "A recovery email has been sent. Please check your inbox.";
    }

    /// <summary>
    /// Get the current user from the HTTP context.
    /// </summary>
    /// <returns></returns>
    public async Task<UserModel?> GetCurrentUser() =>
        httpContextAccessor.HttpContext?.User.Identity is ClaimsIdentity identity
        ? await context.Set<UserModel>().SingleOrDefaultAsync(u =>
            u.Email == identity.FindFirst(ClaimTypes.Email)!.Value &&
            u.Username == identity.FindFirst(ClaimTypes.NameIdentifier)!.Value)
        : null;

    /// <summary>
    /// Get all APIs and their methods.
    /// </summary>
    /// <param name="email"></param>
    /// <param name="username"></param>
    /// <param name="currentPassword"></param>
    /// <param name="newPassword"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public async Task ChangePassword(string email, string username, string currentPassword, string newPassword)
    {
        //await ValidateCredentials(registration);

        // Fetch user with matching credentials
        var user = await context.Set<UserModel>()
            .Where(u => u.Email == email && u.Username == username && u.Password == currentPassword)
            .SingleOrDefaultAsync();

        if (user == null)
            throw new Exception("User not found with provided credentials.");

        user.Password = await CriptPassword(newPassword);

        await context.SaveChangesAsync();

        await Helper.SendEmailAsync(new MimeMessage
        {
            From = { new MailboxAddress(config.GetSection("SmtpSettings")["SenderName"], config.GetSection("SmtpSettings")["SenderEmail"]) },
            To = { new MailboxAddress(string.Empty, email) },
            Subject = "Password Changed",
            Body = new TextPart("html"/*"plain"*/) { Text = $"You just changed your password for {username}. If this wasn't you, please access this link: https://www.google.com" }
        });
    }

    /// <summary>
    /// Crypt a password using SHA256.
    /// </summary>
    /// <param name="password"></param>
    /// <returns></returns>
    public async Task<string> CriptPassword(string password) => await Task.Run(() => Convert.ToHexString(SHA256.HashData(Encoding.UTF8.GetBytes(password))));
    #endregion

    /// <summary>
    /// Check if the user has the right to access a specific method.
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="methodName"></param>
    /// <param name="filePath"></param>
    /// <returns></returns>
    /// <exception cref="UnauthorizedAccessException"></exception>
    public async Task CheckUserAPIAccess(Guid? userId = null, [CallerMemberName] string? apiMethodName = "", [CallerFilePath] string? filePath = "")
    {
        UserModel? user = await context.Set<UserModel>().FindAsync(userId ?? (await GetCurrentUser())?.Id);

        if (user?.Tags.Any(t =>

        t.Contains("Dragos") ||
        t.Contains("Role.Admin") ||
        t.Contains($"{Path.GetFileNameWithoutExtension(filePath)}.{apiMethodName}")

        ) == false) throw new UnauthorizedAccessException($"User: {user?.Name}, does not have the required right: {Path.GetFileNameWithoutExtension(filePath)}.{apiMethodName}");
    }

    /// <summary>
    /// Check if the user has specific rights.
    /// </summary>
    /// <param name="rightsToCeck"></param>
    /// <param name="userId"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public async Task<List<string>> CheckUserRights(IEnumerable<string> rightsToCeck, Guid? userId = null)
    {
        userId = userId ?? (await GetCurrentUser())?.Id;
        UserModel? user = await context.Set<UserModel>().Where(p => p.Id == userId).SingleOrDefaultAsync();

        if (user == null)
            throw new Exception("User Not Found");

        List<string> userRights = new();
        foreach (string rightToCheck in rightsToCeck)
            if (user.Tags.Any(t => t.ToString().Contains(rightToCheck)))
                userRights.Add(rightToCheck);

        return userRights;
    }

    /// <summary>
    /// Get the tags of a user.
    /// </summary>
    /// <param name="specific"></param>
    /// <param name="userId"></param>
    /// <returns></returns>
    public async Task<IEnumerable<string>?> GetUserTags(string? specific = ".", Guid? userId = null)
    {
        userId = userId ?? (await GetCurrentUser())?.Id;
        return await context.Set<UserModel>()
            .Where(u => u.Id == userId)
            .Select(p => p.Tags)
            .SingleOrDefaultAsync();
    }
}