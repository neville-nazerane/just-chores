using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JustChores.CodeGenerator
{
    internal static class Utils
    {

        internal static string ToFullPath(this string folder, string[] possibleCurrentProjNames = null)
        {
            var projName = typeof(Program).Assembly.GetName().Name;
            var possibleCurrentFolders = new List<string> { projName };
            if (possibleCurrentProjNames is not null)
                possibleCurrentFolders.AddRange(possibleCurrentProjNames);

            var rootDir = Directory.GetCurrentDirectory()
                         .Split(possibleCurrentFolders.ToArray(), StringSplitOptions.None)
                         .First();

            return Path.Combine(rootDir, folder);
        }

        internal static string Combine(this string path1, string path2) => Path.Combine(path1, path2);

        internal static string StripFileName(this string fullName)
        {
            var info = new FileInfo(fullName);
            return info.Name[..^info.Extension.Length];
        }

    }
}
