using System.Text.Json.Serialization;

namespace TikTokLoader.Logic.Data
{
    public class TikTokMediaUrl
    {
        [JsonPropertyName("uri")]
        public string? Uri { get; set; }

        [JsonPropertyName("url_list")]
        public string[]? UriList { get; set; }

        [JsonPropertyName("width")]
        public int? Width { get; set; }

        [JsonPropertyName("height")]
        public int? Height { get; set; }
    }
}
