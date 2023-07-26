using CommunityToolkit.Maui;
using MauiIcons.Material;
using Microsoft.Extensions.Logging;
using The49.Maui.BottomSheet;

namespace TikTokLoaderMAUI;

/// <summary>
///		Entry class for the MAUI app
/// </summary>
public static class MauiProgram
{
	/// <summary>
	///		Creates the MAUI app
	/// </summary>
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
            .UseMauiCommunityToolkit()
            .ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			})
            .UseMaterialMauiIcons()
            .UseBottomSheet();

#if DEBUG
		builder.Logging.AddDebug();
#endif

		return builder.Build();
	}
}
