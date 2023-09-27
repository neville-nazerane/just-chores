using CommunityToolkit.Maui;
using CommunityToolkit.Maui.Markup;
using JustChores.MobileApp.Generated;
using JustChores.MobileApp.Services;
using Microsoft.Extensions.Logging;

namespace JustChores.MobileApp
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {


            var builder = MauiApp.CreateBuilder();

            builder.Services.AddGeneratedInjections()
                            .AddSingleton<MainRepository>();

            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .UseMauiCommunityToolkitMarkup()
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
}