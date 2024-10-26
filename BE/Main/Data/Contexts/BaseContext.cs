#pragma warning disable CS8604, CS8601

using Main.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Main.Data.Contexts;

public partial class BaseContext : DbContext
{
    #region Dependency Injection
    private readonly IConfiguration _config;
    //private readonly IAuditService _audit;
    //private readonly IAuthService _auth;

    public BaseContext
    (
        DbContextOptions<BaseContext> options,
        IConfiguration config
    //IAuditService audit,
    //IAuthService auth
    ) : base(options)
    {
        _config = config;
        //_audit = audit;
        //_auth = auth;
    }
    #endregion

    #region SQL Tables
    public DbSet<AuditModel> Audit { get; set; }
    public DbSet<UserModel> Users { get; set; }
    public DbSet<NotificationModel> Notifications { get; set; }
    public DbSet<SettingsModel> Settings { get; set; }
    #endregion

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("base");

        #region Default Users
        foreach (UserModel defaultUser in new List<UserModel> {
        new UserModel
        {
            Name = _config["AdminAccount:Username"],
            Username = _config["AdminAccount:Username"],
            Password = _config["AdminAccount:Password"], //_auth.CriptPassword(_config["AdminAccount:Password"]),
            Tags = [
                _config["AdminAccount:Username"],
                "Admin"
            ]
        },
        new UserModel
        {
            Name = _config["AdminAccount:Username"] + "ADM",
            Username = _config["AdminAccount:Username"] + "ADM",
            Password = _config["AdminAccount:Password"], //_auth.CriptPassword(_config["AdminAccount:Password"]),
            Tags = [
                _config["AdminAccount:Username"] + "ADM",
                "Admin"
            ]
        }}) modelBuilder.Entity<UserModel>().HasData(defaultUser);
        #endregion

        #region Default Tags
        //modelBuilder.Entity<TagModel>().HasData(new TagModel { Tag = $"Role.Admin" });
        //foreach (var controller in Assembly.GetExecutingAssembly().GetTypes().Where(type => typeof(ControllerBase).IsAssignableFrom(type)))
        //{
        //    if (!controller.IsDefined(typeof(AllowAnonymousAttribute), inherit: true))
        //    {
        //        string controllerName = new Regex("[^a-zA-Z0-9 -]").Replace(controller.Name.Replace("Controller", ""), "");
        //        foreach (var method in controller.GetMethods(BindingFlags.Public | BindingFlags.Instance))
        //        {
        //            if (method.DeclaringType == controller)
        //            {
        //                modelBuilder.Entity<TagModel>().HasData(
        //                    new TagModel { Tag = $"Controller.{controllerName}.{new Regex("[^a-zA-Z0-9 -]").Replace(method.Name, "")}" }
        //                );
        //            }
        //        }
        //    }
        //}
        #endregion

        #region Default Settings
        int settingIndex = 1;
        foreach (SettingsModel defaultSetting in new List<SettingsModel> {
        new SettingsModel
        {
            Id = settingIndex++,
            Key = "Client Name",
            Value = "DragosClient",
        },
        new SettingsModel
        {
            Id = settingIndex++,
            Key = "Client BE URL",
            Value = _config["Server"],
        },
        new SettingsModel
        {
            Id = settingIndex++,
            Key = "Client Environment",
            Value = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT"),
        },
        new SettingsModel
        {
            Id = settingIndex++,
            Key = "Client Colors",
            Value = "2f42a6,845dbb",
        }}) { modelBuilder.Entity<SettingsModel>().HasData(defaultSetting); }
        #endregion
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        foreach (var entry in ChangeTracker.Entries()) //.Where(e => e.State == EntityState.Added || e.State == EntityState.Modified))
        {
            //PropertyInfo? passwordProperty = model.GetType().GetProperty("Password");
            //passwordProperty?.SetValue(model, _auth.CriptPassword((string)passwordProperty?.GetValue(model)));
            entry.Property("ExecutionDate").CurrentValue = DateTime.Now;
            entry.Property("AuthorId").CurrentValue = 1;//(await _auth.GetCurrentUser())?.Id;
        }
        return await base.SaveChangesAsync(cancellationToken);
    }
}
