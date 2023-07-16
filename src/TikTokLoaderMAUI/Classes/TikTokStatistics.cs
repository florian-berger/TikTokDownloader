using System.Text.Json.Serialization;

namespace TikTokLoaderMAUI.Classes
{
    public class TikTokStatistics
    {
        [JsonPropertyName("comment_count")]
        public long CommentsCount { get; set; }

        [JsonPropertyName("digg_count")]
        public long DiggCount { get; set; }

        [JsonPropertyName("play_count")]
        public long PlayCount { get; set; }

        [JsonPropertyName("share_count")]
        public long ShareCount { get; set; }

        [JsonPropertyName("collect_count")]
        public long CollectsCount { get; set; }
    }
}
