using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace Main.Modules.Auth;

public partial class AuthContext : DbContext
{
    #region Dependency Injection
    private readonly IConfiguration _config;

    public AuthContext
    (
        DbContextOptions<AuthContext> options,
        IConfiguration config
    ) : base(options)
    {
        _config = config;
    }
    #endregion

    #region SQL Tables
    public required DbSet<UserModel> Users { get; set; }
    #endregion

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("base");

        #region Default Users
        new List<UserModel> {
            new UserModel
            {
                Name = _config["AdminAccount:Username"]!,
                Username = _config["AdminAccount:Username"]!,
                Password = Convert.ToHexString(SHA256.HashData(Encoding.UTF8.GetBytes(_config["AdminAccount:Password"]!))),
                Tags = [
                    _config["AdminAccount:Username"]!,
                    "Admin"
                ]
            }
        }.ForEach(defaultUser => modelBuilder.Entity<UserModel>().HasData(defaultUser));
        #endregion
    }

    //modelBuilder.Entity<TagModel>().HasData(new TagModel { Tag = $"Role.Admin" });
    //foreach (var controller in Assembly.GetExecutingAssembly().GetTypes().Where(type => typeof(ControllerBase).IsAssignableFrom(type)))
    //{
    //    if (!controller.IsDefined(typeof(AllowAnonymousAttribute), inherit: true))
    //    {
    //        string controllerName = new Regex("[^a-zA-Z0-9 -]").Replace(controller.Name.Replace("Controller", ""), "");
    //        foreach (var method in controller.GetMethods(BindingFlags.Public | BindingFlags.Instance))
    //        {
    //            if (method.DeclaringType == controller)
    //            {
    //                modelBuilder.Entity<TagModel>().HasData(
    //                    new TagModel { Tag = $"Controller.{controllerName}.{new Regex("[^a-zA-Z0-9 -]").Replace(method.Name, "")}" }
    //                );
    //            }
    //        }
    //    }
    //}

    //public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    //{
    //    foreach (EntityEntry entry in ChangeTracker.Entries()) //.Where(e => e.State == EntityState.Added || e.State == EntityState.Modified))
    //    {
    //        entry.Property("ExecutionDate").CurrentValue = DateTime.Now;
    //        //entry.Property("AuthorId").CurrentValue = Guid.NewGuid();
    //    }
    //    return await base.SaveChangesAsync(cancellationToken);
    //}
}
