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
    [Migration("20240204144618_Retus2")]
    partial class Retus2
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

                    b.Property<int?>("Id_Executioner")
                        .HasColumnType("int");

                    b.Property<string>("Payload")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

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

                    b.Property<DateTime>("BeginTime")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("Deleted")
                        .HasColumnType("bit");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("EndTime")
                        .HasColumnType("datetime2");

                    b.Property<int?>("Id_CreatedBy")
                        .HasColumnType("int");

                    b.Property<int?>("Id_LastUpdateBy")
                        .HasColumnType("int");

                    b.Property<int>("Id_User")
                        .HasColumnType("int");

                    b.Property<DateTime?>("LastUpdateDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("Id_CreatedBy");

                    b.HasIndex("Id_LastUpdateBy");

                    b.HasIndex("Id_User");

                    b.ToTable("Pontaj");
                });

            modelBuilder.Entity("Setia.Models.UserModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool>("Active")
                        .HasColumnType("bit");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("Deleted")
                        .HasColumnType("bit");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("Id_CreatedBy")
                        .HasColumnType("int");

                    b.Property<int?>("Id_LastUpdateBy")
                        .HasColumnType("int");

                    b.Property<DateTime?>("LastUpdateDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Rights")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("StatusCode")
                        .HasColumnType("int");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("Id_CreatedBy");

                    b.HasIndex("Id_LastUpdateBy");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Setia.Models.PontajModel", b =>
                {
                    b.HasOne("Setia.Models.UserModel", "CreatedBy")
                        .WithMany()
                        .HasForeignKey("Id_CreatedBy");

                    b.HasOne("Setia.Models.UserModel", "LastUpdateBy")
                        .WithMany()
                        .HasForeignKey("Id_LastUpdateBy");

                    b.HasOne("Setia.Models.UserModel", "User")
                        .WithMany()
                        .HasForeignKey("Id_User")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CreatedBy");

                    b.Navigation("LastUpdateBy");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Setia.Models.UserModel", b =>
                {
                    b.HasOne("Setia.Models.UserModel", "CreatedBy")
                        .WithMany()
                        .HasForeignKey("Id_CreatedBy");

                    b.HasOne("Setia.Models.UserModel", "LastUpdateBy")
                        .WithMany()
                        .HasForeignKey("Id_LastUpdateBy");

                    b.Navigation("CreatedBy");

                    b.Navigation("LastUpdateBy");
                });
#pragma warning restore 612, 618
        }
    }
}
