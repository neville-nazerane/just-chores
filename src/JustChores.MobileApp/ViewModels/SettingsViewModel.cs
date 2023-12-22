using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using JustChores.MobileApp.Services;
using Microsoft.AppCenter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JustChores.MobileApp.ViewModels
{
    public partial class SettingsViewModel(MainRepository repository) : ViewModelBase
    {

        private readonly MainRepository _repository = repository;


        [ObservableProperty]
        private AppTheme currentTheme;

        [ObservableProperty]
        private bool isDiagnosticsEnabled;

        [RelayCommand]
        Task BackAsync() => RedirectToAsync("//chores");

        [RelayCommand]
        Task BackupAsync() => _repository.BackupAsync();

        public override void OnNavigatedTo()
        {
            CurrentTheme = App.Current.UserAppTheme;
            IsDiagnosticsEnabled = DiagnosticsUtil.IsEnabled;
            base.OnNavigatedTo();
        }

        partial void OnCurrentThemeChanged(AppTheme oldValue, AppTheme newValue)
        {
            App.Current.UserAppTheme = newValue;
        }

        [RelayCommand]
        async Task SwitchDiagnosticsEnabledAsync()
        {
            var newVal = !IsDiagnosticsEnabled;
            await DiagnosticsUtil.SetEnabledAsync(newVal);
            IsDiagnosticsEnabled = newVal;
        }

        [RelayCommand]
        Task GoToPrivacyPolicyAsync() => Browser.OpenAsync("https://nevillenazerane.com");

    }
}
