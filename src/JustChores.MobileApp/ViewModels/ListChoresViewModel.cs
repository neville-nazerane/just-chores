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
        
        [ObservableProperty]
        IEnumerable<Chore> chores = Array.Empty<Chore>();

        [ObservableProperty]
        bool isRefreshing;

        [ObservableProperty]
        AsyncBindable<DateTime> dateControl;

        //[ObservableProperty]
        //bool isDateFocused;

        public ListChoresViewModel(MainRepository repository)
        {
            _repository = repository;
        }

        public override void OnNavigatedTo()
        {
            DateControl = new();
            WeakReferenceMessenger.Default.Register<BackupRestoredMessage>(this, OnBackupRestored);
            Refresh();
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
        async Task DeleteAsync(int id)
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
            var completedOn = await DateControl.FetchAsync();
            _repository.Completed(id, completedOn);
            Refresh();
        }

        [RelayCommand]
        Task EditAsync(int id) => RedirectToAsync($"//editor?id={id}");

        [RelayCommand]
        Task AddAsync() => RedirectToAsync($"//editor?id=null");

        [RelayCommand]
        Task BackupAsync() => _repository.BackupAsync();

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
