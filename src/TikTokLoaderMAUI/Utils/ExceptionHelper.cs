using TikTokLoaderMAUI.Exceptions;

namespace TikTokLoaderMAUI.Utils
{
    public static class ExceptionHelper
    {
        public static async void DisplayExceptionMessage(Exception exception)
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
