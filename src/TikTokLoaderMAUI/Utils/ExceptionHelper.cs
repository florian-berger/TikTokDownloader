using TikTokLoader.Exception;
using TikTokLoaderMAUI.i18n;

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
            string? errorMessage;
            if (exception is DownloaderException downEx)
            {
                var errorCodeString = downEx.Code.ToString();

                errorMessage = ExceptionResource.ResourceManager.GetString(errorCodeString);
                if (string.IsNullOrWhiteSpace(errorMessage))
                {
                    errorMessage = string.Format(ExceptionResource.ErrorCodeNotFound, errorCodeString, downEx.Message);
                }
            }
            else
            {
                errorMessage = exception.Message;
            }

            if (string.IsNullOrWhiteSpace(errorMessage))
            {
                errorMessage = string.Format(ExceptionResource.UnknownError, exception.GetType().Name);
            }

            await Shell.Current.DisplayAlert(GlobalResource.Error, errorMessage, GlobalResource.Ok);
        }
    }
}
