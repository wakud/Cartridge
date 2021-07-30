using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Cartridge.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ModelCartridge",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Photo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Baraban = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ModelCartridge", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Punkt",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Punkt", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Service",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateInput = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateOut = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Service", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Stans",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stans", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ModelPrinter",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Photo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModelCartridgeId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ModelPrinter", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ModelPrinter_ModelCartridge_ModelCartridgeId",
                        column: x => x.ModelCartridgeId,
                        principalTable: "ModelCartridge",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OperationTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FillDefCheck = table.Column<bool>(type: "bit", nullable: false),
                    PunktId = table.Column<int>(type: "int", nullable: true),
                    StanId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OperationTypes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OperationTypes_Punkt_PunktId",
                        column: x => x.PunktId,
                        principalTable: "Punkt",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OperationTypes_Stans_StanId",
                        column: x => x.StanId,
                        principalTable: "Stans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Cartridge",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<int>(type: "int", nullable: false),
                    DateInsert = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateDel = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    PunktId = table.Column<int>(type: "int", nullable: true),
                    ModelCartridgeId = table.Column<int>(type: "int", nullable: false),
                    StanId = table.Column<int>(type: "int", nullable: false),
                    ModelPrinterId = table.Column<int>(type: "int", nullable: true),
                    ServiceId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cartridge", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cartridge_ModelCartridge_ModelCartridgeId",
                        column: x => x.ModelCartridgeId,
                        principalTable: "ModelCartridge",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Cartridge_ModelPrinter_ModelPrinterId",
                        column: x => x.ModelPrinterId,
                        principalTable: "ModelPrinter",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Cartridge_Punkt_PunktId",
                        column: x => x.PunktId,
                        principalTable: "Punkt",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Cartridge_Service_ServiceId",
                        column: x => x.ServiceId,
                        principalTable: "Service",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Cartridge_Stans_StanId",
                        column: x => x.StanId,
                        principalTable: "Stans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ModelPrinterPunkt",
                columns: table => new
                {
                    PrintersId = table.Column<int>(type: "int", nullable: false),
                    PunktsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ModelPrinterPunkt", x => new { x.PrintersId, x.PunktsId });
                    table.ForeignKey(
                        name: "FK_ModelPrinterPunkt_ModelPrinter_PrintersId",
                        column: x => x.PrintersId,
                        principalTable: "ModelPrinter",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ModelPrinterPunkt_Punkt_PunktsId",
                        column: x => x.PunktsId,
                        principalTable: "Punkt",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Operation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateOperation = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PrevPunktId = table.Column<int>(type: "int", nullable: true),
                    NextPunktId = table.Column<int>(type: "int", nullable: true),
                    CartridgeId = table.Column<int>(type: "int", nullable: true),
                    OperationTypeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Operation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Operation_Cartridge_CartridgeId",
                        column: x => x.CartridgeId,
                        principalTable: "Cartridge",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Operation_OperationTypes_OperationTypeId",
                        column: x => x.OperationTypeId,
                        principalTable: "OperationTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Operation_Punkt_NextPunktId",
                        column: x => x.NextPunktId,
                        principalTable: "Punkt",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Operation_Punkt_PrevPunktId",
                        column: x => x.PrevPunktId,
                        principalTable: "Punkt",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cartridge_ModelCartridgeId",
                table: "Cartridge",
                column: "ModelCartridgeId");

            migrationBuilder.CreateIndex(
                name: "IX_Cartridge_ModelPrinterId",
                table: "Cartridge",
                column: "ModelPrinterId");

            migrationBuilder.CreateIndex(
                name: "IX_Cartridge_PunktId",
                table: "Cartridge",
                column: "PunktId");

            migrationBuilder.CreateIndex(
                name: "IX_Cartridge_ServiceId",
                table: "Cartridge",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_Cartridge_StanId",
                table: "Cartridge",
                column: "StanId");

            migrationBuilder.CreateIndex(
                name: "IX_ModelPrinter_ModelCartridgeId",
                table: "ModelPrinter",
                column: "ModelCartridgeId");

            migrationBuilder.CreateIndex(
                name: "IX_ModelPrinterPunkt_PunktsId",
                table: "ModelPrinterPunkt",
                column: "PunktsId");

            migrationBuilder.CreateIndex(
                name: "IX_Operation_CartridgeId",
                table: "Operation",
                column: "CartridgeId");

            migrationBuilder.CreateIndex(
                name: "IX_Operation_NextPunktId",
                table: "Operation",
                column: "NextPunktId");

            migrationBuilder.CreateIndex(
                name: "IX_Operation_OperationTypeId",
                table: "Operation",
                column: "OperationTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Operation_PrevPunktId",
                table: "Operation",
                column: "PrevPunktId");

            migrationBuilder.CreateIndex(
                name: "IX_OperationTypes_PunktId",
                table: "OperationTypes",
                column: "PunktId");

            migrationBuilder.CreateIndex(
                name: "IX_OperationTypes_StanId",
                table: "OperationTypes",
                column: "StanId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ModelPrinterPunkt");

            migrationBuilder.DropTable(
                name: "Operation");

            migrationBuilder.DropTable(
                name: "Cartridge");

            migrationBuilder.DropTable(
                name: "OperationTypes");

            migrationBuilder.DropTable(
                name: "ModelPrinter");

            migrationBuilder.DropTable(
                name: "Service");

            migrationBuilder.DropTable(
                name: "Punkt");

            migrationBuilder.DropTable(
                name: "Stans");

            migrationBuilder.DropTable(
                name: "ModelCartridge");
        }
    }
}
