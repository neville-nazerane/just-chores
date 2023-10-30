using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using JustChores.MobileApp;
using JustChores.MobileApp.Services;

[assembly: Dependency(typeof(FileOpener))]
namespace JustChores.MobileApp
{
    public class FileOpener : IFileOpener
    {
        public void OpenFile(string filePath)
        {
            Android.Net.Uri uri = Android.Net.Uri.FromFile(new Java.IO.File(filePath));
            Intent intent = new(Intent.ActionView);
            intent.SetDataAndType(uri, "application/*");
            intent.SetFlags(ActivityFlags.NewTask);

            Android.App.Application.Context.StartActivity(intent);
        }
    }

}
