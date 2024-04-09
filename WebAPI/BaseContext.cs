using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Setia.Models;
using System.Reflection;

namespace Base;

public partial class BaseContext : DbContext
{
    public BaseContext(DbContextOptions<BaseContext> options) : base(options) { }

    public DbSet<PontajModel> Pontaj { get; set; }
    public DbSet<AuditModel> Audit { get; set; }
    public DbSet<UserModel> Users { get; set; }
    public DbSet<TagModel> Tags { get; set; }

    protected override async void OnModelCreating(ModelBuilder modelBuilder)
    {
        //modelBuilder.HasDefaultSchema("base");

        Guid SetTag(string name, Guid? parentGuid = null)
        {
            Guid myGuid = Guid.NewGuid();
            modelBuilder.Entity<TagModel>().HasData(
                new TagModel() { ParentTagGuid = parentGuid, Guid = myGuid, Name = name }
            );
            return myGuid;
        }

        foreach (var controller in Assembly.GetExecutingAssembly().GetTypes().Where(type => typeof(ControllerBase).IsAssignableFrom(type)))
        {
            if (!controller.IsDefined(typeof(AllowAnonymousAttribute), inherit: true))
            {
                Guid controllerGuid = SetTag(controller.Name.Replace("Controller", ""));
                foreach (var method in controller.GetMethods(BindingFlags.Public | BindingFlags.Instance))
                {
                    if (method.DeclaringType == controller) SetTag(method.Name, controllerGuid);
                }
            }
        }
    }
}
