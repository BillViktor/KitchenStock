using KitchenStock.Data;
using KitchenStock.Models;

namespace KitchenStock.Components.ViewModels
{
    public class ViewModel
    {
        #region Fields
        private readonly KitchenStockRepository mKitchenStockRepository;
        private List<string> mErrors = new List<string>();
        private List<string> mSuccessMessages = new List<string>();
        private bool mIsBusy = false;

        private List<LocationModel> mLocations = new List<LocationModel>();
        private List<ArticleModel> mArticles = new List<ArticleModel>();
        private List<StockModel> mStock = new List<StockModel>();
        private List<CategoryModel> mCategories = new List<CategoryModel>();
        private List<IngredientModel> mIngredients = new List<IngredientModel>();
        private List<RecipeModel> mRecipes = new List<RecipeModel>();
        private List<SuggestedRecipeModel> mSuggestedRecipes = new List<SuggestedRecipeModel>();
        private List<StockLogModel> mStockLogs = new List<StockLogModel>();
        private List<ShoppingModel> mShoppingList = new List<ShoppingModel>();
        #endregion

        #region Properties
        public List<string> Errors { get { return mErrors; } set { mErrors = value; } }
        public List<string> SuccessMessages { get { return mSuccessMessages; } set { mSuccessMessages = value; } }
        public bool IsBusy { get { return mIsBusy; } set { mIsBusy = value; } }

        public List<LocationModel> Locations { get {  return mLocations; } set { mLocations = value; } }
        public List<ArticleModel> Articles { get { return mArticles; } set { mArticles = value; } }
        public List<StockModel> Stock { get { return mStock; } set { mStock = value; } }
        public List<CategoryModel> Categories { get { return mCategories; } set { mCategories = value; } }
        public List<IngredientModel> Ingredients { get { return mIngredients; } set { mIngredients = value; } }
        public List<RecipeModel> Recipes { get { return mRecipes; } set { mRecipes = value; } }
        public List<SuggestedRecipeModel> SuggestedRecipes { get { return mSuggestedRecipes; } set { mSuggestedRecipes = value; } }
        public List<StockLogModel> StockLogs { get { return mStockLogs; } set { mStockLogs = value; } }
        public List<ShoppingModel> ShoppingList { get { return mShoppingList; } set { mShoppingList = value; } }
        #endregion

        public ViewModel(KitchenStockRepository aKitchenStockRepository)
        {
            mKitchenStockRepository = aKitchenStockRepository;
        }


        #region Articles
        /// <summary>
        /// Gets all ArticleModels from the repository
        /// If an exception occurs, the message is added to the list of errors, which is then displayed on the MessageComponent
        /// </summary>
        public async Task GetArticles()
        {
            IsBusy = true;

            try
            {
                mArticles = await mKitchenStockRepository.GetArticlesWithCategories();
            }
            catch(Exception ex)
            {
                mErrors.Add(ex.Message);
            }
            finally
            {
                IsBusy = false;
            }
        }

        /// <summary>
        /// Adds an ArticleModel to the repository
        /// If an exception occurs, the message is added to the list of errors, which is then displayed on the MessageComponent
        /// Success message is added on success
        /// </summary>
        /// <param name="aArticleModel">The ArticleModel to add</param>
        /// <returns>True if successfully added, false otherwise</returns>
        public async Task<bool> AddArticle(ArticleModel aArticleModel)
        {
            IsBusy = true;
            bool sSuccess = true;

            try
            {
                await mKitchenStockRepository.AddArticle(aArticleModel);

                mSuccessMessages.Add($"Successfully added {aArticleModel.Name}!");
            }
            catch (Exception ex)
            {
                sSuccess = false;
                mErrors.Add(ex.Message);
            }
            finally
            {
                IsBusy = false;
            }

            return sSuccess;
        }

        /// <summary>
        /// Updates an ArticleModel in the repository
        /// If an exception occurs, the message is added to the list of errors, which is then displayed on the MessageComponent
        /// Success message is added on success
        /// </summary>
        /// <param name="aArticleModel">The ArticleModel to update</param>
        /// <returns>True if successfully updated, false otherwise</returns>
        public async Task<bool> UpdateArticle(ArticleModel aArticleModel)
        {
            IsBusy = true;
            bool sSuccess = true;

            try
            {
                await mKitchenStockRepository.UpdateArticle(aArticleModel);

                mSuccessMessages.Add($"Successfully updates {aArticleModel.Name}!");
            }
            catch (Exception ex)
            {
                sSuccess = false;
                mErrors.Add(ex.Message);
            }
            finally
            {
                IsBusy = false;
            }

            return sSuccess;
        }

