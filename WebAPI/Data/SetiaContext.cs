using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using WebAPI.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.UserSecrets;

namespace WebAPI.Data;

public partial class SetiaContext : DbContext
{
    public SetiaContext()
    {
    }

    public SetiaContext(DbContextOptions<SetiaContext> options)
        : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Server=DRAGOS;Database=Setia;Trusted_Connection=True;TrustServerCertificate=True;");

    public DbSet<UserModel> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

}
