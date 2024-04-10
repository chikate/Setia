using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Setia.Models;
using System.Reflection;

namespace Base;

public partial class BaseContext(DbContextOptions<BaseContext> options) : DbContext(options)
{
    public DbSet<PontajModel> Pontaj { get; set; }
    public DbSet<AuditModel> Audit { get; set; }
    public DbSet<UserModel> Users { get; set; }
    public DbSet<TagModel> Tags { get; set; }
    public DbSet<UserTagModel> UserTags { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //modelBuilder.HasDefaultSchema("base");

        modelBuilder.Entity<UserModel>().HasData(
            new UserModel() { Username = "testUsername", Password = "testPassword", Name = "Test Name" }
        );

        // Tags
        Guid SetTag(string name, Guid? parentTagGuid = null)
        {
            Guid tagGuid = Guid.NewGuid();
            modelBuilder.Entity<TagModel>().HasData(
                new TagModel() { ParentTagId = parentTagGuid, Id = tagGuid, Name = name }
            );
            return tagGuid;
        }

        foreach (var controller in Assembly.GetExecutingAssembly().GetTypes().Where(type => typeof(ControllerBase).IsAssignableFrom(type)))
        {
            if (!controller.IsDefined(typeof(AllowAnonymousAttribute), inherit: true))
            {
                Guid controllerTagId = SetTag(controller.Name.Replace("Controller", ""));
                foreach (var method in controller.GetMethods(BindingFlags.Public | BindingFlags.Instance))
                {
                    if (method.DeclaringType == controller) SetTag(method.Name, controllerTagId);
                }
            }
        }
    }
}
