using System.Text.Json.Serialization;

namespace TikTokLoader.Logic.Data
{
    public class TikTokMusic
    {
        [JsonPropertyName("title")]
        public string? Title { get; set; }

        [JsonPropertyName("play_url")]
        public TikTokMediaUrl? PlayUri { get; set; }
    }
}
