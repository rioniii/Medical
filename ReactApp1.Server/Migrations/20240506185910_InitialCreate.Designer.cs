﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ReactApp1.Server.Data.Models;

#nullable disable

namespace ReactApp1.Server.Migrations
{
    [DbContext(typeof(AppDBContext))]
    [Migration("20240506185910_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("MjekuReparti", b =>
                {
                    b.Property<int>("MjeketMjeku_Id")
                        .HasColumnType("int");

                    b.Property<int>("RepartetReparti_Id")
                        .HasColumnType("int");

                    b.HasKey("MjeketMjeku_Id", "RepartetReparti_Id");

                    b.HasIndex("RepartetReparti_Id");

                    b.ToTable("MjekuReparti");
                });

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
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Subject")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Contact_Id");

                    b.ToTable("Contact");
                });

            modelBuilder.Entity("ReactApp1.Server.Data.Models.Dhoma", b =>
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

                    b.ToTable("Dhoma");
                });

            modelBuilder.Entity("ReactApp1.Server.Data.Models.Faturimi", b =>
                {
                    b.Property<int>("Fatura_Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Fatura_Id"));

                    b.Property<string>("Data")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Pacienti_Id")
                        .HasColumnType("int");

                    b.Property<int>("Pershkrimi_ID")
                        .HasColumnType("int");

                    b.Property<int?>("Sherbimet_Id")
                        .HasColumnType("int");

                    b.Property<double>("Shuma")
                        .HasColumnType("float");

                    b.Property<string>("Statusi")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Fatura_Id");

                    b.HasIndex("Sherbimet_Id");

                    b.ToTable("Faturimi");
                });

            modelBuilder.Entity("ReactApp1.Server.Data.Models.Infermier", b =>
                {
                    b.Property<int>("Infermier_Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Infermier_Id"));

                    b.Property<string>("Emri")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Mbiemri")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nderrimi")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Pershkrimi_Id")
                        .HasColumnType("int");

                    b.Property<string>("Reparti")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Infermier_Id");

                    b.ToTable("Infermier");
                });

            modelBuilder.Entity("ReactApp1.Server.Data.Models.Mjeku", b =>
                {
                    b.Property<int>("Mjeku_Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Mjeku_Id"));

                    b.Property<string>("Angazhimi")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Emri")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Mbiemri")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nderrimi")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Reparti")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Specializimi")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Mjeku_Id");

                    b.ToTable("Mjeku");
                });

            modelBuilder.Entity("ReactApp1.Server.Data.Models.Pacienti", b =>
                {
                    b.Property<int>("Patient_Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Patient_Id"));

                    b.Property<string>("Alergjite")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Dhoma_Id")
                        .HasColumnType("int");

                    b.Property<int?>("Dhoma_Id1")
                        .HasColumnType("int");

                    b.Property<string>("Emri")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("FaturimiFatura_Id")
                        .HasColumnType("int");

                    b.Property<string>("Mbiemri")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NumriTel")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("PacientiPatient_Id")
                        .HasColumnType("int");

                    b.Property<string>("Pranimi")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Recepcionisti_Id")
                        .HasColumnType("int");

                    b.Property<string>("Recepcionisti_Id1")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Vendbanimi")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("VitiLindjes")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Patient_Id");

                    b.HasIndex("Dhoma_Id1");

                    b.HasIndex("FaturimiFatura_Id");

                    b.HasIndex("PacientiPatient_Id");

                    b.HasIndex("Recepcionisti_Id1");

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

                    b.Property<int>("Faturimi_Id")
                        .HasColumnType("int");

                    b.Property<int>("Infermier_Id1")
                        .HasColumnType("int");

                    b.Property<int>("Mjeku_Id")
                        .HasColumnType("int");

                    b.Property<int>("Mjeku_Id1")
                        .HasColumnType("int");

                    b.Property<int>("Pacient_Id")
                        .HasColumnType("int");

                    b.Property<int>("PacientiPatient_Id")
                        .HasColumnType("int");

                    b.Property<string>("Perfundimi")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Terapia")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Pershkrimi_Id");

                    b.HasIndex("Faturimi_Id")
                        .IsUnique();

                    b.HasIndex("Infermier_Id1");

                    b.HasIndex("Mjeku_Id1");

                    b.HasIndex("PacientiPatient_Id");

                    b.ToTable("Pershkrimi");
                });

            modelBuilder.Entity("ReactApp1.Server.Data.Models.Recepcionisti", b =>
                {
                    b.Property<string>("Recepcionisti_Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("Dhoma_Id1")
                        .HasColumnType("int");

                    b.Property<string>("Emri")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Mjeku_Id1")
                        .HasColumnType("int");

                    b.HasKey("Recepcionisti_Id");

                    b.HasIndex("Dhoma_Id1");

                    b.HasIndex("Mjeku_Id1");

                    b.ToTable("Recepcionisti");
                });

            modelBuilder.Entity("ReactApp1.Server.Data.Models.Register", b =>
                {
                    b.Property<int>("Register_Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Register_Id"));

                    b.HasKey("Register_Id");

                    b.ToTable("Register");
                });

            modelBuilder.Entity("ReactApp1.Server.Data.Models.Reparti", b =>
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

                    b.Property<int>("Mjeku_Id")
                        .HasColumnType("int");

                    b.Property<int>("NrDhomave")
                        .HasColumnType("int");

                    b.Property<string>("Recepcionisti_Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int?>("Reparti_Id1")
                        .HasColumnType("int");

                    b.HasKey("Reparti_Id");

                    b.HasIndex("Recepcionisti_Id");

                    b.HasIndex("Reparti_Id1");

                    b.ToTable("Reparti");
                });

            modelBuilder.Entity("ReactApp1.Server.Data.Models.Sherbimet", b =>
                {
                    b.Property<int>("Sherbimet_Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Sherbimet_Id"));

                    b.Property<double>("Cmimi")
                        .HasColumnType("float");

                    b.Property<int>("Infermier_Id")
                        .HasColumnType("int");

                    b.Property<string>("Sherbimi")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Sherbimet_Id");

                    b.HasIndex("Infermier_Id");

                    b.ToTable("Sherbimet");
                });

            modelBuilder.Entity("ReactApp1.Server.Data.Models.Termini", b =>
                {
                    b.Property<int>("Termini_Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Termini_Id"));

                    b.Property<int>("Data")
                        .HasColumnType("int");

                    b.Property<int?>("Mjeku_Id")
                        .HasColumnType("int");

                    b.Property<int>("PacientiPatient_Id")
                        .HasColumnType("int");

                    b.Property<int>("Pacienti_Id")
                        .HasColumnType("int");

                    b.Property<string>("Statusi")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Termini_Id");

                    b.HasIndex("Mjeku_Id");

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

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("MjekuReparti", b =>
                {
                    b.HasOne("ReactApp1.Server.Data.Models.Mjeku", null)
                        .WithMany()
                        .HasForeignKey("MjeketMjeku_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ReactApp1.Server.Data.Models.Reparti", null)
                        .WithMany()
                        .HasForeignKey("RepartetReparti_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ReactApp1.Server.Data.Models.Dhoma", b =>
                {
                    b.HasOne("ReactApp1.Server.Data.Models.Reparti", "Reparti")
                        .WithMany("Dhomat")
                        .HasForeignKey("Reparti_Id1")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Reparti");
                });

            modelBuilder.Entity("ReactApp1.Server.Data.Models.Faturimi", b =>
                {
                    b.HasOne("ReactApp1.Server.Data.Models.Sherbimet", null)
                        .WithMany("Faturat")
                        .HasForeignKey("Sherbimet_Id");
                });

            modelBuilder.Entity("ReactApp1.Server.Data.Models.Pacienti", b =>
                {
                    b.HasOne("ReactApp1.Server.Data.Models.Dhoma", null)
                        .WithMany("Pacientat")
                        .HasForeignKey("Dhoma_Id1");

                    b.HasOne("ReactApp1.Server.Data.Models.Faturimi", "Faturimi")
                        .WithMany("Pacientat")
                        .HasForeignKey("FaturimiFatura_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ReactApp1.Server.Data.Models.Pacienti", null)
                        .WithMany("Pacientet")
                        .HasForeignKey("PacientiPatient_Id");

                    b.HasOne("ReactApp1.Server.Data.Models.Recepcionisti", "Recepcionisti")
                        .WithMany("Pacientet")
                        .HasForeignKey("Recepcionisti_Id1")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Faturimi");

                    b.Navigation("Recepcionisti");
                });

            modelBuilder.Entity("ReactApp1.Server.Data.Models.Pershkrimi", b =>
                {
                    b.HasOne("ReactApp1.Server.Data.Models.Faturimi", "Faturimet")
                        .WithOne("Pershkrimi")
                        .HasForeignKey("ReactApp1.Server.Data.Models.Pershkrimi", "Faturimi_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ReactApp1.Server.Data.Models.Infermier", "Infermier")
                        .WithMany("Pershkrimet")
                        .HasForeignKey("Infermier_Id1")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ReactApp1.Server.Data.Models.Mjeku", "Mjeku")
                        .WithMany("Pershkrimet")
                        .HasForeignKey("Mjeku_Id1")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ReactApp1.Server.Data.Models.Pacienti", "Pacienti")
                        .WithMany("Pershkrimi")
                        .HasForeignKey("PacientiPatient_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Faturimet");

                    b.Navigation("Infermier");

                    b.Navigation("Mjeku");

                    b.Navigation("Pacienti");
                });

            modelBuilder.Entity("ReactApp1.Server.Data.Models.Recepcionisti", b =>
                {
                    b.HasOne("ReactApp1.Server.Data.Models.Dhoma", "Dhoma")
                        .WithMany("Recepcionistat")
                        .HasForeignKey("Dhoma_Id1")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ReactApp1.Server.Data.Models.Mjeku", "Mjeku")
                        .WithMany("Recepcionistat")
                        .HasForeignKey("Mjeku_Id1")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Dhoma");

                    b.Navigation("Mjeku");
                });

            modelBuilder.Entity("ReactApp1.Server.Data.Models.Reparti", b =>
                {
                    b.HasOne("ReactApp1.Server.Data.Models.Recepcionisti", null)
                        .WithMany("Reparti")
                        .HasForeignKey("Recepcionisti_Id");

                    b.HasOne("ReactApp1.Server.Data.Models.Reparti", null)
                        .WithMany("Repartet")
                        .HasForeignKey("Reparti_Id1");
                });

            modelBuilder.Entity("ReactApp1.Server.Data.Models.Sherbimet", b =>
                {
                    b.HasOne("ReactApp1.Server.Data.Models.Infermier", "Infermier")
                        .WithMany()
                        .HasForeignKey("Infermier_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Infermier");
                });

            modelBuilder.Entity("ReactApp1.Server.Data.Models.Termini", b =>
                {
                    b.HasOne("ReactApp1.Server.Data.Models.Mjeku", null)
                        .WithMany("Terminet")
                        .HasForeignKey("Mjeku_Id");

                    b.HasOne("ReactApp1.Server.Data.Models.Pacienti", "Pacienti")
                        .WithMany("Terminet")
                        .HasForeignKey("PacientiPatient_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Pacienti");
                });

            modelBuilder.Entity("ReactApp1.Server.Data.Models.Dhoma", b =>
                {
                    b.Navigation("Pacientat");

                    b.Navigation("Recepcionistat");
                });

            modelBuilder.Entity("ReactApp1.Server.Data.Models.Faturimi", b =>
                {
                    b.Navigation("Pacientat");

                    b.Navigation("Pershkrimi")
                        .IsRequired();
                });

            modelBuilder.Entity("ReactApp1.Server.Data.Models.Infermier", b =>
                {
                    b.Navigation("Pershkrimet");
                });

            modelBuilder.Entity("ReactApp1.Server.Data.Models.Mjeku", b =>
                {
                    b.Navigation("Pershkrimet");

                    b.Navigation("Recepcionistat");

                    b.Navigation("Terminet");
                });

            modelBuilder.Entity("ReactApp1.Server.Data.Models.Pacienti", b =>
                {
                    b.Navigation("Pacientet");

                    b.Navigation("Pershkrimi");

                    b.Navigation("Terminet");
                });

            modelBuilder.Entity("ReactApp1.Server.Data.Models.Recepcionisti", b =>
                {
                    b.Navigation("Pacientet");

                    b.Navigation("Reparti");
                });

            modelBuilder.Entity("ReactApp1.Server.Data.Models.Reparti", b =>
                {
                    b.Navigation("Dhomat");

                    b.Navigation("Repartet");
                });

            modelBuilder.Entity("ReactApp1.Server.Data.Models.Sherbimet", b =>
                {
                    b.Navigation("Faturat");
                });
#pragma warning restore 612, 618
        }
    }
}
