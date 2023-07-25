using Foundation;

namespace TikTokLoaderMAUI;

/// <summary>
///		Application delegate for the MacApp
/// </summary>
[Register("AppDelegate")]
public class AppDelegate : MauiUIApplicationDelegate
{
	/// <summary>
	///		Creates the MAUI App
	/// </summary>
	protected override MauiApp CreateMauiApp() => MauiProgram.CreateMauiApp();
}
