using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Main.Data.Migrations.Gov
{
    /// <inheritdoc />
    public partial class _1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "gov");

            migrationBuilder.CreateTable(
                name: "AuditModel",
                schema: "gov",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Description = table.Column<string>(type: "text", nullable: true),
                    Details = table.Column<string>(type: "text", nullable: true),
                    EntityId = table.Column<string>(type: "text", nullable: true),
                    Entity = table.Column<string>(type: "text", nullable: true),
                    Payload = table.Column<string>(type: "text", nullable: true),
                    ExecutionDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    AuthorId = table.Column<Guid>(type: "uuid", nullable: true),
                    AuthorDataId = table.Column<long>(type: "bigint", nullable: true),
                    Tags = table.Column<List<string>>(type: "text[]", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuditModel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AuditModel_AuditModel_AuthorDataId",
                        column: x => x.AuthorDataId,
                        principalSchema: "gov",
                        principalTable: "AuditModel",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Pontaj",
                schema: "gov",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: true),
                    UserDataId = table.Column<long>(type: "bigint", nullable: true),
                    BeginTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    EndTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    ExecutionDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    AuthorId = table.Column<Guid>(type: "uuid", nullable: true),
                    AuthorDataId = table.Column<long>(type: "bigint", nullable: true),
                    Tags = table.Column<List<string>>(type: "text[]", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pontaj", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Pontaj_AuditModel_AuthorDataId",
                        column: x => x.AuthorDataId,
                        principalSchema: "gov",
                        principalTable: "AuditModel",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Pontaj_AuditModel_UserDataId",
                        column: x => x.UserDataId,
                        principalSchema: "gov",
                        principalTable: "AuditModel",
                        principalColumn: "Id");
                });

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
                    AuthorId = table.Column<Guid>(type: "uuid", nullable: true),
                    AuthorDataId = table.Column<long>(type: "bigint", nullable: true),
                    Tags = table.Column<List<string>>(type: "text[]", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Questions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Questions_AuditModel_AuthorDataId",
                        column: x => x.AuthorDataId,
                        principalSchema: "gov",
                        principalTable: "AuditModel",
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
                    AuthorId = table.Column<Guid>(type: "uuid", nullable: true),
                    AuthorDataId = table.Column<long>(type: "bigint", nullable: true),
                    Tags = table.Column<List<string>>(type: "text[]", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Posts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Post_Question",
                        column: x => x.QuestionId,
                        principalSchema: "gov",
                        principalTable: "Questions",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Posts_AuditModel_AuthorDataId",
                        column: x => x.AuthorDataId,
                        principalSchema: "gov",
                        principalTable: "AuditModel",
                        principalColumn: "Id");
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
                    AuthorId = table.Column<Guid>(type: "uuid", nullable: true),
                    AuthorDataId = table.Column<long>(type: "bigint", nullable: true),
                    Tags = table.Column<List<string>>(type: "text[]", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestionAnswers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_QuestionAnswer_Question",
                        column: x => x.QuestionId,
                        principalSchema: "gov",
                        principalTable: "Questions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_QuestionAnswers_AuditModel_AuthorDataId",
                        column: x => x.AuthorDataId,
                        principalSchema: "gov",
                        principalTable: "AuditModel",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_AuditModel_AuthorDataId",
                schema: "gov",
                table: "AuditModel",
                column: "AuthorDataId");

            migrationBuilder.CreateIndex(
                name: "IX_Pontaj_AuthorDataId",
                schema: "gov",
                table: "Pontaj",
                column: "AuthorDataId");

            migrationBuilder.CreateIndex(
                name: "IX_Pontaj_UserDataId",
                schema: "gov",
                table: "Pontaj",
                column: "UserDataId");

            migrationBuilder.CreateIndex(
                name: "IX_Posts_AuthorDataId",
                schema: "gov",
                table: "Posts",
                column: "AuthorDataId");

            migrationBuilder.CreateIndex(
                name: "IX_Posts_QuestionId",
                schema: "gov",
                table: "Posts",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionAnswers_AuthorDataId",
                schema: "gov",
                table: "QuestionAnswers",
                column: "AuthorDataId");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionAnswers_QuestionId",
                schema: "gov",
                table: "QuestionAnswers",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_Questions_AuthorDataId",
                schema: "gov",
                table: "Questions",
                column: "AuthorDataId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Pontaj",
                schema: "gov");

            migrationBuilder.DropTable(
                name: "Posts",
                schema: "gov");

            migrationBuilder.DropTable(
                name: "QuestionAnswers",
                schema: "gov");

            migrationBuilder.DropTable(
                name: "Questions",
                schema: "gov");

            migrationBuilder.DropTable(
                name: "AuditModel",
                schema: "gov");
        }
    }
}
