using CodeGeneratorHelpers.Maui.Models;
using Maui.CodeGeneratorHelpers;


await CodeGenerationBuilder.WithNewInstance()
                           .WithMobileProjectName("JustChores.MobileApp")

                           .AddPageToViewModelEvent(PageEventType.OnNavigatedTo, "OnNavigatedTo")
                           .AddPageToViewModelEvent(PageEventType.OnNavigatedTo, "OnNavigatedToAsync", true)

                          .GenerateAsync();