using Microsoft.EntityFrameworkCore.Migrations;

namespace ReactApp1.Server.Migrations
{
    /// <inheritdoc />
    public partial class Dhomacs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Dhomat",
                columns: table => new
                {
                    Dhoma_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NrDhoma = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Kapaciteti = table.Column<int>(type: "int", nullable: false),
                    NrPacienteve = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dhomat", x => x.Dhoma_Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Dhomat");
        }
    }
}
