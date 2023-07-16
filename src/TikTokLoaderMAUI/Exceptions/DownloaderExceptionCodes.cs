namespace TikTokLoaderMAUI.Exceptions
{
    public enum DownloaderExceptionCodes
    {
        #region General

        Unknown = 0,

        #endregion General

        #region Download (Offset 10)

        InvalidUri = 10,

        NoUriDefined = 11,

        VideoIdNotFound = 12,

        MediaUriNotFound = 13,

        #endregion Download (Offset 10)
    }
}
