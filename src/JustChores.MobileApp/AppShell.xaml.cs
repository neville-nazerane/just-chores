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