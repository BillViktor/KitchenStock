using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;

namespace KitchenStock.Components.Layout
{
    public partial class MainLayout
    {
        [Inject] ILocalStorageService LocalStorageService { get; set; }

        private bool mDraweropen = true;
        private bool mDarkMode = true;

        /// <summary>
        /// On the first render, get the darkMode setting in LocalStorage and update the theme if necessary
        /// </summary>
        protected override async Task OnAfterRenderAsync(bool aFirstRender)
        {
            if (aFirstRender)
            {
                if (await LocalStorageService.ContainKeyAsync("darkMode"))
                {
                    var sResult = await LocalStorageService.GetItemAsync<bool>("darkMode");
                    if (!sResult)
                    {
                        mDarkMode = false;
                    }
                }
            }
        }

        /// <summary>
        /// Save the Dark Mode toggle value in LocalStorage
        /// </summary>
        private async Task DarkModeToggle()
        {
            await LocalStorageService.SetItemAsync<bool>("darkMode", mDarkMode);
        }

        /// <summary>
        /// Toggles the drawer
        /// </summary>
        private void DrawerToggle()
        {
            mDraweropen = !mDraweropen;
        }
    }
}
