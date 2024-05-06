using KitchenStock.Data;
using KitchenStock.Models;

namespace KitchenStock.Components.ViewModels
{
    public class ViewModel
    {
        private readonly KitchenStockRepository mKitchenStockRepository;

        //Fields
        private List<string> mErrors = new List<string>();
        private List<string> mSuccessMessages = new List<string>();
        private bool mIsBusy = false;

        private List<LocationModel> mLocations = new List<LocationModel>();
        private List<ArticleModel> mArticles = new List<ArticleModel>();
        private List<StockModel> mStock = new List<StockModel>();
        private List<CategoryModel> mCategories = new List<CategoryModel>();

        //Properties
        public List<string> Errors { get { return mErrors; } set { mErrors = value; } }
        public List<string> SuccessMessages { get { return mSuccessMessages; } set { mSuccessMessages = value; } }
        public bool IsBusy { get { return mIsBusy; } set { mIsBusy = value; } }

        public List<LocationModel> Locations { get {  return mLocations; } set { mLocations = value; } }
        public List<ArticleModel> Articles { get { return mArticles; } set { mArticles = value; } }
        public List<StockModel> Stock { get { return mStock; } set { mStock = value; } }
        public List<CategoryModel> Categories { get { return mCategories; } set { mCategories = value; } }

        public ViewModel(KitchenStockRepository aKitchenStockRepository)
        {
            mKitchenStockRepository = aKitchenStockRepository;
        }


        #region Articles
        public async Task GetArticles()
        {
            try
            {
                mArticles = await mKitchenStockRepository.GetArticles();
            }
            catch(Exception ex)
            {
                mErrors.Add(ex.Message);
            }
        }

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

            return sSuccess;
        }

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

            return sSuccess;
        }

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

            return sSuccess;
        }
        #endregion


        #region Categories
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


        #region Locations
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


        #region Stock
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

        public async Task<bool> AddStock(StockModel aStockModel, int aQuantity)
        {
            IsBusy = true;
            bool sSuccess = true;

            try
            {
                await mKitchenStockRepository.AddStock(aStockModel, aQuantity);

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

        public async Task<bool> UpdateStock(StockModel aStockModel)
        {
            IsBusy = true;
            bool sSuccess = true;

            try
            {
                await mKitchenStockRepository.UpdateStock(aStockModel);

                mSuccessMessages.Add($"Successfully updated the stock of {aStockModel.Article.Name} with Id {aStockModel.Id}!");
            }
            catch (Exception ex)
            {
                sSuccess = false;
                mErrors.Add(ex.Message);
            }

            return sSuccess;
        }

        public async Task<bool> RemoveStock(StockModel aStockModel)
        {
            IsBusy = true;
            bool sSuccess = true;

            try
            {
                await mKitchenStockRepository.RemoveStock(aStockModel);

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

        public async Task<bool> RemoveStock(List<StockModel> aStockModelList)
        {
            IsBusy = true;
            bool sSuccess = true;

            try
            {
                await mKitchenStockRepository.RemoveStock(aStockModelList);

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


        #region Messages
        public void AddError(string aError)
        {
            mErrors.Add(aError);
        }

        public void AddSuccess(string aSuccess)
        {
            mSuccessMessages.Add(aSuccess);
        }
        #endregion
    }
}
