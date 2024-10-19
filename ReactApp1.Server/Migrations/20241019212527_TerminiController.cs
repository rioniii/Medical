using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ReactApp1.Server.Migrations
{
    /// <inheritdoc />
    public partial class TerminiController : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "10b68af0-245f-43db-9795-80dd44399958");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "cca31275-44de-4a57-a76e-6c82ffcb8766");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "edef91e5-8a87-4841-ba15-b8653715bc3a");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
                    { "10b68af0-245f-43db-9795-80dd44399958", "3", "Doctor", "Doctor" },
                    { "cca31275-44de-4a57-a76e-6c82ffcb8766", "2", "User", "User" },
                    { "edef91e5-8a87-4841-ba15-b8653715bc3a", "1", "Admin", "Admin" }
                });
        }
    }
}
