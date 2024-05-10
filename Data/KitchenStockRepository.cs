using KitchenStock.Models;
using Microsoft.EntityFrameworkCore;

namespace KitchenStock.Data
{
    public class KitchenStockRepository
    {
        private readonly KitchenStockDbContext mKitchenStockDbContext;
        private readonly ILogger<KitchenStockRepository> mLogger;

        public KitchenStockRepository(KitchenStockDbContext aKitchenStockDbContext, ILogger<KitchenStockRepository> aLogger) 
        {
            mKitchenStockDbContext = aKitchenStockDbContext;
            mLogger = aLogger;
        }

        #region Articles
        /// <summary>
        /// Gets all ArticleModels, including their catagories
        /// </summary>
        /// <returns>A List of ArticleModels</returns>
        public async Task<List<ArticleModel>> GetArticlesWithCategories()
        {
            return await mKitchenStockDbContext.Articles
                .Include(x => x.Categories.OrderBy(y => y.Name))
                .ToListAsync();
        }

        /// <summary>
        /// Adds a new ArticleModel
        /// </summary>
        /// <param name="aArticleModel">The ArticleModel to add</param>
        /// <returns>The ArticleModel added</returns>
        public async Task<ArticleModel> AddArticle(ArticleModel aArticleModel)
        {
            await mKitchenStockDbContext.Articles.AddAsync(aArticleModel);
            await mKitchenStockDbContext.SaveChangesAsync();
            return aArticleModel;
        }

        /// <summary>
        /// Updates an existing ArticleModel
        /// </summary>
        /// <param name="aArticleModel">The ArticleModel to modify</param>
        /// <returns>The updated ArticleModel</returns>
        public async Task<ArticleModel> UpdateArticle(ArticleModel aArticleModel)
        {
            mKitchenStockDbContext.Articles.Update(aArticleModel);
            await mKitchenStockDbContext.SaveChangesAsync();
            return aArticleModel;
        }

        /// <summary>
        /// Removes an article
        /// </summary>
        /// <param name="aArticleModel">The ArticleModel to remove</param>
        public async Task RemoveArticle(ArticleModel aArticleModel)
        {
            mKitchenStockDbContext.Articles.Remove(aArticleModel);
            await mKitchenStockDbContext.SaveChangesAsync();
        }
        #endregion


        #region Categories
        /// <summary>
        /// Gets all CategoryModels, sorted by name
        /// </summary>
        /// <returns>A List of all CategoryModels</returns>
        public async Task<List<CategoryModel>> GetCategories()
        {
            return await mKitchenStockDbContext.Categories.OrderBy(c => c.Name).ToListAsync();
        }

        /// <summary>
        /// Adds a new CategoryModel
        /// </summary>
        /// <param name="aCategoryModel">The CategoryModel to add</param>
        /// <returns>The added CategoryModel</returns>
        public async Task<CategoryModel> AddCategory(CategoryModel aCategoryModel)
        {
            await mKitchenStockDbContext.Categories.AddAsync(aCategoryModel);
            await mKitchenStockDbContext.SaveChangesAsync();
            return aCategoryModel;
        }

        /// <summary>
        /// Updates an existing CategoryModel
        /// </summary>
        /// <param name="aCategoryModel">The CatgoryModel to update</param>
        /// <returns>The updated CategoryModel</returns>
        public async Task<CategoryModel> UpdateCategory(CategoryModel aCategoryModel)
        {
            mKitchenStockDbContext.Categories.Update(aCategoryModel);
            await mKitchenStockDbContext.SaveChangesAsync();
            return aCategoryModel;
        }

        /// <summary>
        /// Removes a category
        /// </summary>
        /// <param name="aCategoryModel">The CategoryModel to remove</param>
        public async Task RemoveCategory(CategoryModel aCategoryModel)
        {
            mKitchenStockDbContext.Categories.Remove(aCategoryModel);
            await mKitchenStockDbContext.SaveChangesAsync();
        }
        #endregion


        #region Categories
        /// <summary>
        /// Gets all IngredientModels, sorted by name
        /// </summary>
        /// <returns>A List of all IngredientModels</returns>
        public async Task<List<IngredientModel>> GetIngredients()
        {
            return await mKitchenStockDbContext.Ingredients.OrderBy(c => c.Name).ToListAsync();
        }

        /// <summary>
        /// Adds a new IngredientModel
        /// </summary>
        /// <param name="aIngredientModel">The IngredientModel to add</param>
        /// <returns>The added IngredientModel</returns>
        public async Task<IngredientModel> AddIngredient(IngredientModel aIngredientModel)
        {
            await mKitchenStockDbContext.Ingredients.AddAsync(aIngredientModel);
            await mKitchenStockDbContext.SaveChangesAsync();
            return aIngredientModel;
        }

