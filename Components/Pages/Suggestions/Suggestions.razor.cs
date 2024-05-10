using KitchenStock.Components.Pages.Dialogs.Recipes;
using KitchenStock.Components.ViewModels;
using KitchenStock.Models;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace KitchenStock.Components.Pages.Suggestions
{
    public partial class Suggestions
    {
        [Inject] ViewModel ViewModel { get; set; }
        [Inject] IDialogService DialogService { get; set; }

        //Field
        private string mSearchString = "";
        private bool mShowPopover = false;

        /// <summary>
        /// Get Stock on page initilization
        /// </summary>
        protected override async Task OnInitializedAsync()
        {
            await ViewModel.GetStock();
            await ViewModel.GetLocations(); //Get Locations for NavMenu
        }

        /// <summary>
        /// Gets the suggested recipes
        /// </summary>
        private async Task GetSuggestedRecipes()
        {
            await ViewModel.GetSuggestedRecipesFromStock();
        }

        /// <summary>
        /// Shows the recipe
        /// </summary>
        /// <param name="aRecipeModel">The recipe to show</param>
        private async Task ShowRecipe(SuggestedRecipeModel aSuggestedRecipeModel)
        {
            var sParameter = new DialogParameters
            {
                { "mRecipeModel", aSuggestedRecipeModel.Recipe },
                { "mSuggestedRecipeModel", aSuggestedRecipeModel }
            };

            var sOption = new DialogOptions
            {
                FullWidth = true,
            };

            await DialogService.ShowAsync<ShowRecipeDialog>(aSuggestedRecipeModel.Recipe.Name, sParameter, sOption);

            ViewModel.IsBusy = false;
        }

        #region Helpers
        private bool FilterFunction1(SuggestedRecipeModel aSuggestedRecipeModel) => FilterFunction2(aSuggestedRecipeModel, mSearchString);

        /// <summary>
        /// Returns true if the string properties of the SuggestedRecipeModel contains the search string
        /// </summary>
        /// <param name="aSuggestedRecipeModel">The category</param>
        /// <param name="mSearchString">The search string</param>
        /// <returns>True if it any specified property contains the search string, false otherwise</returns>
        private bool FilterFunction2(SuggestedRecipeModel aSuggestedRecipeModel, string mSearchString)
        {
            if (string.IsNullOrWhiteSpace(mSearchString))
                return true;
            if (aSuggestedRecipeModel.Recipe.Name.Contains(mSearchString, StringComparison.OrdinalIgnoreCase))
                return true;
            if (aSuggestedRecipeModel.Recipe.GetIngredientsString().Contains(mSearchString, StringComparison.OrdinalIgnoreCase))
                return true;
            return false;
        }
        #endregion
    }
}
