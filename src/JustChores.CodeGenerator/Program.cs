﻿using JustChores.CodeGenerator;


// SETUP


#region setting paths

string mobileProject = "JustChores.MobileApp";
var otherProjects = new[]
    {
        mobileProject
    };

string fullMobilePath = mobileProject.ToFullPath(otherProjects);

string viewModelPath = fullMobilePath.Combine("ViewModels");
string pagesPath = fullMobilePath.Combine("Pages");
string generatedPath = fullMobilePath.Combine("Generated");



if (Directory.Exists(generatedPath))
    Directory.Delete(generatedPath, true);
Directory.CreateDirectory(generatedPath);

#endregion

#region Fetching file names

Console.WriteLine($"Pages path: {pagesPath}");
Console.WriteLine($"View Model path: {viewModelPath}");

var pageNames = new DirectoryInfo(pagesPath)
                            .GetFiles()
                            .Where(p => p.Name.EndsWith("Page.xaml"))
                            .Select(p => p.Name.StripFileName())
                            .ToArray();

var viewModelNames = new DirectoryInfo(viewModelPath)
                            .GetFiles()
                            .Where(p => p.Name.EndsWith("ViewModel.cs"))
                            .Select(p => p.Name.StripFileName())
                            .ToArray();



#endregion



string utilClass = @$"
using {mobileProject}.ViewModels;
using {mobileProject}.Pages;


namespace {mobileProject}.Generated;

public static class GeneratedUtils 
{{
    {GeneratePageAndViewModelInjections()}
}}


";

await File.WriteAllTextAsync(generatedPath.Combine("GeneratedUtils.g.cs"), utilClass.Trim());

foreach (var name in pageNames)
{
    string file = generatedPath.Combine($"{name}.g.cs");
    string code = GeneratePageCode(name);
    if (code is not null)
        await File.WriteAllTextAsync(file, code);
}

string GeneratePageAndViewModelInjections()
{
    var injectPieces = pageNames.Union(viewModelNames)
                                .Select(s => $".AddTransient<{s}>()");


    var function = $@"
    public static IServiceCollection AddPagesAndViewModels(this IServiceCollection services)
        => services{string.Join("\n                   ", injectPieces)};
";


    return function;
}


string GeneratePageCode(string pageName)
{
    var viewModel = viewModelNames
                        .SingleOrDefault(v => v.TrimViewModel() == pageName.TrimPage());

    if (viewModel is not null)
    {
        var code = @$"
using {mobileProject}.ViewModels;

namespace {mobileProject}.Pages;

public partial class {pageName} 
{{

    {viewModel} viewModel;
    public {viewModel} ViewModel 
        => viewModel ??= Shell.Current.Handler.MauiContext.Services.GetService<{viewModel}>();

    protected override async void OnNavigatedTo(NavigatedToEventArgs args)
    {{
        base.OnNavigatedTo(args);
        BindingContext = viewModel;
        await viewModel.OnNavigatedToAsync();
    }}

}}

";

        return code.Trim();    
    }
    return null;
}

