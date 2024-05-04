using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Base.Migrations
{
    /// <inheritdoc />
    public partial class SetiaBase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "base");

            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:PostgresExtension:ltree", ",,");

            migrationBuilder.CreateTable(
                name: "Users",
                schema: "base",
                columns: table => new
                {
                    Email = table.Column<string>(type: "text", nullable: false),
                    Username = table.Column<string>(type: "text", nullable: false),
                    Password = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    EmailVerifiedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    ExecutionDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    AuthorId = table.Column<string>(type: "text", nullable: true),
                    Active = table.Column<bool>(type: "boolean", nullable: false),
                    Deleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Email);
                    table.ForeignKey(
                        name: "FK_Users_Users_AuthorId",
                        column: x => x.AuthorId,
                        principalSchema: "base",
                        principalTable: "Users",
                        principalColumn: "Email");
                });

            migrationBuilder.CreateTable(
                name: "Audit",
                schema: "base",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Description = table.Column<string>(type: "text", nullable: false),
                    Details = table.Column<string>(type: "text", nullable: false),
                    Entity = table.Column<string>(type: "text", nullable: false),
                    EntityId = table.Column<int>(type: "integer", nullable: true),
                    Payload = table.Column<string>(type: "text", nullable: false),
                    ExecutionDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    AuthorId = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Audit", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Audit_Users_AuthorId",
                        column: x => x.AuthorId,
                        principalSchema: "base",
                        principalTable: "Users",
                        principalColumn: "Email");
                });

            migrationBuilder.CreateTable(
                name: "Tags",
                schema: "base",
                columns: table => new
                {
                    Tag = table.Column<string>(type: "ltree", nullable: false),
                    Comments = table.Column<string>(type: "text", nullable: true),
                    ExecutionDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    AuthorId = table.Column<string>(type: "text", nullable: true),
                    Active = table.Column<bool>(type: "boolean", nullable: false),
                    Deleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tags", x => x.Tag);
                    table.ForeignKey(
                        name: "FK_Tags_Users_AuthorId",
                        column: x => x.AuthorId,
                        principalSchema: "base",
                        principalTable: "Users",
                        principalColumn: "Email");
                });

            migrationBuilder.CreateTable(
                name: "UserTags",
                schema: "base",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Tag = table.Column<string>(type: "ltree", nullable: false),
                    User = table.Column<string>(type: "text", nullable: false),
                    ExecutionDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    AuthorId = table.Column<string>(type: "text", nullable: true),
                    Active = table.Column<bool>(type: "boolean", nullable: false),
                    Deleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTags", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserTags_Users_AuthorId",
                        column: x => x.AuthorId,
                        principalSchema: "base",
                        principalTable: "Users",
                        principalColumn: "Email");
                });

            migrationBuilder.InsertData(
                schema: "base",
                table: "Tags",
                columns: new[] { "Tag", "Active", "AuthorId", "Comments", "Deleted", "ExecutionDate" },
                values: new object[,]
                {
                    { "CRUD1.Add", true, null, null, false, new DateTime(2024, 5, 2, 12, 16, 16, 263, DateTimeKind.Utc).AddTicks(7792) },
                    { "CRUD1.Delete", true, null, null, false, new DateTime(2024, 5, 2, 12, 16, 16, 263, DateTimeKind.Utc).AddTicks(8260) },
                    { "CRUD1.Get", true, null, null, false, new DateTime(2024, 5, 2, 12, 16, 16, 263, DateTimeKind.Utc).AddTicks(7754) },
                    { "CRUD1.Update", true, null, null, false, new DateTime(2024, 5, 2, 12, 16, 16, 263, DateTimeKind.Utc).AddTicks(8146) },
                    { "Helper.GetUserTags", true, null, null, false, new DateTime(2024, 5, 2, 12, 16, 16, 263, DateTimeKind.Utc).AddTicks(8781) },
                    { "Helper.Upload", true, null, null, false, new DateTime(2024, 5, 2, 12, 16, 16, 263, DateTimeKind.Utc).AddTicks(8759) },
                    { "Role.Admin", true, null, null, false, new DateTime(2024, 5, 2, 12, 16, 16, 263, DateTimeKind.Utc).AddTicks(6969) }
                });

            migrationBuilder.InsertData(
                schema: "base",
                table: "UserTags",
                columns: new[] { "Id", "Active", "AuthorId", "Deleted", "ExecutionDate", "Tag", "User" },
                values: new object[,]
                {
                    { new Guid("07190912-34dd-462f-853e-6671f6c3992a"), true, null, false, new DateTime(2024, 5, 2, 12, 16, 16, 263, DateTimeKind.Utc).AddTicks(6943), "Role.Admin", "testUser" },
                    { new Guid("f29afff1-488b-4612-a3c2-1ca8e34e6e2c"), true, null, false, new DateTime(2024, 5, 2, 12, 16, 16, 263, DateTimeKind.Utc).AddTicks(6958), "Dragos", "testUser" }
                });

            migrationBuilder.InsertData(
                schema: "base",
                table: "Users",
                columns: new[] { "Email", "Active", "AuthorId", "Deleted", "EmailVerifiedDate", "ExecutionDate", "Name", "Password", "Username" },
                values: new object[] { "", true, null, false, null, new DateTime(2024, 5, 2, 12, 16, 16, 263, DateTimeKind.Utc).AddTicks(6350), "Test Name", "FD5CB51BAFD60F6FDBEDDE6E62C473DA6F247DB271633E15919BAB78A02EE9EB", "testUser" });

            migrationBuilder.CreateIndex(
                name: "IX_Audit_AuthorId",
                schema: "base",
                table: "Audit",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_Tags_AuthorId",
                schema: "base",
                table: "Tags",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_AuthorId",
                schema: "base",
                table: "Users",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_UserTags_AuthorId",
                schema: "base",
                table: "UserTags",
                column: "AuthorId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Audit",
                schema: "base");

            migrationBuilder.DropTable(
                name: "Tags",
                schema: "base");

            migrationBuilder.DropTable(
                name: "UserTags",
                schema: "base");

            migrationBuilder.DropTable(
                name: "Users",
                schema: "base");
        }
    }
}
