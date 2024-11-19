using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ReactApp1.Server.Migrations
{
    /// <inheritdoc />
    public partial class Fatura : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Faturat",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PacientId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PacientiId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    SherbimiId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Shuma = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Data = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Paguar = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Faturat", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Faturat_Pacientet_PacientiId",
                        column: x => x.PacientiId,
                        principalTable: "Pacientet",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Faturat_Sherbimet_SherbimiId",
                        column: x => x.SherbimiId,
                        principalTable: "Sherbimet",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Faturat_PacientiId",
                table: "Faturat",
                column: "PacientiId");

            migrationBuilder.CreateIndex(
                name: "IX_Faturat_SherbimiId",
                table: "Faturat",
                column: "SherbimiId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Faturat");
        }
    }
}
