using CommunityToolkit.Maui.Storage;
using TikTokLoader.Exception;

namespace TikTokLoaderMAUI.Utils
{
    /// <summary>
    ///     Provides helper functions for downloading files
    /// </summary>
    public class DownloadHelper
    {
        #region Public methods

        /// <summary>
        ///     Downloads the video (or sound) from the passed URI and saves it
        /// </summary>
        /// <param name="downloadUrl">URI the media should be downloaded from</param>
        /// <param name="fileName">Name of the file that should be saved</param>
        /// <returns>True when the download succeeded</returns>
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
