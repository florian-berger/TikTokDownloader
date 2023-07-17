using System.Text.Json.Serialization;

namespace TikTokLoader.Logic.Data
{
    public class TikTokResultAweme
    {
        [JsonPropertyName("aweme_id")]
        public string? Id { get; set; }

        [JsonPropertyName("desc")]
        public string? Description { get; set; }

        [JsonPropertyName("create_time")]
        public long? CreationTime { get; set; }

        [JsonPropertyName("video")]
        public TikTokResultVideo? Video { get; set; }

        [JsonPropertyName("author")]
        public TikTokAuthor? Author { get; set; }

        [JsonPropertyName("music")]
        public TikTokMusic? Music { get; set; }

        [JsonPropertyName("statistics")]
        public TikTokStatistics? Statistics { get; set; }
    }
}
