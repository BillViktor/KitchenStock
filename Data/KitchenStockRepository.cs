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
        public async Task<List<ArticleModel>> GetArticles()
        {
            return await mKitchenStockDbContext.Articles
                .Include(x => x.Categories.OrderBy(y => y.Name))
                .ToListAsync();
        }

        public async Task<ArticleModel> AddArticle(ArticleModel aArticleModel)
        {
            await mKitchenStockDbContext.Articles.AddAsync(aArticleModel);
            await mKitchenStockDbContext.SaveChangesAsync();
            return aArticleModel;
        }

        public async Task<ArticleModel> UpdateArticle(ArticleModel aArticleModel)
        {
            mKitchenStockDbContext.Articles.Update(aArticleModel);
            await mKitchenStockDbContext.SaveChangesAsync();
            return aArticleModel;
        }

        public async Task RemoveArticle(ArticleModel aArticleModel)
        {
            mKitchenStockDbContext.Articles.Remove(aArticleModel);
            await mKitchenStockDbContext.SaveChangesAsync();
        }
        #endregion


        #region Categories
        public async Task<List<CategoryModel>> GetCategories()
        {
            return await mKitchenStockDbContext.Categories.OrderBy(x => x.Name).ToListAsync();
        }

        public async Task<CategoryModel> AddCategory(CategoryModel aCategoryModel)
        {
            await mKitchenStockDbContext.Categories.AddAsync(aCategoryModel);
            await mKitchenStockDbContext.SaveChangesAsync();
            return aCategoryModel;
        }

        public async Task<CategoryModel> UpdateCategory(CategoryModel aCategoryModel)
        {
            mKitchenStockDbContext.Categories.Update(aCategoryModel);
            await mKitchenStockDbContext.SaveChangesAsync();
            return aCategoryModel;
        }

        public async Task RemoveCategory(CategoryModel aCategoryModel)
        {
            mKitchenStockDbContext.Categories.Remove(aCategoryModel);
            await mKitchenStockDbContext.SaveChangesAsync();
        }
        #endregion


        #region Locations
        public async Task<List<LocationModel>> GetLocations()
        {
            return await mKitchenStockDbContext.Locations.OrderBy(x => x.SortOrder).ToListAsync();
        }

        public async Task<LocationModel> AddLocation(LocationModel aLocationModel)
        {
            await mKitchenStockDbContext.Locations.AddAsync(aLocationModel);
            await mKitchenStockDbContext.SaveChangesAsync();
            return aLocationModel;
        }

        public async Task<LocationModel> UpdateLocation(LocationModel aLocationModel)
        {
            mKitchenStockDbContext.Locations.Update(aLocationModel);
            await mKitchenStockDbContext.SaveChangesAsync();
            return aLocationModel;
        }

        public async Task RemoveLocation(LocationModel aLocationModel)
        {
            mKitchenStockDbContext.Locations.Remove(aLocationModel);
            await mKitchenStockDbContext.SaveChangesAsync();
        }
        #endregion


        #region Stock
        public async Task<List<StockModel>> GetStock()
        {
            return await mKitchenStockDbContext.Stock
                .OrderBy(x => x.BestBeforeDate)
                .Include(x => x.Article)
                    .ThenInclude(x => x.Categories.OrderBy(x => x.Name))
                .ToListAsync();
        }

        public async Task AddStock(StockModel aStockModel, int Quantity)
        {
            for(int i = 0; i<Quantity; i++)
            {
                await mKitchenStockDbContext.Stock.AddAsync(aStockModel);
            }
            
            await mKitchenStockDbContext.SaveChangesAsync();
        }

        public async Task<StockModel> UpdateStock(StockModel aStockModel)
        {
            mKitchenStockDbContext.Stock.Update(aStockModel);
            await mKitchenStockDbContext.SaveChangesAsync();
            return aStockModel;
        }

        public async Task RemoveStock(StockModel aStockModel)
        {
            mKitchenStockDbContext.Stock.Remove(aStockModel);
            await mKitchenStockDbContext.SaveChangesAsync();
        }

        public async Task RemoveStock(List<StockModel> aStockModelList)
        {
            mKitchenStockDbContext.Stock.RemoveRange(aStockModelList);
            await mKitchenStockDbContext.SaveChangesAsync();
        }
        #endregion
    }
}
