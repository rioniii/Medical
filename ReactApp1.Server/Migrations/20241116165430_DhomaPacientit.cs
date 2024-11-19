using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ReactApp1.Server.Migrations
{
    /// <inheritdoc />
    public partial class DhomaPacientit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DhomaPacienteve",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PacientId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PacientiId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    DhomaId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CheckInDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CheckOutDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DhomaPacienteve", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DhomaPacienteve_Dhomat_DhomaId",
                        column: x => x.DhomaId,
                        principalTable: "Dhomat",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DhomaPacienteve_Pacientet_PacientiId",
                        column: x => x.PacientiId,
                        principalTable: "Pacientet",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_DhomaPacienteve_DhomaId",
                table: "DhomaPacienteve",
                column: "DhomaId");

            migrationBuilder.CreateIndex(
                name: "IX_DhomaPacienteve_PacientiId",
                table: "DhomaPacienteve",
                column: "PacientiId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DhomaPacienteve");
        }
    }
}
