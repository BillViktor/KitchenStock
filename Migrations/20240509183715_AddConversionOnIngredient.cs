using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KitchenStock.Migrations
{
    /// <inheritdoc />
    public partial class AddConversionOnIngredient : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "WeightOfOneHundredMilliliters",
                table: "Ingredients",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "WeightOfOneQuantity",
                table: "Ingredients",
                type: "float",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "WeightOfOneHundredMilliliters",
                table: "Ingredients");

            migrationBuilder.DropColumn(
                name: "WeightOfOneQuantity",
                table: "Ingredients");
        }
    }
}
