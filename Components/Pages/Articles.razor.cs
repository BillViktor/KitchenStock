using KitchenStock.Components.Pages.Dialogs;
using KitchenStock.Components.ViewModels;
using KitchenStock.Models;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace KitchenStock.Components.Pages
{
    public partial class Articles
    {
        [Inject] MasterViewModel MasterViewModel { get; set; }
        [Inject] IDialogService DialogService { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await MasterViewModel.GetArticles();
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
                await MasterViewModel.GetArticles();
            }
        }
    }
}
