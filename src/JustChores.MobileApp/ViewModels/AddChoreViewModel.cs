using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using JustChores.MobileApp.Models;
using JustChores.MobileApp.Services;
using Microsoft.UI.Input;
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

        [ObservableProperty]
        int frequency;

        [ObservableProperty]
        FrequencyType frequencyType;

        [ObservableProperty]
        int frequencyIndex;

        [ObservableProperty]
        Dictionary<FrequencyType, string> listedFrequencies;

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

        partial void OnFrequencyChanged(int oldValue, int newValue)
        {
            Model.Frequency = newValue;
            if (oldValue == 1 ^ newValue == 1)
            {
                if (newValue > 1)
                    ListedFrequencies = Enum.GetValues<FrequencyType>().ToDictionary(f => f, f => $"{f.ToString()}s");
                else
                    ListedFrequencies = Enum.GetValues<FrequencyType>().ToDictionary(f => f, f => f.ToString());
                UpdateFrequencyIndex();
            }
        }

        partial void OnFrequencyTypeChanged(FrequencyType value)
        {
            Model.FrequencyType = value;
        }

        partial void OnFrequencyIndexChanged(int value)
        {
            Model.FrequencyType = ListedFrequencies.ElementAt(value).Key;
        }

        private void UpdateFrequencyIndex()
        {
            FrequencyIndex = ListedFrequencies.ToList().FindIndex(l => l.Key == Model.FrequencyType);
        }

    }
}
