using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ReactApp1.Server.Migrations
{
    /// <inheritdoc />
    public partial class Pershkrimics : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Pershkrimi",
                columns: table => new
                {
                    Pershkrimi_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Anamneza_Statusi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Ekzaminimi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Diagnoza = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Terapia = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Perfundimi = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pershkrimi", x => x.Pershkrimi_Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Pershkrimi");
        }
    }
}
