﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Setia.Data;

#nullable disable

namespace Setia.Migrations
{
    [DbContext(typeof(SetiaContext))]
    [Migration("20240210092148_Setia")]
    partial class Setia
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Setia.Models.AuditModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("Author_Id")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Details")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Entity")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("ExecutionDate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("Id_Entity")
                        .HasColumnType("int");

                    b.Property<string>("Payload")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("Author_Id");

                    b.ToTable("Audit");
                });

            modelBuilder.Entity("Setia.Models.PontajModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool>("Active")
                        .HasColumnType("bit");

                    b.Property<int?>("Author_Id")
                        .HasColumnType("int");

                    b.Property<DateTime>("BeginTime")
                        .HasColumnType("datetime2");

                    b.Property<bool>("Deleted")
                        .HasColumnType("bit");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("EndTime")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("ExecutionDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("Id_User")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("Author_Id");

                    b.HasIndex("Id_User");

                    b.ToTable("Pontaj");
                });

            modelBuilder.Entity("Setia.Models.RightModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Action")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Active")
                        .HasColumnType("bit");

                    b.Property<int?>("Author_Id")
                        .HasColumnType("int");

                    b.Property<bool>("Deleted")
                        .HasColumnType("bit");

                    b.Property<DateTime>("ExecutionDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Filter")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("Author_Id");

                    b.ToTable("Rights");
                });

            modelBuilder.Entity("Setia.Models.RoleModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool>("Active")
                        .HasColumnType("bit");

                    b.Property<int?>("Author_Id")
                        .HasColumnType("int");

                    b.Property<bool>("Deleted")
                        .HasColumnType("bit");

                    b.Property<DateTime>("ExecutionDate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("InheritsRoleId")
                        .HasColumnType("int");

                    b.Property<int?>("InheritsRole_Id")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Rights")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("Author_Id");

                    b.HasIndex("InheritsRoleId");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("Setia.Models.UserModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool>("Active")
                        .HasColumnType("bit");

                    b.Property<int?>("Author_Id")
                        .HasColumnType("int");

                    b.Property<bool>("Deleted")
                        .HasColumnType("bit");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("ExecutionDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("Author_Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Setia.Models.AuditModel", b =>
                {
                    b.HasOne("Setia.Models.UserModel", "Author")
                        .WithMany()
                        .HasForeignKey("Author_Id");

                    b.Navigation("Author");
                });

            modelBuilder.Entity("Setia.Models.PontajModel", b =>
                {
                    b.HasOne("Setia.Models.UserModel", "Author")
                        .WithMany()
                        .HasForeignKey("Author_Id");

                    b.HasOne("Setia.Models.UserModel", "User")
                        .WithMany()
                        .HasForeignKey("Id_User")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Author");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Setia.Models.RightModel", b =>
                {
                    b.HasOne("Setia.Models.UserModel", "Author")
                        .WithMany()
                        .HasForeignKey("Author_Id");

                    b.Navigation("Author");
                });

            modelBuilder.Entity("Setia.Models.RoleModel", b =>
                {
                    b.HasOne("Setia.Models.UserModel", "Author")
                        .WithMany()
                        .HasForeignKey("Author_Id");

                    b.HasOne("Setia.Models.RoleModel", "InheritsRole")
                        .WithMany()
                        .HasForeignKey("InheritsRoleId");

                    b.Navigation("Author");

                    b.Navigation("InheritsRole");
                });

            modelBuilder.Entity("Setia.Models.UserModel", b =>
                {
                    b.HasOne("Setia.Models.UserModel", "Author")
                        .WithMany()
                        .HasForeignKey("Author_Id");

                    b.Navigation("Author");
                });
#pragma warning restore 612, 618
        }
    }
}
