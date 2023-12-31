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
        string summary;

        [ObservableProperty]
        DateTime dueOn;

        [ObservableProperty]
        string submitText;

        [ObservableProperty]
        string title;

        [ObservableProperty]
        FrequencyType frequencyType;

        [ObservableProperty]
        string dateLabel;

        [ObservableProperty]
        string selectedInputKey;

        [ObservableProperty]
        bool titleIsFocused;

        [ObservableProperty]
        bool isInitialized;

        public AddChoreViewModel(MainRepository repository)
        {
            _repository = repository;
            Reset();
        }

        public override void OnNavigatedTo()
        {
            Reset();
        }

        private void Reset()
        {
            IsInitialized = false;
            if (ChoreId is not null)
            {
                Title = "Update Chore";
                DateLabel = "Starting Date";
                SubmitText = "Update";
                Model = _repository.GetChore(ChoreId.Value);
                Frequency = Model.Frequency;
                FrequencyType = Model.FrequencyType;
                DueOn = Model.DueOn ?? DateTime.Now.Date;
            }
            else
            {
                Title = "Create a Chore";
                DateLabel = "Due Date";
                SubmitText = "Add";
                Model = new()
                {
                    FrequencyType = FrequencyType.Day,
                };
                Frequency = 1;
                FrequencyType = FrequencyType.Day;
                DueOn = DateTime.Now;
            }
            IsInitialized = true;
        }

        [RelayCommand]
        async Task SubmitAsync()
        {
            if (string.IsNullOrEmpty(Model.Title))
            {
                await DisplayAlertAsync("Failed to add", "You need to provide a title", "OK");
                return;
            }
            if (ChoreId is null)
                _repository.InsertChore(Model);
            else _repository.UpdateChore(Model);
            Reset();
            await RedirectToAsync("//chores");
        }

        [RelayCommand]
        Task BackAsync() => RedirectToAsync("//chores");

        [RelayCommand]
        Task ToListAsync() => RedirectToAsync("//chores");

        //[RelayCommand]
        //void SetFrequencyType(FrequencyType frequencyType)
        //{
        //    FrequencyType = frequencyType;
        //    SelectedInputKey = "FrequencyType";
        //}

        [RelayCommand]
        void IncreaseFrequency()
        {
            Frequency++;
            SelectedInputKey = "Frequency";
        }

        [RelayCommand]
        void DecreaseFrequency()
        {
            if (Frequency > 1) Frequency--;
            SelectedInputKey = "Frequency";
        }

        [RelayCommand]
        void TitleSelected() => SelectedInputKey = "Title";

        [RelayCommand]
        void DueOnSelected() => SelectedInputKey = "DueOn";

        partial void OnTitleIsFocusedChanged(bool value)
        {
            if (value)
                SelectedInputKey = "Title";
        }

        partial void OnChoreIdChanged(int? value)
        {
            ChoreId = value;
            Reset();
        }
        partial void OnDueOnChanged(DateTime value)
        {
            Model.DueOn = value.Date;
            UpdateSummary();
            if (IsInitialized)
                SelectedInputKey = "DueOn";
        }

        partial void OnFrequencyChanged(int oldValue, int newValue)
        {
            if (newValue < 1)
            {
                Frequency = 1;
                return;
            }
            Model.Frequency = newValue;
            UpdateSummary();
        }

        partial void OnFrequencyTypeChanged(FrequencyType oldValue, FrequencyType newValue)
        {
            Model.FrequencyType = newValue;
            SelectedInputKey = "FrequencyType";
            UpdateSummary();
        }

        private void UpdateSummary()
        {
            var dueOn = Model.DueOn ?? DateTime.Now;
            
            Summary = Model.FrequencyType switch
            {
                FrequencyType.Day => $"This would be {GetFrequencyString()}{(Frequency > 1 ? " " : null)}day{(Frequency > 2 ? "s" : null)}",
                FrequencyType.Week => $"This would be {GetFrequencyString()} {dueOn.DayOfWeek.ToString().ToLowerInvariant()}",
                FrequencyType.Month => $"This would be on the {GetWithOrdinal(dueOn.Day)} of {GetFrequencyString()} month{GetFrequencyPlural()}",
                FrequencyType.Year => $"This would be on the {GetWithOrdinal(dueOn.Day)} {dueOn:MMMM} {GetFrequencyString()} year{GetFrequencyPlural()}",
                _ => null,
            };
        }

        string GetFrequencyString() => Frequency switch
        {
            1 => "every",
            2 => "every other",
            _ => $"every {(Model.FrequencyType == FrequencyType.Week ? GetWithOrdinal(Frequency) : Frequency)}"
        };

        string GetFrequencyPlural() => Frequency > 1 ? "s" : null;

        private static string GetWithOrdinal(int number) => $"{number}<sup>{GetOrdinalSuffix(number)}</sup>";

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
            else ChoreId = null;
        }
    }
}
