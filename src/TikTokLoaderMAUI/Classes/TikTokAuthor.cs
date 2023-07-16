using System.Text.Json.Serialization;

namespace TikTokLoaderMAUI.Classes
{
    public class TikTokAuthor
    {
        [JsonPropertyName("nickname")]
        public string Name { get; set; }

        [JsonPropertyName("avatar_larger")]
        public TikTokMediaUrl LargerAvatarMedia { get; set; }
    }
}
