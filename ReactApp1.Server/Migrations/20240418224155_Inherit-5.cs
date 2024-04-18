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
                name: "Contacti",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Subject = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contacti", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Contacti");

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
