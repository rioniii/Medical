using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ReactApp1.Server.Migrations
{
    /// <inheritdoc />
    public partial class Sherbimi : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "031771a6-2aa2-47b2-8f1b-f21893e0f133");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "186b98a8-cdc0-49b7-8d5b-9e4ed900d56f");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c90b25ca-2554-4098-9cb9-69bb2c8d5673");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "1181980d-bee9-4bc4-9df7-c373f3966971", "2", "User", "User" },
                    { "4b0d1bc7-f9c4-4a9a-9fb9-e65ae4471ba5", "1", "Admin", "Admin" },
                    { "61909a2c-f4d4-4b06-8926-6a3516cd9c7f", "3", "Doctor", "Doctor" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1181980d-bee9-4bc4-9df7-c373f3966971");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4b0d1bc7-f9c4-4a9a-9fb9-e65ae4471ba5");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "61909a2c-f4d4-4b06-8926-6a3516cd9c7f");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "031771a6-2aa2-47b2-8f1b-f21893e0f133", "3", "Doctor", "Doctor" },
                    { "186b98a8-cdc0-49b7-8d5b-9e4ed900d56f", "1", "Admin", "Admin" },
                    { "c90b25ca-2554-4098-9cb9-69bb2c8d5673", "2", "User", "User" }
                });
        }
    }
}
