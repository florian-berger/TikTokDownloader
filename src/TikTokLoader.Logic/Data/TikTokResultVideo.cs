using System.Text.Json.Serialization;

namespace TikTokLoader.Logic.Data
{
    public class TikTokResultVideo
    {
        [JsonPropertyName("play_addr")]
        public TikTokMediaUrl? PlayAddress { get; set; }

        [JsonPropertyName("download_addr")]
        public TikTokMediaUrl? DownloadAddress { get; set; }

        [JsonPropertyName("cover")]
        public TikTokMediaUrl? Thumbnail { get; set; }
    }
}
