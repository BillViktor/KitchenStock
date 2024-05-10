namespace KitchenStock.Models
{
    public class StockTransactionRecipeModel
    {
        public bool IsEnoughStock { get; set; } = false;
        public int WeightNeededInGrams { get; set; }
        public double Multiplier { get; set; }
        public RecipeIngredientModel RecipeIngredient { get; set; }
        public Dictionary<StockModel, double> StockAndNewPercentageLeft { get; set; } = new Dictionary<StockModel, double>();

        #region Constructors
        //Default empty
        public StockTransactionRecipeModel() { } 

        /// <summary>
        /// Constructor with arguments
        /// </summary>
        /// <param name="aIngredientModel">The ingredient for this stock transaction suggestion</param>
        public StockTransactionRecipeModel(RecipeIngredientModel aRecipeIngredientModel, double aMulitplier)
        {
            RecipeIngredient = aRecipeIngredientModel;
            Multiplier = aMulitplier;
        }
        #endregion
    }
}
