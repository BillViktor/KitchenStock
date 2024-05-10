using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KitchenStock.Migrations
{
    /// <inheritdoc />
    public partial class Logging : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "StockLogs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ArticleId = table.Column<int>(type: "int", nullable: true),
                    PreviousLocationId = table.Column<int>(type: "int", nullable: true),
                    NewLocationId = table.Column<int>(type: "int", nullable: true),
                    PreviousPercentageLeft = table.Column<int>(type: "int", nullable: true),
                    NewPercentageLeft = table.Column<int>(type: "int", nullable: true),
                    PreviousBestBeforeDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    NewBestBeforeDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StockLogs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StockLogs_Articles_ArticleId",
                        column: x => x.ArticleId,
                        principalTable: "Articles",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_StockLogs_Locations_NewLocationId",
                        column: x => x.NewLocationId,
                        principalTable: "Locations",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_StockLogs_Locations_PreviousLocationId",
                        column: x => x.PreviousLocationId,
                        principalTable: "Locations",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_StockLogs_ArticleId",
                table: "StockLogs",
                column: "ArticleId");

            migrationBuilder.CreateIndex(
                name: "IX_StockLogs_NewLocationId",
                table: "StockLogs",
                column: "NewLocationId");

            migrationBuilder.CreateIndex(
                name: "IX_StockLogs_PreviousLocationId",
                table: "StockLogs",
                column: "PreviousLocationId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StockLogs");
        }
    }
}
