using KitchenStock.Components.Pages.Dialogs;
using KitchenStock.Components.ViewModels;
using KitchenStock.Models;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace KitchenStock.Components.Pages
{
    public partial class Articles
    {
        [Inject] ViewModel ViewModel { get; set; }
        [Inject] IDialogService DialogService { get; set; }

        private string mSearchString = "";

        /// <summary>
        /// Get Articles on page initilization
        /// </summary>
        /// <returns></returns>
        protected override async Task OnInitializedAsync()
        {
            await ViewModel.GetArticles();
        }

        /// <summary>
        /// Shows a dialog where the user can either add or edit a Location
        /// </summary>
        /// <param name="aLocationModel">The Location to edit, or a new Location</param>
        /// <param name="aNewLocation">Indicates if the Location already exsists or not</param>
        private async Task AddOrEditArticle(ArticleModel aArticleModel, bool aNewArticle)
        {
            //Set the title
            string sDialogTitle = "Add New Article";
            if (!aNewArticle)
            {
                sDialogTitle = $"Edit Article: {aArticleModel.Name}";
            }

            var sParameters = new DialogParameters<ArticleDialog>
            {
                {
                    "mArticleModel", aArticleModel
                },
                {
                    "mNewArticle", aNewArticle
                }
            };

            var sDialog = await DialogService.ShowAsync<ArticleDialog>(sDialogTitle, sParameters);
            var sResult = await sDialog.Result;

            if (!sResult.Canceled)
            {
                await ViewModel.GetArticles();
            }
        }

        #region Helpers
        private bool FilterFunction1(ArticleModel aArticleModel) => FilterFunction2(aArticleModel, mSearchString);

        /// <summary>
        /// Returns true if the string properties of the ArticleModel contains the search string
        /// </summary>
        /// <param name="aArticleModel">The category</param>
        /// <param name="mSearchString">The search string</param>
        /// <returns>True if it any specified property contains the search string, false otherwise</returns>
        private bool FilterFunction2(ArticleModel aArticleModel, string mSearchString)
        {
            if (string.IsNullOrWhiteSpace(mSearchString))
                return true;
            if (aArticleModel.Name.Contains(mSearchString, StringComparison.OrdinalIgnoreCase))
                return true;
            if (aArticleModel.Description != null && aArticleModel.Description.Contains(mSearchString, StringComparison.OrdinalIgnoreCase))
                return true;
            if (aArticleModel.GetCategoryString().Contains(mSearchString, StringComparison.OrdinalIgnoreCase))
                return true;
            if (aArticleModel.EAN != null && aArticleModel.EAN.Contains(mSearchString, StringComparison.OrdinalIgnoreCase))
                return true;
            return false;
        }
        #endregion
    }
}
