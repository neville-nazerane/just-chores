using CodeGeneratorHelpers.Maui.Models;
using Maui.CodeGeneratorHelpers;


await CodeGenerationBuilder.WithNewInstance()
                           .WithMobileProjectName("JustChores.MobileApp")
                           .WithExecutionLocations("JustChores.CodeGenerator")

                           .AddPageToViewModelEvent(PageEventType.OnNavigatedTo, "OnNavigatedTo")
                           .AddPageToViewModelEvent(PageEventType.OnNavigatedTo, "OnNavigatedToAsync", true)
                           .AddPageToViewModelEvent(PageEventType.OnNavigatedFrom, "OnNavigatedFromAsync", true)

                          .GenerateAsync();