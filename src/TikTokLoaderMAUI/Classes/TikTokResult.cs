using System.Text.Json.Serialization;

namespace TikTokLoaderMAUI.Classes
{
    public class TikTokResult
    {
        [JsonPropertyName("status_code")]
        public int StatusCode { get; set; }

        [JsonPropertyName("aweme_list")]
        public TikTokResultAweme[] MediaList { get; set; }
    }
}