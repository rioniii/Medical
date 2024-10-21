using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ReactApp1.Server.Migrations
{
    /// <inheritdoc />
    public partial class Pacienti : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "16e9155a-f42f-4897-a913-b7f516792b61");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e34e94ec-e0d8-4200-afa2-7dfa29ffa70e");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e8da42f8-3c92-41fb-95bc-7c2770ee4587");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
                    { "16e9155a-f42f-4897-a913-b7f516792b61", "3", "Doctor", "Doctor" },
                    { "e34e94ec-e0d8-4200-afa2-7dfa29ffa70e", "1", "Admin", "Admin" },
                    { "e8da42f8-3c92-41fb-95bc-7c2770ee4587", "2", "User", "User" }
                });
        }
    }
}
