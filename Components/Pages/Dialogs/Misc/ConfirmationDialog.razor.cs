using KitchenStock.Components.ViewModels;
using KitchenStock.Models;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace KitchenStock.Components.Pages.Dialogs.Misc
{
    public partial class ConfirmationDialog
    {
        [CascadingParameter] MudDialogInstance MudDialog { get; set; }

        //Parameter
        [Parameter] public string mMessage { get; set; }
    }
}
