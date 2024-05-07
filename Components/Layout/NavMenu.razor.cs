using KitchenStock.Components.ViewModels;
using Microsoft.AspNetCore.Components;

namespace KitchenStock.Components.Layout
{
    public partial class NavMenu
    {
        [Inject] ViewModel MasterViewModel { get; set; }

        private int mMaxTries = 10;
        private int mDelayInMs = 100;

        /// <summary>
        /// Wait for the Locations to load, then update UI to show the locations in the navmenu
        /// </summary>
        /// <returns></returns>
        protected override async Task OnParametersSetAsync()
        {
            for(int i = 0; i < mMaxTries; i++)
            {
                if(MasterViewModel.Locations.Count > 0)
                {
                    StateHasChanged();
                    break;
                }
                
                await Task.Delay(mDelayInMs);
            }
        }
    }
}
