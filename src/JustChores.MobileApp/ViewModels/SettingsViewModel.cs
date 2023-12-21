using CommunityToolkit.Mvvm.Input;
using JustChores.MobileApp.Services;
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

        [RelayCommand]
        Task BackAsync() => RedirectToAsync("//chores");

        [RelayCommand]
        Task BackupAsync() => _repository.BackupAsync();


    }
}
