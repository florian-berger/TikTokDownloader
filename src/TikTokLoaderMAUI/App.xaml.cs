﻿using TikTokLoaderMAUI.Utils;

namespace TikTokLoaderMAUI;

public partial class App
{
	public App()
	{
		InitializeComponent();

        var theme = (AppTheme) AppSettings.UsedTheme;
        SetTheme(theme);

		MainPage = new AppShell();
	}

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
