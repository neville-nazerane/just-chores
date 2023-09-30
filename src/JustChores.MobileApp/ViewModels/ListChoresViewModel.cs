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
    public partial class ListChoresViewModel : ViewModelBase
    {
        private readonly MainRepository _repository;
        
        [ObservableProperty]
        IEnumerable<Chore> chores = Array.Empty<Chore>();

        public ListChoresViewModel(MainRepository repository)
        {
            _repository = repository;
        }

        [RelayCommand]
        void Refresh()
        {
            Chores = _repository.GetChores();
        }

        [RelayCommand]
        void Delete(int id)
        {
            _repository.DeleteChore(id);
            Refresh();
        }

        [RelayCommand]
        void Complete(int id)
        {
            _repository.Completed(id);
            Refresh();
        }

        [RelayCommand]
        Task EditAsync(int id) => RedirectToAsync($"//editor/{id}");

        public override void OnNavigatedTo() => Refresh();

    }
}
