using Microsoft.EntityFrameworkCore;

namespace Main.Modules.Sessions;

public partial class SessionsContext : DbContext
{
    #region Dependency Injection
    private readonly IConfiguration _config;

    public SessionsContext
    (
        DbContextOptions<SessionsContext> options,
        IConfiguration config
    ) : base(options)
    {
        _config = config;
    }
    #endregion

    #region SQL Tables
    //public DbSet<MessageModel> Data { get; set; }
    #endregion

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("base");
    }
}