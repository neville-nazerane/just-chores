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
        void Refresh() => Chores = _repository.GetChores();

        public override void OnNavigatedTo() => Refresh();

    }
}
