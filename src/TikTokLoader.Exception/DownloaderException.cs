using System.Runtime.Serialization;

namespace TikTokLoader.Exception
{
    public class DownloaderException : System.Exception
    {
        #region Properties

        /// <summary>
        ///     Exception code
        /// </summary>
        public DownloaderExceptionCodes Code { get; set; }

        #endregion Properties

        #region Constructor

        public DownloaderException(DownloaderExceptionCodes code)
        {
            Code = code;
        }

        public DownloaderException(DownloaderExceptionCodes code, string message) : base(message)
        {
            Code = code;
        }

        public DownloaderException(DownloaderExceptionCodes code, string message, System.Exception innerException) : base(message, innerException)
        {
            Code = code;
        }

        public DownloaderException(DownloaderExceptionCodes code, SerializationInfo info, StreamingContext context) : base(info, context)
        {
            Code = code;
        }

        #endregion Constructor
    }
}
