using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ReactApp1.Server.Migrations
{
    /// <inheritdoc />
    public partial class TerminetRelations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PacientiPatient_Id",
                table: "Termini",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Pacienti_Id",
                table: "Termini",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Termini_PacientiPatient_Id",
                table: "Termini",
                column: "PacientiPatient_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Termini_Pacienti_PacientiPatient_Id",
                table: "Termini",
                column: "PacientiPatient_Id",
                principalTable: "Pacienti",
                principalColumn: "Patient_Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Termini_Pacienti_PacientiPatient_Id",
                table: "Termini");

            migrationBuilder.DropIndex(
                name: "IX_Termini_PacientiPatient_Id",
                table: "Termini");

            migrationBuilder.DropColumn(
                name: "PacientiPatient_Id",
                table: "Termini");

            migrationBuilder.DropColumn(
                name: "Pacienti_Id",
                table: "Termini");
        }
    }
}
