using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using JustChores.MobileApp.Services;

namespace JustChores.MobileApp
{


    [IntentFilter(
            new[] { Intent.ActionView, Intent.ActionEdit },
            Categories = new[] { Intent.CategoryDefault },
            DataScheme = "content",
            DataMimeType = "text/plain",
            Label = "Restore from backup"
        )]
    [Activity(Theme = "@style/Maui.SplashTheme",
              MainLauncher = true, 
            Exported = true,
              ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize | ConfigChanges.Density)]
    public class MainActivity : MauiAppCompatActivity
    {
        protected override async void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            Platform.Init(this, savedInstanceState);

            Intent intent = Intent;
            Android.Net.Uri data = intent.Data;

            if (data != null)
            {
                using var stream = ContentResolver.OpenInputStream(data);
                var repo = MauiProgram.ServiceProvider.GetService<MainRepository>();
                await repo.RestoreAsync(stream);
            }
        }

    }
}