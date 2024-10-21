using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ReactApp1.Server.Migrations
{
    /// <inheritdoc />
    public partial class Mjeku : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Historiqet_Mjeket_MjekuId",
                table: "Historiqet");

            migrationBuilder.DropForeignKey(
                name: "FK_Historiqet_Pacientet_PacientId",
                table: "Historiqet");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Historiqet",
                table: "Historiqet");

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

            migrationBuilder.RenameTable(
                name: "Historiqet",
                newName: "Historiks");

            migrationBuilder.RenameIndex(
                name: "IX_Historiqet_PacientId",
                table: "Historiks",
                newName: "IX_Historiks_PacientId");

            migrationBuilder.RenameIndex(
                name: "IX_Historiqet_MjekuId",
                table: "Historiks",
                newName: "IX_Historiks_MjekuId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Historiks",
                table: "Historiks",
                column: "Id");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "16e9155a-f42f-4897-a913-b7f516792b61", "3", "Doctor", "Doctor" },
                    { "e34e94ec-e0d8-4200-afa2-7dfa29ffa70e", "1", "Admin", "Admin" },
                    { "e8da42f8-3c92-41fb-95bc-7c2770ee4587", "2", "User", "User" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Historiks_Mjeket_MjekuId",
                table: "Historiks",
                column: "MjekuId",
                principalTable: "Mjeket",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Historiks_Pacientet_PacientId",
                table: "Historiks",
                column: "PacientId",
                principalTable: "Pacientet",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Historiks_Mjeket_MjekuId",
                table: "Historiks");

            migrationBuilder.DropForeignKey(
                name: "FK_Historiks_Pacientet_PacientId",
                table: "Historiks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Historiks",
                table: "Historiks");

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

            migrationBuilder.RenameTable(
                name: "Historiks",
                newName: "Historiqet");

            migrationBuilder.RenameIndex(
                name: "IX_Historiks_PacientId",
                table: "Historiqet",
                newName: "IX_Historiqet_PacientId");

            migrationBuilder.RenameIndex(
                name: "IX_Historiks_MjekuId",
                table: "Historiqet",
                newName: "IX_Historiqet_MjekuId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Historiqet",
                table: "Historiqet",
                column: "Id");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "464a115c-8dff-426b-a5f4-24530fbc8dcf", "1", "Admin", "Admin" },
                    { "8aef10f4-ac67-41a2-8048-1f037c660774", "2", "User", "User" },
                    { "b069e9ec-8bc0-4350-9041-3ba8fb4aef0b", "3", "Doctor", "Doctor" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Historiqet_Mjeket_MjekuId",
                table: "Historiqet",
                column: "MjekuId",
                principalTable: "Mjeket",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Historiqet_Pacientet_PacientId",
                table: "Historiqet",
                column: "PacientId",
                principalTable: "Pacientet",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
