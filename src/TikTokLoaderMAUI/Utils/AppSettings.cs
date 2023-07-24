namespace TikTokLoaderMAUI.Utils
{
    /// <summary>
    ///     Class for accessing app settings and their values
    /// </summary>
    internal static class AppSettings
    {
        /// <summary>
        ///     Information if the clipboard should be analyzed when the page was loaded
        /// </summary>
        public static bool AnalyzeClipboardOnLoad
        {
            get => Preferences.Default.Get(nameof(AnalyzeClipboardOnLoad), true);
            set => Preferences.Default.Set(nameof(AnalyzeClipboardOnLoad), value);
        }

        /// <summary>
        ///     Theme that should be used in the app
        /// </summary>
        public static int UsedTheme
        {
            get => Preferences.Default.Get(nameof(UsedTheme), 0);
            set => Preferences.Default.Set(nameof(UsedTheme), value);
        }
    }
}
