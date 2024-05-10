using KitchenStock.Components.Pages.Dialogs.Stock;
using KitchenStock.Components.ViewModels;
using KitchenStock.Models;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using KitchenStock.Components.Pages.Dialogs.Misc;

namespace KitchenStock.Components.Pages.StockAtLocation
{
    public partial class StockAtLocation
    {
        [Inject] ViewModel ViewModel { get; set; }
        [Inject] NavigationManager NavigationManager { get; set; }
        [Inject] IDialogService DialogService { get; set; }

        //Parameter
        [Parameter] public string aLocationId { get; set; }

        //Fields
        private string mSearchString = "";
        private HashSet<StockModel> mSelectedStock = new HashSet<StockModel>(); //Keeps track of all selected stock in the table
        private LocationModel mLocationModel { get; set; } //Keeps track of the current LocationModel

        /// <summary>
        /// Check that the routing leads to a valid Location, if not, navigate to index
        /// </summary>
        protected override async Task OnParametersSetAsync()
        {
            //Set the LocationModel to null
            mLocationModel = null;

            //Make sure that the routing parameter has a value, if not, route to index and show error
            if (string.IsNullOrEmpty(aLocationId))
            {
                ViewModel.AddError("Invalid routing!");
                NavigationManager.NavigateTo("/");
            }
            else
            {
                //Make sure that the routing parameter is an int (we're looking for a LocationModelId)
                int aParsedLocationId;
                if (!int.TryParse(aLocationId, out aParsedLocationId))
                {
                    ViewModel.AddError("Invalid routing!");
                    NavigationManager.NavigateTo("/");
                }

                //Get all locations
                await ViewModel.GetLocations();

                //Make sure that the Location exists
                if (!ViewModel.Locations.Any(x => x.Id == aParsedLocationId))
                {
                    ViewModel.AddError($"There exists no location with Id {aParsedLocationId}!");
                    NavigationManager.NavigateTo("/");
                }
                else
                {
                    //Set the LocationModel
                    mLocationModel = ViewModel.Locations.First(x => x.Id == aParsedLocationId);
                }
            }

            //All OK, get the stock and re-render
            await ViewModel.GetStock();
            StateHasChanged();
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
