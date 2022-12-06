using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastracture.Migrations
{
    /// <inheritdoc />
    public partial class logmenus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Menus",
                columns: new[] { "Id", "Action", "Area", "Controller", "CreateDate", "Description", "HasLink", "IsEnabled", "MaterialIcon", "Parameters", "ParentId", "ShowInMenu", "Sort", "Title" },
                values: new object[] { 6L, null, null, null, new DateTime(2022, 7, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, true, "assignment", null, null, true, 10, "لاگ ها" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1L,
                column: "Type",
                value: 1);

            migrationBuilder.InsertData(
                table: "Menus",
                columns: new[] { "Id", "Action", "Area", "Controller", "CreateDate", "Description", "HasLink", "IsEnabled", "MaterialIcon", "Parameters", "ParentId", "ShowInMenu", "Sort", "Title" },
                values: new object[,]
                {
                    { 7L, "index", "logsystem", "smslogs", new DateTime(2022, 7, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), null, true, true, null, null, 6L, true, 1, "پیام های ارسالی" },
                    { 8L, "index", "logsystem", "loginlogs", new DateTime(2022, 11, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), null, true, true, null, null, 6L, true, 3, "ورود و خروج کاربران" },
                    { 9L, "index", "logsystem", "actionlogs", new DateTime(2022, 11, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), null, true, true, null, null, 6L, true, 4, "عملیات کاربران" }
                });

            migrationBuilder.InsertData(
                table: "RoleMenus",
                columns: new[] { "MenuId", "RoleId" },
                values: new object[,]
                {
                    { 6L, 1L },
                    { 7L, 1L },
                    { 8L, 1L },
                    { 9L, 1L }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "RoleMenus",
                keyColumns: new[] { "MenuId", "RoleId" },
                keyValues: new object[] { 6L, 1L });

            migrationBuilder.DeleteData(
                table: "RoleMenus",
                keyColumns: new[] { "MenuId", "RoleId" },
                keyValues: new object[] { 7L, 1L });

            migrationBuilder.DeleteData(
                table: "RoleMenus",
                keyColumns: new[] { "MenuId", "RoleId" },
                keyValues: new object[] { 8L, 1L });

            migrationBuilder.DeleteData(
                table: "RoleMenus",
                keyColumns: new[] { "MenuId", "RoleId" },
                keyValues: new object[] { 9L, 1L });

            migrationBuilder.DeleteData(
                table: "Menus",
                keyColumn: "Id",
                keyValue: 7L);

            migrationBuilder.DeleteData(
                table: "Menus",
                keyColumn: "Id",
                keyValue: 8L);

            migrationBuilder.DeleteData(
                table: "Menus",
                keyColumn: "Id",
                keyValue: 9L);

            migrationBuilder.DeleteData(
                table: "Menus",
                keyColumn: "Id",
                keyValue: 6L);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1L,
                column: "Type",
                value: 0);
        }
    }
}
