using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InventoryManager.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Oils",
                columns: table => new
                {
                    OilId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    BotanicalName = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    ExtractionMethod = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    PlantPart = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Oils", x => x.OilId);
                });

            migrationBuilder.CreateTable(
                name: "Providers",
                columns: table => new
                {
                    ProviderId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    ContactEmail = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    ContactPhone = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Website = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Providers", x => x.ProviderId);
                });

            migrationBuilder.CreateTable(
                name: "OilBatches",
                columns: table => new
                {
                    BatchId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OilId = table.Column<int>(type: "int", nullable: false),
                    ProviderId = table.Column<int>(type: "int", nullable: false),
                    CountryOfOrigin = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    LotNumber = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    ArrivalDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ArrivalWeight_Oz = table.Column<decimal>(type: "decimal(10,3)", nullable: false),
                    CurrentWeight_Oz = table.Column<decimal>(type: "decimal(10,3)", nullable: false),
                    Cost_USD = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    ExpirationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    StorageLocation = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Status = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false, defaultValue: "Active"),
                    COAFilePath = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "SYSUTCDATETIME()"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "SYSUTCDATETIME()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OilBatches", x => x.BatchId);
                    table.ForeignKey(
                        name: "FK_OilBatches_Oils_OilId",
                        column: x => x.OilId,
                        principalTable: "Oils",
                        principalColumn: "OilId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OilBatches_Providers_ProviderId",
                        column: x => x.ProviderId,
                        principalTable: "Providers",
                        principalColumn: "ProviderId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OilBatches_LotNumber",
                table: "OilBatches",
                column: "LotNumber");

            migrationBuilder.CreateIndex(
                name: "IX_OilBatches_OilId_Status",
                table: "OilBatches",
                columns: new[] { "OilId", "Status" });

            migrationBuilder.CreateIndex(
                name: "IX_OilBatches_ProviderId",
                table: "OilBatches",
                column: "ProviderId");

            migrationBuilder.CreateIndex(
                name: "IX_Oils_Name",
                table: "Oils",
                column: "Name",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OilBatches");

            migrationBuilder.DropTable(
                name: "Oils");

            migrationBuilder.DropTable(
                name: "Providers");
        }
    }
}
