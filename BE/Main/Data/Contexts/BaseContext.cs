using Main.Data.Models.Base;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace Main.Data.Contexts;

public partial class BaseContext(DbContextOptions<BaseContext> options) : DbContext(options)
{
    #region SQL Tables
    public DbSet<AuditModel> Audit { get; set; }
    public DbSet<UserModel> Users { get; set; }
    public DbSet<NotificationModel> Notifications { get; set; }
    #endregion

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("base");

        #region Default Users
        UserModel defaultUser = new UserModel { Tags = ["Dragos", "Admin"], Username = "Dragos", Password = Convert.ToHexString(SHA256.HashData(Encoding.Default.GetBytes("Password"))), Name = "Dragos" };
        modelBuilder.Entity<UserModel>().HasData(defaultUser);
        #endregion

        #region Default Tags
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
        #endregion
    }
}
