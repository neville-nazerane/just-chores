// See https://aka.ms/new-console-template for more information


string mobileProject = "JustChores.MobileApp";

var projName = typeof(Program).Assembly.GetName().Name;
var parentDir = Directory.GetCurrentDirectory()
                         .Split(projName)
                         .First()
                         .Split(mobileProject)
                         .First();
Console.WriteLine(parentDir);

var mobileProjectPath = Path.Combine(parentDir, mobileProject);

var file = Path.Combine(mobileProjectPath, "Hello.cs");
Console.WriteLine("this" + mobileProjectPath);

await File.WriteAllTextAsync(file, $@"

public class Stuff {{
    public static string PhoneOk() => ""911"";
}}

");

//var file = Path.Combine(Directory.GetCurrentDirectory());
//await File.WriteAllTextAsync("");