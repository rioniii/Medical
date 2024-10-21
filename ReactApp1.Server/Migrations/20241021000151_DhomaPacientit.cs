using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ReactApp1.Server.Migrations
{
    /// <inheritdoc />
    public partial class DhomaPacientit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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
                    { "38825f45-3f37-4d45-94e1-9d73c02f9112", "2", "User", "User" },
                    { "95951639-284a-4279-a5b3-132ac9cc0d97", "1", "Admin", "Admin" },
                    { "e9c9e528-95e2-4424-89ff-afee63aabf4f", "3", "Doctor", "Doctor" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "38825f45-3f37-4d45-94e1-9d73c02f9112");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "95951639-284a-4279-a5b3-132ac9cc0d97");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e9c9e528-95e2-4424-89ff-afee63aabf4f");

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
    }
}
