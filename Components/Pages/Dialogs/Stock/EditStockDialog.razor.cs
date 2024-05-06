using KitchenStock.Components.ViewModels;
using KitchenStock.Models;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace KitchenStock.Components.Pages.Dialogs.Stock
{
    public partial class EditStockDialog
    {
        [CascadingParameter] MudDialogInstance MudDialog { get; set; }
        [Inject] MasterViewModel MasterViewModel { get; set; }
        [Parameter] public StockModel mStockModel { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await MasterViewModel.GetLocations();
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
            if(await MasterViewModel.UpdateStock(mStockModel))
            {
                MudDialog.Close();
            }
        }

        /// <summary>
        /// Closes the dialog
        /// </summary>
        private void Close()
        {
            MudDialog.Cancel();
        }
    }
}
