using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ReactApp1.Server.Migrations
{
    /// <inheritdoc />
    public partial class Mapping : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pacientet_AspNetUsers_UserId",
                table: "Pacientet");

            migrationBuilder.AddForeignKey(
                name: "FK_Pacientet_AspNetUsers_UserId",
                table: "Pacientet",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pacientet_AspNetUsers_UserId",
                table: "Pacientet");

            migrationBuilder.AddForeignKey(
                name: "FK_Pacientet_AspNetUsers_UserId",
                table: "Pacientet",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
