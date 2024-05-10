using KitchenStock.Components.Pages.Dialogs.Location;
using KitchenStock.Components.ViewModels;
using KitchenStock.Models;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace KitchenStock.Components.Pages.Locations
{
    public partial class Locations
    {
        [Inject] ViewModel ViewModel { get; set; }
        [Inject] IDialogService DialogService { get; set; }

        //Field
        private string mSearchString = "";

        /// <summary>
        /// Get Locations on page initilization
        /// </summary>
        /// <returns></returns>
        protected override async Task OnInitializedAsync()
        {
            await ViewModel.GetLocations();
        }

        /// <summary>
        /// Shows a dialog where the user can either add or edit a Location
        /// </summary>
        /// <param name="aLocationModel">The Location to edit, or a new Location</param>
        /// <param name="aNewLocation">Indicates if the Location already exsists or not</param>
        private async Task AddOrEditLocation(LocationModel aLocationModel, bool aNewLocation)
        {
            //Set the title
            string sDialogTitle = "Add New Location";
            if (!aNewLocation)
            {
                sDialogTitle = $"Edit Location: {aLocationModel.Name}";
            }

            var sParameters = new DialogParameters<LocationDialog>
            {
                {
                    "mLocationModel", aLocationModel
                },
                {
                    "mNewLocation", aNewLocation
                }
            };

            var sDialog = await DialogService.ShowAsync<LocationDialog>(sDialogTitle, sParameters);
            var sResult = await sDialog.Result;

            if (!sResult.Canceled)
            {
                await ViewModel.GetLocations();
            }
        }

        #region Helpers
        private bool FilterFunction1(LocationModel aLocationModel) => FilterFunction2(aLocationModel, mSearchString);

        /// <summary>
        /// Returns true if the string properties of the LocationModel contains the search string
        /// </summary>
        /// <param name="aLocationModel">The location</param>
        /// <param name="mSearchString">The search string</param>
        /// <returns>True if it any specified property contains the search string, false otherwise</returns>
        private bool FilterFunction2(LocationModel aLocationModel, string mSearchString)
        {
            if (string.IsNullOrWhiteSpace(mSearchString))
                return true;
            if (aLocationModel.Name.Contains(mSearchString, StringComparison.OrdinalIgnoreCase))
                return true;
            if (aLocationModel.Description != null && aLocationModel.Description.Contains(mSearchString, StringComparison.OrdinalIgnoreCase))
                return true;
            return false;
        }
        #endregion
    }
}
