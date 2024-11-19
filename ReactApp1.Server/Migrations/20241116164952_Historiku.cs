using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ReactApp1.Server.Migrations
{
    /// <inheritdoc />
    public partial class Historiku : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Historiks",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    MjekuId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PacientId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PacientiId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Data = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Anamneza_Statusi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ekzaminimi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Diagnoza = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Terapia = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Perfundimi = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Historiks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Historiks_Mjeket_MjekuId",
                        column: x => x.MjekuId,
                        principalTable: "Mjeket",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Historiks_Pacientet_PacientiId",
                        column: x => x.PacientiId,
                        principalTable: "Pacientet",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Historiks_MjekuId",
                table: "Historiks",
                column: "MjekuId");

            migrationBuilder.CreateIndex(
                name: "IX_Historiks_PacientiId",
                table: "Historiks",
                column: "PacientiId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Historiks");
        }
    }
}
