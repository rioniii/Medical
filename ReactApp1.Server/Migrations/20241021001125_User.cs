using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ReactApp1.Server.Migrations
{
    /// <inheritdoc />
    public partial class User : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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
                    { "464a115c-8dff-426b-a5f4-24530fbc8dcf", "1", "Admin", "Admin" },
                    { "8aef10f4-ac67-41a2-8048-1f037c660774", "2", "User", "User" },
                    { "b069e9ec-8bc0-4350-9041-3ba8fb4aef0b", "3", "Doctor", "Doctor" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "464a115c-8dff-426b-a5f4-24530fbc8dcf");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8aef10f4-ac67-41a2-8048-1f037c660774");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b069e9ec-8bc0-4350-9041-3ba8fb4aef0b");

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
    }
}
