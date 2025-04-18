using Microsoft.EntityFrameworkCore;

namespace Main.Modules.Chat;

public partial class ChatContext(DbContextOptions<ChatContext> options, IConfiguration config) : DbContext(options)
{
    #region SQL Tables
    public DbSet<MessageModel> Messages { get; set; }
    #endregion

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("chat");
    }
}