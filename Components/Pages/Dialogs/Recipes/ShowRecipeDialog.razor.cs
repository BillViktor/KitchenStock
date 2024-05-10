using KitchenStock.Components.Pages.Dialogs.ShoppingList;
using KitchenStock.Components.Pages.Dialogs.Stock;
using KitchenStock.Components.ViewModels;
using KitchenStock.Models;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace KitchenStock.Components.Pages.Dialogs.Recipes
{
    public partial class ShowRecipeDialog
    {
        [CascadingParameter] MudDialogInstance MudDialog { get; set; }
        [Inject] ViewModel ViewModel { get; set; }
        [Inject] ISnackbar Snackbar { get; set; }
        [Inject] IDialogService DialogService { get; set; }

        //Parameters
        [Parameter] public RecipeModel mRecipeModel { get; set; }
        [Parameter] public SuggestedRecipeModel mSuggestedRecipeModel { get; set; }

        //Fields
        private int mPortionSize = 4;
        private double mMultiplier
        {
            get
            {
                return (double) mPortionSize / mRecipeModel.PortionCount;
            }
        }

        private double mKcalPerPortion = 0;
        private double mCarbsPerPotion = 0;
        private double mProteinPerPortion = 0;
        private double mFatsPerPortion = 0;

        private bool mInaccurateResults = false;
        private bool mShowInaccurateResultsPopOver = false;

        /// <summary>
        /// Set the PortionSize to the recipes portionsize
        /// </summary>
        protected override async Task OnInitializedAsync()
        {
            mPortionSize = mRecipeModel.PortionCount;

            mKcalPerPortion = mRecipeModel.GetKcalPerPortion(out mInaccurateResults);
            mCarbsPerPotion = mRecipeModel.GetCarbsPerPortion(out mInaccurateResults);
            mProteinPerPortion = mRecipeModel.GetProteinsPerPortion(out mInaccurateResults);
            mFatsPerPortion = mRecipeModel.GetFatsPerPortion(out mInaccurateResults);

            if(mSuggestedRecipeModel != null)
            {
                await ViewModel.GetArticles();
                await ViewModel.GetShoppingList();
            }
        }

        /// <summary>
        /// Closes the modal with a cancel result
        /// </summary>
        private void Cancel()
        {
            MudDialog.Cancel();
        }

        /// <summary>
        /// Opens the dialog for adding an item to the shopping list, with preselected ingredient
        /// </summary>
        /// <param name="aIngredientModel"></param>
        /// <returns></returns>
        private async Task AddItemToShoppingList(RecipeIngredientModel aIngredientModel)
        {
            //Make sure that there exists an article connected to the specified ingredient first
            if(!ViewModel.Articles.Any(x => x.Ingredient == aIngredientModel.Ingredient))
            {
                Snackbar.Add("There exists no article connected to the specified ingredient! Please add an article and try again.", Severity.Error);
                return;
            }

            //Set the title
            string sDialogTitle = $"Add Ingredient {aIngredientModel.Ingredient.Name} to Shopping List";

            var sParameters = new DialogParameters<ShoppingItemDialog>
            {
                {
                    "mShoppingModel", new ShoppingModel()
                },
                {
                    "mNewShoppingModel", true
                },
                {
                    "mIngredientModel", aIngredientModel.Ingredient
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
        /// Shows all related stock to an ingredient
        /// </summary>
        /// <param name="aIngredientModel">The ingredient to show stock for</param>
        private async Task ShowStockForIngredient(IngredientModel aIngredientModel)
        {
            //Set the title
            string sDialogTitle = $"Showing Stock Related to Ingredient \"{aIngredientModel.Name}\"";

            var sParameters = new DialogParameters<ShowStockListForIngredientDialog>
            {
                {
                    "mIngredientModel", aIngredientModel
                }
            };

            await DialogService.ShowAsync<ShowStockListForIngredientDialog>(sDialogTitle, sParameters);
        }

        /// <summary>
        /// Automatically updates/removes stock, shows a dialog with changes, allowing the user to confirm
        /// </summary>
        /// <returns></returns>
        private async Task RemoveStockForRecipe()
        {
            List<StockTransactionRecipeModel> sStockTransactionList = await ViewModel.GetStockToRemoveFromRecipe(mRecipeModel, mMultiplier);

            //Set the title
            string sDialogTitle = $"Showing Suggested Stock Transactions For Recipe";

            var sOptions = new DialogOptions
            {
                FullWidth = true,
            };

            var sParameters = new DialogParameters<ShowStockTransactionForRecipeDialog>
            {
                {
                    "mStockTransactionList", sStockTransactionList
                }
            };

            await DialogService.ShowAsync<ShowStockTransactionForRecipeDialog>(sDialogTitle, sParameters, sOptions);
        }

        #region Helpers
        /// <summary>
        /// Returns a styling for the ingredient if the recipe is shown from the Suggested Recipes page
        /// </summary>
        /// <param name="aRecipeIngredientModel"></param>
        /// <returns></returns>
        private string GetIngredientStyling(RecipeIngredientModel aRecipeIngredientModel)
        {
            //No margin on the first ingredient
            if (mRecipeModel.Ingredients.FirstOrDefault() == aRecipeIngredientModel)
            {
                if(mSuggestedRecipeModel != null)
                {
                    return GetColorForIngredient(aRecipeIngredientModel);
                }
                return "";
            }

            //No styling if it's not from the suggestions page
            if (mSuggestedRecipeModel == null) return "margin-top: -10px;";

            //Otherwise, add styling depending on the stock
            return GetColorForIngredient(aRecipeIngredientModel);
        }

        /// <summary>
        /// Returns a red, orange or no color depending on the stock
        /// </summary>
        /// <param name="aRecipeIngredient">The Recipe Ingredient to look up</param>
        /// <returns>A CSS-styling string</returns>
        private string GetColorForIngredient(RecipeIngredientModel aRecipeIngredient)
        {
            if(mSuggestedRecipeModel.IsStockEnough(aRecipeIngredient, mMultiplier) == 2)
            {
                //Full stock
                return "margin-top: -10px;";
            }
            else if (mSuggestedRecipeModel.IsStockEnough(aRecipeIngredient, mMultiplier) == 1)
            {
                //Partial stock
                return "color: darkorange; margin-top: -10px;";
            }
            if (mSuggestedRecipeModel.IsStockEnough(aRecipeIngredient, mMultiplier) == 0)
            {
                //No stock
                return "color: red; margin-top: -10px;";
            }

            return "";
        }
        #endregion
    }
}
