using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Setia.Models.Base;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace Setia.Contexts.Base;

public partial class BaseContext(DbContextOptions<BaseContext> options) : DbContext(options)
{
    public DbSet<AuditModel> Audit { get; set; }
    public DbSet<UserModel> Users { get; set; }
    public DbSet<TagModel> Tags { get; set; }
    public DbSet<UserTagModel> UserTags { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("base");

        #region Default Users
        UserModel defaultUser = new UserModel { Username = "testUser", Password = Convert.ToHexString(SHA256.HashData(Encoding.Default.GetBytes("testPassword"))), Name = "Test Name" };

        modelBuilder.Entity<UserModel>().HasData(defaultUser);

        modelBuilder.Entity<UserTagModel>().HasData(
            new UserTagModel { Username = defaultUser.Username, TagId = "Role.Admin" }
        );

        modelBuilder.Entity<UserTagModel>().HasData(
            new UserTagModel { Username = defaultUser.Username, TagId = "Dragos" }
        );
        #endregion

        #region Default Tags
        modelBuilder.Entity<TagModel>().HasData(new TagModel { Tag = $"Role.Admin" });
        foreach (var controller in Assembly.GetExecutingAssembly().GetTypes().Where(type => typeof(ControllerBase).IsAssignableFrom(type)))
        {
            if (!controller.IsDefined(typeof(AllowAnonymousAttribute), inherit: true))
            {
                string controllerName = new Regex("[^a-zA-Z0-9 -]").Replace(controller.Name.Replace("Controller", ""), "");
                foreach (var method in controller.GetMethods(BindingFlags.Public | BindingFlags.Instance))
                {
                    if (method.DeclaringType == controller)
                    {
                        modelBuilder.Entity<TagModel>().HasData(
                            new TagModel { Tag = $"{controllerName}.{new Regex("[^a-zA-Z0-9 -]").Replace(method.Name, "")}" }
                        );
                    }
                }
            }
            #endregion
        }
    }
}
