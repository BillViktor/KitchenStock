using KitchenStock.Components.Pages.Dialogs;
using KitchenStock.Components.Pages.Dialogs.Stock;
using KitchenStock.Components.ViewModels;
using KitchenStock.Models;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace KitchenStock.Components.Pages
{
    public partial class Home
    {
        [Inject] ViewModel ViewModel { get; set; }
        [Inject] IDialogService DialogService { get; set; }

        //Fields
        private string mSearchString = "";
        private HashSet<StockModel> mSelectedStock = new HashSet<StockModel>(); //HashSet of all Selected Stock in the table

        /// <summary>
        /// On intitialization, get all stock, articles and locatons
        /// </summary>
        /// <returns></returns>
        protected override async Task OnInitializedAsync()
        {
            await ViewModel.GetStock();
            await ViewModel.GetArticles();
            await ViewModel.GetLocations();
        }

        #region CRUD
        /// <summary>
        /// Shows a dialog for adding stock
        /// </summary>
        private async Task AddStock()
        {
            var sOptions = new DialogOptions
            {
                FullWidth = true
            };

            var sDialog = await DialogService.ShowAsync<AddStockDialog>("Add New Stock", sOptions);
            var sResult = await sDialog.Result;

            if (!sResult.Canceled)
            {
                await ViewModel.GetStock();
            }
        }

        /// <summary>
        /// Shows a dialog for editing stock
        /// </summary>
        /// <param name="aStockModel">The stock to edit</param>
        private async Task EditStock(StockModel aStockModel)
        {
            var sOptions = new DialogOptions
            {
                FullWidth = true
            };

            var sParameters = new DialogParameters
            {
                { "mStockModel", aStockModel }
            };

            var sDialog = await DialogService.ShowAsync<EditStockDialog>("Add New Stock", sParameters, sOptions);
            var sResult = await sDialog.Result;

            if (!sResult.Canceled)
            {
                await ViewModel.GetStock();
            }
        }

        /// <summary>
        /// Deletes stock
        /// </summary>
        /// <param name="aStockModel">The stock to delete</param>
        private async Task DeleteStock(StockModel aStockModel)
        {
            if (await ViewModel.RemoveStock(aStockModel))
            {
                await ViewModel.GetStock();
            }
        }

        /// <summary>
        /// Deletes the selected stock
        /// </summary>
        private async Task DeleteSelectedStock()
        {
            //Confirm
            var sParameter = new DialogParameters
            {
                { "mMessage", $"Are you sure you want to delete all selected stock ({mSelectedStock.Count} items)? This action is irreversable!" }
            };

            var sDialog = await DialogService.ShowAsync<ConfirmationDialog>("Confirm", sParameter);
            var sResult = await sDialog.Result;

            if (sResult.Canceled) return;

            if (await ViewModel.RemoveStock(mSelectedStock.ToList()))
            {
                await ViewModel.GetStock();
            }
        }
        #endregion


        #region Helpers
        private bool FilterFunction1(StockModel aStockModel) => FilterFunction2(aStockModel, mSearchString);

        /// <summary>
        /// Returns true if the string properties of the StockModel contains the search string
        /// </summary>
        /// <param name="aStockModel">The stock</param>
        /// <param name="mSearchString">The search string</param>
        /// <returns>True if it any specified property contains the search string, false otherwise</returns>
        private bool FilterFunction2(StockModel aStockModel, string mSearchString)
        {
            if (string.IsNullOrWhiteSpace(mSearchString))
                return true;
            if (aStockModel.Article.Name.Contains(mSearchString, StringComparison.OrdinalIgnoreCase))
                return true;
            if (aStockModel.Article.GetCategoryString().Contains(mSearchString, StringComparison.OrdinalIgnoreCase))
                return true;
            if (aStockModel.Location.Name.Contains(mSearchString, StringComparison.OrdinalIgnoreCase))
                return true;
            return false;
        }
        #endregion
    }
}
