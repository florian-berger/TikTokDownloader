using TikTokLoader.Exception;

namespace TikTokLoaderMAUI.Utils
{
    /// <summary>
    ///     Provides helper functions for handling exceptions
    /// </summary>
    public static class ExceptionHelper
    {
        /// <summary>
        ///     Displays an exception to the user. Handles all custom ExceptionCodes
        /// </summary>
        /// <param name="exception">Exception that should be displayed</param>
        public static async Task DisplayExceptionMessage(Exception exception)
        {
            if (exception is DownloaderException downEx)
            {
                await Shell.Current.DisplayAlert("ERROR", $"Exception Code: {downEx.Code}", "OK");
                return;
            }

            await Shell.Current.DisplayAlert("ERROR", exception.Message, "OK");
        }
    }
}
