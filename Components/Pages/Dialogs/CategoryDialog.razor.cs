using KitchenStock.Components.ViewModels;
using KitchenStock.Models;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace KitchenStock.Components.Pages.Dialogs
{
    public partial class CategoryDialog
    {
        [CascadingParameter] MudDialogInstance MudDialog { get; set; }
        [Parameter] public CategoryModel mCategoryModel { get; set; }
        [Parameter] public bool mNewCategory { get; set; }

        [Inject] MasterViewModel MasterViewModel { get; set; }
        [Inject] ISnackbar Snackbar { get; set; }


        #region CRUD
        /// <summary>
        /// Adds the category and then closes the dialog (if successful)
        /// </summary>
        private async Task Add()
        {
            if (!ValidateInputs()) return;

            if(await MasterViewModel.AddCategory(mCategoryModel))
            {
                MudDialog.Close();
            }
        }

        /// <summary>
        /// Updates the category and then closes the dialog (if successful)
        /// </summary>
        private async Task Update()
        {
            if (!ValidateInputs()) return;

            if (await MasterViewModel.UpdateCategory(mCategoryModel))
            {
                MudDialog.Close();
            }
        }

        /// <summary>
        /// Deletes the category and then closes the dialog (if successful)
        /// </summary>
        private async Task Delete()
        {
            if(await MasterViewModel.RemoveCategory(mCategoryModel))
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

            if(string.IsNullOrEmpty(mCategoryModel.Name))
            {
                Snackbar.Add("Name is mandatory!", Severity.Error);
                sSuccess = false;
            }

            return sSuccess;
        }
    }
}
