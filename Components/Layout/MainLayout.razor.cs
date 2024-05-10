using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace KitchenStock.Components.Layout
{
    public partial class MainLayout
    {
        [Inject] ILocalStorageService LocalStorageService { get; set; }

        //Fields
        private bool mDraweropen = true;
        private bool mDarkMode = true;
        private readonly MudTheme mCustomTheme = new MudTheme //Custom MudTheme
        {
            Palette =
            {
                AppbarBackground = "#5BB450",
            }
        };


        /// <summary>
        /// Get darkMode setting from localstorage and update the UI.
        /// OnInitializedAsync is called twice, once on server side and once on local side
        /// Try/catch catches the exception on server side
        /// </summary>
        protected override async Task OnInitializedAsync()
        {
            try
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
            catch { }
        }

        /// <summary>
        /// Save the Dark Mode toggle value in LocalStorage
        /// </summary>
        private async Task DarkModeToggle()
        {
            //Update the darkmode bool
            mDarkMode = !mDarkMode;

            //Save the state in the local storage
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
