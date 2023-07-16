using System.Text.Json.Serialization;

namespace TikTokLoaderMAUI.Classes
{
    public class TikTokMusic
    {
        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("play_url")]
        public TikTokMediaUrl PlayUri { get; set; }
    }
}
