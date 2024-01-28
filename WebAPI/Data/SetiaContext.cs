using Microsoft.EntityFrameworkCore;
using Setia.Models;

namespace Setia.Data;

public partial class SetiaContext : DbContext
{
    public SetiaContext()
    {
    }

    public SetiaContext(DbContextOptions<SetiaContext> options)
        : base(options)
    {
    }

    public DbSet<UserModel> Users { get; set; }
    public DbSet<PontajModel> Pontaj { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

}
