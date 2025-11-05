using Main.Data.Models;
using Main.Modules.Audit;
using Main.Modules.Auth;
using Microsoft.EntityFrameworkCore;

namespace Main.Data.Context;

public partial class BaseContext(DbContextOptions<BaseContext> options/*, IConfiguration config*/) : DbContext(options)
{
    public required DbSet<AuditModel> Audit { get; set; }
    public required DbSet<GroupModel> Groups { get; set; }
    public required DbSet<NotificationModel> Notifications { get; set; }
    public required DbSet<PostModel> Posts { get; set; }
    public required DbSet<QuestionModel> Questions { get; set; }
    public required DbSet<UserModel> Users { get; set; }
    public required DbSet<LinkModel> Links { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("base");

        modelBuilder.Entity<LinkModel>().HasIndex(l => new { l.SourceType, l.SourceId });
        modelBuilder.Entity<LinkModel>().HasIndex(l => new { l.TargetType, l.TargetId });

        //#region Default Users
        //new List<UserModel> {
        //    new UserModel
        //    {
        //        Name = config["AdminAccount:Username"]!,
        //        Username = config["AdminAccount:Username"]!,
        //        Password = Convert.ToHexString(SHA256.HashData(Encoding.UTF8.GetBytes(config["AdminAccount:Password"]!))),
        //        Tags = [
        //            config["AdminAccount:Username"]!,
        //            "Admin"
        //        ]
        //    }
        //}.ForEach(defaultUser => modelBuilder.Entity<UserModel>().HasData(defaultUser));
        //#endregion

        //#region Default Settings
        //int settingIndex = 1;
        //new List<SettingsModel> {
        //    new SettingsModel
        //    {
        //        Id = settingIndex++,
        //        Key = "Client Name",
        //        Value = "Client",
        //    },
        //    new SettingsModel
        //    {
        //        Id = settingIndex++,
        //        Key = "Client Colors",
        //        Value = "2f42a6,845dbb",
        //    }
        //}.ForEach(defaultSetting => modelBuilder.Entity<SettingsModel>().HasData(defaultSetting));
        //#endregion
    }
}
