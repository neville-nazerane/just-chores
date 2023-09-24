using JustChores.MobileApp.Pages;
using JustChores.MobileApp.ViewModels;

namespace JustChores.MobileApp.Pages;

public partial class HomePage {

    private HomeViewModel viewModel = null;

    public HomeViewModel ViewModel => viewModel ??= Shell.Current.Handler.MauiContext.Services.GetService<HomeViewModel>();
   

    protected override async void OnNavigatedTo(NavigatedToEventArgs args)
    {
        BindingContext = ViewModel;
        await ViewModel.OnNavigatedToAsync(args);
        OnNavigatedToInternal(args);
        base.OnNavigatedTo(args);
    }

    protected virtual void OnNavigatedToInternal(NavigatedToEventArgs args) { }

}