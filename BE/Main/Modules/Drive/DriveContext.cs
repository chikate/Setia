using Microsoft.EntityFrameworkCore;

namespace Main.Modules.Drive;

public partial class DriveContext : DbContext
{
    #region Dependency Injection
    private readonly IConfiguration _config;

    public DriveContext
    (
        DbContextOptions<DriveContext> options,
        IConfiguration config
    ) : base(options)
    {
        _config = config;
    }
    #endregion

    #region SQL Tables
    public required DbSet<FileModel> Files { get; set; }
    #endregion

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("drive");
    }
}
