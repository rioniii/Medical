using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ReactApp1.Server.Migrations
{
    /// <inheritdoc />
    public partial class Roles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Emri = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Paga = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Reparti = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Specializimi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Nderrimi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Angazhimi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RoleStartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RoleEndDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Roles");
        }
    }
}
