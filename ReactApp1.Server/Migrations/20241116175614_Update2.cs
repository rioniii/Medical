using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ReactApp1.Server.Migrations
{
    /// <inheritdoc />
    public partial class Update2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DhomaPacienteve_Dhomat_DhomaId",
                table: "DhomaPacienteve");

            migrationBuilder.DropForeignKey(
                name: "FK_DhomaPacienteve_Pacientet_PacientiId",
                table: "DhomaPacienteve");

            migrationBuilder.DropForeignKey(
                name: "FK_Faturat_Pacientet_PacientiId",
                table: "Faturat");

            migrationBuilder.DropForeignKey(
                name: "FK_Faturat_Sherbimet_SherbimiId",
                table: "Faturat");

            migrationBuilder.DropForeignKey(
                name: "FK_Historiks_Mjeket_MjekuId",
                table: "Historiks");

            migrationBuilder.DropForeignKey(
                name: "FK_Historiks_Pacientet_PacientiId",
                table: "Historiks");

            migrationBuilder.DropForeignKey(
                name: "FK_Terminet_Mjeket_MjekuId",
                table: "Terminet");

            migrationBuilder.DropIndex(
                name: "IX_Terminet_MjekuId",
                table: "Terminet");

            migrationBuilder.DropIndex(
                name: "IX_Historiks_PacientiId",
                table: "Historiks");

            migrationBuilder.DropIndex(
                name: "IX_Faturat_PacientiId",
                table: "Faturat");

            migrationBuilder.DropIndex(
                name: "IX_DhomaPacienteve_PacientiId",
                table: "DhomaPacienteve");

            migrationBuilder.DropColumn(
                name: "MjekuId",
                table: "Terminet");

            migrationBuilder.DropColumn(
                name: "PacientiId",
                table: "Historiks");

            migrationBuilder.DropColumn(
                name: "PacientiId",
                table: "Faturat");

            migrationBuilder.DropColumn(
                name: "PacientiId",
                table: "DhomaPacienteve");

            migrationBuilder.AddColumn<string>(
                name: "DoktorId",
                table: "Terminet",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PacientId",
                table: "Terminet",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "PacientId",
                table: "Historiks",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "PacientId",
                table: "Faturat",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "PacientId",
                table: "DhomaPacienteve",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_Terminet_DoktorId",
                table: "Terminet",
                column: "DoktorId");

            migrationBuilder.CreateIndex(
                name: "IX_Terminet_PacientId",
                table: "Terminet",
                column: "PacientId");

            migrationBuilder.CreateIndex(
                name: "IX_Historiks_PacientId",
                table: "Historiks",
                column: "PacientId");

            migrationBuilder.CreateIndex(
                name: "IX_Faturat_PacientId",
                table: "Faturat",
                column: "PacientId");

            migrationBuilder.CreateIndex(
                name: "IX_DhomaPacienteve_PacientId",
                table: "DhomaPacienteve",
                column: "PacientId");

            migrationBuilder.AddForeignKey(
                name: "FK_DhomaPacienteve_Dhomat_DhomaId",
                table: "DhomaPacienteve",
                column: "DhomaId",
                principalTable: "Dhomat",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DhomaPacienteve_Pacientet_PacientId",
                table: "DhomaPacienteve",
                column: "PacientId",
                principalTable: "Pacientet",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Faturat_Pacientet_PacientId",
                table: "Faturat",
                column: "PacientId",
                principalTable: "Pacientet",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Faturat_Sherbimet_SherbimiId",
                table: "Faturat",
                column: "SherbimiId",
                principalTable: "Sherbimet",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Historiks_Mjeket_MjekuId",
                table: "Historiks",
                column: "MjekuId",
                principalTable: "Mjeket",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Historiks_Pacientet_PacientId",
                table: "Historiks",
                column: "PacientId",
                principalTable: "Pacientet",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Terminet_Mjeket_DoktorId",
                table: "Terminet",
                column: "DoktorId",
                principalTable: "Mjeket",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Terminet_Pacientet_PacientId",
                table: "Terminet",
                column: "PacientId",
                principalTable: "Pacientet",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DhomaPacienteve_Dhomat_DhomaId",
                table: "DhomaPacienteve");

            migrationBuilder.DropForeignKey(
                name: "FK_DhomaPacienteve_Pacientet_PacientId",
                table: "DhomaPacienteve");

            migrationBuilder.DropForeignKey(
                name: "FK_Faturat_Pacientet_PacientId",
                table: "Faturat");

            migrationBuilder.DropForeignKey(
                name: "FK_Faturat_Sherbimet_SherbimiId",
                table: "Faturat");

            migrationBuilder.DropForeignKey(
                name: "FK_Historiks_Mjeket_MjekuId",
                table: "Historiks");

            migrationBuilder.DropForeignKey(
                name: "FK_Historiks_Pacientet_PacientId",
                table: "Historiks");

            migrationBuilder.DropForeignKey(
                name: "FK_Terminet_Mjeket_DoktorId",
                table: "Terminet");

            migrationBuilder.DropForeignKey(
                name: "FK_Terminet_Pacientet_PacientId",
                table: "Terminet");

            migrationBuilder.DropIndex(
                name: "IX_Terminet_DoktorId",
                table: "Terminet");

            migrationBuilder.DropIndex(
                name: "IX_Terminet_PacientId",
                table: "Terminet");

            migrationBuilder.DropIndex(
                name: "IX_Historiks_PacientId",
                table: "Historiks");

            migrationBuilder.DropIndex(
                name: "IX_Faturat_PacientId",
                table: "Faturat");

            migrationBuilder.DropIndex(
                name: "IX_DhomaPacienteve_PacientId",
                table: "DhomaPacienteve");

            migrationBuilder.DropColumn(
                name: "DoktorId",
                table: "Terminet");

            migrationBuilder.DropColumn(
                name: "PacientId",
                table: "Terminet");

            migrationBuilder.AddColumn<string>(
                name: "MjekuId",
                table: "Terminet",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "PacientId",
                table: "Historiks",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "PacientiId",
                table: "Historiks",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "PacientId",
                table: "Faturat",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "PacientiId",
                table: "Faturat",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "PacientId",
                table: "DhomaPacienteve",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "PacientiId",
                table: "DhomaPacienteve",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Terminet_MjekuId",
                table: "Terminet",
                column: "MjekuId");

            migrationBuilder.CreateIndex(
                name: "IX_Historiks_PacientiId",
                table: "Historiks",
                column: "PacientiId");

            migrationBuilder.CreateIndex(
                name: "IX_Faturat_PacientiId",
                table: "Faturat",
                column: "PacientiId");

            migrationBuilder.CreateIndex(
                name: "IX_DhomaPacienteve_PacientiId",
                table: "DhomaPacienteve",
                column: "PacientiId");

            migrationBuilder.AddForeignKey(
                name: "FK_DhomaPacienteve_Dhomat_DhomaId",
                table: "DhomaPacienteve",
                column: "DhomaId",
                principalTable: "Dhomat",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DhomaPacienteve_Pacientet_PacientiId",
                table: "DhomaPacienteve",
                column: "PacientiId",
                principalTable: "Pacientet",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Faturat_Pacientet_PacientiId",
                table: "Faturat",
                column: "PacientiId",
                principalTable: "Pacientet",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Faturat_Sherbimet_SherbimiId",
                table: "Faturat",
                column: "SherbimiId",
                principalTable: "Sherbimet",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Historiks_Mjeket_MjekuId",
                table: "Historiks",
                column: "MjekuId",
                principalTable: "Mjeket",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Historiks_Pacientet_PacientiId",
                table: "Historiks",
                column: "PacientiId",
                principalTable: "Pacientet",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Terminet_Mjeket_MjekuId",
                table: "Terminet",
                column: "MjekuId",
                principalTable: "Mjeket",
                principalColumn: "Id");
        }
    }
}
