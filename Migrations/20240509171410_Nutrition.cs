using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KitchenStock.Migrations
{
    /// <inheritdoc />
    public partial class Nutrition : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RecipeIngredient");

            migrationBuilder.AddColumn<double>(
                name: "CarbsPerHundredGrams",
                table: "Ingredients",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<bool>(
                name: "Checkstock",
                table: "Ingredients",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<double>(
                name: "FatsPerHundredGrams",
                table: "Ingredients",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "KcalPerHundredGrams",
                table: "Ingredients",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "ProteinPerHundredGrams",
                table: "Ingredients",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.CreateTable(
                name: "RecipeIngredientModel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IngredientId = table.Column<int>(type: "int", nullable: false),
                    WeightInGrams = table.Column<int>(type: "int", nullable: true),
                    VolumeInMilliliters = table.Column<int>(type: "int", nullable: true),
                    QuantityInPieces = table.Column<double>(type: "float", nullable: true),
                    MeasurementDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MeasurementType = table.Column<int>(type: "int", nullable: false),
                    RecipeModelId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecipeIngredientModel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RecipeIngredientModel_Ingredients_IngredientId",
                        column: x => x.IngredientId,
                        principalTable: "Ingredients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RecipeIngredientModel_Recipes_RecipeModelId",
                        column: x => x.RecipeModelId,
                        principalTable: "Recipes",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_RecipeIngredientModel_IngredientId",
                table: "RecipeIngredientModel",
                column: "IngredientId");

            migrationBuilder.CreateIndex(
                name: "IX_RecipeIngredientModel_RecipeModelId",
                table: "RecipeIngredientModel",
                column: "RecipeModelId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RecipeIngredientModel");

            migrationBuilder.DropColumn(
                name: "CarbsPerHundredGrams",
                table: "Ingredients");

            migrationBuilder.DropColumn(
                name: "Checkstock",
                table: "Ingredients");

            migrationBuilder.DropColumn(
                name: "FatsPerHundredGrams",
                table: "Ingredients");

            migrationBuilder.DropColumn(
                name: "KcalPerHundredGrams",
                table: "Ingredients");

            migrationBuilder.DropColumn(
                name: "ProteinPerHundredGrams",
                table: "Ingredients");

            migrationBuilder.CreateTable(
                name: "RecipeIngredient",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IngredientId = table.Column<int>(type: "int", nullable: false),
                    MeasurementDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MeasurementType = table.Column<int>(type: "int", nullable: false),
                    QuantityInPieces = table.Column<double>(type: "float", nullable: true),
                    RecipeModelId = table.Column<int>(type: "int", nullable: true),
                    VolumeInMilliliters = table.Column<int>(type: "int", nullable: true),
                    WeightInGrams = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecipeIngredient", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RecipeIngredient_Ingredients_IngredientId",
                        column: x => x.IngredientId,
                        principalTable: "Ingredients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RecipeIngredient_Recipes_RecipeModelId",
                        column: x => x.RecipeModelId,
                        principalTable: "Recipes",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_RecipeIngredient_IngredientId",
                table: "RecipeIngredient",
                column: "IngredientId");

            migrationBuilder.CreateIndex(
                name: "IX_RecipeIngredient_RecipeModelId",
                table: "RecipeIngredient",
                column: "RecipeModelId");
        }
    }
}
