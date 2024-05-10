using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KitchenStock.Migrations
{
    /// <inheritdoc />
    public partial class ChangeRecipePropertyName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CookingInstructionsString",
                table: "Recipes",
                newName: "CookingInstructions");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CookingInstructions",
                table: "Recipes",
                newName: "CookingInstructionsString");
        }
    }
}
