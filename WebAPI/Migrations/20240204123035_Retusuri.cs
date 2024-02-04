using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Setia.Migrations
{
    /// <inheritdoc />
    public partial class Retusuri : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AuthorityCode",
                table: "Users");

            migrationBuilder.AddColumn<string>(
                name: "Rights",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Rights",
                table: "Users");

            migrationBuilder.AddColumn<int>(
                name: "AuthorityCode",
                table: "Users",
                type: "int",
                nullable: true);
        }
    }
}
