using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ReactApp1.Server.Migrations
{
    /// <inheritdoc />
    public partial class Role : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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
                    { "6971cb79-47cc-4d52-85f9-e7f7f842501d", "2", "User", "User" },
                    { "83b926be-419c-4451-994b-4df848dd12d5", "3", "Doctor", "Doctor" },
                    { "db3c815d-5641-44fa-84a0-2186a0f6fe7d", "1", "Admin", "Admin" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6971cb79-47cc-4d52-85f9-e7f7f842501d");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "83b926be-419c-4451-994b-4df848dd12d5");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "db3c815d-5641-44fa-84a0-2186a0f6fe7d");

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
    }
}
