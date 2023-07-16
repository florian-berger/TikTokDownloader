using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;

namespace TikTokLoaderMAUI;

[Activity(Theme = "@style/Maui.SplashTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize | ConfigChanges.Density, ScreenOrientation = ScreenOrientation.Portrait)]
[IntentFilter(new[] { Intent.ActionSend }, Categories = new[] { Intent.CategoryDefault, Intent.CategoryBrowsable }, DataMimeType = "text/plain")]
public class MainActivity : MauiAppCompatActivity
{
    protected override void OnNewIntent(Intent intent)
    {
        base.OnNewIntent(intent);

        var action = intent.Action;
        var strLink = intent.DataString;

        if (Intent.ActionView != action || string.IsNullOrWhiteSpace(strLink))
        {
            return;
        }

        var link = new Uri(strLink);
        Microsoft.Maui.Controls.Application.Current?.SendOnAppLinkRequestReceived(link);
    }
}