        /// <summary>
        /// Updates an existing IngredientModel
        /// </summary>
        /// <param name="aIngredientModel">The IngredientModel to update</param>
        /// <returns>The updated IngredientModel</returns>
        public async Task<IngredientModel> UpdateIngredient(IngredientModel aIngredientModel)
        {
            mKitchenStockDbContext.Ingredients.Update(aIngredientModel);
            await mKitchenStockDbContext.SaveChangesAsync();
            return aIngredientModel;
        }

        /// <summary>
        /// Removes a category
        /// </summary>
        /// <param name="aIngredientModel">The IngredientModel to remove</param>
        public async Task RemoveIngredient(IngredientModel aIngredientModel)
        {
            mKitchenStockDbContext.Ingredients.Remove(aIngredientModel);
            await mKitchenStockDbContext.SaveChangesAsync();
        }
        #endregion


        #region Locations
        /// <summary>
        /// Gets all LocationModel
        /// </summary>
        /// <returns>A List of all LocationModels</returns>
        public async Task<List<LocationModel>> GetLocations()
        {
            return await mKitchenStockDbContext.Locations.ToListAsync();
        }

        /// <summary>
        /// Adds a new LocationModel
        /// </summary>
        /// <param name="aLocationModel">The LocationModel to add</param>
        /// <returns>The added LocationModel</returns>
        public async Task<LocationModel> AddLocation(LocationModel aLocationModel)
        {
            await mKitchenStockDbContext.Locations.AddAsync(aLocationModel);
            await mKitchenStockDbContext.SaveChangesAsync();
            return aLocationModel;
        }

        /// <summary>
        /// Updates an existing LocationModel
        /// </summary>
        /// <param name="aLocationModel">The LocationModel to update</param>
        /// <returns>The updated LocationModel</returns>
        public async Task<LocationModel> UpdateLocation(LocationModel aLocationModel)
        {
            mKitchenStockDbContext.Locations.Update(aLocationModel);
            await mKitchenStockDbContext.SaveChangesAsync();
            return aLocationModel;
        }

        /// <summary>
        /// Removes a LocationModel
        /// </summary>
        /// <param name="aLocationModel">The LocationModel to remove</param>
        public async Task RemoveLocation(LocationModel aLocationModel)
        {
            mKitchenStockDbContext.Locations.Remove(aLocationModel);
            await mKitchenStockDbContext.SaveChangesAsync();
        }
        #endregion


        #region Recipes
        /// <summary>
        /// Gets all RecipeModel, sorted by name
        /// </summary>
        /// <returns>A List of all RecipeModel</returns>
        public async Task<List<RecipeModel>> GetRecipes()
        {
            return await mKitchenStockDbContext.Recipes
                .Include(i => i.Ingredients)
                    .ThenInclude(i => i.Ingredient)
                .OrderBy(c => c.Name).ToListAsync();
        }

        /// <summary>
        /// Adds a new RecipeModel
        /// </summary>
        /// <param name="aRecipeModel">The RecipeModel to add</param>
        /// <returns>The added RecipeModel</returns>
        public async Task<RecipeModel> AddRecipe(RecipeModel aRecipeModel)
        {
            await mKitchenStockDbContext.Recipes.AddAsync(aRecipeModel);
            await mKitchenStockDbContext.SaveChangesAsync();
            return aRecipeModel;
        }

        /// <summary>
        /// Updates an existing RecipeModel
        /// </summary>
        /// <param name="aRecipeModel">The RecipeModel to update</param>
        /// <returns>The updated RecipeModel</returns>
        public async Task<RecipeModel> UpdateRecipe(RecipeModel aRecipeModel)
        {
            mKitchenStockDbContext.Recipes.Update(aRecipeModel);
            await mKitchenStockDbContext.SaveChangesAsync();
            return aRecipeModel;
        }

        /// <summary>
        /// Removes a RecipeModel
        /// </summary>
        /// <param name="aRecipeModel">The RecipeModel to remove</param>
        public async Task RemoveRecipe(RecipeModel aRecipeModel)
        {
            mKitchenStockDbContext.Recipes.Remove(aRecipeModel);
            await mKitchenStockDbContext.SaveChangesAsync();
        }
        #endregion


        #region Shopping List
        #region Stock
        /// <summary>
        /// Gets all ShoppingModel
        /// </summary>
        /// <returns>A List of all ShoppingModel</returns>
        public async Task<List<ShoppingModel>> GetShoppingList()
        {
            return await mKitchenStockDbContext.ShoppingModels
                .Include(a => a.Article)
                    .ThenInclude(c => c.Categories.OrderBy(n => n.Name))
                .ToListAsync();
        }

        /// <summary>
        /// Adds new ShoppingModel
        /// </summary>
        /// <param name="aShoppingModel">The ShoppingModel to add</param>
        public async Task AddShoppingModel(ShoppingModel aShoppingModel)
        {
            await mKitchenStockDbContext.ShoppingModels.AddAsync(aShoppingModel);
            await mKitchenStockDbContext.SaveChangesAsync();
        }

