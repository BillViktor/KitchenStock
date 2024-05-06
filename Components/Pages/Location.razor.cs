using KitchenStock.Components.Pages.Dialogs.Stock;
using KitchenStock.Components.Pages.Dialogs;
using KitchenStock.Components.ViewModels;
using KitchenStock.Models;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace KitchenStock.Components.Pages
{
    public partial class Location
    {
        [Inject] MasterViewModel MasterViewModel { get; set; }
        [Inject] NavigationManager NavigationManager { get; set; }
        [Inject] IDialogService DialogService { get; set; }
        [Parameter] public string aLocationId { get; set; }

        private string? mSearchString = "";
        private HashSet<StockModel> mSelectedStock = new HashSet<StockModel>();
        private LocationModel mLocationModel { get; set; }

        /// <summary>
        /// Check that the routing leads to a valid Location, if not, navigate to index
        /// </summary>
        protected override void OnInitialized()
        {
            if(string.IsNullOrEmpty(aLocationId))
            {
                MasterViewModel.AddError("Invalid routing!");
                NavigationManager.NavigateTo("/");
            }
            else
            {
                int aParsedLocationId;
                if(!int.TryParse(aLocationId, out aParsedLocationId))
                {
                    MasterViewModel.AddError("Invalid routing!");
                    NavigationManager.NavigateTo("/");
                }

                if(!MasterViewModel.Locations.Any(x => x.Id == aParsedLocationId))
                {
                    MasterViewModel.AddError($"There exists no location with Id {aParsedLocationId}!");
                    NavigationManager.NavigateTo("/");
                }
                else
                {
                    mLocationModel = MasterViewModel.Locations.First(x => x.Id == aParsedLocationId);
                }
            }
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
            var sParameter = new DialogParameters
            {
                { "mLocationModel", mLocationModel }
            };

            var sDialog = await DialogService.ShowAsync<AddStockDialog>("Add New Stock", sParameter, sOptions);
            var sResult = await sDialog.Result;

            if (!sResult.Canceled)
            {
                await MasterViewModel.GetStock();
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
                await MasterViewModel.GetStock();
            }
        }

        /// <summary>
        /// Deletes stock
        /// </summary>
        /// <param name="aStockModel">The stock to delete</param>
        private async Task DeleteStock(StockModel aStockModel)
        {
            if (await MasterViewModel.RemoveStock(aStockModel))
            {
                await MasterViewModel.GetStock();
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

            if (await MasterViewModel.RemoveStock(mSelectedStock.ToList()))
            {
                await MasterViewModel.GetStock();
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
