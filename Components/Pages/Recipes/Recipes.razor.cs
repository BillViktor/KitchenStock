using KitchenStock.Components.Pages.Dialogs.Recipes;
using KitchenStock.Components.ViewModels;
using KitchenStock.Models;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace KitchenStock.Components.Pages.Recipes
{
    public partial class Recipes
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
            await ViewModel.GetRecipes();
            await ViewModel.GetLocations(); //Get Locations for NavMenu
        }

        /// <summary>
        /// Shows a dialog where the user can either add or edit a RecipeModel
        /// </summary>
        /// <param name="aLocationModel">The RecipeModel to edit, or a new RecipeModel</param>
        /// <param name="aNewLocation">Indicates if the Location already exsists or not</param>
        private async Task AddOrEditRecipe(RecipeModel aRecipeModel, bool aNewRecipe)
        {
            //Set the title
            string sDialogTitle = "Add New Recipe";
            if (!aNewRecipe)
            {
                sDialogTitle = $"Edit Recipe: {aRecipeModel.Name}";
            }

            var sParameters = new DialogParameters<EditRecipeDialog>
            {
                {
                    "mRecipeModel", aRecipeModel
                },
                {
                    "mNewRecipe", aNewRecipe
                }
            };

            var sDialog = await DialogService.ShowAsync<EditRecipeDialog>(sDialogTitle, sParameters);
            var sResult = await sDialog.Result;

            if (!sResult.Canceled)
            {
                await ViewModel.GetRecipes();
            }
        }

        /// <summary>
        /// Shows the recipe
        /// </summary>
        /// <param name="aRecipeModel">The recipe to show</param>
        private async Task ShowRecipe(RecipeModel aRecipeModel)
        {
            var sParameter = new DialogParameters
            {
                { "mRecipeModel", aRecipeModel }
            };

            var sOption = new DialogOptions
            {
                FullWidth = true,
            };

            await DialogService.ShowAsync<ShowRecipeDialog>(aRecipeModel.Name, sParameter, sOption);
        }

        #region Helpers
        private bool FilterFunction1(RecipeModel aRecipeModel) => FilterFunction2(aRecipeModel, mSearchString);

        /// <summary>
        /// Returns true if the string properties of the RecipeModel contains the search string
        /// </summary>
        /// <param name="aRecipeModel">The category</param>
        /// <param name="mSearchString">The search string</param>
        /// <returns>True if it any specified property contains the search string, false otherwise</returns>
        private bool FilterFunction2(RecipeModel aRecipeModel, string mSearchString)
        {
            if (string.IsNullOrWhiteSpace(mSearchString))
                return true;
            if (aRecipeModel.Name.Contains(mSearchString, StringComparison.OrdinalIgnoreCase))
                return true;
            if (aRecipeModel.GetIngredientsString().Contains(mSearchString, StringComparison.OrdinalIgnoreCase))
                return true;
            return false;
        }
        #endregion
    }
}
