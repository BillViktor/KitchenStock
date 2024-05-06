using KitchenStock.Components.Pages.Dialogs;
using KitchenStock.Components.ViewModels;
using KitchenStock.Models;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace KitchenStock.Components.Pages
{
    public partial class Locations
    {
        [Inject] MasterViewModel MasterViewModel { get; set; }
        [Inject] IDialogService DialogService { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await MasterViewModel.GetLocations();
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
                await MasterViewModel.GetLocations();
            }
        }
    }
}
