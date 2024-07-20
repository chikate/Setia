using System;
using System.Collections.Generic;
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
                    AuthorId = table.Column<Guid>(type: "uuid", nullable: true)
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
                    AuthorId = table.Column<Guid>(type: "uuid", nullable: true),
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
                    Friends = table.Column<List<Guid>>(type: "uuid[]", nullable: true),
                    ExecutionDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    AuthorId = table.Column<Guid>(type: "uuid", nullable: true),
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
                    AuthorId = table.Column<Guid>(type: "uuid", nullable: true),
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
                columns: new[] { "Tag", "AuthorId", "Comments", "ExecutionDate", "Tags" },
                values: new object[,]
                {
                    { "Controller.Auth.CheckUserRights", null, null, new DateTime(2024, 7, 13, 15, 7, 38, 449, DateTimeKind.Utc).AddTicks(8413), null },
                    { "Controller.Auth.Login", null, null, new DateTime(2024, 7, 13, 15, 7, 38, 449, DateTimeKind.Utc).AddTicks(8335), null },
                    { "Controller.Auth.Register", null, null, new DateTime(2024, 7, 13, 15, 7, 38, 449, DateTimeKind.Utc).AddTicks(8391), null },
                    { "Controller.CRUD1.Add", null, null, new DateTime(2024, 7, 13, 15, 7, 38, 449, DateTimeKind.Utc).AddTicks(8162), null },
                    { "Controller.CRUD1.Delete", null, null, new DateTime(2024, 7, 13, 15, 7, 38, 449, DateTimeKind.Utc).AddTicks(8233), null },
                    { "Controller.CRUD1.Get", null, null, new DateTime(2024, 7, 13, 15, 7, 38, 449, DateTimeKind.Utc).AddTicks(8121), null },
                    { "Controller.CRUD1.Update", null, null, new DateTime(2024, 7, 13, 15, 7, 38, 449, DateTimeKind.Utc).AddTicks(8209), null },
                    { "Controller.Helper.AcceptFriendRequest", null, null, new DateTime(2024, 7, 13, 15, 7, 38, 449, DateTimeKind.Utc).AddTicks(8680), null },
                    { "Controller.Helper.GetPostsForUser", null, null, new DateTime(2024, 7, 13, 15, 7, 38, 449, DateTimeKind.Utc).AddTicks(8588), null },
                    { "Controller.Helper.GetUserProfile", null, null, new DateTime(2024, 7, 13, 15, 7, 38, 449, DateTimeKind.Utc).AddTicks(8565), null },
                    { "Controller.Helper.GetUserTags", null, null, new DateTime(2024, 7, 13, 15, 7, 38, 449, DateTimeKind.Utc).AddTicks(8525), null },
                    { "Controller.Helper.SendFriendRequest", null, null, new DateTime(2024, 7, 13, 15, 7, 38, 449, DateTimeKind.Utc).AddTicks(8656), null },
                    { "Controller.Helper.UpdateCurentUserAvatar", null, null, new DateTime(2024, 7, 13, 15, 7, 38, 449, DateTimeKind.Utc).AddTicks(8631), null },
                    { "Controller.Helper.Upload", null, null, new DateTime(2024, 7, 13, 15, 7, 38, 449, DateTimeKind.Utc).AddTicks(8503), null },
                    { "Controller.QuestionAnswers.GetQuestionAnswereDistribution", null, null, new DateTime(2024, 7, 13, 15, 7, 38, 449, DateTimeKind.Utc).AddTicks(8975), null },
                    { "Role.Admin", null, null, new DateTime(2024, 7, 13, 15, 7, 38, 449, DateTimeKind.Utc).AddTicks(7428), null }
                });

            migrationBuilder.InsertData(
                schema: "base",
                table: "Users",
                columns: new[] { "Id", "AuthorId", "Avatar", "Email", "EmailVerifiedDate", "ExecutionDate", "Friends", "Name", "Password", "Signiture", "Tags", "Username" },
                values: new object[] { new Guid("9ef3c815-91f9-4404-b3a2-adfc9b7792e1"), null, null, "", null, new DateTime(2024, 7, 13, 15, 7, 38, 449, DateTimeKind.Utc).AddTicks(6732), null, "Dragos", "E7CF3EF4F17C3999A94F2C6F612E8A888E5B1026878E4E19398B23BD38EC221A", null, new List<string> { "Dragos", "Admin" }, "Dragos" });

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
