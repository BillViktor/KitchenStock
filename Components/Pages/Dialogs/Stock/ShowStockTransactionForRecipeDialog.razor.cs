using KitchenStock.Components.Pages.Dialogs.Misc;
using KitchenStock.Components.ViewModels;
using KitchenStock.Models;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace KitchenStock.Components.Pages.Dialogs.Stock
{
    public partial class ShowStockTransactionForRecipeDialog
    {
        [CascadingParameter] MudDialogInstance MudDialog { get; set; }
        [Inject] ViewModel ViewModel { get; set; }
        [Inject] ISnackbar Snackbar { get; set; }
        [Inject] IDialogService DialogService { get; set; }
        [Parameter] public List<StockTransactionRecipeModel> mStockTransactionList { get; set; }


        /// <summary>
        /// Confirms the stock transactions
        /// </summary>
        /// <returns></returns>
        private async Task Confirm()
        {
            //Show confirm dialog
            var sParameter = new DialogParameters
            {
                { "mMessage", $"Are you sure you want to confirm these Stock Transactions? This action is irreversable!" }
            };

            var sDialog = await DialogService.ShowAsync<ConfirmationDialog>("Confirm", sParameter);
            var sResult = await sDialog.Result;

            if (sResult.Canceled) return;

            //Keep track of StockModels changed & removed
            int sNumbOfRemovedStocks = 0;
            int sNumbOfUpdatedStock = 0;
            bool sSuccess = true;

            //Loop through all ingredients
            foreach (var sIngredient in mStockTransactionList.Where(x => x.StockAndNewPercentageLeft.Count > 0))
            {
                //Loop through all transaction
                foreach(var sTransaction in sIngredient.StockAndNewPercentageLeft)
                {
                    //If the new percentage left is 0, remove the stock
                    if(sTransaction.Value == 0)
                    {
                        if(!await ViewModel.RemoveStock(sTransaction.Key))
                        {
                            sSuccess = false;
                        }
                        sNumbOfRemovedStocks++;
                    }
                    else
                    {
                        //If not, update the stock
                        StockModel sOriginalStock = new StockModel(sTransaction.Key);
                        sTransaction.Key.PercentageLeft = sTransaction.Value;
                        if(!await ViewModel.UpdateStock(sTransaction.Key, sOriginalStock))
                        {
                            sSuccess = false;
                        }
                        sNumbOfUpdatedStock++;
                    }
                }
            }
            
            if(sSuccess)
            {
                Snackbar.Add($"Successfully removed {sNumbOfRemovedStocks} stock and updated {sNumbOfUpdatedStock}", Severity.Success);
                MudDialog.Close();
            }
            else
            {
                Snackbar.Add("One or more errors occured when updating/removing stock! Please check the Stock Log too see which Stock where updated!", Severity.Error);
            }
        }

        /// <summary>
        /// Closes the modal with a cancel result
        /// </summary>
        private void Cancel()
        {
            MudDialog.Cancel();
        }
    }
}
