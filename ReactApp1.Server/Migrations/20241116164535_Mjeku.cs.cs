using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ReactApp1.Server.Migrations
{
    /// <inheritdoc />
    public partial class Mjekucs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MjekuId",
                table: "Terminet",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Mjeket",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Specializimi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NumriLicences = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mjeket", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Mjeket_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Terminet_MjekuId",
                table: "Terminet",
                column: "MjekuId");

            migrationBuilder.CreateIndex(
                name: "IX_Mjeket_UserId",
                table: "Mjeket",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Terminet_Mjeket_MjekuId",
                table: "Terminet",
                column: "MjekuId",
                principalTable: "Mjeket",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Terminet_Mjeket_MjekuId",
                table: "Terminet");

            migrationBuilder.DropTable(
                name: "Mjeket");

            migrationBuilder.DropIndex(
                name: "IX_Terminet_MjekuId",
                table: "Terminet");

            migrationBuilder.DropColumn(
                name: "MjekuId",
                table: "Terminet");
        }
    }
}