        /// <summary>
        /// Updates an existing ShoppingModel
        /// </summary>
        /// <param name="aShoppingModel">The ShoppingModel to update</param>
        /// <returns>The updated ShoppingModel</returns>
        public async Task<ShoppingModel> UpdateShoppingModel(ShoppingModel aShoppingModel)
        {
            mKitchenStockDbContext.ShoppingModels.Update(aShoppingModel);
            await mKitchenStockDbContext.SaveChangesAsync();
            return aShoppingModel;
        }

        /// <summary>
        /// Removes a ShoppingModel
        /// </summary>
        /// <param name="aShoppingModel">The ShoppingModel to remove</param>
        public async Task RemoveShoppingModel(ShoppingModel aShoppingModel)
        {
            mKitchenStockDbContext.ShoppingModels.Remove(aShoppingModel);
            await mKitchenStockDbContext.SaveChangesAsync();
        }

        /// <summary>
        /// Removes a List of ShoppingModel
        /// </summary>
        /// <param name="aShoppingModel">The List of ShoppingModel to remove</param>
        public async Task RemoveShoppingModelList(List<ShoppingModel> aShoppingModel)
        {
            mKitchenStockDbContext.ShoppingModels.RemoveRange(aShoppingModel);
            await mKitchenStockDbContext.SaveChangesAsync();
        }
        #endregion
        #endregion


        #region Stock
        /// <summary>
        /// Gets all stock, ordered by BestBeforeDate, including the article and their categories
        /// </summary>
        /// <returns>A List of all StockModel</returns>
        public async Task<List<StockModel>> GetStock()
        {
            return await mKitchenStockDbContext.Stock
                .OrderBy(b => b.BestBeforeDate)
                .Include(a => a.Article)
                    .ThenInclude(c => c.Categories.OrderBy(n => n.Name))
                .Include(l => l.Location)
                .ToListAsync();
        }

        /// <summary>
        /// Adds new StockModels
        /// </summary>
        /// <param name="aStockModel">The StockModel to add</param>
        /// <param name="Quantity">The quantity of the Stock to add</param>
        public async Task AddStock(StockModel aStockModel, int Quantity)
        {
            //Make copies of the StockModel using a copy constructor in StockModel
            //As Entity Framework keeps track of the entity (not allowing the same model to be added multiple times)
            StockModel aStockModelCopy = new StockModel(aStockModel);
            for(int i = 0; i<Quantity; i++)
            {
                //Add the StockModel, and make a new copy
                await mKitchenStockDbContext.Stock.AddAsync(aStockModelCopy);
                aStockModelCopy = new StockModel(aStockModel);
            }
            
            //Save changes
            await mKitchenStockDbContext.SaveChangesAsync();
        }

        /// <summary>
        /// Updates an existing StockModel
        /// </summary>
        /// <param name="aStockModel">The StockModel to update</param>
        /// <returns>The updated StockModel</returns>
        public async Task<StockModel> UpdateStock(StockModel aStockModel)
        {
            mKitchenStockDbContext.Stock.Update(aStockModel);
            await mKitchenStockDbContext.SaveChangesAsync();
            return aStockModel;
        }

        /// <summary>
        /// Removes a StockModel
        /// </summary>
        /// <param name="aStockModel">The StockModel to remove</param>
        public async Task RemoveStock(StockModel aStockModel)
        {
            mKitchenStockDbContext.Stock.Remove(aStockModel);
            await mKitchenStockDbContext.SaveChangesAsync();
        }

        /// <summary>
        /// Removes a List of StockModels
        /// </summary>
        /// <param name="aStockModelList">The List of StockModels to remove</param>
        public async Task RemoveStock(List<StockModel> aStockModelList)
        {
            mKitchenStockDbContext.Stock.RemoveRange(aStockModelList);
            await mKitchenStockDbContext.SaveChangesAsync();
        }
        #endregion


        #region StockLogs
        /// <summary>
        /// Gets all stock log models, starting from a specified date
        /// </summary>
        /// <param name="aStartDate">The date to start getting from</param>
        /// <returns>List of StockLogModels</returns>
        public async Task<List<StockLogModel>> GetStockLogs(DateTime aStartDate)
        {
            return await mKitchenStockDbContext.StockLogs
                .Where(x => x.CreateDate >= aStartDate)
                .Include(x => x.Article)
                .Include(x => x.PreviousLocation)
                .Include(x => x.NewLocation)
                .ToListAsync();
        }

        /// <summary>
        /// Adds a new StockLogModel
        /// </summary>
        /// <param name="aStockLogModel">The StockLogModel to add</param>
        /// <returns>The StockLogModel added</returns>
        public async Task<StockLogModel> AddStockLog(StockLogModel aStockLogModel)
        {
            await mKitchenStockDbContext.StockLogs.AddAsync(aStockLogModel);
            await mKitchenStockDbContext.SaveChangesAsync();
            return aStockLogModel;
        }
        #endregion
    }
}
