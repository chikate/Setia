using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Base.Migrations.Gov
{
    /// <inheritdoc />
    public partial class _1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ToPostId",
                schema: "gov",
                table: "Posts",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Posts_ToPostId",
                schema: "gov",
                table: "Posts",
                column: "ToPostId");

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_Posts_ToPostId",
                schema: "gov",
                table: "Posts",
                column: "ToPostId",
                principalSchema: "gov",
                principalTable: "Posts",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Posts_Posts_ToPostId",
                schema: "gov",
                table: "Posts");

            migrationBuilder.DropIndex(
                name: "IX_Posts_ToPostId",
                schema: "gov",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "ToPostId",
                schema: "gov",
                table: "Posts");
        }
    }
}
