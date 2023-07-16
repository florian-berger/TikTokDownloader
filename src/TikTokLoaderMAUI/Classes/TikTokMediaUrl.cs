using System.Text.Json.Serialization;

namespace TikTokLoaderMAUI.Classes
{
    public class TikTokMediaUrl
    {
        [JsonPropertyName("uri")]
        public string Uri { get; set; }

        [JsonPropertyName("url_list")]
        public string[] UriList { get; set; }

        [JsonPropertyName("width")]
        public int Width { get; set; }

        [JsonPropertyName("height")]
        public int Height { get; set; }

        //[JsonPropertyName("url_key")]
        //public string? UrlKey { get; set; }

        //[JsonPropertyName("data_size")]
        //public int? DataSize { get; set; }

        //[JsonPropertyName("file_hash")]
        //public string? FileHash { get; set; }
    }
}
