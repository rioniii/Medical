using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ReactApp1.Server.Migrations
{
    /// <inheritdoc />
    public partial class JWT : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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
                    { "9b4951c7-08bb-4176-b485-680cfd04ede5", "1", "Admin", "Admin" },
                    { "c53530de-5058-4476-9ff0-bf919c44742e", "3", "Doctor", "Doctor" },
                    { "fd21a5f1-93cb-4573-b573-9b8a974fbc2b", "2", "User", "User" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9b4951c7-08bb-4176-b485-680cfd04ede5");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c53530de-5058-4476-9ff0-bf919c44742e");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "fd21a5f1-93cb-4573-b573-9b8a974fbc2b");

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
    }
}
