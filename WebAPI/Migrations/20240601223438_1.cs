using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Base.Migrations
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
                .Annotation("Npgsql:PostgresExtension:ltree", ",,");

            migrationBuilder.CreateTable(
                name: "Audit",
                schema: "base",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Description = table.Column<string>(type: "text", nullable: true),
                    Details = table.Column<string>(type: "text", nullable: true),
                    Entity = table.Column<string>(type: "text", nullable: true),
                    EntityId = table.Column<string>(type: "text", nullable: true),
                    Payload = table.Column<string>(type: "text", nullable: true),
                    ExecutionDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Author = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Audit", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Notifications",
                schema: "base",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Icon = table.Column<string>(type: "text", nullable: true),
                    Title = table.Column<string>(type: "text", nullable: true),
                    Comment = table.Column<string>(type: "text", nullable: true),
                    ExecutionDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Author = table.Column<string>(type: "text", nullable: true),
                    Active = table.Column<bool>(type: "boolean", nullable: false),
                    Deleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notifications", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tags",
                schema: "base",
                columns: table => new
                {
                    Tag = table.Column<string>(type: "ltree", nullable: false),
                    Comments = table.Column<string>(type: "text", nullable: true),
                    ExecutionDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Author = table.Column<string>(type: "text", nullable: true),
                    Active = table.Column<bool>(type: "boolean", nullable: false),
                    Deleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tags", x => x.Tag);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                schema: "base",
                columns: table => new
                {
                    Username = table.Column<string>(type: "text", nullable: false),
                    Password = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    EmailVerifiedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    ExecutionDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Author = table.Column<string>(type: "text", nullable: true),
                    Active = table.Column<bool>(type: "boolean", nullable: false),
                    Deleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Username);
                });

            migrationBuilder.CreateTable(
                name: "UserTags",
                schema: "base",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Tag = table.Column<string>(type: "ltree", nullable: false),
                    TagDataTag = table.Column<string>(type: "ltree", nullable: true),
                    User = table.Column<string>(type: "text", nullable: false),
                    UserDataUsername = table.Column<string>(type: "text", nullable: true),
                    ExecutionDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Author = table.Column<string>(type: "text", nullable: true),
                    Active = table.Column<bool>(type: "boolean", nullable: false),
                    Deleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTags", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserTags_Tags_TagDataTag",
                        column: x => x.TagDataTag,
                        principalSchema: "base",
                        principalTable: "Tags",
                        principalColumn: "Tag");
                    table.ForeignKey(
                        name: "FK_UserTags_Users_UserDataUsername",
                        column: x => x.UserDataUsername,
                        principalSchema: "base",
                        principalTable: "Users",
                        principalColumn: "Username");
                });

            migrationBuilder.InsertData(
                schema: "base",
                table: "Tags",
                columns: new[] { "Tag", "Active", "Author", "Comments", "Deleted", "ExecutionDate" },
                values: new object[,]
                {
                    { "Controller.Auth.CheckUserRights", true, null, null, false, new DateTime(2024, 6, 2, 1, 34, 38, 561, DateTimeKind.Utc).AddTicks(9460) },
                    { "Controller.Auth.Login", true, null, null, false, new DateTime(2024, 6, 2, 1, 34, 38, 561, DateTimeKind.Utc).AddTicks(9395) },
                    { "Controller.Auth.Register", true, null, null, false, new DateTime(2024, 6, 2, 1, 34, 38, 561, DateTimeKind.Utc).AddTicks(9418) },
                    { "Controller.CRUD1.Add", true, null, null, false, new DateTime(2024, 6, 2, 1, 34, 38, 561, DateTimeKind.Utc).AddTicks(9211) },
                    { "Controller.CRUD1.Delete", true, null, null, false, new DateTime(2024, 6, 2, 1, 34, 38, 561, DateTimeKind.Utc).AddTicks(9280) },
                    { "Controller.CRUD1.Get", true, null, null, false, new DateTime(2024, 6, 2, 1, 34, 38, 561, DateTimeKind.Utc).AddTicks(9170) },
                    { "Controller.CRUD1.Update", true, null, null, false, new DateTime(2024, 6, 2, 1, 34, 38, 561, DateTimeKind.Utc).AddTicks(9236) },
                    { "Controller.Helper.GetUserTags", true, null, null, false, new DateTime(2024, 6, 2, 1, 34, 38, 562, DateTimeKind.Utc).AddTicks(302) },
                    { "Controller.Helper.Upload", true, null, null, false, new DateTime(2024, 6, 2, 1, 34, 38, 562, DateTimeKind.Utc).AddTicks(245) },
                    { "Controller.Quizz.GetUserTags", true, null, null, false, new DateTime(2024, 6, 2, 1, 34, 38, 562, DateTimeKind.Utc).AddTicks(421) },
                    { "Controller.Quizz.Upload", true, null, null, false, new DateTime(2024, 6, 2, 1, 34, 38, 562, DateTimeKind.Utc).AddTicks(399) },
                    { "Role.Admin", true, null, null, false, new DateTime(2024, 6, 2, 1, 34, 38, 561, DateTimeKind.Utc).AddTicks(8421) }
                });

            migrationBuilder.InsertData(
                schema: "base",
                table: "UserTags",
                columns: new[] { "Id", "Active", "Author", "Deleted", "ExecutionDate", "Tag", "TagDataTag", "User", "UserDataUsername" },
                values: new object[,]
                {
                    { new Guid("6fe9262c-f838-4f76-9041-8e598ee593fa"), true, null, false, new DateTime(2024, 6, 2, 1, 34, 38, 561, DateTimeKind.Utc).AddTicks(8410), "Dragos", null, "testUser", null },
                    { new Guid("78d066c8-877a-4832-b667-9f37017e8ee2"), true, null, false, new DateTime(2024, 6, 2, 1, 34, 38, 561, DateTimeKind.Utc).AddTicks(8394), "Role.Admin", null, "testUser", null }
                });

            migrationBuilder.InsertData(
                schema: "base",
                table: "Users",
                columns: new[] { "Username", "Active", "Author", "Deleted", "Email", "EmailVerifiedDate", "ExecutionDate", "Name", "Password" },
                values: new object[] { "testUser", true, null, false, "", null, new DateTime(2024, 6, 2, 1, 34, 38, 561, DateTimeKind.Utc).AddTicks(7846), "Test Name", "FD5CB51BAFD60F6FDBEDDE6E62C473DA6F247DB271633E15919BAB78A02EE9EB" });

            migrationBuilder.CreateIndex(
                name: "IX_UserTags_TagDataTag",
                schema: "base",
                table: "UserTags",
                column: "TagDataTag");

            migrationBuilder.CreateIndex(
                name: "IX_UserTags_UserDataUsername",
                schema: "base",
                table: "UserTags",
                column: "UserDataUsername");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Audit",
                schema: "base");

            migrationBuilder.DropTable(
                name: "Notifications",
                schema: "base");

            migrationBuilder.DropTable(
                name: "UserTags",
                schema: "base");

            migrationBuilder.DropTable(
                name: "Tags",
                schema: "base");

            migrationBuilder.DropTable(
                name: "Users",
                schema: "base");
        }
    }
}
