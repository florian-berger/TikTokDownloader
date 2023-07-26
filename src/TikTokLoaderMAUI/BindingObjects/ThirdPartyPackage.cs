namespace TikTokLoaderMAUI.BindingObjects
{
    /// <summary>
    ///     Class holding information about every Third Party package used within this software
    /// </summary>
    public class ThirdPartyPackage
    {
        /// <summary>
        ///     Name of the package
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        ///     Author of the package
        /// </summary>
        public string? Author { get; set; }

        /// <summary>
        ///     Version of the package
        /// </summary>
        public string? Version { get; set; }

        /// <summary>
        ///     Uri to the homepage of the package
        /// </summary>
        public string? HomepageUri { get; set; }

        /// <summary>
        ///     License information
        /// </summary>
        public Dictionary<string, string?>? LicenseInfo { get; set; }
    }
}
