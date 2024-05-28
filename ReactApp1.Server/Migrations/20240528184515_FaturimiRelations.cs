using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ReactApp1.Server.Migrations
{
    /// <inheritdoc />
    public partial class FaturimiRelations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Pershkrimi_Id",
                table: "Faturat",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Faturat_Pershkrimi_Id",
                table: "Faturat",
                column: "Pershkrimi_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Faturat_Pershkrimi_Pershkrimi_Id",
                table: "Faturat",
                column: "Pershkrimi_Id",
                principalTable: "Pershkrimi",
                principalColumn: "Pershkrimi_Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Faturat_Pershkrimi_Pershkrimi_Id",
                table: "Faturat");

            migrationBuilder.DropIndex(
                name: "IX_Faturat_Pershkrimi_Id",
                table: "Faturat");

            migrationBuilder.DropColumn(
                name: "Pershkrimi_Id",
                table: "Faturat");
        }
    }
}
