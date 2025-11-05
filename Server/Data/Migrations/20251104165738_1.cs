using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Main.Data.Migrations
{
    /// <inheritdoc />
    public partial class _1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "base");

            migrationBuilder.CreateTable(
                name: "Users",
                schema: "base",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Username = table.Column<string>(type: "text", nullable: false),
                    Password = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    Signiture = table.Column<string>(type: "text", nullable: true),
                    BirthDay = table.Column<DateTimeOffset>(type: "timestamptz", nullable: true),
                    Friends = table.Column<List<Guid>>(type: "uuid[]", nullable: false),
                    Saves = table.Column<List<Guid>>(type: "uuid[]", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    EmailVerifiedDate = table.Column<DateTimeOffset>(type: "timestamptz", nullable: true),
                    Avatar = table.Column<string>(type: "text", nullable: true),
                    ExecutionDate = table.Column<DateTimeOffset>(type: "timestamptz", nullable: true),
                    AuthorId = table.Column<Guid>(type: "uuid", nullable: true),
                    AuthorDataId = table.Column<Guid>(type: "uuid", nullable: true),
                    Tags = table.Column<List<string>>(type: "text[]", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Users_AuthorDataId",
                        column: x => x.AuthorDataId,
                        principalSchema: "base",
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Audit",
                schema: "base",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Action = table.Column<string>(type: "text", nullable: true),
                    Details = table.Column<string>(type: "text", nullable: true),
                    EntityId = table.Column<string>(type: "text", nullable: true),
                    Entity = table.Column<string>(type: "text", nullable: true),
                    Payload = table.Column<string>(type: "text", nullable: true),
                    ExecutionDate = table.Column<DateTimeOffset>(type: "timestamptz", nullable: true),
                    AuthorId = table.Column<Guid>(type: "uuid", nullable: true),
                    AuthorDataId = table.Column<Guid>(type: "uuid", nullable: true),
                    Tags = table.Column<List<string>>(type: "text[]", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Audit", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Audit_Users_AuthorDataId",
                        column: x => x.AuthorDataId,
                        principalSchema: "base",
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Groups",
                schema: "base",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Descriere = table.Column<string>(type: "text", nullable: true),
                    ExecutionDate = table.Column<DateTimeOffset>(type: "timestamptz", nullable: true),
                    AuthorId = table.Column<Guid>(type: "uuid", nullable: true),
                    AuthorDataId = table.Column<Guid>(type: "uuid", nullable: true),
                    Tags = table.Column<List<string>>(type: "text[]", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Groups", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Groups_Users_AuthorDataId",
                        column: x => x.AuthorDataId,
                        principalSchema: "base",
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Links",
                schema: "base",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    SourceType = table.Column<string>(type: "text", nullable: false),
                    SourceId = table.Column<Guid>(type: "uuid", nullable: false),
                    TargetType = table.Column<string>(type: "text", nullable: false),
                    TargetId = table.Column<Guid>(type: "uuid", nullable: false),
                    Metadata = table.Column<string>(type: "text", nullable: true),
                    ExecutionDate = table.Column<DateTimeOffset>(type: "timestamptz", nullable: true),
                    AuthorId = table.Column<Guid>(type: "uuid", nullable: true),
                    AuthorDataId = table.Column<Guid>(type: "uuid", nullable: true),
                    Tags = table.Column<List<string>>(type: "text[]", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Links", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Links_Users_AuthorDataId",
                        column: x => x.AuthorDataId,
                        principalSchema: "base",
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Notifications",
                schema: "base",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Message = table.Column<string>(type: "text", nullable: false),
                    ReadDate = table.Column<DateTimeOffset>(type: "timestamptz", nullable: true),
                    Attachments = table.Column<List<Guid>>(type: "uuid[]", nullable: true),
                    ExecutionDate = table.Column<DateTimeOffset>(type: "timestamptz", nullable: true),
                    AuthorId = table.Column<Guid>(type: "uuid", nullable: true),
                    AuthorDataId = table.Column<Guid>(type: "uuid", nullable: true),
                    Tags = table.Column<List<string>>(type: "text[]", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notifications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Notifications_Users_AuthorDataId",
                        column: x => x.AuthorDataId,
                        principalSchema: "base",
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Posts",
                schema: "base",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    To = table.Column<Guid>(type: "uuid", nullable: true),
                    Message = table.Column<string>(type: "text", nullable: false),
                    SentDate = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    Attachments = table.Column<List<Guid>>(type: "uuid[]", nullable: true),
                    ExecutionDate = table.Column<DateTimeOffset>(type: "timestamptz", nullable: true),
                    AuthorId = table.Column<Guid>(type: "uuid", nullable: true),
                    AuthorDataId = table.Column<Guid>(type: "uuid", nullable: true),
                    Tags = table.Column<List<string>>(type: "text[]", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Posts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Posts_Users_AuthorDataId",
                        column: x => x.AuthorDataId,
                        principalSchema: "base",
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Questions",
                schema: "base",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Title = table.Column<string>(type: "text", nullable: false),
                    Options = table.Column<List<string>>(type: "text[]", nullable: true),
                    Selection = table.Column<int>(type: "integer", nullable: true),
                    CorrectAnswers = table.Column<int>(type: "integer", nullable: true),
                    ExecutionDate = table.Column<DateTimeOffset>(type: "timestamptz", nullable: true),
                    AuthorId = table.Column<Guid>(type: "uuid", nullable: true),
                    AuthorDataId = table.Column<Guid>(type: "uuid", nullable: true),
                    Tags = table.Column<List<string>>(type: "text[]", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Questions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Questions_Users_AuthorDataId",
                        column: x => x.AuthorDataId,
                        principalSchema: "base",
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Audit_AuthorDataId",
                schema: "base",
                table: "Audit",
                column: "AuthorDataId");

            migrationBuilder.CreateIndex(
                name: "IX_Groups_AuthorDataId",
                schema: "base",
                table: "Groups",
                column: "AuthorDataId");

            migrationBuilder.CreateIndex(
                name: "IX_Links_AuthorDataId",
                schema: "base",
                table: "Links",
                column: "AuthorDataId");

            migrationBuilder.CreateIndex(
                name: "IX_Links_SourceType_SourceId",
                schema: "base",
                table: "Links",
                columns: new[] { "SourceType", "SourceId" });

            migrationBuilder.CreateIndex(
                name: "IX_Links_TargetType_TargetId",
                schema: "base",
                table: "Links",
                columns: new[] { "TargetType", "TargetId" });

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_AuthorDataId",
                schema: "base",
                table: "Notifications",
                column: "AuthorDataId");

            migrationBuilder.CreateIndex(
                name: "IX_Posts_AuthorDataId",
                schema: "base",
                table: "Posts",
                column: "AuthorDataId");

            migrationBuilder.CreateIndex(
                name: "IX_Questions_AuthorDataId",
                schema: "base",
                table: "Questions",
                column: "AuthorDataId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_AuthorDataId",
                schema: "base",
                table: "Users",
                column: "AuthorDataId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Audit",
                schema: "base");

            migrationBuilder.DropTable(
                name: "Groups",
                schema: "base");

            migrationBuilder.DropTable(
                name: "Links",
                schema: "base");

            migrationBuilder.DropTable(
                name: "Notifications",
                schema: "base");

            migrationBuilder.DropTable(
                name: "Posts",
                schema: "base");

            migrationBuilder.DropTable(
                name: "Questions",
                schema: "base");

            migrationBuilder.DropTable(
                name: "Users",
                schema: "base");
        }
    }
}