        /// <summary>
        /// Removes an ArticleModel in the repository
        /// If an exception occurs, the message is added to the list of errors, which is then displayed on the MessageComponent
        /// Success message is added on success
        /// </summary>
        /// <param name="aArticleModel">The ArticleModel to remove</param>
        /// <returns>True if successfully removed, false otherwise</returns>
        public async Task<bool> RemoveArticle(ArticleModel aArticleModel)
        {
            IsBusy = true;
            bool sSuccess = true;

            try
            {
                await mKitchenStockRepository.RemoveArticle(aArticleModel);

                mSuccessMessages.Add($"Successfully removed {aArticleModel.Name}!");
            }
            catch (Exception ex)
            {
                sSuccess = false;
                mErrors.Add(ex.Message);
            }
            finally
            {
                IsBusy = false;
            }

            return sSuccess;
        }
        #endregion


        #region Categories
        /// <summary>
        /// Gets all CategoryModels from the repository
        /// If an exception occurs, the message is added to the list of errors, which is then displayed on the MessageComponent
        /// </summary>
        public async Task GetCategories()
        {
            IsBusy = true;

            try
            {
                mCategories = await mKitchenStockRepository.GetCategories();
            }
            catch (Exception ex)
            {
                mErrors.Add(ex.Message);
            }
            finally
            {
                IsBusy = false;
            }
        }

        /// <summary>
        /// Adds a CategoryModel in the repository
        /// If an exception occurs, the message is added to the list of errors, which is then displayed on the MessageComponent
        /// Success message is added on success
        /// </summary>
        /// <param name="aCategoryModel">The CategoryModel to add</param>
        /// <returns>True if successfully added, false otherwise</returns>
        public async Task<bool> AddCategory(CategoryModel aCategoryModel)
        {
            IsBusy = true;
            bool sSuccess = true;

            try
            {
                await mKitchenStockRepository.AddCategory(aCategoryModel);

                mSuccessMessages.Add($"Successfully added category \"{aCategoryModel.Name}\"!");
            }
            catch (Exception ex)
            {
                sSuccess = false;
                mErrors.Add(ex.Message);
            }
            finally
            {
                IsBusy = false;
            }

            return sSuccess;
        }

        /// <summary>
        /// Updates a CategoryModel in the repository
        /// If an exception occurs, the message is added to the list of errors, which is then displayed on the MessageComponent
        /// Success message is added on success
        /// </summary>
        /// <param name="aCategoryModel">The CategoryModel to update</param>
        /// <returns>True if successfully updated, false otherwise</returns>
        public async Task<bool> UpdateCategory(CategoryModel aCategoryModel)
        {
            IsBusy = true;
            bool sSuccess = true;

            try
            {
                await mKitchenStockRepository.UpdateCategory(aCategoryModel);

                mSuccessMessages.Add($"Successfully updated category \"{aCategoryModel.Name}\"!");
            }
            catch (Exception ex)
            {
                sSuccess = false;
                mErrors.Add(ex.Message);
            }
            finally
            {
                IsBusy = false;
            }

            return sSuccess;
        }

        /// <summary>
        /// Removes a CategoryModel in the repository
        /// If an exception occurs, the message is added to the list of errors, which is then displayed on the MessageComponent
        /// Success message is added on success
        /// </summary>
        /// <param name="aCategoryModel">The CategoryModel to remove</param>
        /// <returns>True if successfully removed, false otherwise</returns>
        public async Task<bool> RemoveCategory(CategoryModel aCategoryModel)
        {
            IsBusy = true;
            bool sSuccess = true;

            try
            {
                await mKitchenStockRepository.RemoveCategory(aCategoryModel);

                mSuccessMessages.Add($"Successfully removed category \"{aCategoryModel.Name}\"");
            }
            catch (Exception ex)
            {
                sSuccess = false;
                mErrors.Add(ex.Message);
            }
            finally
            {
                IsBusy = false;
            }

            return sSuccess;
        }
        #endregion


        #region Ingredients
        /// <summary>
        /// Gets all IngredientModels from the repository
        /// If an exception occurs, the message is added to the list of errors, which is then displayed on the MessageComponent
        /// </summary>
        public async Task GetIngredients()
        {
            IsBusy = true;

            try
            {
                mIngredients = await mKitchenStockRepository.GetIngredients();
            }
            catch (Exception ex)
            {
                mErrors.Add(ex.Message);
            }
            finally
            {
                IsBusy = false;
            }
        }

