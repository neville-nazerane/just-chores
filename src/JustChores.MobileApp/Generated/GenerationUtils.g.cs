using JustChores.MobileApp.Pages;
using JustChores.MobileApp.ViewModels;

namespace JustChores.MobileApp.Generated;

public static class GenerationUtils
{
    
   public static IServiceCollection AddGeneratedInjections(this IServiceCollection services)
        => services.AddTransient<AddChorePage>()
                   .AddTransient<BorderPage>()
                   .AddTransient<ListChoresPage>()
                   .AddTransient<SettingsPage>()
                   .AddTransient<AddChoreViewModel>()
                   .AddTransient<ListChoresViewModel>()
                   .AddTransient<SettingsViewModel>();

}