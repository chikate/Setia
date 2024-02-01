using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Setia.Migrations
{
    /// <inheritdoc />
    public partial class AuditWoBase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Audit_Users_Id_CreatedBy",
                table: "Audit");

            migrationBuilder.DropForeignKey(
                name: "FK_Audit_Users_Id_LastUpdateBy",
                table: "Audit");

            migrationBuilder.DropIndex(
                name: "IX_Audit_Id_CreatedBy",
                table: "Audit");

            migrationBuilder.DropIndex(
                name: "IX_Audit_Id_LastUpdateBy",
                table: "Audit");

            migrationBuilder.DropColumn(
                name: "CreationDate",
                table: "Audit");

            migrationBuilder.DropColumn(
                name: "Id_CreatedBy",
                table: "Audit");

            migrationBuilder.DropColumn(
                name: "Id_LastUpdateBy",
                table: "Audit");

            migrationBuilder.DropColumn(
                name: "LastUpdateDate",
                table: "Audit");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreationDate",
                table: "Audit",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "Id_CreatedBy",
                table: "Audit",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Id_LastUpdateBy",
                table: "Audit",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastUpdateDate",
                table: "Audit",
                type: "datetime2",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Audit_Id_CreatedBy",
                table: "Audit",
                column: "Id_CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Audit_Id_LastUpdateBy",
                table: "Audit",
                column: "Id_LastUpdateBy");

            migrationBuilder.AddForeignKey(
                name: "FK_Audit_Users_Id_CreatedBy",
                table: "Audit",
                column: "Id_CreatedBy",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Audit_Users_Id_LastUpdateBy",
                table: "Audit",
                column: "Id_LastUpdateBy",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
