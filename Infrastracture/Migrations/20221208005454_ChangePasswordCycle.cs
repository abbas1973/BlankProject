using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastracture.Migrations
{
    /// <inheritdoc />
    public partial class ChangePasswordCycle : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ChangePasswordCycle",
                table: "Users",
                type: "int",
                nullable: true);

            migrationBuilder.InsertData(
                table: "Constants",
                columns: new[] { "Id", "CreateDate", "Title", "Type", "Value" },
                values: new object[] { 5L, new DateTime(2022, 7, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "اجبار تغییر کلمه عبور کاربر بعد از چند روز؟", 4, "60" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1L,
                column: "ChangePasswordCycle",
                value: null);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Constants",
                keyColumn: "Id",
                keyValue: 5L);

            migrationBuilder.DropColumn(
                name: "ChangePasswordCycle",
                table: "Users");
        }
    }
}
