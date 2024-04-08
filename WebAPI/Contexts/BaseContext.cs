using Microsoft.EntityFrameworkCore;
using Setia.Models;

namespace Setia.Data;

public partial class BaseContext : DbContext
{
    public BaseContext(DbContextOptions<BaseContext> options) : base(options) { }

    public DbSet<PontajModel> Pontaj { get; set; }
    public DbSet<AuditModel> Audit { get; set; }
    public DbSet<UserModel> Users { get; set; }
    public DbSet<TagModel> Roles { get; set; }
}
