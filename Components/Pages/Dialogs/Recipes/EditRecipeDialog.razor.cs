using KitchenStock.Components.Pages.Dialogs.Misc;
using KitchenStock.Components.ViewModels;
using KitchenStock.Models;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace KitchenStock.Components.Pages.Dialogs.Recipes
{
    public partial class EditRecipeDialog
    {
        [CascadingParameter] MudDialogInstance MudDialog { get; set; }
        [Inject] ViewModel ViewModel { get; set; }
        [Inject] ISnackbar Snackbar { get; set; }
        [Inject] IDialogService DialogService { get; set; }

        //Parameters
        [Parameter] public RecipeModel mRecipeModel { get; set; }
        [Parameter] public bool mNewRecipe { get; set; }


        #region CRUD
        /// <summary>
        /// Adds the RecipeModel and then closes the dialog (if successful)
        /// </summary>
        private async Task Add()
        {
            if (!ValidateInputs()) return;

            if (await ViewModel.AddRecipe(mRecipeModel))
            {
                MudDialog.Close();
            }
        }

        /// <summary>
        /// Updates the RecipeModel and then closes the dialog (if successful)
        /// </summary>
        private async Task Update()
        {
            if (!ValidateInputs()) return;

            if (await ViewModel.UpdateRecipe(mRecipeModel))
            {
                MudDialog.Close();
            }
        }

        /// <summary>
        /// Deletes the RecipeModel and then closes the dialog (if successful)
        /// </summary>
        private async Task Delete()
        {
            //Confirm
            var sParameter = new DialogParameters
            {
                { "mMessage", $"Are you sure you want to delete recipe \"{mRecipeModel.Name}\"? This action is irreversable!" }
            };

            var sDialog = await DialogService.ShowAsync<ConfirmationDialog>("Confirm", sParameter);
            var sResult = await sDialog.Result;

            if (sResult.Canceled) return;

            if (await ViewModel.RemoveRecipe(mRecipeModel))
            {
                MudDialog.Close();
            }
        }

        /// <summary>
        /// Shows a dialog where the user can either add or edit an ingredient
        /// </summary>
        /// <param name="aIngredientModel">The IngredientModel to edit, or a new IngredientModel</param>
        /// <param name="aNewIngredient">Indicates if the IngredientModel already exsists or not</param>
        private async Task AddOrEditIngredient(RecipeIngredientModel aIngredientModel, bool aNewIngredient)
        {
            //Set the title
            string sDialogTitle = "Add New Ingredient For Recipe";
            if (!aNewIngredient)
            {
                sDialogTitle = $"Edit Ingredient: {aIngredientModel.Ingredient.Name} In Recipe";
            }

            var sParameters = new DialogParameters<RecipeIngredientDialog>
            {
                {
                    "mRecipeModel", mRecipeModel
                },
                {
                    "mIngredientModel", aIngredientModel
                },
                {
                    "mNewIngredient", aNewIngredient
                }
            };

            var sDialog = await DialogService.ShowAsync<RecipeIngredientDialog>(sDialogTitle, sParameters);
            var sResult = await sDialog.Result;

            //Rerender
            if (!sResult.Canceled)
            {
                StateHasChanged();
            }

        }
        #endregion


        /// <summary>
        /// Closes the modal with a cancel result
        /// </summary>
        private void Cancel()
        {
            MudDialog.Cancel();
        }

        /// <summary>
        /// Helper method that validates the inputs
        /// </summary>
        /// <returns>True if OK, false otherwise</returns>
        private bool ValidateInputs()
        {
            bool sSuccess = true;

            if(string.IsNullOrEmpty(mRecipeModel.Name))
            {
                Snackbar.Add("Name is mandatory!", Severity.Error);
                sSuccess = false;
            }

            if (string.IsNullOrEmpty(mRecipeModel.CookingInstructions))
            {
                Snackbar.Add("Cooking Instructions is mandatory!", Severity.Error);
                sSuccess = false;
            }

            if (mRecipeModel.Ingredients.Count < 1)
            {
                Snackbar.Add("Recipe must have at least one ingredient!", Severity.Error);
                sSuccess = false;
            }

            return sSuccess;
        }
    }
}
