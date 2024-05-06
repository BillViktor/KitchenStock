﻿using KitchenStock.Components.ViewModels;
using KitchenStock.Models;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace KitchenStock.Components.Pages.Dialogs
{
    public partial class ArticleDialog
    {
        [CascadingParameter] MudDialogInstance MudDialog { get; set; }
        [Parameter] public ArticleModel mArticleModel { get; set; }
        [Parameter] public bool mNewArticle { get; set; }

        [Inject] ViewModel ViewModel { get; set; }
        [Inject] ISnackbar Snackbar { get; set; }
        [Inject] IDialogService DialogService { get; set; }

        Dictionary<CategoryModel, bool> mCategoryIdAndAdded = new Dictionary<CategoryModel, bool>();

        protected override async Task OnInitializedAsync()
        {
            await ViewModel.GetCategories();

            InitializeDictionary();
        }

        /// <summary>
        /// Initializes the dictionary with category and bools (indicating if the article belongs to that category)
        /// </summary>
        private void InitializeDictionary()
        {
            foreach(var sCategory in ViewModel.Categories)
            {
                if(mArticleModel.Categories != null && mArticleModel.Categories.Any(x => x.Id == sCategory.Id))
                {
                    mCategoryIdAndAdded.Add(sCategory, true);
                }
                else
                {
                    mCategoryIdAndAdded.Add(sCategory, false);
                }
            }
        }

        #region CRUD
        /// <summary>
        /// Adds the location and then closes the dialog (if successful)
        /// </summary>
        private async Task Add()
        {
            if (!ValidateInputs()) return;

            SetCategories();

            if (await ViewModel.AddArticle(mArticleModel))
            {
                MudDialog.Close();
            }
        }

        /// <summary>
        /// Updates the location and then closes the dialog (if successful)
        /// </summary>
        private async Task Update()
        {
            if (!ValidateInputs()) return;

            SetCategories();

            if (await ViewModel.UpdateArticle(mArticleModel))
            {
                MudDialog.Close();
            }
        }

        /// <summary>
        /// Deletes the location and then closes the dialog (if successful)
        /// </summary>
        private async Task Delete()
        {
            //Confirm
            var sParameter = new DialogParameters
            {
                { "mMessage", $"Are you sure you want to delete article \"{mArticleModel.Name}\"? All related Stock will be deleted! This action is irreversable!" }
            };

            var sDialog = await DialogService.ShowAsync<ConfirmationDialog>("Confirm", sParameter);
            var sResult = await sDialog.Result;

            if (sResult.Canceled) return;

            if (await ViewModel.RemoveArticle(mArticleModel))
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

            if(string.IsNullOrEmpty(mArticleModel.Name))
            {
                Snackbar.Add("Name is mandatory!", Severity.Error);
                sSuccess = false;
            }

            if(!mCategoryIdAndAdded.Any(x => x.Value))
            {
                Snackbar.Add("The article must have at least one category!", Severity.Error);
                sSuccess = false;
            }

            return sSuccess;
        }

        /// <summary>
        /// Helper method, sets the categories for the article
        /// </summary>
        private void SetCategories()
        {
            //Add the correct categories
            mArticleModel.Categories = new List<CategoryModel>();

            foreach (var sCategory in mCategoryIdAndAdded)
            {
                if (sCategory.Value)
                {
                    mArticleModel.Categories.Add(sCategory.Key);
                }
            }
        }
    }
}
