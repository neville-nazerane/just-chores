using JustChores.MobileApp.Pages;
using JustChores.MobileApp.ViewModels;

namespace JustChores.MobileApp.Pages;

public partial class AddChorePage 
{
    
    private AddChoreViewModel viewModel = null;

    public AddChoreViewModel ViewModel
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
            viewModel = Shell.Current.Handler.MauiContext.Services.GetService<AddChoreViewModel>();
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


    protected override async void OnNavigatedFrom(Microsoft.Maui.Controls.NavigatedFromEventArgs args)
    {
        OnNavigatedFromInternal();
        await ViewModel.OnNavigatedFromAsync();

        base.OnNavigatedFrom(args);
    }

    partial void OnNavigatedFromInternal();


    protected override void OnAppearing()
    {
        SetupViewModelIfNotAlready();
        OnAppearingInternal();

        base.OnAppearing();
    }

    partial void OnAppearingInternal();


}