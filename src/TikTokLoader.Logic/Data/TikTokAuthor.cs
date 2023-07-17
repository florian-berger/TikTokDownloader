using System.Text.Json.Serialization;

namespace TikTokLoader.Logic.Data
{
    public class TikTokAuthor
    {
        [JsonPropertyName("nickname")]
        public string? Name { get; set; }

        [JsonPropertyName("avatar_larger")]
        public TikTokMediaUrl? LargerAvatarMedia { get; set; }
    }
}
