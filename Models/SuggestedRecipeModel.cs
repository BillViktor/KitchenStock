namespace KitchenStock.Models
{
    public class SuggestedRecipeModel
    {
        public RecipeModel Recipe { get; set; }
        public Dictionary<RecipeIngredientModel, List<StockModel>> StocksForIngredient { get; set; } = new Dictionary<RecipeIngredientModel, List<StockModel>>();

        #region Constructors
        public SuggestedRecipeModel(RecipeModel aRecipeModel)
        {
            Recipe = aRecipeModel;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Get a list of all ingredients out of stock (completely out of stock)
        /// </summary>
        /// <returns>A list of missing ingredients</returns>
        public List<RecipeIngredientModel> GetMissingIngredients()
        {
            return StocksForIngredient.Where(x => x.Value.Count == 0).Select(x => x.Key).ToList();
        }

        /// <summary>
        /// Get a list of all ingredients in full stock (enough to satisfy the recipe)
        /// </summary>
        /// <returns>A list of ingrediens in full stock</returns>
        public List<RecipeIngredientModel> GetIngredientsWithFullStock()
        {
            List<RecipeIngredientModel> sRecipeIngredients = new List<RecipeIngredientModel>();

            foreach(var sIngredient in Recipe.Ingredients)
            {
                if(IsStockEnough(sIngredient, 1) == 2)
                {
                    sRecipeIngredients.Add(sIngredient);
                }
            }

            return sRecipeIngredients;
        }

        /// <summary>
        /// Calculates if the given ingredient has no stock, partial stock or full stock
        /// </summary>
        /// <param name="aRecipeIngredient">The Ingredient to look up</param>
        /// <param name="aMultiplier">A mulitplier (if the portion count is changed)</param>
        /// <returns>0 for no stock, 1 for partial stock and 2 for full stock</returns>
        public int IsStockEnough(RecipeIngredientModel aRecipeIngredient, double aMultiplier)
        {
            //If ingredient has check stock to false, we return full stock
            if (!aRecipeIngredient.Ingredient.Checkstock) return 2;

            //Check if stock exists, if no, return 0
            if (StocksForIngredient[aRecipeIngredient].Count == 0) return 0;

            //Stock exists, get the type of measurement for the stock
            MeasurementTypeEnum sMeasurementType = aRecipeIngredient.MeasurementType;

            //Now switch depending on the stock
            switch(sMeasurementType)
            {
                case MeasurementTypeEnum.Weight:
                    //If the sum of all the stocks weight (multiplied by their percentage left), enough stock is present, if false, return 1 (partial stock)
                    if (StocksForIngredient[aRecipeIngredient].Sum(x => x.Article.WeightInGrams * (x.PercentageLeft/100)) >= aRecipeIngredient.WeightInGrams * aMultiplier)
                    {
                        return 2;
                    }
                    return 1;

                case MeasurementTypeEnum.Volume:
                    //If the sum of all the stocks volume (multiplied by their percentage left), enough stock is present, if false, return 1 (partial stock)
                    if (StocksForIngredient[aRecipeIngredient].Sum(x => x.Article.VolumeInLiters * (x.PercentageLeft / 100)) >= aRecipeIngredient.VolumeInMilliliters * aMultiplier)
                    {
                        return 2;
                    }
                    return 1;

                case MeasurementTypeEnum.Quantity:
                    //If the sum of all the stocks qty (multiplied by their percentage left), enough stock is present, if false, return 1 (partial stock)
                    if (StocksForIngredient[aRecipeIngredient].Sum(x => x.Article.QuantityInPackage * (x.PercentageLeft / 100)) >= aRecipeIngredient.QuantityInPieces * aMultiplier)
                    {
                        return 2;
                    }
                    return 1;
                
                //No good way to check this
                case MeasurementTypeEnum.Other:
                    return 2;
            }

            return 2;
        }
        #endregion
    }
}
