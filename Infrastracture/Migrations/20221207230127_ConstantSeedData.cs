using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastracture.Migrations
{
    /// <inheritdoc />
    public partial class ConstantSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Constants",
                columns: new[] { "Id", "CreateDate", "Title", "Type", "Value" },
                values: new object[,]
                {
                    { 1L, new DateTime(2022, 7, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "تلفن", 0, null },
                    { 2L, new DateTime(2022, 7, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "ایمیل", 1, null },
                    { 3L, new DateTime(2022, 7, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "آدرس", 2, null },
                    { 4L, new DateTime(2022, 7, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "تعداد دفعات مجاز برای ورود ناموفق", 3, null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Constants",
                keyColumn: "Id",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                table: "Constants",
                keyColumn: "Id",
                keyValue: 2L);

            migrationBuilder.DeleteData(
                table: "Constants",
                keyColumn: "Id",
                keyValue: 3L);

            migrationBuilder.DeleteData(
                table: "Constants",
                keyColumn: "Id",
                keyValue: 4L);
        }
    }
}
