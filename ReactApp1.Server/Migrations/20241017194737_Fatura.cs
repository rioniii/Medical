
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ReactApp1.Server.Migrations
{
    /// <inheritdoc />
    public partial class Fatura : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "082ee9eb-3407-4b1d-bc3c-238998a9ff61");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4879c19f-3cc3-4c4f-8d1a-03cdc29c66fa");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9dd4b81d-fd22-4a5a-a996-c442df8d2218");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
                    { "082ee9eb-3407-4b1d-bc3c-238998a9ff61", "2", "User", "User" },
                    { "4879c19f-3cc3-4c4f-8d1a-03cdc29c66fa", "1", "Admin", "Admin" },
                    { "9dd4b81d-fd22-4a5a-a996-c442df8d2218", "3", "Doctor", "Doctor" }
                });
        }
    }
}
