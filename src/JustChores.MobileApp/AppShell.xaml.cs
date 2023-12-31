using JustChores.MobileApp.Models;
using JustChores.MobileApp.Services;
using Microsoft.AppCenter;
using Microsoft.Maui.Controls;
using System.Reflection;

namespace JustChores.MobileApp
{
    public partial class AppShell : Shell
    {
        private const string HOME_ROUTE = "//chores";

        public AppShell()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            if (!DiagnosticsUtil.HasUserConfirmedYet())
            {
                var confirm = await DisplayAlert("Enable diagnostics",
                                                   "Can you live with your self with diagnostics enabled?",
                                                   "YES!",
                                                   "No");

                await DiagnosticsUtil.SetEnabledAsync(confirm);
            }
            else await DiagnosticsUtil.StartIfNeededAsync();

            base.OnAppearing();
        }


        protected override bool OnBackButtonPressed()
        {
            var currentRoute = Current.CurrentState.Location.ToString();
            if (currentRoute == HOME_ROUTE)
                return false;
            _ = Current.GoToAsync(HOME_ROUTE);
            return true;
        }

    }
}