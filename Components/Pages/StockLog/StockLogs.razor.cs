using KitchenStock.Components.Pages.Dialogs;
using KitchenStock.Components.ViewModels;
using KitchenStock.Models;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace KitchenStock.Components.Pages.StockLog
{
    public partial class StockLogs
    {
        [Inject] ViewModel ViewModel { get; set; }

        //Field
        private string mSearchString = "";

        //The start date of the logs
        private DateTime mStartDate = DateTime.Now.AddDays(-90);

        /// <summary>
        /// Get Articles on page initilization
        /// </summary>
        /// <returns></returns>
        protected override async Task OnInitializedAsync()
        {
            await ViewModel.GetStockLogModels(mStartDate);
            await ViewModel.GetLocations(); //Get Locations (for navmenu)
        }

        #region Helpers
        private bool FilterFunction1(StockLogModel aStockLogModel) => FilterFunction2(aStockLogModel, mSearchString);

        /// <summary>
        /// Returns true if the string properties of the StockLogModel contains the search string
        /// </summary>
        /// <param name="aStockLogModel">The StockLogModel</param>
        /// <param name="mSearchString">The search string</param>
        /// <returns>True if it any specified property contains the search string, false otherwise</returns>
        private bool FilterFunction2(StockLogModel aStockLogModel, string mSearchString)
        {
            if (string.IsNullOrWhiteSpace(mSearchString))
                return true;
            if (aStockLogModel.Article.Name.Contains(mSearchString, StringComparison.OrdinalIgnoreCase))
                return true;
            if (aStockLogModel.PreviousLocation.Name.Contains(mSearchString, StringComparison.OrdinalIgnoreCase))
                return true;
            if (aStockLogModel.NewLocation.Name.Contains(mSearchString, StringComparison.OrdinalIgnoreCase))
                return true;
            if (aStockLogModel.Description.Contains(mSearchString, StringComparison.OrdinalIgnoreCase))
                return true;
            return false;
        }
        #endregion
    }
}
