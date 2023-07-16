using CommunityToolkit.Maui.Storage;
using System.Net;
using System.Text.Json;
using System.Text.RegularExpressions;
using TikTokLoaderMAUI.Classes;
using TikTokLoaderMAUI.Exceptions;

namespace TikTokLoaderMAUI
{
    public class TikTokLoader
    {
        #region Public methods

        public static async Task<DownloadDetails> AnalyzeUri(string uri)
        {
            if (string.IsNullOrWhiteSpace(uri))
            {
                throw new DownloaderException(DownloaderExceptionCodes.NoUriDefined, "There was no URI provided.");
            }

            var videoId = await GetVideoId(uri);
            if (string.IsNullOrWhiteSpace(videoId))
            {
                throw new DownloaderException(DownloaderExceptionCodes.VideoIdNotFound, "Video ID could not be found with the entered URI.");
            }

            var apiUri = $"https://api19-core-c-useast1a.musical.ly/aweme/v1/feed/?aweme_id={videoId}&version_code=262&app_name=musical_ly&channel=App&device_id=null&os_version=14.4.2&device_platform=iphone&device_type=iPhone9";

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.TryAddWithoutValidation("User-Agent", "TikTok 16.0.16 rv:103005 (iPhone; iOS 11.1.4; en_EN) Cronet");

                using (var response = await client.GetAsync(apiUri))
                {
                    response.EnsureSuccessStatusCode();

                    var tikTokResultAsString = await response.Content.ReadAsStringAsync();

                    var tikTokResult = JsonSerializer.Deserialize<TikTokResult>(tikTokResultAsString);

                    return new DownloadDetails
                    {
                        Id = tikTokResult.MediaList[0].Id,
                        Description= tikTokResult.MediaList[0].Description,
                        CreationTimeStamp = tikTokResult.MediaList[0].CreationTime,
                        WatermarkVideoUri = tikTokResult.MediaList[0].Video.DownloadAddress.UriList.FirstOrDefault(),
                        NoWatermarkVideoUri = tikTokResult.MediaList[0].Video.PlayAddress.UriList.FirstOrDefault(),
                        MusicUri = tikTokResult.MediaList[0].Music.PlayUri.UriList.FirstOrDefault(),
                        ThumbnailUri = tikTokResult.MediaList[0].Video.Thumbnail.UriList.LastOrDefault(),
                        UploadUser = tikTokResult.MediaList[0].Author.Name,
                        UploadUserAvatar = tikTokResult.MediaList[0].Author.LargerAvatarMedia.UriList.LastOrDefault(),
                        Statistics = tikTokResult.MediaList[0].Statistics
                    };
                }
            }
        }

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

        #region Private methods

        private static async Task<string> GetVideoId(string uri)
        {
            string videoUri;
            if (IsRedirectUri(uri))
            {
                videoUri = await GetRedirectTargetUrl(uri);
            }
            else if (uri.Contains("/video/"))
            {
                videoUri = uri;
            }
            else
            {
                throw new DownloaderException(DownloaderExceptionCodes.InvalidUri, "Invalid URI provided (maybe no TikTok URI?)");
            }

            var finalId = string.Empty;
            var match = Regex.Match(videoUri, "^https?://.*/video/(?<id>.*)$");
            if (match.Success)
            {
                finalId = match.Groups["id"].Value;
            }

            if (string.IsNullOrWhiteSpace(finalId))
            {
                throw new DownloaderException(DownloaderExceptionCodes.VideoIdNotFound, "Video ID could not be detected correctly.");
            }

            finalId = finalId.Contains('?') ? finalId.Split('?')[0] : finalId;

            return finalId.TrimEnd('/', ' ');
        }

        private static bool IsRedirectUri(string uri)
        {
            return uri.Contains("vm.tiktok.com") || uri.Contains("vt.tiktok.com");
        }

        private static async Task<string> GetRedirectTargetUrl(string url)
        {
            var handler = new HttpClientHandler
            {
                AllowAutoRedirect = false
            };

            using (var client = new HttpClient(handler))
            {
                using (var response = await client.GetAsync(url))
                {
                    if (response.StatusCode == HttpStatusCode.Moved || response.StatusCode == HttpStatusCode.MovedPermanently)
                    {
                        var headers = response.Headers;
                        if (headers.Location != null)
                        {
                            url = headers.Location.AbsoluteUri;
                        }
                    }
                }
            }

            return url;
        }

        #endregion Private methods
    }
}
