using KitchenStock.Components.Pages.Dialogs.Misc;
using KitchenStock.Components.Pages.Dialogs.ShoppingList;
using KitchenStock.Components.Pages.Dialogs.Stock;
using KitchenStock.Components.ViewModels;
using KitchenStock.Models;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace KitchenStock.Components.Pages.ShoppingList
{
    public partial class ShoppingList
    {
        [Inject] ViewModel ViewModel { get; set; }
        [Inject] IDialogService DialogService { get; set; }

        //Field
        private string mSearchString = "";
        private HashSet<ShoppingModel> mSelectedShoppingListItems = new HashSet<ShoppingModel>(); //HashSet of all Selected Stock in the table

        /// <summary>
        /// Get Categories on page initilization
        /// </summary>
        protected override async Task OnInitializedAsync()
        {
            await ViewModel.GetShoppingList();
            await ViewModel.GetLocations(); //Get Locations for NavMenu
        }

        /// <summary>
        /// Shows a dialog where the user can either add or edit a ShoppingModel
        /// </summary>
        /// <param name="aLocationModel">The RecipeModel to edit, or a new RecipeModel</param>
        /// <param name="aNewLocation">Indicates if the Location already exsists or not</param>
        private async Task AddOrEditShoppingListItem(ShoppingModel aShoppingModel, bool aNewShoppingModel)
        {
            //Set the title
            string sDialogTitle = "Add New Item";
            if (!aNewShoppingModel)
            {
                sDialogTitle = $"Edit Item: {aShoppingModel.Article.Name}";
            }

            var sParameters = new DialogParameters<ShoppingItemDialog>
            {
                {
                    "mShoppingModel", aShoppingModel
                },
                {
                    "mNewShoppingModel", aNewShoppingModel
                }
            };

            var sDialog = await DialogService.ShowAsync<ShoppingItemDialog>(sDialogTitle, sParameters);
            var sResult = await sDialog.Result;

            if (!sResult.Canceled)
            {
                await ViewModel.GetShoppingList();
            }
        }

        /// <summary>
        /// Shows the add Stock Dialog with quantity and article already set, only Location & BestBeforeDate needed
        /// </summary>
        /// <param name="aShoppingModel"></param>
        private async Task AddToStock(ShoppingModel aShoppingModel)
        {
            var sOptions = new DialogOptions
            {
                FullWidth = true
            };

            var sParameters = new DialogParameters
            {
                { "mArticleModel", aShoppingModel.Article },
                { "mQuantityParam", aShoppingModel.Quantity }
            };

            var sDialog = await DialogService.ShowAsync<AddStockDialog>($"Add {aShoppingModel.Quantity}x of {aShoppingModel.Article.Name} to stock", sParameters, sOptions);
            var sResult = await sDialog.Result;

            if (!sResult.Canceled)
            {
                await ViewModel.GetShoppingList();
            }
        }

        /// <summary>
        /// Deletes the selected shopping list items
        /// </summary>
        private async Task DeleteSelectedStock()
        {
            //Confirm
            var sParameter = new DialogParameters
            {
                { "mMessage", $"Are you sure you want to delete all selected shopping list items ({mSelectedShoppingListItems.Count} items)? The stock will not be added! This action is irreversable!" }
            };

            var sDialog = await DialogService.ShowAsync<ConfirmationDialog>("Confirm", sParameter);
            var sResult = await sDialog.Result;

            if (sResult.Canceled) return;

            if (await ViewModel.RemoveShoppingListItems(mSelectedShoppingListItems.ToList()))
            {
                await ViewModel.GetShoppingList();
            }
        }

        #region Helpers
        private bool FilterFunction1(ShoppingModel aShoppingModel) => FilterFunction2(aShoppingModel, mSearchString);

        /// <summary>
        /// Returns true if the string properties of the ShoppingModel contains the search string
        /// </summary>
        /// <param name="aShoppingModel">The category</param>
        /// <param name="mSearchString">The search string</param>
        /// <returns>True if it any specified property contains the search string, false otherwise</returns>
        private bool FilterFunction2(ShoppingModel aShoppingModel, string mSearchString)
        {
            if (string.IsNullOrWhiteSpace(mSearchString))
                return true;
            if (aShoppingModel.Article.Name.Contains(mSearchString, StringComparison.OrdinalIgnoreCase))
                return true;
            return false;
        }
        #endregion
    }
}
