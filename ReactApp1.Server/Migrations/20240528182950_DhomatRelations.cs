using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ReactApp1.Server.Migrations
{
    /// <inheritdoc />
    public partial class DhomatRelations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DhomatDhoma_Id",
                table: "Pacienti",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Reparti_Id1",
                table: "Dhomat",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Pacienti_DhomatDhoma_Id",
                table: "Pacienti",
                column: "DhomatDhoma_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Dhomat_Reparti_Id1",
                table: "Dhomat",
                column: "Reparti_Id1");

            migrationBuilder.AddForeignKey(
                name: "FK_Dhomat_Reparti_Reparti_Id1",
                table: "Dhomat",
                column: "Reparti_Id1",
                principalTable: "Reparti",
                principalColumn: "Reparti_Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Pacienti_Dhomat_DhomatDhoma_Id",
                table: "Pacienti",
                column: "DhomatDhoma_Id",
                principalTable: "Dhomat",
                principalColumn: "Dhoma_Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Dhomat_Reparti_Reparti_Id1",
                table: "Dhomat");

            migrationBuilder.DropForeignKey(
                name: "FK_Pacienti_Dhomat_DhomatDhoma_Id",
                table: "Pacienti");

            migrationBuilder.DropIndex(
                name: "IX_Pacienti_DhomatDhoma_Id",
                table: "Pacienti");

            migrationBuilder.DropIndex(
                name: "IX_Dhomat_Reparti_Id1",
                table: "Dhomat");

            migrationBuilder.DropColumn(
                name: "DhomatDhoma_Id",
                table: "Pacienti");

            migrationBuilder.DropColumn(
                name: "Reparti_Id1",
                table: "Dhomat");
        }
    }
}
