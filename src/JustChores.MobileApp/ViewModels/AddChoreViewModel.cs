using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using JustChores.MobileApp.Models;
using JustChores.MobileApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JustChores.MobileApp.ViewModels
{
    public partial class AddChoreViewModel : ViewModelBase
    {
        private readonly MainRepository _repository;

        [ObservableProperty]
        Chore model;

        public AddChoreViewModel(MainRepository repository)
        {
            _repository = repository;
            model = new();
        }

        [RelayCommand]
        async Task AddAsync()
        {
            if (string.IsNullOrEmpty(Model.Title))
            {
                await DisplayAlertAsync("Failed to add", "You need to provide a title", "OK");
                return;
            }
            _repository.InsertChore(Model);
        }

    }
}
