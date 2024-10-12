using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

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
                    Tags = table.Column<List<string>>(type: "text[]", nullable: true)
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
                    Tags = table.Column<List<string>>(type: "text[]", nullable: true)
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
                name: "Users",
                schema: "base",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Username = table.Column<string>(type: "text", nullable: false),
                    Password = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    Signiture = table.Column<string>(type: "text", nullable: false),
                    BirthDay = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Friends = table.Column<List<Guid>>(type: "uuid[]", nullable: true),
                    Saves = table.Column<Dictionary<string, string>>(type: "hstore", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: false),
                    EmailVerifiedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Avatar = table.Column<string>(type: "text", nullable: true),
                    ExecutionDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    AuthorId = table.Column<Guid>(type: "uuid", nullable: true),
                    AuthorDataId = table.Column<long>(type: "bigint", nullable: true),
                    Tags = table.Column<List<string>>(type: "text[]", nullable: true)
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
                table: "Users",
                columns: new[] { "Id", "AuthorDataId", "AuthorId", "Avatar", "BirthDay", "Email", "EmailVerifiedDate", "ExecutionDate", "Friends", "Name", "Password", "Saves", "Signiture", "Tags", "Username" },
                values: new object[] { new Guid("97710da1-e06b-4e90-bf82-5f95d58aed50"), null, null, null, null, "", null, new DateTime(2024, 10, 11, 19, 28, 7, 416, DateTimeKind.Utc).AddTicks(7388), null, "Dragos", "E7CF3EF4F17C3999A94F2C6F612E8A888E5B1026878E4E19398B23BD38EC221A", null, "", new List<string> { "Dragos", "Admin" }, "Dragos" });

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
                name: "Users",
                schema: "base");

            migrationBuilder.DropTable(
                name: "Audit",
                schema: "base");
        }
    }
}
