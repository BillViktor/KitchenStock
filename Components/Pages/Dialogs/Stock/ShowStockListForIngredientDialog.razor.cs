using KitchenStock.Components.ViewModels;
using KitchenStock.Models;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace KitchenStock.Components.Pages.Dialogs.Stock
{
    public partial class ShowStockListForIngredientDialog
    {
        [CascadingParameter] MudDialogInstance MudDialog { get; set; }
        [Inject] ViewModel ViewModel { get; set; }
        [Parameter] public IngredientModel mIngredientModel { get; set; }

        //Private field
        private List<StockModel> mStockForIngredient = new List<StockModel>();

        /// <summary>
        /// Get all Locations on initialization
        /// </summary>
        /// <returns></returns>
        protected override async Task OnInitializedAsync()
        {
            await ViewModel.GetStock();
            mStockForIngredient = ViewModel.Stock.Where(x => x.Article.Ingredient == mIngredientModel).ToList();
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
