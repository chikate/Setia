using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Base.Migrations
{
    /// <inheritdoc />
    public partial class _1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "base",
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("cdd24812-b1f7-4a4e-b4ea-02593b066ba1"));

            migrationBuilder.UpdateData(
                schema: "base",
                table: "Tags",
                keyColumn: "Tag",
                keyValue: "Controller.Auth.CheckUserRights",
                column: "ExecutionDate",
                value: new DateTime(2024, 6, 10, 18, 59, 8, 731, DateTimeKind.Utc).AddTicks(4394));

            migrationBuilder.UpdateData(
                schema: "base",
                table: "Tags",
                keyColumn: "Tag",
                keyValue: "Controller.Auth.Login",
                column: "ExecutionDate",
                value: new DateTime(2024, 6, 10, 18, 59, 8, 731, DateTimeKind.Utc).AddTicks(4320));

            migrationBuilder.UpdateData(
                schema: "base",
                table: "Tags",
                keyColumn: "Tag",
                keyValue: "Controller.Auth.Register",
                column: "ExecutionDate",
                value: new DateTime(2024, 6, 10, 18, 59, 8, 731, DateTimeKind.Utc).AddTicks(4342));

            migrationBuilder.UpdateData(
                schema: "base",
                table: "Tags",
                keyColumn: "Tag",
                keyValue: "Controller.CRUD1.Add",
                column: "ExecutionDate",
                value: new DateTime(2024, 6, 10, 18, 59, 8, 731, DateTimeKind.Utc).AddTicks(4138));

            migrationBuilder.UpdateData(
                schema: "base",
                table: "Tags",
                keyColumn: "Tag",
                keyValue: "Controller.CRUD1.Delete",
                column: "ExecutionDate",
                value: new DateTime(2024, 6, 10, 18, 59, 8, 731, DateTimeKind.Utc).AddTicks(4201));

            migrationBuilder.UpdateData(
                schema: "base",
                table: "Tags",
                keyColumn: "Tag",
                keyValue: "Controller.CRUD1.Get",
                column: "ExecutionDate",
                value: new DateTime(2024, 6, 10, 18, 59, 8, 731, DateTimeKind.Utc).AddTicks(4095));

            migrationBuilder.UpdateData(
                schema: "base",
                table: "Tags",
                keyColumn: "Tag",
                keyValue: "Controller.CRUD1.Update",
                column: "ExecutionDate",
                value: new DateTime(2024, 6, 10, 18, 59, 8, 731, DateTimeKind.Utc).AddTicks(4179));

            migrationBuilder.UpdateData(
                schema: "base",
                table: "Tags",
                keyColumn: "Tag",
                keyValue: "Controller.Helper.GetPostsForUser",
                column: "ExecutionDate",
                value: new DateTime(2024, 6, 10, 18, 59, 8, 731, DateTimeKind.Utc).AddTicks(5209));

            migrationBuilder.UpdateData(
                schema: "base",
                table: "Tags",
                keyColumn: "Tag",
                keyValue: "Controller.Helper.GetUserProfile",
                column: "ExecutionDate",
                value: new DateTime(2024, 6, 10, 18, 59, 8, 731, DateTimeKind.Utc).AddTicks(5159));

            migrationBuilder.UpdateData(
                schema: "base",
                table: "Tags",
                keyColumn: "Tag",
                keyValue: "Controller.Helper.GetUserTags",
                column: "ExecutionDate",
                value: new DateTime(2024, 6, 10, 18, 59, 8, 731, DateTimeKind.Utc).AddTicks(5137));

            migrationBuilder.UpdateData(
                schema: "base",
                table: "Tags",
                keyColumn: "Tag",
                keyValue: "Controller.Helper.UpdateCurentUserAvatar",
                column: "ExecutionDate",
                value: new DateTime(2024, 6, 10, 18, 59, 8, 731, DateTimeKind.Utc).AddTicks(5232));

            migrationBuilder.UpdateData(
                schema: "base",
                table: "Tags",
                keyColumn: "Tag",
                keyValue: "Controller.Helper.Upload",
                column: "ExecutionDate",
                value: new DateTime(2024, 6, 10, 18, 59, 8, 731, DateTimeKind.Utc).AddTicks(5113));

            migrationBuilder.UpdateData(
                schema: "base",
                table: "Tags",
                keyColumn: "Tag",
                keyValue: "Role.Admin",
                column: "ExecutionDate",
                value: new DateTime(2024, 6, 10, 18, 59, 8, 731, DateTimeKind.Utc).AddTicks(3409));

            migrationBuilder.InsertData(
                schema: "base",
                table: "Users",
                columns: new[] { "Id", "Author", "Avatar", "Email", "EmailVerifiedDate", "ExecutionDate", "Name", "Password", "Signiture", "Tags", "Username" },
                values: new object[] { new Guid("0e1ebce8-ef2e-4902-9be9-85a88f1601b5"), null, null, "", null, new DateTime(2024, 6, 10, 18, 59, 8, 731, DateTimeKind.Utc).AddTicks(2933), "Test Name", "FD5CB51BAFD60F6FDBEDDE6E62C473DA6F247DB271633E15919BAB78A02EE9EB", null, new List<string> { "Dragos" }, "testUser" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "base",
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("0e1ebce8-ef2e-4902-9be9-85a88f1601b5"));

            migrationBuilder.UpdateData(
                schema: "base",
                table: "Tags",
                keyColumn: "Tag",
                keyValue: "Controller.Auth.CheckUserRights",
                column: "ExecutionDate",
                value: new DateTime(2024, 6, 9, 20, 9, 10, 18, DateTimeKind.Utc).AddTicks(272));

            migrationBuilder.UpdateData(
                schema: "base",
                table: "Tags",
                keyColumn: "Tag",
                keyValue: "Controller.Auth.Login",
                column: "ExecutionDate",
                value: new DateTime(2024, 6, 9, 20, 9, 10, 18, DateTimeKind.Utc).AddTicks(205));

            migrationBuilder.UpdateData(
                schema: "base",
                table: "Tags",
                keyColumn: "Tag",
                keyValue: "Controller.Auth.Register",
                column: "ExecutionDate",
                value: new DateTime(2024, 6, 9, 20, 9, 10, 18, DateTimeKind.Utc).AddTicks(250));

            migrationBuilder.UpdateData(
                schema: "base",
                table: "Tags",
                keyColumn: "Tag",
                keyValue: "Controller.CRUD1.Add",
                column: "ExecutionDate",
                value: new DateTime(2024, 6, 9, 20, 9, 10, 18, DateTimeKind.Utc).AddTicks(16));

            migrationBuilder.UpdateData(
                schema: "base",
                table: "Tags",
                keyColumn: "Tag",
                keyValue: "Controller.CRUD1.Delete",
                column: "ExecutionDate",
                value: new DateTime(2024, 6, 9, 20, 9, 10, 18, DateTimeKind.Utc).AddTicks(90));

            migrationBuilder.UpdateData(
                schema: "base",
                table: "Tags",
                keyColumn: "Tag",
                keyValue: "Controller.CRUD1.Get",
                column: "ExecutionDate",
                value: new DateTime(2024, 6, 9, 20, 9, 10, 17, DateTimeKind.Utc).AddTicks(9976));

            migrationBuilder.UpdateData(
                schema: "base",
                table: "Tags",
                keyColumn: "Tag",
                keyValue: "Controller.CRUD1.Update",
                column: "ExecutionDate",
                value: new DateTime(2024, 6, 9, 20, 9, 10, 18, DateTimeKind.Utc).AddTicks(68));

            migrationBuilder.UpdateData(
                schema: "base",
                table: "Tags",
                keyColumn: "Tag",
                keyValue: "Controller.Helper.GetPostsForUser",
                column: "ExecutionDate",
                value: new DateTime(2024, 6, 9, 20, 9, 10, 18, DateTimeKind.Utc).AddTicks(1031));

            migrationBuilder.UpdateData(
                schema: "base",
                table: "Tags",
                keyColumn: "Tag",
                keyValue: "Controller.Helper.GetUserProfile",
                column: "ExecutionDate",
                value: new DateTime(2024, 6, 9, 20, 9, 10, 18, DateTimeKind.Utc).AddTicks(1008));

            migrationBuilder.UpdateData(
                schema: "base",
                table: "Tags",
                keyColumn: "Tag",
                keyValue: "Controller.Helper.GetUserTags",
                column: "ExecutionDate",
                value: new DateTime(2024, 6, 9, 20, 9, 10, 18, DateTimeKind.Utc).AddTicks(972));

            migrationBuilder.UpdateData(
                schema: "base",
                table: "Tags",
                keyColumn: "Tag",
                keyValue: "Controller.Helper.UpdateCurentUserAvatar",
                column: "ExecutionDate",
                value: new DateTime(2024, 6, 9, 20, 9, 10, 18, DateTimeKind.Utc).AddTicks(1053));

            migrationBuilder.UpdateData(
                schema: "base",
                table: "Tags",
                keyColumn: "Tag",
                keyValue: "Controller.Helper.Upload",
                column: "ExecutionDate",
                value: new DateTime(2024, 6, 9, 20, 9, 10, 18, DateTimeKind.Utc).AddTicks(948));

            migrationBuilder.UpdateData(
                schema: "base",
                table: "Tags",
                keyColumn: "Tag",
                keyValue: "Role.Admin",
                column: "ExecutionDate",
                value: new DateTime(2024, 6, 9, 20, 9, 10, 17, DateTimeKind.Utc).AddTicks(9302));

            migrationBuilder.InsertData(
                schema: "base",
                table: "Users",
                columns: new[] { "Id", "Author", "Avatar", "Email", "EmailVerifiedDate", "ExecutionDate", "Name", "Password", "Signiture", "Tags", "Username" },
                values: new object[] { new Guid("cdd24812-b1f7-4a4e-b4ea-02593b066ba1"), null, null, "", null, new DateTime(2024, 6, 9, 20, 9, 10, 17, DateTimeKind.Utc).AddTicks(8829), "Test Name", "FD5CB51BAFD60F6FDBEDDE6E62C473DA6F247DB271633E15919BAB78A02EE9EB", null, new List<string> { "Dragos" }, "testUser" });
        }
    }
}
