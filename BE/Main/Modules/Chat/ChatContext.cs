using Microsoft.EntityFrameworkCore;

namespace Main.Modules.Chat;

public partial class ChatContext : DbContext
{
    #region Dependency Injection
    private readonly IConfiguration _config;

    public ChatContext
    (
        DbContextOptions<ChatContext> options,
        IConfiguration config
    ) : base(options)
    {
        _config = config;
    }
    #endregion

    #region SQL Tables
    public DbSet<MessageModel> Messages { get; set; }
    #endregion

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("base");
    }
}