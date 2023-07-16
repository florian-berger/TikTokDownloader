using System.Runtime.Serialization;

namespace TikTokLoaderMAUI.Exceptions
{
    public class DownloaderException : Exception
    {
        #region Properties

        /// <summary>
        ///     Exception code
        /// </summary>
        public DownloaderExceptionCodes Code { get; set; }

        #endregion Properties

        #region Constructor

        public DownloaderException(DownloaderExceptionCodes code) : base()
        {
            Code = code;
        }

        public DownloaderException(DownloaderExceptionCodes code, string message) : base(message)
        {
            Code = code;
        }

        public DownloaderException(DownloaderExceptionCodes code, string message, Exception innerException) : base(message, innerException)
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
