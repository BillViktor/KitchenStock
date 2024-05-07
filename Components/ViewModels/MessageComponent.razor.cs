using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace KitchenStock.Components.ViewModels
{
    public partial class MessageComponent
    {
        //Component Parameters
        [Parameter] public bool IsBusy { get { return mIsBusy; } set { mIsBusy = value; } }
        [Parameter] public List<string> Errors { get { return mErrors; } set { mErrors = value; } }
        [Parameter] public List<string> SuccessMessages { get { return mSuccessMessages; } set { mSuccessMessages = value; } }

        //Private fields
        private bool mIsBusy = false;
        private List<string> mErrors = new List<string>();
        private List<string> mSuccessMessages = new List<string>();

        /// <summary>
        /// Shows an error message, then removes the message to avoid showing the same message multiple times
        /// </summary>
        /// <param name="aMessage">The error message to show</param>
        private object ShowError(string aError)
        {
            Snackbar.Add(aError, Severity.Error);
            mErrors.Remove(aError);
            return null;
        }

        /// <summary>
        /// Shows a success message, then removes the message to avoid showing the same message multiple times
        /// </summary>
        /// <param name="aMessage">The success message to show</param>
        private object ShowSuccess(string aMessage)
        {
            Snackbar.Add(aMessage, Severity.Success);
            mSuccessMessages.Remove(aMessage);
            return null;
        }
    }
}
