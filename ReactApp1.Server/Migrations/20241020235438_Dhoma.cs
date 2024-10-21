using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ReactApp1.Server.Migrations
{
    /// <inheritdoc />
    public partial class Dhoma : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0a64feca-8bd6-48fc-a3b6-7ea41647822e");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "63fd1c23-2419-4e40-8fff-8a01d915a5e1");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "755f7643-5dbd-404f-98ae-cc3bf55ec148");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "02aca383-ab5a-4c50-829e-21cad53996a0", "1", "Admin", "Admin" },
                    { "246b45f0-8724-4d9e-b0fb-02b120ee8294", "3", "Doctor", "Doctor" },
                    { "5de7fd96-2013-42fd-86ce-c7574f11c379", "2", "User", "User" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "02aca383-ab5a-4c50-829e-21cad53996a0");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "246b45f0-8724-4d9e-b0fb-02b120ee8294");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5de7fd96-2013-42fd-86ce-c7574f11c379");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "0a64feca-8bd6-48fc-a3b6-7ea41647822e", "2", "User", "User" },
                    { "63fd1c23-2419-4e40-8fff-8a01d915a5e1", "3", "Doctor", "Doctor" },
                    { "755f7643-5dbd-404f-98ae-cc3bf55ec148", "1", "Admin", "Admin" }
                });
        }
    }
}
