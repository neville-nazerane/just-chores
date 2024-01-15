using CommunityToolkit.Maui.Core;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using JustChores.MobileApp.Models;
using JustChores.MobileApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JustChores.MobileApp.ViewModels
{
    public partial class ListChoresViewModel : ViewModelBase
    {
        private readonly MainRepository _repository;
        private readonly IPopupService _popupService;
        [ObservableProperty]
        IEnumerable<Chore> chores = Array.Empty<Chore>();

        [ObservableProperty]
        bool isRefreshing;

        public ListChoresViewModel(MainRepository repository,
                                   IPopupService popupService)
        {
            _repository = repository;
            _popupService = popupService;
        }

        [RelayCommand]
        void Refresh()
        {
            IsRefreshing = true;

            try
            {
                Chores = _repository.GetChores();
            }
            finally
            {
                IsRefreshing = false;
            }
        }

        [RelayCommand]
        async Task Delete(int id)
        {
            bool isConfirmed = await DisplayConfirmationAsync("Delete Chore",
                                                              "Are you sure you want to delete?");

            if (isConfirmed)
            {
                _repository.DeleteChore(id);
                Refresh();
            }
        }

        [RelayCommand]
        async Task CompleteAsync(int id)
        {
            var res = await _popupService.ShowPopupAsync<DatePickerPopupModel>(vm =>
            {
                vm.Date = DateTime.Now;
                vm.DateLabel = "Click the date below to change completion date";
                vm.Message = "Mark as Complete?";
            });

            var date = res as DateTime?;
            if (date is null) return;
            _repository.Completed(id, date.Value);
            Refresh();
        }

        [RelayCommand]
        Task EditAsync(int id) => RedirectToAsync($"//editor?id={id}");

        [RelayCommand]
        Task AddAsync() => RedirectToAsync($"//editor?id=null");

        [RelayCommand]
        Task GoToSettingsAsync() => RedirectToAsync("//settings");

        public override void OnNavigatedTo()
        {
            WeakReferenceMessenger.Default.Register<BackupRestoredMessage>(this, OnBackupRestored);
            Refresh();
        }

        void OnBackupRestored(object source, BackupRestoredMessage message)
        {
            Refresh();
        }

        public override Task OnNavigatedFromAsync()
        {
            WeakReferenceMessenger.Default.UnregisterAll(this);
            return base.OnNavigatedFromAsync();
        }

    }
}
