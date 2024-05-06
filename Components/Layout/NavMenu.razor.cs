using KitchenStock.Components.ViewModels;
using KitchenStock.Models;
using Microsoft.AspNetCore.Components;

namespace KitchenStock.Components.Layout
{
    public partial class NavMenu
    {
        [Inject] MasterViewModel MasterViewModel { get; set; }

        public List<LocationModel> mLocations = new List<LocationModel>();

        //protected override async Task OnInitializedAsync()
        //{
        //    await MasterViewModel.GetLocations();
        //}
    }
}
