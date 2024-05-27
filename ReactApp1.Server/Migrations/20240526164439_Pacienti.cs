using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ReactApp1.Server.Migrations
{
    /// <inheritdoc />
    public partial class Pacienti : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "Paga",
                table: "Roles",
                type: "float",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.CreateTable(
                name: "Pacienti",
                columns: table => new
                {
                    Patient_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Emri = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Mbiemri = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Gjinia = table.Column<string>(type: "nvarchar(1)", nullable: false),
                    VitiLindjes = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Vendbanimi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Alergjite = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NumriTel = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Pranimi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Termini_Id = table.Column<int>(type: "int", nullable: false),
                    Recepcionisti_Id = table.Column<int>(type: "int", nullable: false),
                    Faturimi_Id = table.Column<int>(type: "int", nullable: false),
                    Dhoma_Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pacienti", x => x.Patient_Id);
                });

            migrationBuilder.CreateTable(
                name: "Reparti",
                columns: table => new
                {
                    Reparti_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Emri = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Kati = table.Column<int>(type: "int", nullable: false),
                    NrDhomave = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reparti", x => x.Reparti_Id);
                });

            migrationBuilder.CreateTable(
                name: "Sherbimi",
                columns: table => new
                {
                    Sherbimet_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Sherbimi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Cmimi = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sherbimi", x => x.Sherbimet_Id);
                });

            migrationBuilder.CreateTable(
                name: "Termini",
                columns: table => new
                {
                    Termini_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Statusi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Data = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Termini", x => x.Termini_Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Pacienti");

            migrationBuilder.DropTable(
                name: "Reparti");

            migrationBuilder.DropTable(
                name: "Sherbimi");

            migrationBuilder.DropTable(
                name: "Termini");

            migrationBuilder.AlterColumn<decimal>(
                name: "Paga",
                table: "Roles",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");
        }
    }
}
