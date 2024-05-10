using KitchenStock.Components.ViewModels;
using KitchenStock.Models;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace KitchenStock.Components.Pages.Dialogs.ShoppingList;

public partial class ShoppingItemDialog
{
    [CascadingParameter] MudDialogInstance MudDialog { get; set; }
    [Inject] ViewModel ViewModel { get; set; }
    [Inject] ISnackbar Snackbar { get; set; }

    //Parameters
    [Parameter] public ShoppingModel mShoppingModel { get; set; }
    [Parameter] public bool mNewShoppingModel { get; set; }
    [Parameter] public IngredientModel mIngredientModel { get; set; }

    //Fields
    private List<ArticleModel> mArticles = new List<ArticleModel>();


    /// <summary>
    /// Fetch articles for Article Selection
    /// </summary>
    protected override async Task OnInitializedAsync()
    {
        if(mNewShoppingModel)
        {
            await ViewModel.GetArticles();
        }

        if(mIngredientModel != null)
        {
            //Only show related articles
            mArticles = ViewModel.Articles.Where(x => x.Ingredient == mIngredientModel).ToList();
        }
    }

    #region CRUD
    /// <summary>
    /// Adds the ShoppingModel and then closes the dialog (if successful)
    /// </summary>
    private async Task Add()
    {
        if (!ValidateInputs()) return;

        if (await ViewModel.AddShoppingListItem(mShoppingModel))
        {
            MudDialog.Close();
        }
    }

    /// <summary>
    /// Updates the ShoppingModel and then closes the dialog (if successful)
    /// </summary>
    private async Task Update()
    {
        if (!ValidateInputs()) return;

        if (await ViewModel.UpdateShoppingListItem(mShoppingModel))
        {
            MudDialog.Close();
        }
    }

    /// <summary>
    /// Deletes the ShoppingModel and then closes the dialog (if successful)
    /// </summary>
    private async Task Delete()
    {
        if (await ViewModel.RemoveShoppingListItem(mShoppingModel))
        {
            MudDialog.Close();
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

    #region Helpers
    /// <summary>
    /// Helper method that validates the inputs
    /// </summary>
    /// <returns>True if OK, false otherwise</returns>
    private bool ValidateInputs()
    {
        bool sSuccess = true;

        if(mShoppingModel.Article == null)
        {
            Snackbar.Add("You must choose an article!", Severity.Error);
            sSuccess = false;
        }

        if(mShoppingModel.Quantity < 1 || mShoppingModel.Quantity > 100)
        {
            Snackbar.Add("Quantity must be between 1-100!", Severity.Error);
            sSuccess = false;
        }

        return sSuccess;
    }

    /// <summary>
    /// AutoComplete Search function, searches the ViewModel articles or the local list (if routed from recipe)
    /// </summary>
    /// <param name="aValue">The value to search for</param>
    /// <returns>All ArticleModels with matching name</returns>
    private async Task<IEnumerable<ArticleModel>> Search(string aValue)
    {
        await Task.Delay(0);
        // if text is null or empty, show complete list
        if (string.IsNullOrEmpty(aValue))
        {
            if(mIngredientModel == null) return ViewModel.Articles;
            return mArticles;
        }

        //Otherwise, return matching article names
        if (mIngredientModel == null) return ViewModel.Articles.Where(x => x.Name.Contains(aValue, StringComparison.InvariantCultureIgnoreCase));
        return mArticles.Where(x => x.Name.Contains(aValue, StringComparison.InvariantCultureIgnoreCase));
    }
    #endregion
}
