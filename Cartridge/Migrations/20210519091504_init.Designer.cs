﻿// <auto-generated />
using System;
using Cartridge.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Cartridge.Migrations
{
    [DbContext(typeof(MainContext))]
    [Migration("20210519091504_init")]
    partial class init
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.6")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Cartridge.Models.Cartridges", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Code")
                        .HasColumnType("int");

                    b.Property<DateTime?>("DateDel")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateInsert")
                        .HasColumnType("datetime2");

                    b.Property<int>("ModelCartridgeId")
                        .HasColumnType("int");

                    b.Property<int?>("ModelPrinterId")
                        .HasColumnType("int");

                    b.Property<int?>("PunktId")
                        .HasColumnType("int");

                    b.Property<int?>("ServiceId")
                        .HasColumnType("int");

                    b.Property<int>("StanId")
                        .HasColumnType("int");

                    b.Property<bool>("Status")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("ModelCartridgeId");

                    b.HasIndex("ModelPrinterId");

                    b.HasIndex("PunktId");

                    b.HasIndex("ServiceId");

                    b.HasIndex("StanId");

                    b.ToTable("Cartridge");
                });

            modelBuilder.Entity("Cartridge.Models.ModelCartridge", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Baraban")
                        .HasColumnType("bit");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Photo")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("ModelCartridge");
                });

            modelBuilder.Entity("Cartridge.Models.ModelPrinter", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("ModelCartridgeId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Photo")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ModelCartridgeId");

                    b.ToTable("ModelPrinter");
                });

            modelBuilder.Entity("Cartridge.Models.OperationType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("FillDefCheck")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("PunktId")
                        .HasColumnType("int");

                    b.Property<int>("StanId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PunktId");

                    b.HasIndex("StanId");

                    b.ToTable("OperationTypes");
                });

            modelBuilder.Entity("Cartridge.Models.Operations", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("CartridgeId")
                        .HasColumnType("int");

                    b.Property<int>("CatridgeId")
                        .HasColumnType("int");

                    b.Property<DateTime>("DateOperation")
                        .HasColumnType("datetime2");

                    b.Property<int>("NextPlaceId")
                        .HasColumnType("int");

                    b.Property<int?>("NextPunktId")
                        .HasColumnType("int");

                    b.Property<int>("OperationTypeId")
                        .HasColumnType("int");

                    b.Property<int>("PrevPlaceId")
                        .HasColumnType("int");

                    b.Property<int?>("PrevPunktId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CartridgeId");

                    b.HasIndex("NextPunktId");

                    b.HasIndex("OperationTypeId");

                    b.HasIndex("PrevPunktId");

                    b.ToTable("Operation");
                });

            modelBuilder.Entity("Cartridge.Models.Punkt", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Punkt");
                });

            modelBuilder.Entity("Cartridge.Models.Service", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DateInput")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateOut")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Service");
                });

            modelBuilder.Entity("Cartridge.Models.Stan", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Stans");
                });

            modelBuilder.Entity("ModelPrinterPunkt", b =>
                {
                    b.Property<int>("PrintersId")
                        .HasColumnType("int");

                    b.Property<int>("PunktsId")
                        .HasColumnType("int");

                    b.HasKey("PrintersId", "PunktsId");

                    b.HasIndex("PunktsId");

                    b.ToTable("ModelPrinterPunkt");
                });

            modelBuilder.Entity("Cartridge.Models.Cartridges", b =>
                {
                    b.HasOne("Cartridge.Models.ModelCartridge", "GetModelCartridge")
                        .WithMany("Cartridges")
                        .HasForeignKey("ModelCartridgeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Cartridge.Models.ModelPrinter", null)
                        .WithMany("GetCartridges")
                        .HasForeignKey("ModelPrinterId");

                    b.HasOne("Cartridge.Models.Punkt", "GetPunkt")
                        .WithMany("Cartridges")
                        .HasForeignKey("PunktId");

                    b.HasOne("Cartridge.Models.Service", null)
                        .WithMany("GetCartridges")
                        .HasForeignKey("ServiceId");

                    b.HasOne("Cartridge.Models.Stan", "GetStan")
                        .WithMany()
                        .HasForeignKey("StanId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("GetModelCartridge");

                    b.Navigation("GetPunkt");

                    b.Navigation("GetStan");
                });

            modelBuilder.Entity("Cartridge.Models.ModelPrinter", b =>
                {
                    b.HasOne("Cartridge.Models.ModelCartridge", null)
                        .WithMany("Printers")
                        .HasForeignKey("ModelCartridgeId");
                });

            modelBuilder.Entity("Cartridge.Models.OperationType", b =>
                {
                    b.HasOne("Cartridge.Models.Punkt", "GetPunkt")
                        .WithMany()
                        .HasForeignKey("PunktId");

                    b.HasOne("Cartridge.Models.Stan", "GetStan")
                        .WithMany()
                        .HasForeignKey("StanId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("GetPunkt");

                    b.Navigation("GetStan");
                });

            modelBuilder.Entity("Cartridge.Models.Operations", b =>
                {
                    b.HasOne("Cartridge.Models.Cartridges", "Cartridge")
                        .WithMany("Operation")
                        .HasForeignKey("CartridgeId");

                    b.HasOne("Cartridge.Models.Punkt", "NextPunkt")
                        .WithMany()
                        .HasForeignKey("NextPunktId");

                    b.HasOne("Cartridge.Models.OperationType", "Type")
                        .WithMany()
                        .HasForeignKey("OperationTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Cartridge.Models.Punkt", "PrevPunkt")
                        .WithMany()
                        .HasForeignKey("PrevPunktId");

                    b.Navigation("Cartridge");

                    b.Navigation("NextPunkt");

                    b.Navigation("PrevPunkt");

                    b.Navigation("Type");
                });

            modelBuilder.Entity("ModelPrinterPunkt", b =>
                {
                    b.HasOne("Cartridge.Models.ModelPrinter", null)
                        .WithMany()
                        .HasForeignKey("PrintersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Cartridge.Models.Punkt", null)
                        .WithMany()
                        .HasForeignKey("PunktsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Cartridge.Models.Cartridges", b =>
                {
                    b.Navigation("Operation");
                });

            modelBuilder.Entity("Cartridge.Models.ModelCartridge", b =>
                {
                    b.Navigation("Cartridges");

                    b.Navigation("Printers");
                });

            modelBuilder.Entity("Cartridge.Models.ModelPrinter", b =>
                {
                    b.Navigation("GetCartridges");
                });

            modelBuilder.Entity("Cartridge.Models.Punkt", b =>
                {
                    b.Navigation("Cartridges");
                });

            modelBuilder.Entity("Cartridge.Models.Service", b =>
                {
                    b.Navigation("GetCartridges");
                });
#pragma warning restore 612, 618
        }
    }
}
