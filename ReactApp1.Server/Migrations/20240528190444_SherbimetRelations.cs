using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ReactApp1.Server.Migrations
{
    /// <inheritdoc />
    public partial class SherbimetRelations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Sherbimet_Id",
                table: "Faturat",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Faturat_Sherbimet_Id",
                table: "Faturat",
                column: "Sherbimet_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Faturat_Sherbimi_Sherbimet_Id",
                table: "Faturat",
                column: "Sherbimet_Id",
                principalTable: "Sherbimi",
                principalColumn: "Sherbimet_Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Faturat_Sherbimi_Sherbimet_Id",
                table: "Faturat");

            migrationBuilder.DropIndex(
                name: "IX_Faturat_Sherbimet_Id",
                table: "Faturat");

            migrationBuilder.DropColumn(
                name: "Sherbimet_Id",
                table: "Faturat");
        }
    }
}
