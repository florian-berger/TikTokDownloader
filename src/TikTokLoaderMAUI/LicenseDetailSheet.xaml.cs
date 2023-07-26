using TikTokLoaderMAUI.BindingObjects;

namespace TikTokLoaderMAUI;

/// <summary>
///     BottomSheet for displaying details of a third party package
/// </summary>
public partial class LicenseDetailSheet
{
    #region Properties

    /// <summary>
    ///     Package that details should be displayed
    /// </summary>
    public ThirdPartyPackage Package { get; }

    #endregion Properties

    #region Constructor

    /// <summary>
    ///     Creates the sheet
    /// </summary>
    public LicenseDetailSheet(ThirdPartyPackage package)
	{
        Package = package;

        BindingContext = this;
        InitializeComponent();
    }

    #endregion Constructor
}