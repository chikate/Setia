﻿// <auto-generated />
using System;
using System.Collections.Generic;
using Main.Data.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Main.Data.Migrations.Base
{
    [DbContext(typeof(BaseContext))]
    [Migration("20240803123052_1")]
    partial class _1
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("base")
                .HasAnnotation("ProductVersion", "8.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.HasPostgresExtension(modelBuilder, "hstore");
            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Main.Data.Models.Base.AuditModel", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<Guid?>("AuthorDataId")
                        .HasColumnType("uuid");

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

                    b.Property<List<string>>("Tags")
                        .HasColumnType("text[]");

                    b.HasKey("Id");

                    b.HasIndex("AuthorDataId");

                    b.ToTable("Audit", "base");
                });

            modelBuilder.Entity("Main.Data.Models.Base.NotificationModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid?>("AuthorDataId")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("AuthorId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("ExecutionDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Dictionary<string, string>>("ExtraMessages")
                        .HasColumnType("hstore");

                    b.Property<string>("Icon")
                        .HasColumnType("text");

                    b.Property<string>("Message")
                        .HasColumnType("text");

                    b.Property<string>("Severity")
                        .HasColumnType("text");

                    b.Property<string>("SubTitle")
                        .HasColumnType("text");

                    b.Property<List<string>>("Tags")
                        .HasColumnType("text[]");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid?>("ToUserDataId")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("ToUserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("AuthorDataId");

                    b.HasIndex("ToUserDataId");

                    b.ToTable("Notifications", "base");
                });

            modelBuilder.Entity("Main.Data.Models.Base.UserModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid?>("AuthorDataId")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("AuthorId")
                        .HasColumnType("uuid");

                    b.Property<string>("Avatar")
                        .HasColumnType("text");

                    b.Property<DateTime?>("BirthDay")
                        .HasColumnType("timestamp with time zone");

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

                    b.Property<Dictionary<string, string>>("Saves")
                        .HasColumnType("hstore");

                    b.Property<string>("Signiture")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<List<string>>("Tags")
                        .HasColumnType("text[]");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("AuthorDataId");

                    b.ToTable("Users", "base");

                    b.HasData(
                        new
                        {
                            Id = new Guid("7b3627a4-0969-4ad3-a7a9-6d4d64f2f122"),
                            Email = "",
                            ExecutionDate = new DateTime(2024, 8, 3, 15, 30, 52, 710, DateTimeKind.Utc).AddTicks(207),
                            Name = "Dragos",
                            Password = "E7CF3EF4F17C3999A94F2C6F612E8A888E5B1026878E4E19398B23BD38EC221A",
                            Signiture = "",
                            Tags = new List<string> { "Dragos", "Admin" },
                            Username = "Dragos"
                        });
                });

            modelBuilder.Entity("Main.Data.Models.Base.AuditModel", b =>
                {
                    b.HasOne("Main.Data.Models.Base.UserModel", "AuthorData")
                        .WithMany()
                        .HasForeignKey("AuthorDataId");

                    b.Navigation("AuthorData");
                });

            modelBuilder.Entity("Main.Data.Models.Base.NotificationModel", b =>
                {
                    b.HasOne("Main.Data.Models.Base.UserModel", "AuthorData")
                        .WithMany()
                        .HasForeignKey("AuthorDataId");

                    b.HasOne("Main.Data.Models.Base.UserModel", "ToUserData")
                        .WithMany()
                        .HasForeignKey("ToUserDataId");

                    b.Navigation("AuthorData");

                    b.Navigation("ToUserData");
                });

            modelBuilder.Entity("Main.Data.Models.Base.UserModel", b =>
                {
                    b.HasOne("Main.Data.Models.Base.UserModel", "AuthorData")
                        .WithMany()
                        .HasForeignKey("AuthorDataId");

                    b.Navigation("AuthorData");
                });
#pragma warning restore 612, 618
        }
    }
}