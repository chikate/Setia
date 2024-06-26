﻿// <auto-generated />
using System;
using System.Collections.Generic;
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
                .HasAnnotation("ProductVersion", "8.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Setia.Models.Base.UserModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Author")
                        .HasColumnType("text");

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

                    b.ToTable("UserModel", "gov");
                });

            modelBuilder.Entity("Setia.Models.Gov.PontajModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Author")
                        .HasColumnType("text");

                    b.Property<DateTime>("BeginTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<DateTime?>("EndTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("ExecutionDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<List<string>>("Tags")
                        .HasColumnType("text[]");

                    b.Property<Guid?>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Pontaj", "gov");
                });

            modelBuilder.Entity("Setia.Models.Gov.PostModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Author")
                        .HasColumnType("text");

                    b.Property<string>("Entity")
                        .HasColumnType("text");

                    b.Property<string>("EntityId")
                        .HasColumnType("text");

                    b.Property<DateTime>("ExecutionDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Message")
                        .HasColumnType("text");

                    b.Property<Guid?>("QuestionId")
                        .HasColumnType("uuid");

                    b.Property<List<string>>("Tags")
                        .HasColumnType("text[]");

                    b.HasKey("Id");

                    b.HasIndex("QuestionId");

                    b.ToTable("Posts", "gov");
                });

            modelBuilder.Entity("Setia.Models.Gov.QuestionAnswerModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<List<string>>("Answer")
                        .IsRequired()
                        .HasColumnType("text[]");

                    b.Property<string>("Author")
                        .HasColumnType("text");

                    b.Property<DateTime>("ExecutionDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("QuestionId")
                        .HasColumnType("uuid");

                    b.Property<List<string>>("Tags")
                        .HasColumnType("text[]");

                    b.HasKey("Id");

                    b.HasIndex("QuestionId");

                    b.ToTable("QuestionAnswers", "gov");
                });

            modelBuilder.Entity("Setia.Models.Gov.QuestionModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Author")
                        .HasColumnType("text");

                    b.Property<string>("Comment")
                        .HasColumnType("text");

                    b.Property<string>("EndOption")
                        .HasColumnType("text");

                    b.Property<DateTime>("ExecutionDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime?>("Expires")
                        .HasColumnType("timestamp with time zone");

                    b.Property<List<string>>("Options")
                        .HasColumnType("text[]");

                    b.Property<List<string>>("Selection")
                        .HasColumnType("text[]");

                    b.Property<List<string>>("Tags")
                        .HasColumnType("text[]");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Questions", "gov");
                });

            modelBuilder.Entity("Setia.Models.Gov.UserCollectionModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Author")
                        .HasColumnType("text");

                    b.Property<DateTime>("ExecutionDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid?>("PostId")
                        .HasColumnType("uuid");

                    b.Property<List<string>>("Tags")
                        .HasColumnType("text[]");

                    b.HasKey("Id");

                    b.HasIndex("PostId");

                    b.ToTable("UsersCollection", "gov");
                });

            modelBuilder.Entity("Setia.Models.Gov.PontajModel", b =>
                {
                    b.HasOne("Setia.Models.Base.UserModel", "UserData")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .HasConstraintName("FK_Pontaj_User");

                    b.Navigation("UserData");
                });

            modelBuilder.Entity("Setia.Models.Gov.PostModel", b =>
                {
                    b.HasOne("Setia.Models.Gov.QuestionModel", "QuestionData")
                        .WithMany()
                        .HasForeignKey("QuestionId")
                        .HasConstraintName("FK_Post_Question");

                    b.Navigation("QuestionData");
                });

            modelBuilder.Entity("Setia.Models.Gov.QuestionAnswerModel", b =>
                {
                    b.HasOne("Setia.Models.Gov.QuestionModel", "QuestionData")
                        .WithMany()
                        .HasForeignKey("QuestionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_QuestionAnswer_Question");

                    b.Navigation("QuestionData");
                });

            modelBuilder.Entity("Setia.Models.Gov.UserCollectionModel", b =>
                {
                    b.HasOne("Setia.Models.Gov.PostModel", "PostData")
                        .WithMany()
                        .HasForeignKey("PostId")
                        .HasConstraintName("FK_UserCollection_Post");

                    b.Navigation("PostData");
                });
#pragma warning restore 612, 618
        }
    }
}
