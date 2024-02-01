using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Setia.Migrations
{
    /// <inheritdoc />
    public partial class AuditWEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Entity",
                table: "Audit",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Entity",
                table: "Audit");
        }
    }
}
