using Main.Data.Contexts;
using Main.Modules.Chat;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Main.Modules.Adm;

public interface IAdmService
{
    Task<Dictionary<string, List<string>>> GetAllAPIs();
}

public class AdmService : IAdmService
{
    #region Dependency Injection 
    private readonly AdmContext _admContext;
    private readonly ILogger<AdmService> _logger;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IConfiguration _config;

    public AdmService
    (
        AdmContext admContext,
        ILogger<AdmService> logger,
        IHttpContextAccessor httpContextAccessor,
        IConfiguration config
    )
    {
        _admContext = admContext;
        _logger = logger;
        _httpContextAccessor = httpContextAccessor;
        _config = config;
    }
    #endregion

    public async Task<Dictionary<string, List<string>>> GetAllAPIs() => await Task.Run(() =>
        Assembly.GetExecutingAssembly()
                .GetTypes()
                .Where(t => t.Namespace == "Main.Controller" && t.IsClass && !t.Name.Contains("<") && !t.GetCustomAttributes(typeof(AllowAnonymousAttribute), true).Any())
                .ToDictionary(
                    t => t.Name,
                    t => t.GetMethods(BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static | BindingFlags.FlattenHierarchy)
                            .Where(m => !m.GetCustomAttributes(typeof(AllowAnonymousAttribute), true).Any())
                            .Select(m => m.Name)
                            .Concat(t.BaseType?.GetMethods(BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static)
                                .Where(m => !m.GetCustomAttributes(typeof(AllowAnonymousAttribute), true).Any()) // Exclude base class methods with [AllowAnonymous]
                                .Select(m => m.Name) ?? Enumerable.Empty<string>())
                            .ToList()));
}