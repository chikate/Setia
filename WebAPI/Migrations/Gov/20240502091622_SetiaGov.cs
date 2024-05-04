using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Base.Migrations.Gov
{
    /// <inheritdoc />
    public partial class SetiaGov : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "gov");

            migrationBuilder.CreateTable(
                name: "UserModel",
                schema: "gov",
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
                    table.PrimaryKey("PK_UserModel", x => x.Email);
                    table.ForeignKey(
                        name: "FK_UserModel_UserModel_AuthorId",
                        column: x => x.AuthorId,
                        principalSchema: "gov",
                        principalTable: "UserModel",
                        principalColumn: "Email");
                });

            migrationBuilder.CreateTable(
                name: "Pontaj",
                schema: "gov",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    User = table.Column<string>(type: "text", nullable: true),
                    UserDataEmail = table.Column<string>(type: "text", nullable: true),
                    BeginTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    EndTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: false),
                    ExecutionDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    AuthorId = table.Column<string>(type: "text", nullable: true),
                    Active = table.Column<bool>(type: "boolean", nullable: false),
                    Deleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pontaj", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Pontaj_UserModel_AuthorId",
                        column: x => x.AuthorId,
                        principalSchema: "gov",
                        principalTable: "UserModel",
                        principalColumn: "Email");
                    table.ForeignKey(
                        name: "FK_Pontaj_UserModel_UserDataEmail",
                        column: x => x.UserDataEmail,
                        principalSchema: "gov",
                        principalTable: "UserModel",
                        principalColumn: "Email");
                });

            migrationBuilder.CreateTable(
                name: "Votes",
                schema: "gov",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Title = table.Column<string>(type: "text", nullable: false),
                    Options = table.Column<string>(type: "text", nullable: false),
                    EndOption = table.Column<string>(type: "text", nullable: true),
                    Available = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    ExecutionDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    AuthorId = table.Column<string>(type: "text", nullable: true),
                    Active = table.Column<bool>(type: "boolean", nullable: false),
                    Deleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Votes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Votes_UserModel_AuthorId",
                        column: x => x.AuthorId,
                        principalSchema: "gov",
                        principalTable: "UserModel",
                        principalColumn: "Email");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Pontaj_AuthorId",
                schema: "gov",
                table: "Pontaj",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_Pontaj_UserDataEmail",
                schema: "gov",
                table: "Pontaj",
                column: "UserDataEmail");

            migrationBuilder.CreateIndex(
                name: "IX_UserModel_AuthorId",
                schema: "gov",
                table: "UserModel",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_Votes_AuthorId",
                schema: "gov",
                table: "Votes",
                column: "AuthorId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Pontaj",
                schema: "gov");

            migrationBuilder.DropTable(
                name: "Votes",
                schema: "gov");

            migrationBuilder.DropTable(
                name: "UserModel",
                schema: "gov");
        }
    }
}
