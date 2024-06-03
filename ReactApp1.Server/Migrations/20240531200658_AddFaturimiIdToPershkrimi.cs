using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ReactApp1.Server.Migrations
{
    /// <inheritdoc />
    public partial class AddFaturimiIdToPershkrimi : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UsersRoless_Roles_RolesId",
                table: "UsersRoless");

            migrationBuilder.DropForeignKey(
                name: "FK_UsersRoless_Users_UserId",
                table: "UsersRoless");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UsersRoless",
                table: "UsersRoless");

            migrationBuilder.DropColumn(
                name: "Dhoma_Id",
                table: "Pacienti");

            migrationBuilder.DropColumn(
                name: "Emri",
                table: "Pacienti");

            migrationBuilder.DropColumn(
                name: "Faturimi_Id",
                table: "Pacienti");

            migrationBuilder.DropColumn(
                name: "Gjinia",
                table: "Pacienti");

            migrationBuilder.DropColumn(
                name: "Mbiemri",
                table: "Pacienti");

            migrationBuilder.DropColumn(
                name: "NumriTel",
                table: "Pacienti");

            migrationBuilder.DropColumn(
                name: "Recepcionisti_Id",
                table: "Pacienti");

            migrationBuilder.DropColumn(
                name: "Termini_Id",
                table: "Pacienti");

            migrationBuilder.DropColumn(
                name: "Vendbanimi",
                table: "Pacienti");

            migrationBuilder.DropColumn(
                name: "VitiLindjes",
                table: "Pacienti");

            migrationBuilder.RenameTable(
                name: "UsersRoless",
                newName: "UsersRole");

            migrationBuilder.RenameIndex(
                name: "IX_UsersRoless_UserId",
                table: "UsersRole",
                newName: "IX_UsersRole_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_UsersRoless_RolesId",
                table: "UsersRole",
                newName: "IX_UsersRole_RolesId");

            migrationBuilder.AddColumn<int>(
                name: "Faturimi_Id",
                table: "Pershkrimi",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TerminetTermini_Id",
                table: "Pershkrimi",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Pacienti",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Pershkrimi_ID",
                table: "Faturat",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Sherbimet_Id",
                table: "Faturat",
                type: "int",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_UsersRole",
                table: "UsersRole",
                column: "UsersRolesId");

            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Emri = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Mbiemri = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Ditelindja = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Gjinia = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Adresa = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Qyteti = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Shteti = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Kontakti = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Specializimi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Departmenti = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Infermieret",
                columns: table => new
                {
                    Infermier_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Reparti = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Angazhimet = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PershkrimetPershkrimi_Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Infermieret", x => x.Infermier_Id);
                    table.ForeignKey(
                        name: "FK_Infermieret_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Infermieret_Pershkrimi_PershkrimetPershkrimi_Id",
                        column: x => x.PershkrimetPershkrimi_Id,
                        principalTable: "Pershkrimi",
                        principalColumn: "Pershkrimi_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Mjekat",
                columns: table => new
                {
                    Mjeku_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Reparti = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Nderrimi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Angazhimi = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mjekat", x => x.Mjeku_Id);
                    table.ForeignKey(
                        name: "FK_Mjekat_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Pershkrimi_TerminetTermini_Id",
                table: "Pershkrimi",
                column: "TerminetTermini_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Pacienti_UserId",
                table: "Pacienti",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Faturat_Pershkrimi_ID",
                table: "Faturat",
                column: "Pershkrimi_ID",
                unique: true,
                filter: "[Pershkrimi_ID] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Faturat_Sherbimet_Id",
                table: "Faturat",
                column: "Sherbimet_Id");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Infermieret_PershkrimetPershkrimi_Id",
                table: "Infermieret",
                column: "PershkrimetPershkrimi_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Infermieret_UserId",
                table: "Infermieret",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Mjekat_UserId",
                table: "Mjekat",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Faturat_Pershkrimi_Pershkrimi_ID",
                table: "Faturat",
                column: "Pershkrimi_ID",
                principalTable: "Pershkrimi",
                principalColumn: "Pershkrimi_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Faturat_Sherbimi_Sherbimet_Id",
                table: "Faturat",
                column: "Sherbimet_Id",
                principalTable: "Sherbimi",
                principalColumn: "Sherbimet_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Pacienti_AspNetUsers_UserId",
                table: "Pacienti",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Pershkrimi_Termini_TerminetTermini_Id",
                table: "Pershkrimi",
                column: "TerminetTermini_Id",
                principalTable: "Termini",
                principalColumn: "Termini_Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UsersRole_Roles_RolesId",
                table: "UsersRole",
                column: "RolesId",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UsersRole_Users_UserId",
                table: "UsersRole",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Faturat_Pershkrimi_Pershkrimi_ID",
                table: "Faturat");

            migrationBuilder.DropForeignKey(
                name: "FK_Faturat_Sherbimi_Sherbimet_Id",
                table: "Faturat");

            migrationBuilder.DropForeignKey(
                name: "FK_Pacienti_AspNetUsers_UserId",
                table: "Pacienti");

            migrationBuilder.DropForeignKey(
                name: "FK_Pershkrimi_Termini_TerminetTermini_Id",
                table: "Pershkrimi");

            migrationBuilder.DropForeignKey(
                name: "FK_UsersRole_Roles_RolesId",
                table: "UsersRole");

            migrationBuilder.DropForeignKey(
                name: "FK_UsersRole_Users_UserId",
                table: "UsersRole");

            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "Infermieret");

            migrationBuilder.DropTable(
                name: "Mjekat");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_Pershkrimi_TerminetTermini_Id",
                table: "Pershkrimi");

            migrationBuilder.DropIndex(
                name: "IX_Pacienti_UserId",
                table: "Pacienti");

            migrationBuilder.DropIndex(
                name: "IX_Faturat_Pershkrimi_ID",
                table: "Faturat");

            migrationBuilder.DropIndex(
                name: "IX_Faturat_Sherbimet_Id",
                table: "Faturat");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UsersRole",
                table: "UsersRole");

            migrationBuilder.DropColumn(
                name: "Faturimi_Id",
                table: "Pershkrimi");

            migrationBuilder.DropColumn(
                name: "TerminetTermini_Id",
                table: "Pershkrimi");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Pacienti");

            migrationBuilder.DropColumn(
                name: "Pershkrimi_ID",
                table: "Faturat");

            migrationBuilder.DropColumn(
                name: "Sherbimet_Id",
                table: "Faturat");

            migrationBuilder.RenameTable(
                name: "UsersRole",
                newName: "UsersRoless");

            migrationBuilder.RenameIndex(
                name: "IX_UsersRole_UserId",
                table: "UsersRoless",
                newName: "IX_UsersRoless_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_UsersRole_RolesId",
                table: "UsersRoless",
                newName: "IX_UsersRoless_RolesId");

            migrationBuilder.AddColumn<int>(
                name: "Dhoma_Id",
                table: "Pacienti",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Emri",
                table: "Pacienti",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Faturimi_Id",
                table: "Pacienti",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Gjinia",
                table: "Pacienti",
                type: "nvarchar(1)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Mbiemri",
                table: "Pacienti",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "NumriTel",
                table: "Pacienti",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Recepcionisti_Id",
                table: "Pacienti",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Termini_Id",
                table: "Pacienti",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Vendbanimi",
                table: "Pacienti",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "VitiLindjes",
                table: "Pacienti",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddPrimaryKey(
                name: "PK_UsersRoless",
                table: "UsersRoless",
                column: "UsersRolesId");

            migrationBuilder.AddForeignKey(
                name: "FK_UsersRoless_Roles_RolesId",
                table: "UsersRoless",
                column: "RolesId",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UsersRoless_Users_UserId",
                table: "UsersRoless",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
