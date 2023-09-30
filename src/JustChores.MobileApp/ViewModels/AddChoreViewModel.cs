using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using JustChores.MobileApp.Models;
using JustChores.MobileApp.Services;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JustChores.MobileApp.ViewModels
{
    public partial class AddChoreViewModel : ViewModelBase, IQueryAttributable
    {
        private readonly MainRepository _repository;

        [ObservableProperty]
        int? choreId;

        [ObservableProperty]
        Chore model;

        [ObservableProperty]
        int frequency;

        [ObservableProperty]
        int frequencyIndex;

        [ObservableProperty]
        string summary;

        [ObservableProperty]
        DateTime dueOn;

        [ObservableProperty]
        Dictionary<FrequencyType, string> listedFrequencies;

        public AddChoreViewModel(MainRepository repository)
        {
            _repository = repository;
            Reset();
        }

        private void Reset()
        {
            if (ChoreId is not null)
                Model = _repository.GetChore(ChoreId.Value);
            else
            {
                Model = new()
                {
                    FrequencyType = FrequencyType.Day,
                };
                Frequency = 1;
                FrequencyIndex = 0;
                DueOn = DateTime.Now;
            }
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
            Reset();
            await RedirectToAsync("//chores");
        }

        partial void OnChoreIdChanged(int? value)
        {
            if (value is not null)
            {
                ChoreId = value;
                Reset();
            }
        }
        partial void OnDueOnChanged(DateTime value)
        {
            Model.DueOn = value.Date;
            UpdateSummary();
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
            }
            UpdateSummary();
        }

        partial void OnFrequencyIndexChanged(int value)
        {
            Model.FrequencyType = ListedFrequencies.ElementAtOrDefault(value).Key;
            UpdateSummary();
        }

        private void UpdateSummary()
        {
            var dueOn = Model.DueOn ?? DateTime.Now;

            Summary = Model.FrequencyType switch
            {
                FrequencyType.Day => "This would be everyday",
                FrequencyType.Week => $"This would be every {dueOn.DayOfWeek.ToString().ToLowerInvariant()}",
                FrequencyType.Month => $"This would be on the {GetWithOrdinal(dueOn.Day)} of each month",
                FrequencyType.Year => $"This would be on the {GetWithOrdinal(dueOn.Day)} of every {dueOn:MMMM}",
                _ => null,
            };
        }

        private static string GetWithOrdinal(int number) => $"{number}{GetOrdinalSuffix(number)}";

        private static string GetOrdinalSuffix(int number)
        {
            if (number % 100 >= 11 && number % 100 <= 13) return "th";

            return (number % 10) switch
            {
                1 => "st",
                2 => "nd",
                3 => "rd",
                _ => "th",
            };
        }

        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            if (query.TryGetValue("id", out var val) && int.TryParse(val.ToString(), out int id))
                ChoreId = id;
        }
    }
}
