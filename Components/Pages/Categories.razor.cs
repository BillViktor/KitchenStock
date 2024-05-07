using KitchenStock.Components.Pages.Dialogs;
using KitchenStock.Components.ViewModels;
using KitchenStock.Models;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace KitchenStock.Components.Pages
{
    public partial class Categories
    {
        [Inject] ViewModel ViewModel { get; set; }
        [Inject] IDialogService DialogService { get; set; }

        //Field
        private string mSearchString = "";

        /// <summary>
        /// Get Categories on page initilization
        /// </summary>
        protected override async Task OnInitializedAsync()
        {
            await ViewModel.GetCategories();
            await ViewModel.GetLocations(); //Get Locations (for navmenu)
        }

        /// <summary>
        /// Shows a dialog where the user can either add or edit a Category
        /// </summary>
        /// <param name="aLocationModel">The Location to edit, or a new Location</param>
        /// <param name="aNewLocation">Indicates if the Location already exsists or not</param>
        private async Task AddOrEditCategory(CategoryModel aCateGoryModel, bool aNewCategory)
        {
            //Set the title
            string sDialogTitle = "Add New Category";
            if (!aNewCategory)
            {
                sDialogTitle = $"Edit Category: {aCateGoryModel.Name}";
            }

            var sParameters = new DialogParameters<CategoryDialog>
            {
                {
                    "mCategoryModel", aCateGoryModel
                },
                {
                    "mNewCategory", aNewCategory
                }
            };

            var sDialog = await DialogService.ShowAsync<CategoryDialog>(sDialogTitle, sParameters);
            var sResult = await sDialog.Result;

            if (!sResult.Canceled)
            {
                await ViewModel.GetCategories();
            }
        }

        #region Helpers
        private bool FilterFunction1(CategoryModel aCategoryModel) => FilterFunction2(aCategoryModel, mSearchString);

        /// <summary>
        /// Returns true if the string properties of the CategoryModel contains the search string
        /// </summary>
        /// <param name="aCategoryModel">The category</param>
        /// <param name="mSearchString">The search string</param>
        /// <returns>True if it any specified property contains the search string, false otherwise</returns>
        private bool FilterFunction2(CategoryModel aCategoryModel, string mSearchString)
        {
            if (string.IsNullOrWhiteSpace(mSearchString))
                return true;
            if (aCategoryModel.Name.Contains(mSearchString, StringComparison.OrdinalIgnoreCase))
                return true;
            if (aCategoryModel.Description != null && aCategoryModel.Description.Contains(mSearchString, StringComparison.OrdinalIgnoreCase))
                return true;
            return false;
        }
        #endregion
    }
}
