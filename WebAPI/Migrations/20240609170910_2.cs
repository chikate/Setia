using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Base.Migrations
{
    /// <inheritdoc />
    public partial class _2 : Migration
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
                name: "Tags",
                schema: "base",
                columns: table => new
                {
                    Tag = table.Column<string>(type: "ltree", nullable: false),
                    Comments = table.Column<string>(type: "text", nullable: true),
                    ExecutionDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Author = table.Column<string>(type: "text", nullable: true),
                    Tags = table.Column<List<string>>(type: "text[]", nullable: true)
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
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Notifications",
                schema: "base",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: true),
                    UserDataId = table.Column<Guid>(type: "uuid", nullable: true),
                    Icon = table.Column<string>(type: "text", nullable: true),
                    Title = table.Column<string>(type: "text", nullable: true),
                    Comment = table.Column<string>(type: "text", nullable: true),
                    ExecutionDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Author = table.Column<string>(type: "text", nullable: true),
                    Tags = table.Column<List<string>>(type: "text[]", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notifications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Notifications_Users_UserDataId",
                        column: x => x.UserDataId,
                        principalSchema: "base",
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                schema: "base",
                table: "Tags",
                columns: new[] { "Tag", "Author", "Comments", "ExecutionDate", "Tags" },
                values: new object[,]
                {
                    { "Controller.Auth.CheckUserRights", null, null, new DateTime(2024, 6, 9, 20, 9, 10, 18, DateTimeKind.Utc).AddTicks(272), null },
                    { "Controller.Auth.Login", null, null, new DateTime(2024, 6, 9, 20, 9, 10, 18, DateTimeKind.Utc).AddTicks(205), null },
                    { "Controller.Auth.Register", null, null, new DateTime(2024, 6, 9, 20, 9, 10, 18, DateTimeKind.Utc).AddTicks(250), null },
                    { "Controller.CRUD1.Add", null, null, new DateTime(2024, 6, 9, 20, 9, 10, 18, DateTimeKind.Utc).AddTicks(16), null },
                    { "Controller.CRUD1.Delete", null, null, new DateTime(2024, 6, 9, 20, 9, 10, 18, DateTimeKind.Utc).AddTicks(90), null },
                    { "Controller.CRUD1.Get", null, null, new DateTime(2024, 6, 9, 20, 9, 10, 17, DateTimeKind.Utc).AddTicks(9976), null },
                    { "Controller.CRUD1.Update", null, null, new DateTime(2024, 6, 9, 20, 9, 10, 18, DateTimeKind.Utc).AddTicks(68), null },
                    { "Controller.Helper.GetPostsForUser", null, null, new DateTime(2024, 6, 9, 20, 9, 10, 18, DateTimeKind.Utc).AddTicks(1031), null },
                    { "Controller.Helper.GetUserProfile", null, null, new DateTime(2024, 6, 9, 20, 9, 10, 18, DateTimeKind.Utc).AddTicks(1008), null },
                    { "Controller.Helper.GetUserTags", null, null, new DateTime(2024, 6, 9, 20, 9, 10, 18, DateTimeKind.Utc).AddTicks(972), null },
                    { "Controller.Helper.UpdateCurentUserAvatar", null, null, new DateTime(2024, 6, 9, 20, 9, 10, 18, DateTimeKind.Utc).AddTicks(1053), null },
                    { "Controller.Helper.Upload", null, null, new DateTime(2024, 6, 9, 20, 9, 10, 18, DateTimeKind.Utc).AddTicks(948), null },
                    { "Role.Admin", null, null, new DateTime(2024, 6, 9, 20, 9, 10, 17, DateTimeKind.Utc).AddTicks(9302), null }
                });

            migrationBuilder.InsertData(
                schema: "base",
                table: "Users",
                columns: new[] { "Id", "Author", "Avatar", "Email", "EmailVerifiedDate", "ExecutionDate", "Name", "Password", "Signiture", "Tags", "Username" },
                values: new object[] { new Guid("cdd24812-b1f7-4a4e-b4ea-02593b066ba1"), null, null, "", null, new DateTime(2024, 6, 9, 20, 9, 10, 17, DateTimeKind.Utc).AddTicks(8829), "Test Name", "FD5CB51BAFD60F6FDBEDDE6E62C473DA6F247DB271633E15919BAB78A02EE9EB", null, new List<string> { "Dragos" }, "testUser" });

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_UserDataId",
                schema: "base",
                table: "Notifications",
                column: "UserDataId");
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
                name: "Tags",
                schema: "base");

            migrationBuilder.DropTable(
                name: "Users",
                schema: "base");
        }
    }
}
