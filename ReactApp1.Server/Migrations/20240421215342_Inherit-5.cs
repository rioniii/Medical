using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ReactApp1.Server.Migrations
{
    /// <inheritdoc />
    public partial class Inherit5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Staf",
                table: "Staf");

            migrationBuilder.RenameTable(
                name: "Staf",
                newName: "Stafi");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Stafi",
                table: "Stafi",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Reparti",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    kati = table.Column<int>(type: "int", nullable: false),
                    NrDhomave = table.Column<int>(type: "int", nullable: false),
                    LlojRepartit = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NrDoktoreve = table.Column<int>(type: "int", nullable: false),
                    NrAssistenteve = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reparti", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Reparti");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Stafi",
                table: "Stafi");

            migrationBuilder.RenameTable(
                name: "Stafi",
                newName: "Staf");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Staf",
                table: "Staf",
                column: "Id");
        }
    }
}
