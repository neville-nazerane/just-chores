using JustChores.MobileApp.Pages;
using JustChores.MobileApp.ViewModels;

namespace JustChores.MobileApp.Pages;

public partial class ListChoresPage 
{
    
    private ListChoresViewModel viewModel = null;

    public ListChoresViewModel ViewModel
    {
        get
        {
            SetupViewModelIfNotAlready();
            return viewModel;
        }
    }

    private void SetupViewModelIfNotAlready()
    {
        if (viewModel is null)
        {
            viewModel = Shell.Current.Handler.MauiContext.Services.GetService<ListChoresViewModel>();
            BindingContext = viewModel;
        }
    }


    protected override async void OnNavigatedTo(Microsoft.Maui.Controls.NavigatedToEventArgs args)
    {
        OnNavigatedToInternal();
         ViewModel.OnNavigatedTo();
        await ViewModel.OnNavigatedToAsync();

        base.OnNavigatedTo(args);
    }

    partial void OnNavigatedToInternal();


    protected override void OnAppearing()
    {
        SetupViewModelIfNotAlready();
        OnAppearingInternal();

        base.OnAppearing();
    }

    partial void OnAppearingInternal();


}