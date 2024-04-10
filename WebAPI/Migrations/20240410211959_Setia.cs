using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Base.Migrations
{
    /// <inheritdoc />
    public partial class Setia : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Username = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ExecutionDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AuthorId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    Deleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Username);
                    table.ForeignKey(
                        name: "FK_Users_Users_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "Users",
                        principalColumn: "Username");
                });

            migrationBuilder.CreateTable(
                name: "Audit",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Details = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Entity = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EntityId = table.Column<int>(type: "int", nullable: true),
                    Payload = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ExecutionDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AuthorId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Audit", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Audit_Users_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "Users",
                        principalColumn: "Username");
                });

            migrationBuilder.CreateTable(
                name: "Pontaj",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    BeginTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ExecutionDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AuthorId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    Deleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pontaj", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Pontaj_Users_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "Users",
                        principalColumn: "Username");
                    table.ForeignKey(
                        name: "FK_Pontaj_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Username");
                });

            migrationBuilder.CreateTable(
                name: "Tags",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ParentTagId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ExecutionDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AuthorId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    Deleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tags", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tags_Tags_ParentTagId",
                        column: x => x.ParentTagId,
                        principalTable: "Tags",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Tags_Users_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "Users",
                        principalColumn: "Username");
                });

            migrationBuilder.CreateTable(
                name: "UserTags",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Tag = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    User = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ExecutionDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AuthorId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    Deleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTags", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserTags_Users_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "Users",
                        principalColumn: "Username");
                });

            migrationBuilder.InsertData(
                table: "Tags",
                columns: new[] { "Id", "Active", "AuthorId", "Deleted", "ExecutionDate", "Name", "ParentTagId" },
                values: new object[,]
                {
                    { new Guid("3939b0a5-ac95-418a-9b1c-f4ef8069d66a"), true, null, false, new DateTime(2024, 4, 11, 0, 19, 59, 464, DateTimeKind.Local).AddTicks(4346), "Helper", null },
                    { new Guid("61758a3b-c8c2-4681-a832-4b76e5a56934"), true, null, false, new DateTime(2024, 4, 11, 0, 19, 59, 464, DateTimeKind.Local).AddTicks(4519), "Tags", null },
                    { new Guid("71c45523-ec17-4559-9540-4db4168a4243"), true, null, false, new DateTime(2024, 4, 11, 0, 19, 59, 464, DateTimeKind.Local).AddTicks(4709), "CRUD`1", null },
                    { new Guid("75cdcab4-22b9-423c-834f-acaffd2f7592"), true, null, false, new DateTime(2024, 4, 11, 0, 19, 59, 464, DateTimeKind.Local).AddTicks(4576), "Users", null },
                    { new Guid("9f507e3b-aa56-4fb7-b40a-795480c3a584"), true, null, false, new DateTime(2024, 4, 11, 0, 19, 59, 464, DateTimeKind.Local).AddTicks(4653), "UserTags", null },
                    { new Guid("ea544fcc-b71a-4aaf-856b-c5ca97d5973a"), true, null, false, new DateTime(2024, 4, 11, 0, 19, 59, 464, DateTimeKind.Local).AddTicks(4460), "Pontaj", null }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Username", "Active", "AuthorId", "Deleted", "Email", "ExecutionDate", "Name", "Password" },
                values: new object[] { "testUsername", true, null, false, "", new DateTime(2024, 4, 11, 0, 19, 59, 464, DateTimeKind.Local).AddTicks(4068), "Test Name", "testPassword" });

            migrationBuilder.InsertData(
                table: "Tags",
                columns: new[] { "Id", "Active", "AuthorId", "Deleted", "ExecutionDate", "Name", "ParentTagId" },
                values: new object[,]
                {
                    { new Guid("1376ba52-5094-4604-ae20-93e7857ffaf6"), true, null, false, new DateTime(2024, 4, 11, 0, 19, 59, 464, DateTimeKind.Local).AddTicks(4782), "Add", new Guid("71c45523-ec17-4559-9540-4db4168a4243") },
                    { new Guid("36c1feee-8870-421d-9df9-2ca2dcc27711"), true, null, false, new DateTime(2024, 4, 11, 0, 19, 59, 464, DateTimeKind.Local).AddTicks(4791), "Update", new Guid("71c45523-ec17-4559-9540-4db4168a4243") },
                    { new Guid("69170a16-b595-4d84-bab6-0158d86f3852"), true, null, false, new DateTime(2024, 4, 11, 0, 19, 59, 464, DateTimeKind.Local).AddTicks(4391), "Upload", new Guid("3939b0a5-ac95-418a-9b1c-f4ef8069d66a") },
                    { new Guid("8064bc03-6a27-43ee-9f29-5316d341a5f1"), true, null, false, new DateTime(2024, 4, 11, 0, 19, 59, 464, DateTimeKind.Local).AddTicks(4773), "Get", new Guid("71c45523-ec17-4559-9540-4db4168a4243") },
                    { new Guid("c5c1750e-bbe1-46e2-a062-12fcab5ddc33"), true, null, false, new DateTime(2024, 4, 11, 0, 19, 59, 464, DateTimeKind.Local).AddTicks(4801), "Delete", new Guid("71c45523-ec17-4559-9540-4db4168a4243") }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Audit_AuthorId",
                table: "Audit",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_Pontaj_AuthorId",
                table: "Pontaj",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_Pontaj_UserId",
                table: "Pontaj",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Tags_AuthorId",
                table: "Tags",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_Tags_ParentTagId",
                table: "Tags",
                column: "ParentTagId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_AuthorId",
                table: "Users",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_UserTags_AuthorId",
                table: "UserTags",
                column: "AuthorId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Audit");

            migrationBuilder.DropTable(
                name: "Pontaj");

            migrationBuilder.DropTable(
                name: "Tags");

            migrationBuilder.DropTable(
                name: "UserTags");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
