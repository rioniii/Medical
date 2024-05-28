using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ReactApp1.Server.Migrations
{
    /// <inheritdoc />
    public partial class RepartRelations2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reparti_Reparti_Reparti_Id1",
                table: "Reparti");

            migrationBuilder.DropIndex(
                name: "IX_Reparti_Reparti_Id1",
                table: "Reparti");

            migrationBuilder.DropColumn(
                name: "Reparti_Id1",
                table: "Reparti");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Reparti_Id1",
                table: "Reparti",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Reparti_Reparti_Id1",
                table: "Reparti",
                column: "Reparti_Id1");

            migrationBuilder.AddForeignKey(
                name: "FK_Reparti_Reparti_Reparti_Id1",
                table: "Reparti",
                column: "Reparti_Id1",
                principalTable: "Reparti",
                principalColumn: "Reparti_Id");
        }
    }
}
