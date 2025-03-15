using Microsoft.EntityFrameworkCore;

namespace Main.Modules.Sessions;

public partial class SessionsContext(DbContextOptions<SessionsContext> options, IConfiguration config) : DbContext(options)
{
    #region SQL Tables
    //public DbSet<MessageModel> Data { get; set; }
    #endregion

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("base");
    }
}