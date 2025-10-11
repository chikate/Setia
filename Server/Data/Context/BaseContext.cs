using Main.Data.Models;
using Main.Modules.Adm;
using Main.Modules.Audit;
using Main.Modules.Auth;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Main.Data.Context;

public partial class BaseContext(DbContextOptions<BaseContext> options/*, IConfiguration config*/) : DbContext(options)
{
    #region SQL Tables
    public required DbSet<UserModel> Users { get; set; }
    public required DbSet<SettingsModel> Settings { get; set; }
    public required DbSet<AuditModel> Audit { get; set; }
    public required DbSet<MessageModel> Posts { get; set; }
    #endregion

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("base");

        modelBuilder.Entity<MessageModel>(builder =>
        {
            builder.HasKey(p => p.Id);
        });

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
