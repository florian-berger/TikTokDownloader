using CommunityToolkit.Maui.Storage;
using TikTokLoader.Exception;

namespace TikTokLoaderMAUI.Utils
{
    public class DownloadHelper
    {
        #region Public methods

        public static async Task<bool> DownloadMediaAsync(string downloadUrl, string fileName)
        {
            if (string.IsNullOrWhiteSpace(downloadUrl))
            {
                throw new DownloaderException(DownloaderExceptionCodes.MediaUriNotFound, "URI for the selected media not available.");
            }

            using var httpClient = new HttpClient();
            using (var webStream = await httpClient.GetStreamAsync(downloadUrl))
            {
                var fileSaverResult = await FileSaver.Default.SaveAsync("Downloads/TikTok-Downloader", fileName, webStream, CancellationToken.None);
                return fileSaverResult.IsSuccessful;
            }
        }

        #endregion Public methods
    }
}
