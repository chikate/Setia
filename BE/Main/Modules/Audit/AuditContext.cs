using Microsoft.EntityFrameworkCore;

namespace Main.Modules.Audit;

public partial class AuditContext : DbContext
{
    #region Dependency Injection
    private readonly IConfiguration _config;

    public AuditContext
    (
        DbContextOptions<AuditContext> options,
        IConfiguration config
    ) : base(options)
    {
        _config = config;
    }
    #endregion

    #region SQL Tables
    public required DbSet<AuditModel> Audit { get; set; }
    #endregion

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("base");
    }
}
