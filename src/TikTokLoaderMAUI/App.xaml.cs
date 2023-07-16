using TikTokLoaderMAUI.ViewModel;

namespace TikTokLoaderMAUI;

public partial class App : Application
{
	public App()
	{
		InitializeComponent();

#if ANDROID
        (Application.Current as IApplicationController)?.SetAppIndexingProvider(new 
Microsoft.Maui.Controls.Compatibility.Platform.Android.AndroidAppIndexProvider(Android.App.Application.Context));
#endif

        Current.UserAppTheme = AppTheme.Dark;
		MainPage = new AppShell();
	}

    protected override void OnAppLinkRequestReceived(Uri uri)
    {
        if (uri.Host.Contains("tiktok.com", StringComparison.OrdinalIgnoreCase))
        {
            if ((MainPage as Shell)?.CurrentPage is MainPage mainPage)
            {
                if (mainPage.BindingContext is MainViewModel mainViewModel)
                {
                    MainThread.BeginInvokeOnMainThread(() =>
                    {
                        mainViewModel.DownloadUri = uri.ToString();
                    });
                }
            }
        }
    }
}
