using MauiApp1.Models;
using MauiApp1.Services;
using Microsoft.Extensions.Logging;

namespace MauiApp1;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();

		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});
		var settings = new AppSettings();
		builder.Services.AddTransient<MainPage>();
        builder.Services.AddSingleton<ProcessService>();
		builder.Services.AddSingleton<UpdateService>();
		builder.Services.AddSingleton(settings);

#if DEBUG
        builder.Logging.AddDebug();
#endif

		return builder.Build();
	}
}
