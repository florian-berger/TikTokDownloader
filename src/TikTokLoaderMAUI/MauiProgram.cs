using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;

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
			});

#if DEBUG
		builder.Logging.AddDebug();
#endif

		return builder.Build();
	}
}
