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

        //Constructor
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
