using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ReactApp1.Server.Migrations
{
    /// <inheritdoc />
    public partial class PershkrimiRelations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Faturat_Pershkrimi_Pershkrimi_Id",
                table: "Faturat");

            migrationBuilder.DropIndex(
                name: "IX_Faturat_Pershkrimi_Id",
                table: "Faturat");

            migrationBuilder.RenameColumn(
                name: "Pershkrimi_Id",
                table: "Faturat",
                newName: "Pershkrimi_ID");

            migrationBuilder.AddColumn<int>(
                name: "Faturimi_Id",
                table: "Pershkrimi",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Pershkrimi_ID",
                table: "Faturat",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Faturat_Pershkrimi_ID",
                table: "Faturat",
                column: "Pershkrimi_ID",
                unique: true,
                filter: "[Pershkrimi_ID] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Faturat_Pershkrimi_Pershkrimi_ID",
                table: "Faturat",
                column: "Pershkrimi_ID",
                principalTable: "Pershkrimi",
                principalColumn: "Pershkrimi_Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Faturat_Pershkrimi_Pershkrimi_ID",
                table: "Faturat");

            migrationBuilder.DropIndex(
                name: "IX_Faturat_Pershkrimi_ID",
                table: "Faturat");

            migrationBuilder.DropColumn(
                name: "Faturimi_Id",
                table: "Pershkrimi");

            migrationBuilder.RenameColumn(
                name: "Pershkrimi_ID",
                table: "Faturat",
                newName: "Pershkrimi_Id");

            migrationBuilder.AlterColumn<int>(
                name: "Pershkrimi_Id",
                table: "Faturat",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

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
    }
}
