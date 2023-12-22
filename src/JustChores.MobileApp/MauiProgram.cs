using Android.Content.Res;
using AndroidX.Core.View;
using CommunityToolkit.Maui;
using CommunityToolkit.Maui.Markup;
using JustChores.MobileApp.Components;
using JustChores.MobileApp.Generated;
using JustChores.MobileApp.Services;
using JustChores.MobileApp.ViewModels;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using Microsoft.Extensions.Logging;

namespace JustChores.MobileApp
{
    public static partial class MauiProgram
    {
        private static readonly string appcenterSecret;


        public static IServiceProvider ServiceProvider { get; private set; }

        public static string AppCenterSecret => appcenterSecret;
        

        public static MauiApp CreateMauiApp()
        {
            SetupUI();

            //AppCenter.Start(appcenterSecret, typeof(Analytics), typeof(Crashes));

            var builder = MauiApp.CreateBuilder();

            builder.Services.AddGeneratedInjections()
                            
                            .AddTransientPopup<DatePickerPopup, DatePickerPopupModel>()

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

            var app = builder.Build();
            ServiceProvider = app.Services;
            return app;
        }

    
        static void SetupUI()
        {
            Microsoft.Maui.Handlers.EntryHandler.Mapper.AppendToMapping(nameof(Entry), (handler, view) =>
            {
#if ANDROID
                handler.PlatformView.BackgroundTintList = ColorStateList.ValueOf(Android.Graphics.Color.Transparent);
#endif
            });

            Microsoft.Maui.Handlers.DatePickerHandler.Mapper.AppendToMapping(nameof(Entry), (handler, view) =>
            {
#if ANDROID
                handler.PlatformView.BackgroundTintList = ColorStateList.ValueOf(Android.Graphics.Color.Transparent);
#endif
            });
        }

    
    }
}