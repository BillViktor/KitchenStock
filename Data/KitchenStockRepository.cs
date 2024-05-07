using KitchenStock.Models;
using Microsoft.EntityFrameworkCore;

namespace KitchenStock.Data
{
    public class KitchenStockRepository
    {
        private readonly KitchenStockDbContext mKitchenStockDbContext;

        public KitchenStockRepository(KitchenStockDbContext aKitchenStockDbContext) 
        {
            mKitchenStockDbContext = aKitchenStockDbContext;
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
        /// Gets all CategoryModels
        /// </summary>
        /// <returns>A List of all CategoryModels</returns>
        public async Task<List<CategoryModel>> GetCategories()
        {
            return await mKitchenStockDbContext.Categories.ToListAsync();
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
    }
}
