using KitchenStock.Components.Pages.Dialogs.Misc;
using KitchenStock.Components.ViewModels;
using KitchenStock.Models;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace KitchenStock.Components.Pages.Dialogs.Ingredient;

public partial class IngredientDialog
{
    [CascadingParameter] MudDialogInstance MudDialog { get; set; }
    [Inject] ViewModel ViewModel { get; set; }
    [Inject] ISnackbar Snackbar { get; set; }
    [Inject] IDialogService DialogService { get; set; }

    //Parameters
    [Parameter] public IngredientModel mIngredientModel { get; set; }
    [Parameter] public bool mNewIngredient { get; set; }


    #region CRUD
    /// <summary>
    /// Adds the ingredient and then closes the dialog (if successful)
    /// </summary>
    private async Task Add()
    {
        if (!ValidateInputs()) return;

        if(await ViewModel.AddIngredient(mIngredientModel))
        {
            MudDialog.Close();
        }
    }

    /// <summary>
    /// Updates the ingredient and then closes the dialog (if successful)
    /// </summary>
    private async Task Update()
    {
        if (!ValidateInputs()) return;

        if (await ViewModel.UpdateIngredient(mIngredientModel))
        {
            MudDialog.Close();
        }
    }

    /// <summary>
    /// Deletes the ingredient and then closes the dialog (if successful)
    /// </summary>
    private async Task Delete()
    {
        //Confirm
        var sParameter = new DialogParameters
        {
            { "mMessage", $"Are you sure you want to delete ingredient \"{mIngredientModel.Name}\"? All related articles, including their stock, and recipes will be deleted! This action is irreversable!" }
        };

        var sDialog = await DialogService.ShowAsync<ConfirmationDialog>("Confirm", sParameter);
        var sResult = await sDialog.Result;

        if (sResult.Canceled) return;

        if (await ViewModel.RemoveIngredient(mIngredientModel))
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

    /// <summary>
    /// Helper method that validates the inputs
    /// </summary>
    /// <returns>True if OK, false otherwise</returns>
    private bool ValidateInputs()
    {
        bool sSuccess = true;

        if(string.IsNullOrEmpty(mIngredientModel.Name))
        {
            Snackbar.Add("Name is mandatory!", Severity.Error);
            sSuccess = false;
        }

        return sSuccess;
    }
}
