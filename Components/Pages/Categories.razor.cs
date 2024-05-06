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

        protected override async Task OnInitializedAsync()
        {
            await ViewModel.GetCategories();
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
    }
}
