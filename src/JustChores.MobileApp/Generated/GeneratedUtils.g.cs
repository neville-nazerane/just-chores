using JustChores.MobileApp.ViewModels;
using JustChores.MobileApp.Pages;


namespace JustChores.MobileApp.Generated;

public static class GeneratedUtils 
{
    
    public static IServiceCollection AddPagesAndViewModels(this IServiceCollection services)
        => services.AddTransient<HomePage>()
                   .AddTransient<HomeViewModel>();

}