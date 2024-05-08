using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ReactApp1.Server.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Contact",
                columns: table => new
                {
                    Contact_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Subject = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contact", x => x.Contact_Id);
                });

            migrationBuilder.CreateTable(
                name: "Infermier",
                columns: table => new
                {
                    Infermier_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Emri = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Mbiemri = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Reparti = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Nderrimi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Pershkrimi_Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Infermier", x => x.Infermier_Id);
                });

            migrationBuilder.CreateTable(
                name: "Mjeku",
                columns: table => new
                {
                    Mjeku_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Emri = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Mbiemri = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Reparti = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Specializimi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Nderrimi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Angazhimi = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mjeku", x => x.Mjeku_Id);
                });

            migrationBuilder.CreateTable(
                name: "Register",
                columns: table => new
                {
                    Register_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Register", x => x.Register_Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ConfirmPassword = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Sherbimet",
                columns: table => new
                {
                    Sherbimet_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Sherbimi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Cmimi = table.Column<double>(type: "float", nullable: false),
                    Infermier_Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sherbimet", x => x.Sherbimet_Id);
                    table.ForeignKey(
                        name: "FK_Sherbimet_Infermier_Infermier_Id",
                        column: x => x.Infermier_Id,
                        principalTable: "Infermier",
                        principalColumn: "Infermier_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Faturimi",
                columns: table => new
                {
                    Fatura_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Shuma = table.Column<double>(type: "float", nullable: false),
                    Data = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Statusi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Pacienti_Id = table.Column<int>(type: "int", nullable: false),
                    Pershkrimi_ID = table.Column<int>(type: "int", nullable: false),
                    Sherbimet_Id = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Faturimi", x => x.Fatura_Id);
                    table.ForeignKey(
                        name: "FK_Faturimi_Sherbimet_Sherbimet_Id",
                        column: x => x.Sherbimet_Id,
                        principalTable: "Sherbimet",
                        principalColumn: "Sherbimet_Id");
                });

            migrationBuilder.CreateTable(
                name: "Dhoma",
                columns: table => new
                {
                    Dhoma_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NrDhoma = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Kapaciteti = table.Column<int>(type: "int", nullable: false),
                    NrPacienteve = table.Column<int>(type: "int", nullable: false),
                    Reparti_Id1 = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dhoma", x => x.Dhoma_Id);
                });

            migrationBuilder.CreateTable(
                name: "Recepcionisti",
                columns: table => new
                {
                    Recepcionisti_Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Emri = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Dhoma_Id1 = table.Column<int>(type: "int", nullable: false),
                    Mjeku_Id1 = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recepcionisti", x => x.Recepcionisti_Id);
                    table.ForeignKey(
                        name: "FK_Recepcionisti_Dhoma_Dhoma_Id1",
                        column: x => x.Dhoma_Id1,
                        principalTable: "Dhoma",
                        principalColumn: "Dhoma_Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Recepcionisti_Mjeku_Mjeku_Id1",
                        column: x => x.Mjeku_Id1,
                        principalTable: "Mjeku",
                        principalColumn: "Mjeku_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Pacienti",
                columns: table => new
                {
                    Patient_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Emri = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Mbiemri = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VitiLindjes = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Vendbanimi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Alergjite = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NumriTel = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Pranimi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Recepcionisti_Id = table.Column<int>(type: "int", nullable: false),
                    Dhoma_Id = table.Column<int>(type: "int", nullable: false),
                    Recepcionisti_Id1 = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FaturimiFatura_Id = table.Column<int>(type: "int", nullable: false),
                    Dhoma_Id1 = table.Column<int>(type: "int", nullable: true),
                    PacientiPatient_Id = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pacienti", x => x.Patient_Id);
                    table.ForeignKey(
                        name: "FK_Pacienti_Dhoma_Dhoma_Id1",
                        column: x => x.Dhoma_Id1,
                        principalTable: "Dhoma",
                        principalColumn: "Dhoma_Id");
                    table.ForeignKey(
                        name: "FK_Pacienti_Faturimi_FaturimiFatura_Id",
                        column: x => x.FaturimiFatura_Id,
                        principalTable: "Faturimi",
                        principalColumn: "Fatura_Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Pacienti_Pacienti_PacientiPatient_Id",
                        column: x => x.PacientiPatient_Id,
                        principalTable: "Pacienti",
                        principalColumn: "Patient_Id");
                    table.ForeignKey(
                        name: "FK_Pacienti_Recepcionisti_Recepcionisti_Id1",
                        column: x => x.Recepcionisti_Id1,
                        principalTable: "Recepcionisti",
                        principalColumn: "Recepcionisti_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Reparti",
                columns: table => new
                {
                    Reparti_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Emri = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Kati = table.Column<int>(type: "int", nullable: false),
                    NrDhomave = table.Column<int>(type: "int", nullable: false),
                    Mjeku_Id = table.Column<int>(type: "int", nullable: false),
                    Recepcionisti_Id = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Reparti_Id1 = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reparti", x => x.Reparti_Id);
                    table.ForeignKey(
                        name: "FK_Reparti_Recepcionisti_Recepcionisti_Id",
                        column: x => x.Recepcionisti_Id,
                        principalTable: "Recepcionisti",
                        principalColumn: "Recepcionisti_Id");
                    table.ForeignKey(
                        name: "FK_Reparti_Reparti_Reparti_Id1",
                        column: x => x.Reparti_Id1,
                        principalTable: "Reparti",
                        principalColumn: "Reparti_Id");
                });

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
                    Perfundimi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Pacient_Id = table.Column<int>(type: "int", nullable: false),
                    Faturimi_Id = table.Column<int>(type: "int", nullable: false),
                    Mjeku_Id = table.Column<int>(type: "int", nullable: false),
                    PacientiPatient_Id = table.Column<int>(type: "int", nullable: false),
                    Mjeku_Id1 = table.Column<int>(type: "int", nullable: false),
                    Infermier_Id1 = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pershkrimi", x => x.Pershkrimi_Id);
                    table.ForeignKey(
                        name: "FK_Pershkrimi_Faturimi_Faturimi_Id",
                        column: x => x.Faturimi_Id,
                        principalTable: "Faturimi",
                        principalColumn: "Fatura_Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Pershkrimi_Infermier_Infermier_Id1",
                        column: x => x.Infermier_Id1,
                        principalTable: "Infermier",
                        principalColumn: "Infermier_Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Pershkrimi_Mjeku_Mjeku_Id1",
                        column: x => x.Mjeku_Id1,
                        principalTable: "Mjeku",
                        principalColumn: "Mjeku_Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Pershkrimi_Pacienti_PacientiPatient_Id",
                        column: x => x.PacientiPatient_Id,
                        principalTable: "Pacienti",
                        principalColumn: "Patient_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Termini",
                columns: table => new
                {
                    Termini_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Statusi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Data = table.Column<int>(type: "int", nullable: false),
                    Pacienti_Id = table.Column<int>(type: "int", nullable: false),
                    PacientiPatient_Id = table.Column<int>(type: "int", nullable: false),
                    Mjeku_Id = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Termini", x => x.Termini_Id);
                    table.ForeignKey(
                        name: "FK_Termini_Mjeku_Mjeku_Id",
                        column: x => x.Mjeku_Id,
                        principalTable: "Mjeku",
                        principalColumn: "Mjeku_Id");
                    table.ForeignKey(
                        name: "FK_Termini_Pacienti_PacientiPatient_Id",
                        column: x => x.PacientiPatient_Id,
                        principalTable: "Pacienti",
                        principalColumn: "Patient_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MjekuReparti",
                columns: table => new
                {
                    MjeketMjeku_Id = table.Column<int>(type: "int", nullable: false),
                    RepartetReparti_Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MjekuReparti", x => new { x.MjeketMjeku_Id, x.RepartetReparti_Id });
                    table.ForeignKey(
                        name: "FK_MjekuReparti_Mjeku_MjeketMjeku_Id",
                        column: x => x.MjeketMjeku_Id,
                        principalTable: "Mjeku",
                        principalColumn: "Mjeku_Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MjekuReparti_Reparti_RepartetReparti_Id",
                        column: x => x.RepartetReparti_Id,
                        principalTable: "Reparti",
                        principalColumn: "Reparti_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Dhoma_Reparti_Id1",
                table: "Dhoma",
                column: "Reparti_Id1");

            migrationBuilder.CreateIndex(
                name: "IX_Faturimi_Sherbimet_Id",
                table: "Faturimi",
                column: "Sherbimet_Id");

            migrationBuilder.CreateIndex(
                name: "IX_MjekuReparti_RepartetReparti_Id",
                table: "MjekuReparti",
                column: "RepartetReparti_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Pacienti_Dhoma_Id1",
                table: "Pacienti",
                column: "Dhoma_Id1");

            migrationBuilder.CreateIndex(
                name: "IX_Pacienti_FaturimiFatura_Id",
                table: "Pacienti",
                column: "FaturimiFatura_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Pacienti_PacientiPatient_Id",
                table: "Pacienti",
                column: "PacientiPatient_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Pacienti_Recepcionisti_Id1",
                table: "Pacienti",
                column: "Recepcionisti_Id1");

            migrationBuilder.CreateIndex(
                name: "IX_Pershkrimi_Faturimi_Id",
                table: "Pershkrimi",
                column: "Faturimi_Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Pershkrimi_Infermier_Id1",
                table: "Pershkrimi",
                column: "Infermier_Id1");

            migrationBuilder.CreateIndex(
                name: "IX_Pershkrimi_Mjeku_Id1",
                table: "Pershkrimi",
                column: "Mjeku_Id1");

            migrationBuilder.CreateIndex(
                name: "IX_Pershkrimi_PacientiPatient_Id",
                table: "Pershkrimi",
                column: "PacientiPatient_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Recepcionisti_Dhoma_Id1",
                table: "Recepcionisti",
                column: "Dhoma_Id1");

            migrationBuilder.CreateIndex(
                name: "IX_Recepcionisti_Mjeku_Id1",
                table: "Recepcionisti",
                column: "Mjeku_Id1");

            migrationBuilder.CreateIndex(
                name: "IX_Reparti_Recepcionisti_Id",
                table: "Reparti",
                column: "Recepcionisti_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Reparti_Reparti_Id1",
                table: "Reparti",
                column: "Reparti_Id1");

            migrationBuilder.CreateIndex(
                name: "IX_Sherbimet_Infermier_Id",
                table: "Sherbimet",
                column: "Infermier_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Termini_Mjeku_Id",
                table: "Termini",
                column: "Mjeku_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Termini_PacientiPatient_Id",
                table: "Termini",
                column: "PacientiPatient_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Dhoma_Reparti_Reparti_Id1",
                table: "Dhoma",
                column: "Reparti_Id1",
                principalTable: "Reparti",
                principalColumn: "Reparti_Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Dhoma_Reparti_Reparti_Id1",
                table: "Dhoma");

            migrationBuilder.DropTable(
                name: "Contact");

            migrationBuilder.DropTable(
                name: "MjekuReparti");

            migrationBuilder.DropTable(
                name: "Pershkrimi");

            migrationBuilder.DropTable(
                name: "Register");

            migrationBuilder.DropTable(
                name: "Termini");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Pacienti");

            migrationBuilder.DropTable(
                name: "Faturimi");

            migrationBuilder.DropTable(
                name: "Sherbimet");

            migrationBuilder.DropTable(
                name: "Infermier");

            migrationBuilder.DropTable(
                name: "Reparti");

            migrationBuilder.DropTable(
                name: "Recepcionisti");

            migrationBuilder.DropTable(
                name: "Dhoma");

            migrationBuilder.DropTable(
                name: "Mjeku");
        }
    }
}
