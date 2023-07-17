using System.Net;
using System.Text.Json;
using System.Text.RegularExpressions;
using TikTokLoader.Exception;
using TikTokLoader.Logic.Data;

namespace TikTokLoader.Logic
{
    /// <summary>
    ///     Analyzer for TikTok URIs
    /// </summary>
    public class Analyzer
    {
        #region Public methods

        /// <summary>
        ///     Analyzes a URI and returns the information from the TikTok video
        /// </summary>
        /// <param name="uri">Uri that information should be loaded</param>
        /// <returns><see cref="DownloadDetails" /> with all relevant links for download</returns>
        public static async Task<DownloadDetails?> AnalyzeUri(string uri)
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

            var apiUri = Constants.GetVideoInformationUri.Replace("{videoId}", videoId);

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.TryAddWithoutValidation("User-Agent", "TikTok 16.0.16 rv:103005 (iPhone; iOS 11.1.4; en_EN) Cronet");

                using (var response = await client.GetAsync(apiUri))
                {
                    response.EnsureSuccessStatusCode();

                    var tikTokResultAsString = await response.Content.ReadAsStringAsync();

                    var tikTokResult = JsonSerializer.Deserialize<TikTokResult>(tikTokResultAsString);
                    if (tikTokResult == null)
                    {
                        return null;
                    }

                    return new DownloadDetails
                    {
                        Id = tikTokResult.MediaList?[0].Id,
                        Description = tikTokResult.MediaList?[0].Description,
                        CreationTimeStamp = tikTokResult.MediaList?[0].CreationTime,
                        WatermarkVideoUri = tikTokResult.MediaList?[0].Video?.DownloadAddress?.UriList?.FirstOrDefault(),
                        NoWatermarkVideoUri = tikTokResult.MediaList?[0].Video?.PlayAddress?.UriList?.FirstOrDefault(),
                        MusicUri = tikTokResult.MediaList?[0].Music?.PlayUri?.UriList?.FirstOrDefault(),
                        ThumbnailUri = tikTokResult.MediaList?[0].Video?.Thumbnail?.UriList?.LastOrDefault(),
                        UploadUser = tikTokResult.MediaList?[0].Author?.Name,
                        UploadUserAvatar = tikTokResult.MediaList?[0].Author?.LargerAvatarMedia?.UriList?.LastOrDefault(),
                        Statistics = tikTokResult.MediaList?[0].Statistics
                    };
                }
            }
        }

        /// <summary>
        ///     Returns the Id of the TikTok video. Also handling redirects
        /// </summary>
        /// <param name="uri">Uri that the Id of the video should be loaded from</param>
        public static async Task<string> GetVideoId(string uri)
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

        #endregion Public methods

        #region Private methods

        /// <summary>
        ///     Checks if the passed URI is a TikTok redirect URI (e.g. shared via Android)
        /// </summary>
        /// <param name="uri">URI that should be checked</param>
        /// <returns>True if the <paramref name="uri" /> is a redirect URI</returns>
        private static bool IsRedirectUri(string uri)
        {
            return uri.Contains("vm.tiktok.com") || uri.Contains("vt.tiktok.com");
        }

        /// <summary>
        ///     Get the redirect target of a redirect URI
        /// </summary>
        /// <param name="url">URI that the target should be resolved</param>
        /// <returns>Target URI of the passed redirect <paramref name="url" />.</returns>
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
                    if (response.StatusCode is HttpStatusCode.Moved or HttpStatusCode.MovedPermanently)
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