        /// <summary>
        /// Adds an IngredientModel to the repository
        /// If an exception occurs, the message is added to the list of errors, which is then displayed on the MessageComponent
        /// Success message is added on success
        /// </summary>
        /// <param name="aIngredientModel">The IngredientModel to add</param>
        /// <returns>True if successfully added, false otherwise</returns>
        public async Task<bool> AddIngredient(IngredientModel aIngredientModel)
        {
            IsBusy = true;
            bool sSuccess = true;

            try
            {
                await mKitchenStockRepository.AddIngredient(aIngredientModel);

                mSuccessMessages.Add($"Successfully added {aIngredientModel.Name}!");
            }
            catch (Exception ex)
            {
                sSuccess = false;
                mErrors.Add(ex.Message);
            }
            finally
            {
                IsBusy = false;
            }

            return sSuccess;
        }

        /// <summary>
        /// Updates an IngredientModel in the repository
        /// If an exception occurs, the message is added to the list of errors, which is then displayed on the MessageComponent
        /// Success message is added on success
        /// </summary>
        /// <param name="aIngredientModel">The IngredientModel to update</param>
        /// <returns>True if successfully updated, false otherwise</returns>
        public async Task<bool> UpdateIngredient(IngredientModel aIngredientModel)
        {
            IsBusy = true;
            bool sSuccess = true;

            try
            {
                await mKitchenStockRepository.UpdateIngredient(aIngredientModel);

                mSuccessMessages.Add($"Successfully updates {aIngredientModel.Name}!");
            }
            catch (Exception ex)
            {
                sSuccess = false;
                mErrors.Add(ex.Message);
            }
            finally
            {
                IsBusy = false;
            }

            return sSuccess;
        }

        /// <summary>
        /// Removes an IngredientModel in the repository
        /// If an exception occurs, the message is added to the list of errors, which is then displayed on the MessageComponent
        /// Success message is added on success
        /// </summary>
        /// <param name="aIngredientModel">The IngredientModel to remove</param>
        /// <returns>True if successfully removed, false otherwise</returns>
        public async Task<bool> RemoveIngredient(IngredientModel aIngredientModel)
        {
            IsBusy = true;
            bool sSuccess = true;

            try
            {
                await mKitchenStockRepository.RemoveIngredient(aIngredientModel);

                mSuccessMessages.Add($"Successfully removed {aIngredientModel.Name}!");
            }
            catch (Exception ex)
            {
                sSuccess = false;
                mErrors.Add(ex.Message);
            }
            finally
            {
                IsBusy = false;
            }

            return sSuccess;
        }
        #endregion


        #region Locations
        /// <summary>
        /// Gets all Locations from the repository
        /// If an exception occurs, the message is added to the list of errors, which is then displayed on the MessageComponent
        /// </summary>
        public async Task GetLocations()
        {
            IsBusy = true;

            try
            {
                mLocations = await mKitchenStockRepository.GetLocations();
            }
            catch (Exception ex)
            {
                mErrors.Add(ex.Message);
            }
            finally
            {
                IsBusy = false;
            }
        }

        /// <summary>
        /// Adds a LocationModel in the repository
        /// If an exception occurs, the message is added to the list of errors, which is then displayed on the MessageComponent
        /// Success message is added on success
        /// </summary>
        /// <param name="aLocationModel">The LocationModel to update</param>
        /// <returns>True if successfully added, false otherwise</returns>
        public async Task<bool> AddLocation(LocationModel aLocationModel)
        {
            IsBusy = true;
            bool sSuccess = true;
            await Task.Delay(1000);

            try
            {
                await mKitchenStockRepository.AddLocation(aLocationModel);

                mSuccessMessages.Add($"Successfully added {aLocationModel.Name}!");
            }
            catch(Exception ex)
            {
                sSuccess = false;
                mErrors.Add(ex.Message);
            }
            finally
            {
                IsBusy = false;
            }

            return sSuccess;
        }

        /// <summary>
        /// Updates a LocationModel in the repository
        /// If an exception occurs, the message is added to the list of errors, which is then displayed on the MessageComponent
        /// Success message is added on success
        /// </summary>
        /// <param name="aLocationModel">The LocationModel to update</param>
        /// <returns>True if successfully updated, false otherwise</returns>
        public async Task<bool> UpdateLocation(LocationModel aLocationModel)
        {
            IsBusy = true;
            bool sSuccess = true;

            try
            {
                await mKitchenStockRepository.UpdateLocation(aLocationModel);

                mSuccessMessages.Add($"Successfully updated {aLocationModel.Name}!");
            }
            catch(Exception ex)
            {
                sSuccess = false;
                mErrors.Add(ex.Message);
            }
            finally
            {
                IsBusy = false;
            }

            return sSuccess;
        }

