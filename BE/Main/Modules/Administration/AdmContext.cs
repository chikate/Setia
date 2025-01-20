using Microsoft.EntityFrameworkCore;

namespace Main.Modules.Adm;

public partial class AdmContext : DbContext
{
    #region Dependency Injection
    private readonly IConfiguration _config;

    public AdmContext
    (
        DbContextOptions<AdmContext> options,
        IConfiguration config
    ) : base(options)
    {
        _config = config;
    }
    #endregion

    #region SQL Tables
    public required DbSet<NotificationModel> Notifications { get; set; }
    public required DbSet<SettingsModel> Settings { get; set; }
    #endregion

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("adm");

        #region Default Settings
        int settingIndex = 1;
        new List<SettingsModel> {
            new SettingsModel
            {
                Id = settingIndex++,
                Key = "Client Name",
                Value = "Client",
            },
            new SettingsModel
            {
                Id = settingIndex++,
                Key = "Client Colors",
                Value = "2f42a6,845dbb",
            }
        }.ForEach(defaultSetting => modelBuilder.Entity<SettingsModel>().HasData(defaultSetting));
        #endregion
    }
}
