using Main.Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace Main.Data.Contexts;

public partial class BaseContext : DbContext
{
    #region Dependency Injection
    private readonly IConfiguration _config;

    public BaseContext(DbContextOptions<BaseContext> options, IConfiguration config) : base(options)
    {
        _config = config;
    }
    #endregion

    #region SQL Tables
    public DbSet<AuditModel> Audit { get; set; }
    public DbSet<UserModel> Users { get; set; }
    public DbSet<NotificationModel> Notifications { get; set; }
    public DbSet<SettingsModel> Settings { get; set; }
    #endregion

    private static string HashPassword(string plainTextPassword) => Convert.ToHexString(SHA256.HashData(Encoding.UTF8.GetBytes(plainTextPassword)));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("base");

//        #region Default Users
//#pragma warning disable CS8604, CS8601 
//        foreach (UserModel defaultUser in new List<UserModel> {
//        new UserModel
//        {
//            Name = _config["AdminAccount:Username"],
//            Username = _config["AdminAccount:Username"],
//            Password = HashPassword(_config["AdminAccount:Password"]),
//            Tags = [
//                _config["AdminAccount:Username"],
//                "Admin"
//            ]
//        },
//        new UserModel
//        {
//            Name = _config["AdminAccount:Username"] + "2",
//            Username = _config["AdminAccount:Username"] + "2",
//            Password = HashPassword(_config["AdminAccount:Password"]),
//            Tags = [
//                _config["AdminAccount:Username"] + "2",
//                "Admin"
//            ]
//        }}) modelBuilder.Entity<UserModel>().HasData(defaultUser);
//#pragma warning restore CS8604, CS8601
//        #endregion

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
#pragma warning disable CS8604, CS8601
        int defaultSettingIndex = 1;
        foreach (SettingsModel defaultSetting in new List<SettingsModel> {
        new SettingsModel
        {
            Key = "Client Name",
            Value = "DragosClient",
        },
        new SettingsModel
        {
            Key = "Client BE URL",
            Value = _config["Server"],
        },
        new SettingsModel
        {
            Key = "Client Environment",
            Value = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT"),
        },
        new SettingsModel
        {
            Key = "Client Colors",
            Value = "2f42a6,845dbb",
        }})
        {
            defaultSetting.Id = defaultSettingIndex;
            defaultSettingIndex++;
            modelBuilder.Entity<SettingsModel>().HasData(defaultSetting);
        }
#pragma warning restore CS8604, CS8601
        #endregion
    }
}
