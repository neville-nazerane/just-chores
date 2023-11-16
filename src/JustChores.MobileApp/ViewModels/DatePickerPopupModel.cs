using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JustChores.MobileApp.ViewModels
{
    public partial class DatePickerPopupModel : ViewModelBase
    {

        [ObservableProperty]
        private string message;

        [ObservableProperty]
        private DateTime date;

        [ObservableProperty]
        private string dateLabel;

    }
}
