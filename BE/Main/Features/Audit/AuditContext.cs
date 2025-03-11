using Microsoft.EntityFrameworkCore;

namespace Main.Modules.Audit;

public partial class AuditContext(DbContextOptions<AuditContext> options, IConfiguration config) : DbContext(options)
{
    #region SQL Tables
    public required DbSet<AuditModel> Audit { get; set; }
    #endregion

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("base");
    }
}
