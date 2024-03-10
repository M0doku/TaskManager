using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using LocalizationResourceManager.Maui;
using Maui.TouchEffect.Hosting;


namespace TaskManager
{
	public static class MauiProgram
	{
		public static MauiApp CreateMauiApp()
		{
			
			var builder = MauiApp.CreateBuilder();
			builder
				.UseMauiApp<App>()
				.UseMauiCommunityToolkit()
				.UseMauiTouchEffect()
			    .UseLocalizationResourceManager(settings =>
				{
					settings.RestoreLatestCulture(true);
					settings.AddResource(AppResources.ResourceManager);
				})
				.ConfigureFonts(fonts =>
				{
					fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
					fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
					fonts.AddFont("couriernew.ttf", "Courier New");
					fonts.AddFont("NikkyouSans.ttf", "NikkyouSans");
					fonts.AddFont("Moonspace.ttf", "Moonspace");
				});


#if DEBUG
			builder.Logging.AddDebug();
#endif
			builder.Services.AddTransient<Settings>();
			builder.Services.AddTransient<MainPage>();
			return builder.Build();
		}
	}
}
