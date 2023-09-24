using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JustChores.MobileApp.ViewModels
{
    public class ViewModelBase : ObservableObject
    {

        public virtual Task OnNavigatedToAsync() => Task.CompletedTask;
        public virtual void OnNavigatedTo() { }

        protected static Task DisplayAlertAsync(string title, string body, string cancelLbl)
            => Shell.Current.DisplayAlert(title, body, cancelLbl);

    }
}
