namespace TikTokLoader.Exception
{
    public enum DownloaderExceptionCodes
    {
        #region General

        Unknown = 0x0,

        #endregion General

        #region Download (Offset 10)

        InvalidUri = 0x10,

        NoUriDefined = 0x11,

        VideoIdNotFound = 0x12,

        MediaUriNotFound = 0x13,

        #endregion Download (Offset 10)
    }
}
