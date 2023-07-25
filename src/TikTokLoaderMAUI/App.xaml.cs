using System.Globalization;
using TikTokLoaderMAUI.i18n;
using TikTokLoaderMAUI.Utils;

namespace TikTokLoaderMAUI;

/// <summary>
///     Representing the whole App object
/// </summary>
public partial class App
{
    private static readonly CultureInfo SystemCulture = CultureInfo.CurrentCulture;

    /// <summary>
    ///     Creates an instance of the app
    /// </summary>
	public App()
	{
		InitializeComponent();

        SetLanguage(AppSettings.AppLanguage);

        var theme = (AppTheme) AppSettings.UsedTheme;
        SetTheme(theme);

		MainPage = new AppShell();
	}

    /// <summary>
    ///     Set the theme of the application
    /// </summary>
    /// <param name="theme">Theme that should be set for the app</param>
    public static void SetTheme(AppTheme theme)
    {
        if (Current == null)
        {
            return;
        }

        Current.UserAppTheme = theme;

#if ANDROID
        // Force the dialogs to be in the correct theme - will be implemented with .NET 8,
        // see https://github.com/dotnet/maui/issues/6092
        // and https://github.com/dotnet/maui/commit/b601c4b8bf0dbf805180e0838943f347dc01a11d
        AndroidX.AppCompat.App.AppCompatDelegate.DefaultNightMode = Current.UserAppTheme switch
        {
            AppTheme.Light => AndroidX.AppCompat.App.AppCompatDelegate.ModeNightNo,
            AppTheme.Dark => AndroidX.AppCompat.App.AppCompatDelegate.ModeNightYes,
            _ => AndroidX.AppCompat.App.AppCompatDelegate.ModeNightFollowSystem
        };
#endif
    }

    /// <summary>
    ///     Sets the language of the app
    /// </summary>
    /// <param name="culture">Language that should be set - use IetfLanguageTag</param>
    public static void SetLanguage(string culture)
    {
        var cultureInfo = culture == "" ? SystemCulture : new CultureInfo(culture);

        CultureInfo.CurrentCulture = cultureInfo;
        CultureInfo.CurrentUICulture = cultureInfo;
        CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
        CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;

        DownloadResource.Culture = cultureInfo;
        ExceptionResource.Culture = cultureInfo;
        GlobalResource.Culture = cultureInfo;
        SettingsResource.Culture = cultureInfo;
    }

    // TODO: AppLinkRequest is not received, so it's not working.
    // TODO: Check against MAUI documentation, if there is some
    //protected override void OnAppLinkRequestReceived(Uri uri)
    //{
    //    if (uri.Host.Contains("tiktok.com", StringComparison.OrdinalIgnoreCase))
    //    {
    //        if ((MainPage as Shell)?.CurrentPage is MainPage mainPage)
    //        {
    //            if (mainPage.BindingContext is MainViewModel mainViewModel)
    //            {
    //                MainThread.BeginInvokeOnMainThread(() =>
    //                {
    //                    mainViewModel.DownloadUri = uri.ToString();
    //                });
    //            }
    //        }
    //    }
    //}
}
