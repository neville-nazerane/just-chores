using JustChores.MobileApp.ViewModels;

namespace JustChores.MobileApp.Pages;

public partial class HomePage 
{

    HomeViewModel viewModel;
    public HomeViewModel ViewModel 
        => viewModel ??= Shell.Current.Handler.MauiContext.Services.GetService<HomeViewModel>();



    protected override async void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);
        BindingContext = viewModel;
        await viewModel.OnNavigatedToAsync();
    }

}