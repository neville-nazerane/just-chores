using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JustChores.MobileApp.Services
{
    public static class DiagnosticsUtil
    {

        const string KEY = "appcenter_enabled";

        static bool isStarted = false;

        public static bool IsEnabled => Preferences.Default.Get(KEY, false);

        public static bool HasUserConfirmedYet() => Preferences.Default.ContainsKey(KEY);

        public static async Task StartIfNeededAsync()
        {
            if (Preferences.Default.Get(KEY, false))
                await AppCenter.SetEnabledAsync(true);
        }

        public static async Task SetEnabledAsync(bool isEnabled)
        {
            Preferences.Default.Set(KEY, isEnabled);
            if (!isStarted && isEnabled)
            {
                AppCenter.Start(MauiProgram.AppCenterSecret, typeof(Analytics), typeof(Crashes));
                isStarted = true;
            }
            else await AppCenter.SetEnabledAsync(isEnabled);
        }

    }
}
