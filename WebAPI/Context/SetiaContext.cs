using Microsoft.EntityFrameworkCore;
using Setia.Models;

namespace Setia.Data;

public partial class SetiaContext : DbContext
{
    public SetiaContext(DbContextOptions<SetiaContext> options) : base(options) { }

    public DbSet<PontajModel> Pontaj { get; set; }
    public DbSet<AuditModel> Audit { get; set; }
    public DbSet<UserModel> Users { get; set; }
    public DbSet<RoleModel> Roles { get; set; }
}
