﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Setia.Contexts.Base;

#nullable disable

namespace Base.Migrations
{
    [DbContext(typeof(BaseContext))]
    [Migration("20240421123523_SetiaBase")]
    partial class SetiaBase
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("base")
                .HasAnnotation("ProductVersion", "8.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.HasPostgresExtension(modelBuilder, "ltree");
            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Setia.Models.Base.AuditModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("AuthorId")
                        .HasColumnType("text");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Details")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Entity")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int?>("EntityId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("ExecutionDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Payload")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("AuthorId");

                    b.ToTable("Audit", "base");
                });

            modelBuilder.Entity("Setia.Models.Base.TagModel", b =>
                {
                    b.Property<string>("Tag")
                        .HasColumnType("ltree");

                    b.Property<bool>("Active")
                        .HasColumnType("boolean");

                    b.Property<string>("AuthorId")
                        .HasColumnType("text");

                    b.Property<string>("Comments")
                        .HasColumnType("text");

                    b.Property<bool>("Deleted")
                        .HasColumnType("boolean");

                    b.Property<DateTime>("ExecutionDate")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Tag");

                    b.HasIndex("AuthorId");

                    b.ToTable("Tags", "base");

                    b.HasData(
                        new
                        {
                            Tag = "Tags.Role.Admin",
                            Active = true,
                            Deleted = false,
                            ExecutionDate = new DateTime(2024, 4, 21, 15, 35, 23, 353, DateTimeKind.Utc).AddTicks(5726)
                        },
                        new
                        {
                            Tag = "Tags.CRUD1.Get",
                            Active = true,
                            Deleted = false,
                            ExecutionDate = new DateTime(2024, 4, 21, 15, 35, 23, 353, DateTimeKind.Utc).AddTicks(6479)
                        },
                        new
                        {
                            Tag = "Tags.CRUD1.Add",
                            Active = true,
                            Deleted = false,
                            ExecutionDate = new DateTime(2024, 4, 21, 15, 35, 23, 353, DateTimeKind.Utc).AddTicks(6530)
                        },
                        new
                        {
                            Tag = "Tags.CRUD1.Update",
                            Active = true,
                            Deleted = false,
                            ExecutionDate = new DateTime(2024, 4, 21, 15, 35, 23, 353, DateTimeKind.Utc).AddTicks(6578)
                        },
                        new
                        {
                            Tag = "Tags.CRUD1.Delete",
                            Active = true,
                            Deleted = false,
                            ExecutionDate = new DateTime(2024, 4, 21, 15, 35, 23, 353, DateTimeKind.Utc).AddTicks(6604)
                        },
                        new
                        {
                            Tag = "Tags.Helper.Upload",
                            Active = true,
                            Deleted = false,
                            ExecutionDate = new DateTime(2024, 4, 21, 15, 35, 23, 353, DateTimeKind.Utc).AddTicks(7007)
                        },
                        new
                        {
                            Tag = "Tags.Helper.GetUserTags",
                            Active = true,
                            Deleted = false,
                            ExecutionDate = new DateTime(2024, 4, 21, 15, 35, 23, 353, DateTimeKind.Utc).AddTicks(7032)
                        });
                });

            modelBuilder.Entity("Setia.Models.Base.UserModel", b =>
                {
                    b.Property<string>("Username")
                        .HasColumnType("text");

                    b.Property<bool>("Active")
                        .HasColumnType("boolean");

                    b.Property<string>("AuthorId")
                        .HasColumnType("text");

                    b.Property<bool>("Deleted")
                        .HasColumnType("boolean");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime?>("EmailVerifiedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("ExecutionDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Username");

                    b.HasIndex("AuthorId");

                    b.ToTable("Users", "base");

                    b.HasData(
                        new
                        {
                            Username = "testUser",
                            Active = true,
                            Deleted = false,
                            Email = "",
                            ExecutionDate = new DateTime(2024, 4, 21, 15, 35, 23, 353, DateTimeKind.Utc).AddTicks(5537),
                            Name = "Test Name",
                            Password = "testPassword"
                        });
                });

            modelBuilder.Entity("Setia.Models.Base.UserTagModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<bool>("Active")
                        .HasColumnType("boolean");

                    b.Property<string>("AuthorId")
                        .HasColumnType("text");

                    b.Property<bool>("Deleted")
                        .HasColumnType("boolean");

                    b.Property<DateTime>("ExecutionDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Tag")
                        .IsRequired()
                        .HasColumnType("ltree");

                    b.Property<string>("User")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("AuthorId");

                    b.ToTable("UserTags", "base");

                    b.HasData(
                        new
                        {
                            Id = new Guid("e6bc5ebb-58dc-42f4-959c-42c2481632b0"),
                            Active = true,
                            Deleted = false,
                            ExecutionDate = new DateTime(2024, 4, 21, 15, 35, 23, 353, DateTimeKind.Utc).AddTicks(5705),
                            Tag = "Role.Admin",
                            User = "testUser"
                        },
                        new
                        {
                            Id = new Guid("daf97eb0-47dd-4afa-b7ff-bd2d203bf034"),
                            Active = true,
                            Deleted = false,
                            ExecutionDate = new DateTime(2024, 4, 21, 15, 35, 23, 353, DateTimeKind.Utc).AddTicks(5715),
                            Tag = "Dragos",
                            User = "testUser"
                        });
                });

            modelBuilder.Entity("Setia.Models.Base.AuditModel", b =>
                {
                    b.HasOne("Setia.Models.Base.UserModel", "Author")
                        .WithMany()
                        .HasForeignKey("AuthorId");

                    b.Navigation("Author");
                });

            modelBuilder.Entity("Setia.Models.Base.TagModel", b =>
                {
                    b.HasOne("Setia.Models.Base.UserModel", "Author")
                        .WithMany()
                        .HasForeignKey("AuthorId");

                    b.Navigation("Author");
                });

            modelBuilder.Entity("Setia.Models.Base.UserModel", b =>
                {
                    b.HasOne("Setia.Models.Base.UserModel", "Author")
                        .WithMany()
                        .HasForeignKey("AuthorId");

                    b.Navigation("Author");
                });

            modelBuilder.Entity("Setia.Models.Base.UserTagModel", b =>
                {
                    b.HasOne("Setia.Models.Base.UserModel", "Author")
                        .WithMany()
                        .HasForeignKey("AuthorId");

                    b.Navigation("Author");
                });
#pragma warning restore 612, 618
        }
    }
}
