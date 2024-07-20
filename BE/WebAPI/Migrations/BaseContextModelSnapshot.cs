﻿// <auto-generated />
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Setia.Contexts.Base;

#nullable disable

namespace Base.Migrations
{
    [DbContext(typeof(BaseContext))]
    partial class BaseContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("base")
                .HasAnnotation("ProductVersion", "8.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.HasPostgresExtension(modelBuilder, "ltree");
            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Setia.Models.Base.AuditModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<Guid?>("AuthorId")
                        .HasColumnType("uuid");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<string>("Details")
                        .HasColumnType("text");

                    b.Property<string>("Entity")
                        .HasColumnType("text");

                    b.Property<string>("EntityId")
                        .HasColumnType("text");

                    b.Property<DateTime>("ExecutionDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Payload")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Audit", "base");
                });

            modelBuilder.Entity("Setia.Models.Base.NotificationModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid?>("AuthorId")
                        .HasColumnType("uuid");

                    b.Property<string>("Comment")
                        .HasColumnType("text");

                    b.Property<DateTime>("ExecutionDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Icon")
                        .HasColumnType("text");

                    b.Property<List<string>>("Tags")
                        .HasColumnType("text[]");

                    b.Property<string>("Title")
                        .HasColumnType("text");

                    b.Property<Guid?>("UserDataId")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("UserDataId");

                    b.ToTable("Notifications", "base");
                });

            modelBuilder.Entity("Setia.Models.Base.TagModel", b =>
                {
                    b.Property<string>("Tag")
                        .HasColumnType("ltree");

                    b.Property<Guid?>("AuthorId")
                        .HasColumnType("uuid");

                    b.Property<string>("Comments")
                        .HasColumnType("text");

                    b.Property<DateTime>("ExecutionDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<List<string>>("Tags")
                        .HasColumnType("text[]");

                    b.HasKey("Tag");

                    b.ToTable("Tags", "base");

                    b.HasData(
                        new
                        {
                            Tag = "Role.Admin",
                            ExecutionDate = new DateTime(2024, 7, 13, 15, 7, 38, 449, DateTimeKind.Utc).AddTicks(7428)
                        },
                        new
                        {
                            Tag = "Controller.CRUD1.Get",
                            ExecutionDate = new DateTime(2024, 7, 13, 15, 7, 38, 449, DateTimeKind.Utc).AddTicks(8121)
                        },
                        new
                        {
                            Tag = "Controller.CRUD1.Add",
                            ExecutionDate = new DateTime(2024, 7, 13, 15, 7, 38, 449, DateTimeKind.Utc).AddTicks(8162)
                        },
                        new
                        {
                            Tag = "Controller.CRUD1.Update",
                            ExecutionDate = new DateTime(2024, 7, 13, 15, 7, 38, 449, DateTimeKind.Utc).AddTicks(8209)
                        },
                        new
                        {
                            Tag = "Controller.CRUD1.Delete",
                            ExecutionDate = new DateTime(2024, 7, 13, 15, 7, 38, 449, DateTimeKind.Utc).AddTicks(8233)
                        },
                        new
                        {
                            Tag = "Controller.Auth.Login",
                            ExecutionDate = new DateTime(2024, 7, 13, 15, 7, 38, 449, DateTimeKind.Utc).AddTicks(8335)
                        },
                        new
                        {
                            Tag = "Controller.Auth.Register",
                            ExecutionDate = new DateTime(2024, 7, 13, 15, 7, 38, 449, DateTimeKind.Utc).AddTicks(8391)
                        },
                        new
                        {
                            Tag = "Controller.Auth.CheckUserRights",
                            ExecutionDate = new DateTime(2024, 7, 13, 15, 7, 38, 449, DateTimeKind.Utc).AddTicks(8413)
                        },
                        new
                        {
                            Tag = "Controller.Helper.Upload",
                            ExecutionDate = new DateTime(2024, 7, 13, 15, 7, 38, 449, DateTimeKind.Utc).AddTicks(8503)
                        },
                        new
                        {
                            Tag = "Controller.Helper.GetUserTags",
                            ExecutionDate = new DateTime(2024, 7, 13, 15, 7, 38, 449, DateTimeKind.Utc).AddTicks(8525)
                        },
                        new
                        {
                            Tag = "Controller.Helper.GetUserProfile",
                            ExecutionDate = new DateTime(2024, 7, 13, 15, 7, 38, 449, DateTimeKind.Utc).AddTicks(8565)
                        },
                        new
                        {
                            Tag = "Controller.Helper.GetPostsForUser",
                            ExecutionDate = new DateTime(2024, 7, 13, 15, 7, 38, 449, DateTimeKind.Utc).AddTicks(8588)
                        },
                        new
                        {
                            Tag = "Controller.Helper.UpdateCurentUserAvatar",
                            ExecutionDate = new DateTime(2024, 7, 13, 15, 7, 38, 449, DateTimeKind.Utc).AddTicks(8631)
                        },
                        new
                        {
                            Tag = "Controller.Helper.SendFriendRequest",
                            ExecutionDate = new DateTime(2024, 7, 13, 15, 7, 38, 449, DateTimeKind.Utc).AddTicks(8656)
                        },
                        new
                        {
                            Tag = "Controller.Helper.AcceptFriendRequest",
                            ExecutionDate = new DateTime(2024, 7, 13, 15, 7, 38, 449, DateTimeKind.Utc).AddTicks(8680)
                        },
                        new
                        {
                            Tag = "Controller.QuestionAnswers.GetQuestionAnswereDistribution",
                            ExecutionDate = new DateTime(2024, 7, 13, 15, 7, 38, 449, DateTimeKind.Utc).AddTicks(8975)
                        });
                });

            modelBuilder.Entity("Setia.Models.Base.UserModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid?>("AuthorId")
                        .HasColumnType("uuid");

                    b.Property<string>("Avatar")
                        .HasColumnType("text");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime?>("EmailVerifiedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("ExecutionDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<List<Guid>>("Friends")
                        .HasColumnType("uuid[]");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Signiture")
                        .HasColumnType("text");

                    b.Property<List<string>>("Tags")
                        .HasColumnType("text[]");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Users", "base");

                    b.HasData(
                        new
                        {
                            Id = new Guid("9ef3c815-91f9-4404-b3a2-adfc9b7792e1"),
                            Email = "",
                            ExecutionDate = new DateTime(2024, 7, 13, 15, 7, 38, 449, DateTimeKind.Utc).AddTicks(6732),
                            Name = "Dragos",
                            Password = "E7CF3EF4F17C3999A94F2C6F612E8A888E5B1026878E4E19398B23BD38EC221A",
                            Tags = new List<string> { "Dragos", "Admin" },
                            Username = "Dragos"
                        });
                });

            modelBuilder.Entity("Setia.Models.Base.NotificationModel", b =>
                {
                    b.HasOne("Setia.Models.Base.UserModel", "UserData")
                        .WithMany()
                        .HasForeignKey("UserDataId");

                    b.Navigation("UserData");
                });
#pragma warning restore 612, 618
        }
    }
}
