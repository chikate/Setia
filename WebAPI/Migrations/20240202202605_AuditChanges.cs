using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Setia.Migrations
{
    /// <inheritdoc />
    public partial class AuditChanges : Migration
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
                name: "Id_CreatedBy",
                table: "Audit");

            migrationBuilder.DropColumn(
                name: "LastUpdateDate",
                table: "Audit");

            migrationBuilder.RenameColumn(
                name: "Id_LastUpdateBy",
                table: "Audit",
                newName: "Id_Executioner");

            migrationBuilder.RenameColumn(
                name: "CreationDate",
                table: "Audit",
                newName: "ExecutionDate");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id_Executioner",
                table: "Audit",
                newName: "Id_LastUpdateBy");

            migrationBuilder.RenameColumn(
                name: "ExecutionDate",
                table: "Audit",
                newName: "CreationDate");

            migrationBuilder.AddColumn<int>(
                name: "Id_CreatedBy",
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
