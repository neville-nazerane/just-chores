using JustChores.MobileApp.Pages;
using JustChores.MobileApp.ViewModels;

namespace JustChores.MobileApp.Generated;

public static class GenerationUtils
{
    
   public static IServiceCollection AddGeneratedInjections(this IServiceCollection services)
        => services.AddTransient<AddChorePage>()
                   .AddTransient<HomePage>()
                   .AddTransient<ListChoresPage>()
                   .AddTransient<AddChoreViewModel>()
                   .AddTransient<HomeViewModel>()
                   .AddTransient<ListChoresViewModel>();

}