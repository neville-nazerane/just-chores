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

        [ObservableProperty]
        int frequency;

        [ObservableProperty]
        int frequencyIndex;

        [ObservableProperty]
        string summary;

        [ObservableProperty]
        Dictionary<FrequencyType, string> listedFrequencies;

        public AddChoreViewModel(MainRepository repository)
        {
            _repository = repository;
            model = new()
            {
                FrequencyType = FrequencyType.Day
            };
            Frequency = 1;
            FrequencyIndex = 0;
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
            if (newValue < 1)
            {
                Frequency = 1;
                return;
            }
            Model.Frequency = newValue;
            if (true || oldValue == 1 ^ newValue == 1)
            {
                var oldIndex = FrequencyIndex;
                if (newValue > 1)
                    ListedFrequencies = Enum.GetValues<FrequencyType>().ToDictionary(f => f, f => $"{f.ToString()}s");
                else
                    ListedFrequencies = Enum.GetValues<FrequencyType>().ToDictionary(f => f, f => f.ToString());
                FrequencyIndex = oldIndex;
                UpdateFrequencyIndex();
            }
            UpdateStatus();
        }

        partial void OnFrequencyIndexChanged(int value)
        {
            Model.FrequencyType = ListedFrequencies.ElementAtOrDefault(value).Key;
            UpdateStatus();
        }

        private void UpdateFrequencyIndex()
        {
            //FrequencyIndex = ListedFrequencies.ToList().FindIndex(l => l.Key == Model.FrequencyType);
            //UpdateStatus();
        }

        private void UpdateStatus()
        {
            Summary = $"This would be every {Model.Frequency}{GetOrdinalSuffix(Model.Frequency)} {Model.FrequencyType.ToString().ToLower()}{(Model.Frequency == 1 ? null : "s")}";
        }

        private static string GetOrdinalSuffix(int number)
        {
            if (number % 100 >= 11 && number % 100 <= 13)
                return "th";

            return (number % 10) switch
            {
                1 => "st",
                2 => "nd",
                3 => "rd",
                _ => "th",
            };
        }

    }
}
