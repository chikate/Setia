﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Setia.Contexts.Gov;

#nullable disable

namespace Base.Migrations.Gov
{
    [DbContext(typeof(GovContext))]
    partial class GovContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("gov")
                .HasAnnotation("ProductVersion", "8.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Setia.Models.Base.UserModel", b =>
                {
                    b.Property<string>("Email")
                        .HasColumnType("text");

                    b.Property<bool>("Active")
                        .HasColumnType("boolean");

                    b.Property<string>("AuthorId")
                        .HasColumnType("text");

                    b.Property<bool>("Deleted")
                        .HasColumnType("boolean");

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

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Email");

                    b.HasIndex("AuthorId");

                    b.ToTable("UserModel", "gov");
                });

            modelBuilder.Entity("Setia.Models.Gov.PontajModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<bool>("Active")
                        .HasColumnType("boolean");

                    b.Property<string>("AuthorId")
                        .HasColumnType("text");

                    b.Property<DateTime>("BeginTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool>("Deleted")
                        .HasColumnType("boolean");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime?>("EndTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("ExecutionDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("User")
                        .HasColumnType("text");

                    b.Property<string>("UserDataEmail")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("AuthorId");

                    b.HasIndex("UserDataEmail");

                    b.ToTable("Pontaj", "gov");
                });

            modelBuilder.Entity("Setia.Models.Gov.VoteModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<bool>("Active")
                        .HasColumnType("boolean");

                    b.Property<string>("AuthorId")
                        .HasColumnType("text");

                    b.Property<DateTime?>("Available")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool>("Deleted")
                        .HasColumnType("boolean");

                    b.Property<string>("EndOption")
                        .HasColumnType("text");

                    b.Property<DateTime>("ExecutionDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Options")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("AuthorId");

                    b.ToTable("Votes", "gov");
                });

            modelBuilder.Entity("Setia.Models.Base.UserModel", b =>
                {
                    b.HasOne("Setia.Models.Base.UserModel", "Author")
                        .WithMany()
                        .HasForeignKey("AuthorId");

                    b.Navigation("Author");
                });

            modelBuilder.Entity("Setia.Models.Gov.PontajModel", b =>
                {
                    b.HasOne("Setia.Models.Base.UserModel", "Author")
                        .WithMany()
                        .HasForeignKey("AuthorId");

                    b.HasOne("Setia.Models.Base.UserModel", "UserData")
                        .WithMany()
                        .HasForeignKey("UserDataEmail");

                    b.Navigation("Author");

                    b.Navigation("UserData");
                });

            modelBuilder.Entity("Setia.Models.Gov.VoteModel", b =>
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
