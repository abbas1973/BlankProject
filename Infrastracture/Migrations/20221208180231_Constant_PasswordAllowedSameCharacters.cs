using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastracture.Migrations
{
    /// <inheritdoc />
    public partial class ConstantPasswordAllowedSameCharacters : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Constants",
                columns: new[] { "Id", "CreateDate", "Title", "Type", "Value" },
                values: new object[] { 6L, new DateTime(2022, 7, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "تعداد کاراکتر های تکراری مجاز در تغییر کلمه عبور", 5, "4" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Constants",
                keyColumn: "Id",
                keyValue: 6L);
        }
    }
}
