using Microsoft.EntityFrameworkCore;

namespace Main.Modules.Drive;

public partial class DriveContext(DbContextOptions<DriveContext> options, IConfiguration config) : DbContext(options)
{
    public required DbSet<FileModel> Files { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("drive");
    }
}
