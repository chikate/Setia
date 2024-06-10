using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Base.Migrations.Gov
{
    /// <inheritdoc />
    public partial class _2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "gov");

            migrationBuilder.CreateTable(
                name: "Questions",
                schema: "gov",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Title = table.Column<string>(type: "text", nullable: false),
                    Comment = table.Column<string>(type: "text", nullable: true),
                    Options = table.Column<List<string>>(type: "text[]", nullable: true),
                    Selection = table.Column<List<string>>(type: "text[]", nullable: true),
                    EndOption = table.Column<string>(type: "text", nullable: true),
                    Expires = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    ExecutionDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Author = table.Column<string>(type: "text", nullable: true),
                    Tags = table.Column<List<string>>(type: "text[]", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Questions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserModel",
                schema: "gov",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Username = table.Column<string>(type: "text", nullable: false),
                    Password = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    EmailVerifiedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Avatar = table.Column<string>(type: "text", nullable: true),
                    Signiture = table.Column<string>(type: "text", nullable: true),
                    ExecutionDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Author = table.Column<string>(type: "text", nullable: true),
                    Tags = table.Column<List<string>>(type: "text[]", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserModel", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "QuestionAnswers",
                schema: "gov",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Answer = table.Column<List<string>>(type: "text[]", nullable: false),
                    QuestionId = table.Column<Guid>(type: "uuid", nullable: false),
                    ExecutionDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Author = table.Column<string>(type: "text", nullable: true),
                    Tags = table.Column<List<string>>(type: "text[]", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestionAnswers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_QuestionAnswers_Questions_QuestionId",
                        column: x => x.QuestionId,
                        principalSchema: "gov",
                        principalTable: "Questions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Pontaj",
                schema: "gov",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: true),
                    BeginTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    EndTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    ExecutionDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Author = table.Column<string>(type: "text", nullable: true),
                    Tags = table.Column<List<string>>(type: "text[]", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pontaj", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Pontaj_UserModel_UserId",
                        column: x => x.UserId,
                        principalSchema: "gov",
                        principalTable: "UserModel",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Posts",
                schema: "gov",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Message = table.Column<string>(type: "text", nullable: true),
                    QuestionId = table.Column<Guid>(type: "uuid", nullable: true),
                    ExecutionDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Author = table.Column<string>(type: "text", nullable: true),
                    Tags = table.Column<List<string>>(type: "text[]", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Posts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Posts_UserModel_QuestionId",
                        column: x => x.QuestionId,
                        principalSchema: "gov",
                        principalTable: "UserModel",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "UsersCollection",
                schema: "gov",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    PostId = table.Column<Guid>(type: "uuid", nullable: false),
                    ExecutionDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Author = table.Column<string>(type: "text", nullable: true),
                    Tags = table.Column<List<string>>(type: "text[]", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsersCollection", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UsersCollection_Posts_PostId",
                        column: x => x.PostId,
                        principalSchema: "gov",
                        principalTable: "Posts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Pontaj_UserId",
                schema: "gov",
                table: "Pontaj",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Posts_QuestionId",
                schema: "gov",
                table: "Posts",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionAnswers_QuestionId",
                schema: "gov",
                table: "QuestionAnswers",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_UsersCollection_PostId",
                schema: "gov",
                table: "UsersCollection",
                column: "PostId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Pontaj",
                schema: "gov");

            migrationBuilder.DropTable(
                name: "QuestionAnswers",
                schema: "gov");

            migrationBuilder.DropTable(
                name: "UsersCollection",
                schema: "gov");

            migrationBuilder.DropTable(
                name: "Questions",
                schema: "gov");

            migrationBuilder.DropTable(
                name: "Posts",
                schema: "gov");

            migrationBuilder.DropTable(
                name: "UserModel",
                schema: "gov");
        }
    }
}
