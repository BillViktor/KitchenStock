using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KitchenStock.Migrations
{
    /// <inheritdoc />
    public partial class RemoveUpdateDate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UpdateDate",
                table: "StockLogs");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "UpdateDate",
                table: "StockLogs",
                type: "datetime2",
                nullable: true);
        }
    }
}
