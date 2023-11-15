using CommunityToolkit.Maui.Views;
using JustChores.MobileApp.ViewModels;

namespace JustChores.MobileApp.Components;

public partial class DatePickerPopup : Popup
{
	public DatePickerPopup(DatePickerPopupModel viewModel)
	{
        BindingContext = ViewModel = viewModel;
		InitializeComponent();
    }

    public DatePickerPopupModel ViewModel { get; }

    private void OkClicked(object sender, EventArgs e)
    {
        Close(ViewModel.Date);
    }

    private void CanceledClicked(object sender, EventArgs e)
    {
        Close();
    }

}