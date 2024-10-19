using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Main.Data.Migrations.Base
{
    /// <inheritdoc />
    public partial class _1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "base");

            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:PostgresExtension:hstore", ",,");

            migrationBuilder.CreateTable(
                name: "Audit",
                schema: "base",
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
                    Tags = table.Column<List<string>>(type: "text[]", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Audit", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Audit_Audit_AuthorDataId",
                        column: x => x.AuthorDataId,
                        principalSchema: "base",
                        principalTable: "Audit",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Notifications",
                schema: "base",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ToUserId = table.Column<Guid>(type: "uuid", nullable: true),
                    ToUserDataId = table.Column<long>(type: "bigint", nullable: true),
                    Icon = table.Column<string>(type: "text", nullable: true),
                    Severity = table.Column<string>(type: "text", nullable: true),
                    Title = table.Column<string>(type: "text", nullable: false),
                    SubTitle = table.Column<string>(type: "text", nullable: true),
                    Message = table.Column<string>(type: "text", nullable: true),
                    ExtraMessages = table.Column<Dictionary<string, string>>(type: "hstore", nullable: true),
                    ExecutionDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    AuthorId = table.Column<Guid>(type: "uuid", nullable: true),
                    AuthorDataId = table.Column<long>(type: "bigint", nullable: true),
                    Tags = table.Column<List<string>>(type: "text[]", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notifications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Notifications_Audit_AuthorDataId",
                        column: x => x.AuthorDataId,
                        principalSchema: "base",
                        principalTable: "Audit",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Notifications_Audit_ToUserDataId",
                        column: x => x.ToUserDataId,
                        principalSchema: "base",
                        principalTable: "Audit",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Settings",
                schema: "base",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Key = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    Value = table.Column<string>(type: "text", nullable: true),
                    ExecutionDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    AuthorId = table.Column<Guid>(type: "uuid", nullable: true),
                    AuthorDataId = table.Column<long>(type: "bigint", nullable: true),
                    Tags = table.Column<List<string>>(type: "text[]", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Settings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Settings_Audit_AuthorDataId",
                        column: x => x.AuthorDataId,
                        principalSchema: "base",
                        principalTable: "Audit",
                        principalColumn: "Id");
                });

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
                    BirthDay = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Friends = table.Column<List<Guid>>(type: "uuid[]", nullable: false),
                    Saves = table.Column<Dictionary<string, string>>(type: "hstore", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    EmailVerifiedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Avatar = table.Column<string>(type: "text", nullable: true),
                    ExecutionDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    AuthorId = table.Column<Guid>(type: "uuid", nullable: true),
                    AuthorDataId = table.Column<long>(type: "bigint", nullable: true),
                    Tags = table.Column<List<string>>(type: "text[]", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Audit_AuthorDataId",
                        column: x => x.AuthorDataId,
                        principalSchema: "base",
                        principalTable: "Audit",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                schema: "base",
                table: "Settings",
                columns: new[] { "Id", "AuthorDataId", "AuthorId", "Description", "ExecutionDate", "Key", "Tags", "Value" },
                values: new object[,]
                {
                    { 1L, null, null, "", new DateTime(2024, 10, 16, 20, 12, 4, 242, DateTimeKind.Utc).AddTicks(4102), "Client Name", new List<string>(), "DragosClient" },
                    { 2L, null, null, "", new DateTime(2024, 10, 16, 20, 12, 4, 242, DateTimeKind.Utc).AddTicks(4146), "Client BE URL", new List<string>(), "https://localhost:44381" },
                    { 3L, null, null, "", new DateTime(2024, 10, 16, 20, 12, 4, 242, DateTimeKind.Utc).AddTicks(4185), "Client Environment", new List<string>(), "Development" },
                    { 4L, null, null, "", new DateTime(2024, 10, 16, 20, 12, 4, 242, DateTimeKind.Utc).AddTicks(4214), "Client Colors", new List<string>(), "2f42a6,845dbb" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Audit_AuthorDataId",
                schema: "base",
                table: "Audit",
                column: "AuthorDataId");

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_AuthorDataId",
                schema: "base",
                table: "Notifications",
                column: "AuthorDataId");

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_ToUserDataId",
                schema: "base",
                table: "Notifications",
                column: "ToUserDataId");

            migrationBuilder.CreateIndex(
                name: "IX_Settings_AuthorDataId",
                schema: "base",
                table: "Settings",
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
                name: "Notifications",
                schema: "base");

            migrationBuilder.DropTable(
                name: "Settings",
                schema: "base");

            migrationBuilder.DropTable(
                name: "Users",
                schema: "base");

            migrationBuilder.DropTable(
                name: "Audit",
                schema: "base");
        }
    }
}