        /// <summary>
        /// Removes a LocationModel in the repository
        /// If an exception occurs, the message is added to the list of errors, which is then displayed on the MessageComponent
        /// Success message is added on success
        /// </summary>
        /// <param name="aLocationModel">The LocationModel to remove</param>
        /// <returns>True if successfully remove, false otherwise</returns>
        public async Task<bool> RemoveLocation(LocationModel aLocationModel)
        {
            IsBusy = true;
            bool sSuccess = true;

            try
            {
                await mKitchenStockRepository.RemoveLocation(aLocationModel);

                mSuccessMessages.Add($"Successfully removed {aLocationModel.Name}!");
            }
            catch (Exception ex)
            {
                sSuccess = false;
                mErrors.Add(ex.Message);
            }
            finally
            {
                IsBusy = false;
            }

            return sSuccess;
        }
        #endregion


        #region Recipes
        /// <summary>
        /// Gets all RecipeModels from the repository
        /// If an exception occurs, the message is added to the list of errors, which is then displayed on the MessageComponent
        /// </summary>
        public async Task GetRecipes()
        {
            IsBusy = true;

            try
            {
                mRecipes = await mKitchenStockRepository.GetRecipes();
            }
            catch (Exception ex)
            {
                mErrors.Add(ex.Message);
            }
            finally
            {
                IsBusy = false;
            }
        }

        /// <summary>
        /// Adds an RecipeModel to the repository
        /// If an exception occurs, the message is added to the list of errors, which is then displayed on the MessageComponent
        /// Success message is added on success
        /// </summary>
        /// <param name="aRecipeModel">The RecipeModel to add</param>
        /// <returns>True if successfully added, false otherwise</returns>
        public async Task<bool> AddRecipe(RecipeModel aRecipeModel)
        {
            IsBusy = true;
            bool sSuccess = true;

            try
            {
                await mKitchenStockRepository.AddRecipe(aRecipeModel);

                mSuccessMessages.Add($"Successfully added {aRecipeModel.Name}!");
            }
            catch (Exception ex)
            {
                sSuccess = false;
                mErrors.Add(ex.Message);
            }
            finally
            {
                IsBusy = false;
            }

            return sSuccess;
        }

        /// <summary>
        /// Updates an RecipeModel in the repository
        /// If an exception occurs, the message is added to the list of errors, which is then displayed on the MessageComponent
        /// Success message is added on success
        /// </summary>
        /// <param name="aRecipeModel">The RecipeModel to update</param>
        /// <returns>True if successfully updated, false otherwise</returns>
        public async Task<bool> UpdateRecipe(RecipeModel aRecipeModel)
        {
            IsBusy = true;
            bool sSuccess = true;

            try
            {
                await mKitchenStockRepository.UpdateRecipe(aRecipeModel);

                mSuccessMessages.Add($"Successfully updates {aRecipeModel.Name}!");
            }
            catch (Exception ex)
            {
                sSuccess = false;
                mErrors.Add(ex.Message);
            }
            finally
            {
                IsBusy = false;
            }

            return sSuccess;
        }

        /// <summary>
        /// Removes an RecipeModel in the repository
        /// If an exception occurs, the message is added to the list of errors, which is then displayed on the MessageComponent
        /// Success message is added on success
        /// </summary>
        /// <param name="aRecipeModel">The RecipeModel to remove</param>
        /// <returns>True if successfully removed, false otherwise</returns>
        public async Task<bool> RemoveRecipe(RecipeModel aRecipeModel)
        {
            IsBusy = true;
            bool sSuccess = true;

            try
            {
                await mKitchenStockRepository.RemoveRecipe(aRecipeModel);

                mSuccessMessages.Add($"Successfully removed {aRecipeModel.Name}!");
            }
            catch (Exception ex)
            {
                sSuccess = false;
                mErrors.Add(ex.Message);
            }
            finally
            {
                IsBusy = false;
            }

            return sSuccess;
        }


