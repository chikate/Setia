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
                    Tags = table.Column<List<string>>(type: "text[]", nullable: false)
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
                    Tags = table.Column<List<string>>(type: "text[]", nullable: false)
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
                name: "Settings",
                schema: "base",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Key = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    Value = table.Column<string>(type: "text", nullable: true),
                    ExecutionDate = table.Column<DateTimeOffset>(type: "timestamptz", nullable: true),
                    AuthorId = table.Column<Guid>(type: "uuid", nullable: true),
                    AuthorDataId = table.Column<Guid>(type: "uuid", nullable: true),
                    Tags = table.Column<List<string>>(type: "text[]", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Settings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Settings_Users_AuthorDataId",
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
                name: "Audit",
                schema: "base");

            migrationBuilder.DropTable(
                name: "Settings",
                schema: "base");

            migrationBuilder.DropTable(
                name: "Users",
                schema: "base");
        }
    }
}
