using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Setia.Migrations
{
    /// <inheritdoc />
    public partial class AuditWIDEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Id_Entity",
                table: "Audit",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Id_Entity",
                table: "Audit");
        }
    }
}
