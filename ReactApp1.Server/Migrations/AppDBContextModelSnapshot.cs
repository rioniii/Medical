﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ReactApp1.Server.Data.Models;

#nullable disable

namespace ReactApp1.Server.Migrations
{
    [DbContext(typeof(AppDBContext))]
    partial class AppDBContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ReactApp1.Server.Data.Models.Contact", b =>
                {
                    b.Property<int>("Contact_Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Contact_Id"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Message")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<string>("Subject")
                        .IsRequired()
                        .HasMaxLength(75)
                        .HasColumnType("nvarchar(75)");

                    b.HasKey("Contact_Id");

                    b.ToTable("Contacti");
                });

            modelBuilder.Entity("ReactApp1.Server.Data.Models.Dhomat", b =>
                {
                    b.Property<int>("Dhoma_Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Dhoma_Id"));

                    b.Property<int>("Kapaciteti")
                        .HasColumnType("int");

                    b.Property<string>("NrDhoma")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("NrPacienteve")
                        .HasColumnType("int");

                    b.Property<int>("Reparti_Id1")
                        .HasColumnType("int");

                    b.HasKey("Dhoma_Id");

                    b.HasIndex("Reparti_Id1");

                    b.ToTable("Dhomat");
                });

            modelBuilder.Entity("ReactApp1.Server.Data.Models.Faturimi", b =>
                {
                    b.Property<int>("Fatura_Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Fatura_Id"));

                    b.Property<DateTime>("Data")
                        .HasColumnType("datetime2");

                    b.Property<double>("Shuma")
                        .HasColumnType("float");

                    b.Property<string>("Statusi")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Fatura_Id");

                    b.ToTable("Faturat");
                });

            modelBuilder.Entity("ReactApp1.Server.Data.Models.Pacienti", b =>
                {
                    b.Property<int>("Patient_Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Patient_Id"));

                    b.Property<string>("Alergjite")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Dhoma_Id")
                        .HasColumnType("int");

                    b.Property<int?>("DhomatDhoma_Id")
                        .HasColumnType("int");

                    b.Property<string>("Emri")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Faturimi_Id")
                        .HasColumnType("int");

                    b.Property<string>("Gjinia")
                        .IsRequired()
                        .HasColumnType("nvarchar(1)");

                    b.Property<string>("Mbiemri")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NumriTel")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Pranimi")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Recepcionisti_Id")
                        .HasColumnType("int");

                    b.Property<int>("Termini_Id")
                        .HasColumnType("int");

                    b.Property<string>("Vendbanimi")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("VitiLindjes")
                        .HasColumnType("datetime2");

                    b.HasKey("Patient_Id");

                    b.HasIndex("DhomatDhoma_Id");

                    b.ToTable("Pacienti");
                });

            modelBuilder.Entity("ReactApp1.Server.Data.Models.Pershkrimi", b =>
                {
                    b.Property<int>("Pershkrimi_Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Pershkrimi_Id"));

                    b.Property<string>("Anamneza_Statusi")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Diagnoza")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Ekzaminimi")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Perfundimi")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Terapia")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Pershkrimi_Id");

                    b.ToTable("Pershkrimi");
                });

            modelBuilder.Entity("ReactApp1.Server.Data.Models.Repart", b =>
                {
                    b.Property<int>("Reparti_Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Reparti_Id"));

                    b.Property<string>("Emri")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Kati")
                        .HasColumnType("int");

                    b.Property<int>("NrDhomave")
                        .HasColumnType("int");

                    b.HasKey("Reparti_Id");

                    b.ToTable("Reparti");
                });

            modelBuilder.Entity("ReactApp1.Server.Data.Models.Roles", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Angazhimi")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Emri")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nderrimi")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Paga")
                        .HasColumnType("float");

                    b.Property<string>("Reparti")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("RoleEndDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("RoleStartDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Specializimi")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("ReactApp1.Server.Data.Models.Sherbimet", b =>
                {
                    b.Property<int>("Sherbimet_Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Sherbimet_Id"));

                    b.Property<double>("Cmimi")
                        .HasColumnType("float");

                    b.Property<string>("Sherbimi")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Sherbimet_Id");

                    b.ToTable("Sherbimi");
                });

            modelBuilder.Entity("ReactApp1.Server.Data.Models.Terminet", b =>
                {
                    b.Property<int>("Termini_Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Termini_Id"));

                    b.Property<DateTime>("Data")
                        .HasColumnType("datetime2");

                    b.Property<int>("PacientiPatient_Id")
                        .HasColumnType("int");

                    b.Property<int>("Pacienti_Id")
                        .HasColumnType("int");

                    b.Property<string>("Statusi")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Termini_Id");

                    b.HasIndex("PacientiPatient_Id");

                    b.ToTable("Termini");
                });

            modelBuilder.Entity("ReactApp1.Server.Data.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ConfirmPassword")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NumriKontaktues")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("ReactApp1.Server.Data.Models.UserRole", b =>
                {
                    b.Property<int>("UsersRolesId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UsersRolesId"));

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.Property<int>("RolesId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("UsersRolesId");

                    b.HasIndex("RolesId");

                    b.HasIndex("UserId");

                    b.ToTable("UsersRoless");
                });

            modelBuilder.Entity("ReactApp1.Server.Data.Models.Dhomat", b =>
                {
                    b.HasOne("ReactApp1.Server.Data.Models.Repart", "Repart")
                        .WithMany("Dhomat")
                        .HasForeignKey("Reparti_Id1")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Repart");
                });

            modelBuilder.Entity("ReactApp1.Server.Data.Models.Pacienti", b =>
                {
                    b.HasOne("ReactApp1.Server.Data.Models.Dhomat", null)
                        .WithMany("Pacientat")
                        .HasForeignKey("DhomatDhoma_Id");
                });

            modelBuilder.Entity("ReactApp1.Server.Data.Models.Terminet", b =>
                {
                    b.HasOne("ReactApp1.Server.Data.Models.Pacienti", "Pacienti")
                        .WithMany()
                        .HasForeignKey("PacientiPatient_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Pacienti");
                });

            modelBuilder.Entity("ReactApp1.Server.Data.Models.UserRole", b =>
                {
                    b.HasOne("ReactApp1.Server.Data.Models.Roles", "Roles")
                        .WithMany()
                        .HasForeignKey("RolesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ReactApp1.Server.Data.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Roles");

                    b.Navigation("User");
                });

            modelBuilder.Entity("ReactApp1.Server.Data.Models.Dhomat", b =>
                {
                    b.Navigation("Pacientat");
                });

            modelBuilder.Entity("ReactApp1.Server.Data.Models.Repart", b =>
                {
                    b.Navigation("Dhomat");
                });
#pragma warning restore 612, 618
        }
    }
}
