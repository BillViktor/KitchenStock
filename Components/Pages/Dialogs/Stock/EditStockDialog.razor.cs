using KitchenStock.Components.ViewModels;
using KitchenStock.Models;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace KitchenStock.Components.Pages.Dialogs.Stock
{
    public partial class EditStockDialog
    {
        [CascadingParameter] MudDialogInstance MudDialog { get; set; }
        [Inject] ViewModel ViewModel { get; set; }
        [Parameter] public StockModel mStockModel { get; set; }

        /// <summary>
        /// Get all Locations on initialization
        /// </summary>
        /// <returns></returns>
        protected override async Task OnInitializedAsync()
        {
            await ViewModel.GetLocations();
        }

        /// <summary>
        /// Closes the modal with a cancel result
        /// </summary>
        private void Cancel()
        {
            MudDialog.Cancel();
        }

        /// <summary>
        /// Updates the Stock, then closes the dialog (if successful)
        /// </summary>
        private async Task Save()
        {
            if(await ViewModel.UpdateStock(mStockModel))
            {
                MudDialog.Close();
            }
        }
    }
}
