using KitchenStock.Components.ViewModels;
using KitchenStock.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using MudBlazor;
using static MudBlazor.Colors;

namespace KitchenStock.Components.Pages.Dialogs.Stock
{
    public partial class AddStockDialog
    {
        [CascadingParameter] MudDialogInstance MudDialog { get; set; }
        [Inject] MasterViewModel MasterViewModel { get; set; }
        [Inject] ISnackbar Snackbar { get; set; }
        [Parameter] public LocationModel mLocationModel { get; set; }

        private StockModel mStockModel = new StockModel
        {
            BestBeforeDate = DateTime.Now,
            PercentageLeft = 100,
        };
        private int mQuantity = 1;
        private string mEAN = "";

        protected override async Task OnInitializedAsync()
        {
            await MasterViewModel.GetLocations();

            if(mLocationModel != null)
            {
                mStockModel.Location = mLocationModel;
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
        /// Adds the Stock, then closes the dialog (if successful)
        /// </summary>
        private async Task Add()
        {
            if (!ValidateInputs()) return;

            if(await MasterViewModel.AddStock(mStockModel, mQuantity))
            {
                MudDialog.Close();
            }
        }

        #region Helpers
        /// <summary>
        /// Helper method that validates the inputs
        /// </summary>
        /// <returns>True if OK, false otherwise</returns>
        private bool ValidateInputs()
        {
            bool sSuccess = true;

            if(mStockModel.Article == null)
            {
                Snackbar.Add("You must select an article!", Severity.Error);
                sSuccess = false;
            }

            if (mStockModel.Location == null)
            {
                Snackbar.Add("You must select a location!", Severity.Error);
                sSuccess = false;
            }

            if (mQuantity < 1 || mQuantity > 100)
            {
                Snackbar.Add("Quantity must be between 1-100!", Severity.Error);
                sSuccess = false;
            }

            return sSuccess;
        }

        /// <summary>
        /// Execute on keydown in Article EAN input, calls method to find article if enter was pressed
        /// </summary>
        /// <param name="e"></param>
        private void FindArticle(KeyboardEventArgs e)
        {
            if (MasterViewModel.Articles.Any(x => x.EAN == mEAN))
            {
                mStockModel.Article = MasterViewModel.Articles.First(x => x.EAN == mEAN);
            }
        }

        /// <summary>
        /// AutoComplete Search function
        /// </summary>
        /// <param name="aValue">The value to search for</param>
        /// <returns>All ArticleModels with matching name</returns>
        private async Task<IEnumerable<ArticleModel>> Search(string aValue)
        {
            // if text is null or empty, show complete list
            if (string.IsNullOrEmpty(aValue)) return MasterViewModel.Articles;

            //Otherwise, return matching article names
            return MasterViewModel.Articles.Where(x => x.Name.Contains(aValue, StringComparison.InvariantCultureIgnoreCase));
        }
        #endregion
    }
}
