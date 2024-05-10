using KitchenStock.Components.Pages.Dialogs.Ingredient;
using KitchenStock.Components.ViewModels;
using KitchenStock.Models;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace KitchenStock.Components.Pages.Ingredients
{
    public partial class Ingredients
    {
        [Inject] ViewModel ViewModel { get; set; }
        [Inject] IDialogService DialogService { get; set; }

        //Field
        private string mSearchString = "";

        /// <summary>
        /// Get Ingredients on page initilization
        /// </summary>
        /// <returns></returns>
        protected override async Task OnInitializedAsync()
        {
            await ViewModel.GetIngredients();
            await ViewModel.GetRecipes(); //For seeing if there's an ingredient that needs conversion
            await ViewModel.GetLocations(); //Get Locations for navmenu
        }

        /// <summary>
        /// Shows a dialog where the user can either add or edit an IngredientModel
        /// </summary>
        /// <param name="aLocationModel">The IngredientModel to edit, or a new IngredientModel</param>
        /// <param name="aNewLocation">Indicates if the IngredientModel already exsists or not</param>
        private async Task AddOrEditIngredient(IngredientModel aIngredientModel, bool aNewIngredient)
        {
            //Set the title
            string sDialogTitle = "Add New Ingredient";
            if (!aNewIngredient)
            {
                sDialogTitle = $"Edit Ingredient: {aIngredientModel.Name}";
            }

            var sParameters = new DialogParameters<IngredientDialog>
            {
                {
                    "mIngredientModel", aIngredientModel
                },
                {
                    "mNewIngredient", aNewIngredient
                }
            };

            var sDialog = await DialogService.ShowAsync<IngredientDialog>(sDialogTitle, sParameters);
            var sResult = await sDialog.Result;

            if (!sResult.Canceled)
            {
                await ViewModel.GetIngredients();
            }
        }

        #region Helpers
        private bool FilterFunction1(IngredientModel aIngredientModel) => FilterFunction2(aIngredientModel, mSearchString);

        /// <summary>
        /// Returns true if the string properties of the IngredientModel contains the search string
        /// </summary>
        /// <param name="aIngredientModel">The location</param>
        /// <param name="mSearchString">The search string</param>
        /// <returns>True if it any specified property contains the search string, false otherwise</returns>
        private bool FilterFunction2(IngredientModel aIngredientModel, string mSearchString)
        {
            if (string.IsNullOrWhiteSpace(mSearchString))
                return true;
            if (aIngredientModel.Name.Contains(mSearchString, StringComparison.OrdinalIgnoreCase))
                return true;
            return false;
        }
        #endregion
    }
}
