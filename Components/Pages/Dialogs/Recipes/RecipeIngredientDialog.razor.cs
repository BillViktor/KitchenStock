using KitchenStock.Components.ViewModels;
using KitchenStock.Models;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace KitchenStock.Components.Pages.Dialogs.Recipes
{
    public partial class RecipeIngredientDialog
    {
        [CascadingParameter] MudDialogInstance MudDialog { get; set; }
        [Inject] ViewModel ViewModel { get; set; }
        [Inject] ISnackbar Snackbar { get; set; }

        //Parameters
        [Parameter] public RecipeModel mRecipeModel { get; set; }
        [Parameter] public RecipeIngredientModel mIngredientModel { get; set; }
        [Parameter] public bool mNewIngredient { get; set; }

        private RecipeIngredientModel mIngredientModelCopy;

        /// <summary>
        /// Get all ingredients
        /// Copy the original ingredient, in case of cancel
        /// </summary>
        protected override async Task OnInitializedAsync()
        {
            mIngredientModelCopy = new RecipeIngredientModel(mIngredientModel);

            await ViewModel.GetIngredients();
        }


        #region CRUD
        /// <summary>
        /// Validates the inputs and closes the dialog
        /// </summary>
        private void AddOrUpdate()
        {
            if (!ValidateInputs()) return;

            //If it's a new ingredient we have to add it, if not, it's already updated (reference type)
            if(mNewIngredient)
            {
                mRecipeModel.Ingredients.Add(mIngredientModel);
            }

            MudDialog.Close();
        }

        /// <summary>
        /// Deletes the ingredient
        /// </summary>
        private void Delete()
        {
            mRecipeModel.Ingredients.Remove(mIngredientModel);

            MudDialog.Close();
        }
        #endregion


        /// <summary>
        /// Closes the modal with a cancel result
        /// </summary>
        private void Cancel()
        {
            //Set the ingredient back to the copy/original
            mIngredientModel.QuantityInPieces = mIngredientModelCopy.QuantityInPieces;
            mIngredientModel.VolumeInMilliliters = mIngredientModelCopy.VolumeInMilliliters;
            mIngredientModel.WeightInGrams = mIngredientModelCopy.WeightInGrams;
            mIngredientModel.MeasurementDescription = mIngredientModelCopy.MeasurementDescription;
            mIngredientModel.MeasurementType = mIngredientModelCopy.MeasurementType;

            //Close the dialog (cancel)
            MudDialog.Cancel();
        }

        /// <summary>
        /// Helper method that validates the inputs
        /// </summary>
        /// <returns>True if OK, false otherwise</returns>
        private bool ValidateInputs()
        {
            bool sSuccess = true;

            if(mIngredientModel.Ingredient == null)
            {
                Snackbar.Add("You must choose an ingredient!", Severity.Error);
                sSuccess = false;
            }

            return sSuccess;
        }

        /// <summary>
        /// AutoComplete Search function
        /// </summary>
        /// <param name="aValue">The value to search for</param>
        /// <returns>All IngredientModels with matching name</returns>
        private async Task<IEnumerable<IngredientModel>> Search(string aValue)
        {
            await Task.Delay(0);
            // if text is null or empty, show complete list
            if (string.IsNullOrEmpty(aValue)) return ViewModel.Ingredients;

            //Otherwise, return matching article names
            return ViewModel.Ingredients.Where(x => x.Name.Contains(aValue, StringComparison.InvariantCultureIgnoreCase));
        }
    }
}
