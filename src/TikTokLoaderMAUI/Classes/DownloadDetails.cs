namespace TikTokLoaderMAUI.Classes
{
    public class DownloadDetails
    {
        public string Id { get; init; }

        public string Description { get; init; }

        public long CreationTimeStamp { get; init; }

        public string WatermarkVideoUri { get; init; }

        public string NoWatermarkVideoUri { get; init; }

        public string MusicUri { get; init; }

        public string ThumbnailUri { get; init; }

        public string UploadUser { get; init; }

        public string UploadUserAvatar { get; init; }

        public TikTokStatistics Statistics { get; init; }
    }
}