        public async Task GetSuggestedRecipesFromStock()
        {
            IsBusy = true;

            //Clear the list
            mSuggestedRecipes.Clear();

            //Get all recipes and stock
            await GetRecipes();
            await GetStock();

            //Loop through all recipes
            foreach(var sRecipe in mRecipes)
            {
                //Add the recipe to the list
                SuggestedRecipeModel sRecommendedRecipe = new SuggestedRecipeModel(sRecipe);
                mSuggestedRecipes.Add(sRecommendedRecipe);

                //Loop through all ingredients
                foreach (var sIngredient in sRecipe.Ingredients)
                {
                    //Select all matching stock
                    List<StockModel> sStockForIngredient = mStock.Where(x => x.Article.Ingredient == sIngredient.Ingredient).ToList();

                    //Add the stock to the ingredient dictionary
                    sRecommendedRecipe.StocksForIngredient.Add(sIngredient, sStockForIngredient);
                }
            }

            IsBusy = false;
        }

        /// <summary>
        /// Gets
        /// </summary>
        /// <param name="aRecipeModel">The recipe</param>
        /// <param name="aMultiplier">A multiplier (if portion count changed)</param>
        /// <returns>A List of all suggested stock transactions</returns>
        public async Task<List<StockTransactionRecipeModel>> GetStockToRemoveFromRecipe(RecipeModel aRecipeModel, double aMultiplier)
        {
            List<StockTransactionRecipeModel> sStockTransactionList = new List<StockTransactionRecipeModel>();
            IsBusy = true;

            try
            {
                //Update stock
                await GetStock();

                //Loop through all ingredients that we have to check stock on
                foreach (var sIngredient in aRecipeModel.Ingredients.Where(x => x.Ingredient.Checkstock))
                {
                    //Add a new object to the list
                    StockTransactionRecipeModel sStockTransactionRecipeModel = new StockTransactionRecipeModel(sIngredient, aMultiplier);
                    sStockTransactionList.Add(sStockTransactionRecipeModel);

                    //Calculate how much we need
                    int? sWeightNeeded = 0;

                    if (sIngredient.MeasurementType == MeasurementTypeEnum.Weight)
                    {
                        sWeightNeeded = Convert.ToInt32(sIngredient.WeightInGrams * aMultiplier);
                    }
                    else if (sIngredient.MeasurementType == MeasurementTypeEnum.Volume)
                    {
                        sWeightNeeded = Convert.ToInt32(sIngredient.Ingredient.ConvertVolumeInMillilitersToGrams(Convert.ToInt32(sIngredient.VolumeInMilliliters.Value * aMultiplier)));
                    }
                    else if (sIngredient.MeasurementType == MeasurementTypeEnum.Quantity)
                    {
                        sWeightNeeded = Convert.ToInt32(sIngredient.Ingredient.ConvertQuantityInPiecesToGrams((double)sIngredient.QuantityInPieces * aMultiplier));
                    }
                    else
                    {
                        //Skip
                        sStockTransactionRecipeModel.IsEnoughStock = false;
                        continue;
                    }

                    //Set the weight needed
                    sStockTransactionRecipeModel.WeightNeededInGrams = (int)sWeightNeeded;

                    //Select the related stock, ordered by opened stock, then by BBD
                    List<StockModel> sRelatedStock = mStock.Where(x => x.Article.Ingredient == sIngredient.Ingredient)
                            .OrderBy(x => x.PercentageLeft).
                            ThenBy(x => x.BestBeforeDate).ToList();

                    //Not enough stock, flag
                    if (sRelatedStock.Count == 0)
                    {
                        sStockTransactionRecipeModel.IsEnoughStock = false;
                        continue;
                    }

                    //Loop through all stock
                    foreach (var sStock in sRelatedStock)
                    {
                        //Stock is more enough
                        if (sWeightNeeded <= sStock.Article.WeightInGrams * (sStock.PercentageLeft/100))
                        {
                            double sNewPercentageLeft = sStock.PercentageLeft - ((int)sWeightNeeded*100 / (double)sStock.Article.WeightInGrams);
                            sStockTransactionRecipeModel.StockAndNewPercentageLeft.Add(sStock, sNewPercentageLeft);
                            sStockTransactionRecipeModel.IsEnoughStock = true;
                            break;
                        }
                        else
                        {
                            //Stock not enough, is this the last stock?
                            if (sStock == sRelatedStock.Last())
                            {
                                //Yes
                                sStockTransactionRecipeModel.StockAndNewPercentageLeft.Add(sStock, 0);
                                sStockTransactionRecipeModel.IsEnoughStock = false;
                            }
                            else
                            {
                                //No, remove the stock and continue
                                sWeightNeeded -= Convert.ToInt32(sStock.Article.WeightInGrams * ((double)sStock.PercentageLeft/100));
                                sStockTransactionRecipeModel.StockAndNewPercentageLeft.Add(sStock, 0);
                            }
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                mErrors.Add(ex.Message);
            }
            finally
            {
                IsBusy = false;
            }

            //Order the list by full transactions first, then partial
            return sStockTransactionList.OrderByDescending(x => x.IsEnoughStock).ThenByDescending(x => x.StockAndNewPercentageLeft.Count).ToList();
        }
        #endregion


        #region ShoppingList
        /// <summary>
        /// Gets all ShoppingModel from the repository
        /// If an exception occurs, the message is added to the list of errors, which is then displayed on the MessageComponent
        /// </summary>
        public async Task GetShoppingList()
        {
            IsBusy = true;

            try
            {
                mShoppingList = await mKitchenStockRepository.GetShoppingList();
            }
            catch (Exception ex)
            {
                mErrors.Add(ex.Message);
            }
            finally
            {
                IsBusy = false;
            }
        }

        /// <summary>
        /// Adds a ShoppingModel in the repository
        /// If an exception occurs, the message is added to the list of errors, which is then displayed on the MessageComponent
        /// Success message is added on success
        /// </summary>
        /// <param name="aShoppingModel">The ShoppingModel to add</param>
        /// <returns>True if successfully added, false otherwise</returns>
        public async Task<bool> AddShoppingListItem(ShoppingModel aShoppingModel)
        {
            IsBusy = true;
            bool sSuccess = true;

            try
            {
                await mKitchenStockRepository.AddShoppingModel(aShoppingModel);

                mSuccessMessages.Add($"Successfully added {aShoppingModel.Article.Name}!");
            }
            catch (Exception ex)
            {
                sSuccess = false;
                mErrors.Add(ex.Message);
            }
            finally
            {
                IsBusy = false;
            }

            return sSuccess;
        }

        /// <summary>
        /// Updates a ShoppingModel in the repository
        /// If an exception occurs, the message is added to the list of errors, which is then displayed on the MessageComponent
        /// Success message is added on success
        /// </summary>
        /// <param name="aShoppingModel">The ShoppingModel to update</param>
        /// <returns>True if successfully updated, false otherwise</returns>
        public async Task<bool> UpdateShoppingListItem(ShoppingModel aShoppingModel)
        {
            IsBusy = true;
            bool sSuccess = true;

            try
            {
                await mKitchenStockRepository.UpdateShoppingModel(aShoppingModel);

                mSuccessMessages.Add($"Successfully updated the shopping list item of {aShoppingModel.Article.Name}!");
            }
            catch (Exception ex)
            {
                sSuccess = false;
                mErrors.Add(ex.Message);
            }
            finally
            {
                IsBusy = false;
            }

            return sSuccess;
        }

        /// <summary>
        /// Removes a ShoppingModel in the repository
        /// If an exception occurs, the message is added to the list of errors, which is then displayed on the MessageComponent
        /// Success message is added on success
        /// </summary>
        /// <param name="aShoppingListItem">The ShoppingModel to remove</param>
        /// <returns>True if successfully removed, false otherwise</returns>
        public async Task<bool> RemoveShoppingListItem(ShoppingModel aShoppingListItem)
        {
            IsBusy = true;
            bool sSuccess = true;

            try
            {
                await mKitchenStockRepository.RemoveShoppingModel(aShoppingListItem);
                mSuccessMessages.Add($"Successfully removed {aShoppingListItem.Article.Name} from the shopping list!");
            }
            catch (Exception ex)
            {
                sSuccess = false;
                mErrors.Add(ex.Message);
            }
            finally
            {
                IsBusy = false;
            }

            return sSuccess;
        }

        /// <summary>
        /// Removes a list of ShoppingModel from the repository
        /// If an exception occurs, the message is added to the list of errors, which is then displayed on the MessageComponent
        /// Success message is added on success
        /// </summary>
        /// <param name="aShoppingModelList">The list of ShoppingModel to remove</param>
        /// <returns>True if successfully removed, false otherwise</returns>
        public async Task<bool> RemoveShoppingListItems(List<ShoppingModel> aShoppingModelList)
        {
            IsBusy = true;
            bool sSuccess = true;

            try
            {
                await mKitchenStockRepository.RemoveShoppingModelList(aShoppingModelList);

                mSuccessMessages.Add($"Successfully removed {aShoppingModelList.Count} items from the shopping list!");
            }
            catch (Exception ex)
            {
                sSuccess = false;
                mErrors.Add(ex.Message);
            }
            finally
            {
                IsBusy = false;
            }

            return sSuccess;
        }
        #endregion


        #region Stock
        /// <summary>
        /// Gets all StockModel from the repository
        /// If an exception occurs, the message is added to the list of errors, which is then displayed on the MessageComponent
        /// </summary>
        public async Task GetStock()
        {
            IsBusy = true;

            try
            {
                mStock = await mKitchenStockRepository.GetStock();
            }
            catch (Exception ex)
            {
                mErrors.Add(ex.Message);
            }
            finally
            {
                IsBusy = false;
            }
        }

        /// <summary>
        /// Adds a StockModel in the repository
        /// If an exception occurs, the message is added to the list of errors, which is then displayed on the MessageComponent
        /// Success message is added on success
        /// </summary>
        /// <param name="aStockModel">The StockModel to add</param>
        /// <param name="aQuantity">The Quantity of the StockModel to add</param>
        /// <returns>True if successfully added, false otherwise</returns>
        public async Task<bool> AddStock(StockModel aStockModel, int aQuantity)
        {
            IsBusy = true;
            bool sSuccess = true;

            try
            {
                await mKitchenStockRepository.AddStock(aStockModel, aQuantity);
                await AddStockLogNewStock(aStockModel, aQuantity); //Log

                mSuccessMessages.Add($"Successfully added {aStockModel.Article.Name}!");
            }
            catch (Exception ex)
            {
                sSuccess = false;
                mErrors.Add(ex.Message);
            }
            finally
            {
                IsBusy = false;
            }

            return sSuccess;
        }

        /// <summary>
        /// Updates a StockModel in the repository
        /// If an exception occurs, the message is added to the list of errors, which is then displayed on the MessageComponent
        /// Success message is added on success
        /// </summary>
        /// <param name="aStockModel">The StockModel to update</param>
        /// <returns>True if successfully updated, false otherwise</returns>
        public async Task<bool> UpdateStock(StockModel aStockModel, StockModel aOriginalStock)
        {
            IsBusy = true;
            bool sSuccess = true;

            try
            {
                await mKitchenStockRepository.UpdateStock(aStockModel);

                await AddStockLogUpdateStock(aOriginalStock, aStockModel); //Log

                mSuccessMessages.Add($"Successfully updated the stock of {aStockModel.Article.Name} with Id {aStockModel.Id}!");
            }
            catch (Exception ex)
            {
                sSuccess = false;
                mErrors.Add(ex.Message);
            }
            finally
            {
                IsBusy = false;
            }

            return sSuccess;
        }

        /// <summary>
        /// Removes a StockModel in the repository
        /// If an exception occurs, the message is added to the list of errors, which is then displayed on the MessageComponent
        /// Success message is added on success
        /// </summary>
        /// <param name="aStockModel">The StockModel to remove</param>
        /// <returns>True if successfully removed, false otherwise</returns>
        public async Task<bool> RemoveStock(StockModel aStockModel)
        {
            IsBusy = true;
            bool sSuccess = true;

            try
            {
                await mKitchenStockRepository.RemoveStock(aStockModel);
                await AddStockLogDeleteStock(aStockModel); //Log
                mSuccessMessages.Add($"Successfully removed the stock of {aStockModel.Article.Name} with Id {aStockModel.Id}!");
            }
            catch (Exception ex)
            {
                sSuccess = false;
                mErrors.Add(ex.Message);
            }
            finally
            {
                IsBusy = false;
            }

            return sSuccess;
        }

        /// <summary>
        /// Removes a list of StockModels from the repository
        /// If an exception occurs, the message is added to the list of errors, which is then displayed on the MessageComponent
        /// Success message is added on success
        /// </summary>
        /// <param name="aStockModelList">The list of StockModel to remove</param>
        /// <returns>True if successfully removed, false otherwise</returns>
        public async Task<bool> RemoveStock(List<StockModel> aStockModelList)
        {
            IsBusy = true;
            bool sSuccess = true;

            try
            {
                await mKitchenStockRepository.RemoveStock(aStockModelList);

                //Log
                foreach(var sStock in aStockModelList)
                {
                    await AddStockLogDeleteStock(sStock); 
                }

                mSuccessMessages.Add($"Successfully removed {aStockModelList.Count} entries of Stock!");
            }
            catch (Exception ex)
            {
                sSuccess = false;
                mErrors.Add(ex.Message);
            }
            finally
            {
                IsBusy = false;
            }

            return sSuccess;
        }
        #endregion


        #region StockLogs
        /// <summary>
        /// Gets all Stock Log Models from a given start date
        /// If an exception occurs, the message is added to the list of errors, which is then displayed on the MessageComponent
        /// Success message is added on success
        /// </summary>
        /// <param name="aStartDate">The StartDate of the logs</param>
        public async Task GetStockLogModels(DateTime aStartDate)
        {
            IsBusy = true;

            try
            {
                mStockLogs = await mKitchenStockRepository.GetStockLogs(aStartDate);
            }
            catch (Exception ex)
            {
                mErrors.Add(ex.Message);
            }
            finally
            {
                IsBusy = false;
            }
        }

        /// <summary>
        /// Adds a StockLogModel in the repository
        /// If an exception occurs, the message is added to the list of errors, which is then displayed on the MessageComponent
        /// Success message is added on success
        /// </summary>
        /// <param name="aStockLogModel">The StockLogModel to add</param>
        /// <returns>True if successfully added, false otherwise</returns>
        public async Task<bool> AddStockLog(StockLogModel aStockLogModel)
        {
            IsBusy = true;
            bool sSuccess = true;

            try
            {
                await mKitchenStockRepository.AddStockLog(aStockLogModel);
            }
            catch (Exception ex)
            {
                sSuccess = false;
                mErrors.Add(ex.Message);
            }
            finally
            {
                IsBusy = false;
            }

            return sSuccess;
        }

        /// <summary>
        /// Creates a new StockLogModel for updating stock
        /// </summary>
        /// <param name="aStockModel">The StockModel to log</param>
        public async Task AddStockLogUpdateStock(StockModel aOriginalStockModel, StockModel aNewStockModel)
        {
            StockLogModel sStockLog = new StockLogModel
            {
                Article = aNewStockModel.Article,
                Description = $"Updated stock for ArticleName: {aNewStockModel.Article.Name}",
                PreviousBestBeforeDate = aOriginalStockModel.BestBeforeDate,
                PreviousLocationId = aOriginalStockModel.LocationId,
                PreviousPercentageLeft = aOriginalStockModel.PercentageLeft,
                NewBestBeforeDate = aNewStockModel.BestBeforeDate,
                NewLocation = aNewStockModel.Location,
                NewPercentageLeft = aNewStockModel.PercentageLeft
            };

            await AddStockLog(sStockLog);
        }

        /// <summary>
        /// Creates a new StockLogModel for removing stock
        /// </summary>
        /// <param name="aStockModel">The StockModel to log</param>
        public async Task AddStockLogDeleteStock(StockModel aStockModel)
        {
            StockLogModel sStockLog = new StockLogModel
            {
                Article = aStockModel.Article,
                Description = $"Removed stock for ArticleName: {aStockModel.Article.Name}",
                PreviousBestBeforeDate = aStockModel.BestBeforeDate,
                PreviousLocation = aStockModel.Location,
                PreviousPercentageLeft = aStockModel.PercentageLeft,
                NewBestBeforeDate = null,
                NewLocation = null,
                NewPercentageLeft = 0,
            };

            await AddStockLog(sStockLog);
        }

        /// <summary>
        /// Creates a new StockLogModel for adding new stock
        /// </summary>
        /// <param name="aStockModel">The StockModel to log</param>
        public async Task AddStockLogNewStock(StockModel aStockModel, int aQuantity)
        {
            StockLogModel sStockLog = new StockLogModel
            {
                Article = aStockModel.Article,
                Description = $"Added {aQuantity} new stock for ArticleName: {aStockModel.Article.Name}.",
                NewBestBeforeDate = aStockModel.BestBeforeDate,
                NewLocation = aStockModel.Location,
                NewPercentageLeft = aStockModel.PercentageLeft,
            };

            await AddStockLog(sStockLog);
        }
        #endregion


        #region Messages
        /// <summary>
        /// Adds an error message string to the list of errors
        /// </summary>
        /// <param name="aErrorMessage">The error message to add</param>
        public void AddError(string aErrorMessage)
        {
            mErrors.Add(aErrorMessage);
        }

        /// <summary>
        /// Adds a success message string to the list of success messages
        /// </summary>
        /// <param name="aSuccessMessage">The success message to add</param>
        public void AddSuccess(string aSuccessMessage)
        {
            mSuccessMessages.Add(aSuccessMessage);
        }
        #endregion
    }
}
